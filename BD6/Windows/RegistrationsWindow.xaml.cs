using BD6.DB_Commands;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace BD6.Windows
{
    public partial class RegistrationsWindow : Window
    {
        public RegistrationsWindow()
        {
            InitializeComponent();

            ClientsWindow.DataSaved += RefreshData;
            RegistrarsWindow.DataSaved += RefreshData;
            ServicesWindow.DataSaved += RefreshData;

            ds = new DataSet();
            adapter = new SqlDataAdapter("SELECT * FROM Registrations", connectionString);

            var registrationCommands = new RegistrationCommands(adapter);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                registrationCommands.ConfigureCommands(connection);
            }

            LoadComboBoxData();
        }

        ReportWindow reportWindow;
        FilterWindow filterWindow;
        ClientsWindow clientsWindow;
        RegistrarsWindow registrarsWindow;
        ServicesWindow servicesWindow;

        string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=RegistryOffice; Integrated Security=true;";
        DataSet ds;
        SqlDataAdapter adapter;

        public DataView RegistrarItems { get; set; }
        public DataView ServiceItems { get; set; }
        public DataView ClientItems { get; set; }

        private void RegistrationsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    adapter.SelectCommand = new SqlCommand(query, connection);
                    adapter.Fill(ds, "Registrations");

                    RegistrationsDataGrid.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationsDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)RegistrationsDataGrid.SelectedItem;
                selectedRow.Delete();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ds != null)
            {
                ds.Tables[0].RejectChanges();
                RegistrationsDataGrid.ItemsSource = ds.Tables[0].DefaultView;

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

                    adapter.Update(ds, "Registrations");

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
                RegistrationsDataGrid.SelectedIndex = 0;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationsDataGrid.SelectedIndex < RegistrationsDataGrid.Items.Count - 1)
            {
                RegistrationsDataGrid.SelectedIndex++;
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationsDataGrid.SelectedIndex > 0)
            {
                RegistrationsDataGrid.SelectedIndex--;
            }
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegistrationsDataGrid.SelectedIndex = ds.Tables[0].Rows.Count - 1;
            }
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ds.Tables.Clear();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter.SelectCommand = new SqlCommand(query, connection);
                    adapter.Fill(ds, "Registrations");

                    RegistrationsDataGrid.ItemsSource = ds.Tables["Registrations"].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении данных: {ex.Message}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrarComboBox.SelectedValue == null ||
                ServiceComboBox.SelectedValue == null ||
                Client1ComboBox.SelectedValue == null ||
                DatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(Price.Text))
            {
                MessageBox.Show("Fill in neccessary text!");
                return;
            }

            DataTable dataTable = (RegistrationsDataGrid.ItemsSource as DataView)?.Table;
            if (dataTable == null)
            {
                MessageBox.Show("Mistake: DataGrid doesn't consist data.");
                return;
            }

            try
            {
                DataView dataView = (DataView)RegistrationsDataGrid.ItemsSource;
                DataRow newRow = dataView.Table.NewRow();

                newRow["RegistrarId"] = Convert.ToInt32(RegistrarComboBox.SelectedValue);
                newRow["ServiceId"] = Convert.ToInt32(ServiceComboBox.SelectedValue);
                newRow["ClientId1"] = Convert.ToInt32(Client1ComboBox.SelectedValue);
                newRow["ClientId2"] = Client2ComboBox.SelectedValue != null
                    ? Convert.ToInt32(Client2ComboBox.SelectedValue)
                    : DBNull.Value;
                newRow["RegistrationDate"] = DatePicker.SelectedDate;

                decimal price = decimal.Parse(Price.Text);
                newRow["Price"] = price;
                newRow["ServicePrice"] = price;

                newRow["RegistrarDisplay"] = ((DataRowView)RegistrarComboBox.SelectedItem)["RegistrarDisplay"];
                newRow["ServiceDisplay"] = ((DataRowView)ServiceComboBox.SelectedItem)["ServiceDisplay"];
                newRow["Client1Display"] = ((DataRowView)Client1ComboBox.SelectedItem)["ClientDisplay"];
                if (Client2ComboBox.SelectedItem != null)
                {
                    newRow["Client2Display"] = ((DataRowView)Client2ComboBox.SelectedItem)["ClientDisplay"];
                }

                dataView.Table.Rows.Add(newRow);

                RegistrationsDataGrid.ItemsSource = null;
                RegistrationsDataGrid.ItemsSource = dataView;

                RegistrationsDataGrid.ScrollIntoView(newRow);

                MessageBox.Show("Record was added to table. Click 'save' for saving in DB");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Mistake in record adding: {ex.Message}");
            }
        }

        private void ReportTableButton_Click(object sender, RoutedEventArgs e)
        {
            reportWindow = new ReportWindow();
            reportWindow.ShowDialog();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            filterWindow = new FilterWindow();
            if (filterWindow.ShowDialog() == true)
            {
                if (filterWindow.FilteredDataSet != null && filterWindow.FilteredAdapter != null)
                {
                    ds.Tables.Clear();
                    adapter = filterWindow.FilteredAdapter;
                    ds = filterWindow.FilteredDataSet;

                    var registrationCommands = new RegistrationCommands(adapter);
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        registrationCommands.ConfigureCommands(connection);
                    }

                    RegistrationsDataGrid.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
        }

        private void ServiceWindowButton_Click(object sender, RoutedEventArgs e)
        {
            servicesWindow = new ServicesWindow();
            servicesWindow.ShowDialog();
        }

        private void RegistrarsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            registrarsWindow = new RegistrarsWindow();
            registrarsWindow.ShowDialog();
        }

        private void ClientsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            clientsWindow = new ClientsWindow();
            clientsWindow.ShowDialog();
        }

        private void LoadComboBoxData()
        {
            var currentRegistrar = RegistrarComboBox.SelectedValue;
            var currentService = ServiceComboBox.SelectedValue;
            var currentClient1 = Client1ComboBox.SelectedValue;
            var currentClient2 = Client2ComboBox.SelectedValue;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string registrarQuery = "SELECT RegistrarId, FirstName + ' ' + LastName AS RegistrarDisplay FROM Registrars";
                SqlDataAdapter registrarAdapter = new SqlDataAdapter(registrarQuery, connection);
                DataTable registrarTable = new DataTable();
                registrarAdapter.Fill(registrarTable);
                RegistrarItems = registrarTable.DefaultView;

                string serviceQuery = "SELECT ServiceId, ServiceName AS ServiceDisplay, ServicePrice FROM Services";
                SqlDataAdapter serviceAdapter = new SqlDataAdapter(serviceQuery, connection);
                DataTable serviceTable = new DataTable();
                serviceAdapter.Fill(serviceTable);
                ServiceItems = serviceTable.DefaultView;

                string clientQuery = "SELECT ClientId, FirstName + ' ' + LastName AS ClientDisplay FROM Clients";
                SqlDataAdapter clientAdapter = new SqlDataAdapter(clientQuery, connection);
                DataTable clientTable = new DataTable();
                clientAdapter.Fill(clientTable);
                ClientItems = clientTable.DefaultView;

                RegistrarComboBox.ItemsSource = RegistrarItems;
                RegistrarComboBox.DisplayMemberPath = "RegistrarDisplay";
                RegistrarComboBox.SelectedValuePath = "RegistrarId";

                ServiceComboBox.ItemsSource = ServiceItems;
                ServiceComboBox.DisplayMemberPath = "ServiceDisplay";
                ServiceComboBox.SelectedValuePath = "ServiceId";

                ServiceComboBox.SelectionChanged += ServiceComboBox_SelectionChanged;

                Client1ComboBox.ItemsSource = ClientItems;
                Client1ComboBox.DisplayMemberPath = "ClientDisplay";
                Client1ComboBox.SelectedValuePath = "ClientId";

                Client2ComboBox.ItemsSource = ClientItems;
                Client2ComboBox.DisplayMemberPath = "ClientDisplay";
                Client2ComboBox.SelectedValuePath = "ClientId";

                if (currentRegistrar != null && registrarTable.Rows.Cast<DataRow>().Any(r => r["RegistrarId"].Equals(currentRegistrar)))
                    RegistrarComboBox.SelectedValue = currentRegistrar;

                if (currentService != null && serviceTable.Rows.Cast<DataRow>().Any(r => r["ServiceId"].Equals(currentService)))
                    ServiceComboBox.SelectedValue = currentService;

                if (currentClient1 != null && clientTable.Rows.Cast<DataRow>().Any(r => r["ClientId"].Equals(currentClient1)))
                    Client1ComboBox.SelectedValue = currentClient1;

                if (currentClient2 != null && clientTable.Rows.Cast<DataRow>().Any(r => r["ClientId"].Equals(currentClient2)))
                    Client2ComboBox.SelectedValue = currentClient2;
            }
        }

        private void ServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceComboBox.SelectedItem != null)
            {
                DataRowView selectedService = (DataRowView)ServiceComboBox.SelectedItem;
                decimal servicePrice = Convert.ToDecimal(selectedService["ServicePrice"]);
                Price.Text = servicePrice.ToString();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            RegistrationsDataGrid.CommitEdit(DataGridEditingUnit.Row, true);

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

                            adapter.Update(ds, "Registrations");

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

        private void RefreshData()
        {
            if (IsLoaded)
            {
                try
                {
                    var currentRegistrar = RegistrarComboBox.SelectedValue;
                    var currentService = ServiceComboBox.SelectedValue;
                    var currentClient1 = Client1ComboBox.SelectedValue;
                    var currentClient2 = Client2ComboBox.SelectedValue;
                    var currentDate = DatePicker.SelectedDate;
                    var currentPrice = Price.Text;

                    ds.Tables.Clear();

                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        adapter.SelectCommand = new SqlCommand(query, connection);
                        adapter.Fill(ds, "Registrations");

                        RegistrationsDataGrid.ItemsSource = ds.Tables[0].DefaultView;

                        LoadComboBoxData();
                    }

                    RegistrarComboBox.SelectedValue = currentRegistrar;
                    ServiceComboBox.SelectedValue = currentService;
                    Client1ComboBox.SelectedValue = currentClient1;
                    Client2ComboBox.SelectedValue = currentClient2;
                    DatePicker.SelectedDate = currentDate;
                    Price.Text = currentPrice;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error refreshing data: {ex.Message}");
                }
            }
        }


        private readonly string query = @"
    SELECT r.*, 
        reg.FirstName + ' ' + reg.LastName AS RegistrarDisplay,
        s.ServiceName AS ServiceDisplay,
        s.ServicePrice, 
        c1.FirstName + ' ' + c1.LastName AS Client1Display,
        c2.FirstName + ' ' + c2.LastName AS Client2Display
    FROM Registrations r
    LEFT JOIN Registrars reg ON r.RegistrarId = reg.RegistrarId
    LEFT JOIN Services s ON r.ServiceId = s.ServiceId
    LEFT JOIN Clients c1 ON r.ClientId1 = c1.ClientId
    LEFT JOIN Clients c2 ON r.ClientId2 = c2.ClientId";
    }
}