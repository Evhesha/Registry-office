using BD6.DB_Commands;
using Microsoft.Data.SqlClient;
using Stimulsoft.Report;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace BD6.Windows
{
    public partial class RegistrarsWindow : Window
    {
        public RegistrarsWindow() => InitializeComponent();

        string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=RegistryOffice; Integrated Security=true;";
        DataSet ds;
        SqlDataAdapter adapter;

        public static event Action DataSaved;

        private void RegistrarsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string command = "Select * FROM Registrars";
                    ds = new DataSet();
                    adapter = new SqlDataAdapter(command, connection);
                    adapter.Fill(ds, "Registrars");
                    RegistrarsDataGrid.ItemsSource = ds.Tables[0].DefaultView;

                    if (RegistrarsDataGrid.Columns.Count > 0)
                    {
                        RegistrarsDataGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }

                    var registrarCommands = new RegistrarsCommands(adapter);
                    registrarCommands.ConfigureCommands(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrarsDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)RegistrarsDataGrid.SelectedItem;
                selectedRow.Delete();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ds != null)
            {
                ds.Tables[0].RejectChanges();
                RegistrarsDataGrid.ItemsSource = ds.Tables[0].DefaultView;

                MessageBox.Show("Changes were canceled.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    adapter.InsertCommand.Connection = connection;
                    adapter.UpdateCommand.Connection = connection;
                    adapter.DeleteCommand.Connection = connection;

                    adapter.Update(ds, "Registrars");

                    DataSaved?.Invoke();

                    MessageBox.Show("Changes were successfully saved.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving changes: {ex.Message}");
            }
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegistrarsDataGrid.SelectedIndex = 0;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrarsDataGrid.SelectedIndex < RegistrarsDataGrid.Items.Count - 1)
            {
                RegistrarsDataGrid.SelectedIndex++;
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrarsDataGrid.SelectedIndex > 0)
            {
                RegistrarsDataGrid.SelectedIndex--;
            }
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegistrarsDataGrid.SelectedIndex = ds.Tables[0].Rows.Count - 1;
            }
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string command = "Select * FROM Registrars";
                    ds = new DataSet();
                    adapter = new SqlDataAdapter(command, connection);
                    adapter.Fill(ds, "Registrars");
                    RegistrarsDataGrid.ItemsSource = ds.Tables[0].DefaultView;

                    if (RegistrarsDataGrid.Columns.Count > 0)
                    {
                        RegistrarsDataGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }

                    var registrarCommands = new RegistrarsCommands(adapter);
                    registrarCommands.ConfigureCommands(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RegistrarReportButton_Click(object sender, RoutedEventArgs e)
        {
            string reportText = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;

            if (RegistrarsDataGrid.SelectedItem != null)
            {
                var selectedRow = RegistrarsDataGrid.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    firstName += selectedRow["FirstName"].ToString();
                    lastName += selectedRow["LastName"].ToString();
                }
            }
            else
            {
                MessageBox.Show("You didn't choose registrar");
                return;
            }

            reportText = $"This report proves that {firstName} {lastName} is registry office employee";
            
            var report = new StiReport();

            if (report.Pages.Count == 0)
            {
                throw new InvalidOperationException("В отчете отсутствуют страницы!");
            }

            var firstPage = report.Pages[0];

            var textComponent = new Stimulsoft.Report.Components.StiText
            {
                Text = reportText,
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1, 1, 10, 28),
                CanGrow = true, 
                CanBreak = true,
                WordWrap = true 
            };

            firstPage.Components.Add(textComponent);

            report.Show();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            RegistrarsDataGrid.CommitEdit(DataGridEditingUnit.Row, true);

            if (ds != null && ds.HasChanges())
            {
                MessageBoxResult result = MessageBox.Show(
                    "You have unsaved changes. Do you want to save them before closing?",
                    "Unsaved Changes",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            adapter.InsertCommand.Connection = connection;
                            adapter.UpdateCommand.Connection = connection;
                            adapter.DeleteCommand.Connection = connection;

                            adapter.Update(ds, "Registrars");

                            MessageBox.Show("Changes were successfully saved.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while saving changes: {ex.Message}");
                    }
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}