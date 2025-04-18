using Microsoft.Data.SqlClient;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.Table;
using System.Data;
using System.Windows;

namespace BD6.Windows
{
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=RegistryOffice; Integrated Security=true;";
        DataSet ds;
        SqlDataAdapter adapter;

        private void CreateReportButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? fromDate = FromDatePicker.SelectedDate;
            DateTime? toDate = ToDatePicker.SelectedDate;

            if (!fromDate.HasValue || !toDate.HasValue)
            {
                MessageBox.Show("Выберите даты для отчета.");
                return;
            }

            string formattedFromDate = fromDate.Value.ToString("yyyy-MM-dd");
            string formattedToDate = toDate.Value.ToString("yyyy-MM-dd");

            // Заполняем DataSet данными
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                ds = new DataSet();

                string baseQuery = $"SELECT * FROM dbo.GetRegistrationsDataByDateRange('{formattedFromDate}', '{formattedToDate}')";

                if (ServiceComboBox.SelectedItem != null && RegistrarComboBox.SelectedItem != null)
                {
                    baseQuery += $" WHERE ServiceName = '{ServiceComboBox.SelectedValue}' AND RegistrarName = '{RegistrarComboBox.SelectedValue}'";
                }
                else if (ServiceComboBox.SelectedItem != null)
                {
                    baseQuery += $" WHERE ServiceName = '{ServiceComboBox.SelectedValue}'";
                }
                else if (RegistrarComboBox.SelectedItem != null)
                {
                    baseQuery += $" WHERE RegistrarName = '{RegistrarComboBox.SelectedValue}'";
                }

                adapter = new SqlDataAdapter(baseQuery, connection);
                adapter.Fill(ds);
            }

            var report = new StiReport();
            if (report.Pages.Count == 0)
            {
                report.Pages.Add(new Stimulsoft.Report.Components.StiPage());
            }

            var page = report.Pages[0];

            var headerText = new StiText
            {
                Text = $"Report about registrations in registry office\n" +
                      $"From {fromDate.Value:dd.MM.yyyy} to {toDate.Value:dd.MM.yyyy}",
                HorAlignment = StiTextHorAlignment.Center,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                Height = 1.5,
                Dockable = true,
                ClientRectangle = new RectangleD(0, 0, page.Width, 1.5)
            };
            page.Components.Add(headerText);

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                var noDataText = new StiText
                {
                    Text = "Нет данных за указанный период",
                    HorAlignment = StiTextHorAlignment.Center,
                    Font = new System.Drawing.Font("Arial", 12),
                    Top = 2,
                    Height = 0.5,
                    Dockable = true,
                    ClientRectangle = new RectangleD(0, 2, page.Width, 0.5)
                };
                page.Components.Add(noDataText);
            }
            else
            {
                var panel = new StiPanel
                {
                    Top = 2,
                    Left = 0,
                    Width = page.Width,
                    Height = 0.5 * (ds.Tables[0].Rows.Count + 2), 
                    Dockable = true,
                    Border = new StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid)
                };
                page.Components.Add(panel);

                double columnWidth = panel.Width / ds.Tables[0].Columns.Count;
                double currentTop = 0;

                var headerRow = new StiPanel
                {
                    Top = currentTop,
                    Left = 0,
                    Width = panel.Width,
                    Height = 0.5,
                    Dockable = true
                };
                panel.Components.Add(headerRow);

                double leftPosition = 0;
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    var headerCell = new StiText
                    {
                        Text = ds.Tables[0].Columns[i].ColumnName,
                        HorAlignment = StiTextHorAlignment.Center,
                        Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                        Border = new StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid),
                        ClientRectangle = new RectangleD(leftPosition, 0, columnWidth, 0.5)
                    };
                    headerRow.Components.Add(headerCell);
                    leftPosition += columnWidth;
                }

                currentTop += 0.5;

                decimal finalPrice = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var dataRow = new StiPanel
                    {
                        Top = currentTop,
                        Left = 0,
                        Width = panel.Width,
                        Height = 0.5,
                        Dockable = true
                    };
                    panel.Components.Add(dataRow);

                    leftPosition = 0;
                    for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
                    {
                        // Форматируем значение в зависимости от типа данных
                        string cellValue;
                        if (row[col] == DBNull.Value)
                        {
                            cellValue = "No data";
                        }
                        else if (row[col] is DateTime)
                        {
                            // Форматируем дату без времени
                            cellValue = ((DateTime)row[col]).ToString("dd.MM.yyyy");
                        }
                        else
                        {
                            cellValue = row[col].ToString();
                        }

                        var dataCell = new StiText
                        {
                            Text = cellValue,
                            HorAlignment = StiTextHorAlignment.Left,
                            Border = new StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid),
                            ClientRectangle = new RectangleD(leftPosition, 0, columnWidth, 0.5)
                        };
                        dataRow.Components.Add(dataCell);
                        leftPosition += columnWidth;

                        if (col == 5 && row[col] != DBNull.Value)
                        {
                            finalPrice += Convert.ToDecimal(row[col]);
                        }
                    }
                    currentTop += 0.5;
                }

                var footerRow = new StiPanel
                {
                    Top = currentTop,
                    Left = 0,
                    Width = panel.Width,
                    Height = 0.5,
                    Dockable = true
                };
                panel.Components.Add(footerRow);

                var footerCell = new StiText
                {
                    Text = $"Final sum: {finalPrice}",
                    HorAlignment = StiTextHorAlignment.Right,
                    Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                    ClientRectangle = new RectangleD(0, 0, panel.Width - 10, 0.5)
                };
                footerRow.Components.Add(footerCell);

                panel.Height = currentTop + 0.5;
            }

            report.Show();
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
    }
}