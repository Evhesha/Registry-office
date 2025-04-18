using BD6.DB_Commands;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace BD6.Windows
{
    public partial class ClientsWindow : Window
    {
        public ClientsWindow() => InitializeComponent();

        string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=RegistryOffice; Integrated Security=true;";
        DataSet ds;
        SqlDataAdapter adapter;

        public static event Action DataSaved;

        private void ClientsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string command = "SELECT * FROM Clients";
                    ds = new DataSet();
                    adapter = new SqlDataAdapter(command, connection);
                    adapter.Fill(ds, "Clients");
                    ClientsDataGrid.ItemsSource = ds.Tables[0].DefaultView;

                    if (ClientsDataGrid.Columns.Count > 0)
                    {
                        ClientsDataGrid.Columns[0].Visibility = Visibility.Collapsed; 
                    }

                    var clientCommands = new ClientCommands(adapter);
                    clientCommands.ConfigureCommands(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ClientsDataGrid.SelectedItem;
                selectedRow.Delete();
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ds != null)
            {
                ds.Tables[0].RejectChanges();
                ClientsDataGrid.ItemsSource = ds.Tables[0].DefaultView;

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

                    adapter.Update(ds, "Clients");

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
                ClientsDataGrid.SelectedIndex = 0;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedIndex < ClientsDataGrid.Items.Count - 1)
            {
                ClientsDataGrid.SelectedIndex++;
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedIndex > 0)
            {
                ClientsDataGrid.SelectedIndex--;
            }
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ClientsDataGrid.SelectedIndex = ds.Tables[0].Rows.Count - 1;
            }
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string command = "SELECT * FROM Clients";
                    ds = new DataSet();
                    adapter = new SqlDataAdapter(command, connection);
                    adapter.Fill(ds, "Clients");
                    ClientsDataGrid.ItemsSource = ds.Tables[0].DefaultView;

                    if (ClientsDataGrid.Columns.Count > 0)
                    {
                        ClientsDataGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }

                    var clientCommands = new ClientCommands(adapter);
                    clientCommands.ConfigureCommands(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            ClientsDataGrid.CommitEdit(DataGridEditingUnit.Row, true);

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

                            adapter.Update(ds, "Clients");

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