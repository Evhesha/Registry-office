using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;

namespace BD6.Windows
{
    public partial class FilterWindow : Window
    {
        public SqlDataAdapter FilteredAdapter { get; private set; }
        public DataSet FilteredDataSet { get; private set; }

        public FilterWindow()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=RegistryOffice; Integrated Security=true;";

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                FilteredDataSet = new DataSet();

                SqlCommand command = new SqlCommand(baseQuery, connection);

                if (FromDatePicker.SelectedDate != null && ToDatePicker.SelectedDate != null)
                {
                    string formattedFromDate = FromDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string formattedToDate = ToDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

                    baseQuery += " AND r.RegistrationDate BETWEEN @FromDate AND @ToDate";
                    command.Parameters.AddWithValue("@FromDate", formattedFromDate);
                    command.Parameters.AddWithValue("@ToDate", formattedToDate);
                }

                if (ServiceComboBox.SelectedItem != null)
                {
                    baseQuery += " AND s.ServiceName = @ServiceName";
                    command.Parameters.AddWithValue("@ServiceName", ServiceComboBox.SelectedValue);
                }

                if (RegistrarComboBox.SelectedItem != null)
                {
                    baseQuery += " AND reg.FirstName + ' ' + reg.LastName = @RegistrarName";
                    command.Parameters.AddWithValue("@RegistrarName", RegistrarComboBox.SelectedValue);
                }

                command.CommandText = baseQuery;
                FilteredAdapter = new SqlDataAdapter(command);
                FilteredAdapter.Fill(FilteredDataSet);

                this.DialogResult = true;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ServiceComboBox.IsEnabled = true;
            RegistrarComboBox.IsEnabled = true;
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ServiceComboBox.IsEnabled = false;
            RegistrarComboBox.IsEnabled = false;
        }

        private void LoadComboBoxData()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string registrarQuery = "SELECT FirstName + ' ' + LastName AS RegistrarDisplay FROM Registrars";
                SqlDataAdapter registrarAdapter = new SqlDataAdapter(registrarQuery, connection);
                DataTable registrarTable = new DataTable();
                registrarAdapter.Fill(registrarTable);
                RegistrarComboBox.ItemsSource = registrarTable.DefaultView;
                RegistrarComboBox.DisplayMemberPath = "RegistrarDisplay";
                RegistrarComboBox.SelectedValuePath = "RegistrarDisplay";

                string serviceQuery = "SELECT ServiceName AS ServiceDisplay FROM Services";
                SqlDataAdapter serviceAdapter = new SqlDataAdapter(serviceQuery, connection);
                DataTable serviceTable = new DataTable();
                serviceAdapter.Fill(serviceTable);
                ServiceComboBox.ItemsSource = serviceTable.DefaultView;
                ServiceComboBox.DisplayMemberPath = "ServiceDisplay";
                ServiceComboBox.SelectedValuePath = "ServiceDisplay";
            }
        }

        string baseQuery = @"
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
            LEFT JOIN Clients c2 ON r.ClientId2 = c2.ClientId
            WHERE 1=1";
    }
}
