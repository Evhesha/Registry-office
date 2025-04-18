using BD6.Windows;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace BD6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ClientsWindow clientsWindow;
        private RegistrarsWindow registrarsWindow;
        private RegistrationsWindow registrationsWindow;
        private ServicesWindow servicesWindow;

        private void TablesButton_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            if (toggleButton != null && toggleButton.ContextMenu != null)
            {
                toggleButton.ContextMenu.PlacementTarget = toggleButton;
                toggleButton.ContextMenu.IsOpen = true;
            }
        }

        private void ClientsWindow_Click(object sender, RoutedEventArgs e)
        {
            clientsWindow = new ClientsWindow();
            clientsWindow.Show();
        }

        private void RegistrarsWindow_Click(object sender, RoutedEventArgs e)
        {
            registrarsWindow = new RegistrarsWindow();
            registrarsWindow.Show();
        }
        
        private void RegistrationsWindow_Click(object sender, RoutedEventArgs e)
        {
            registrationsWindow = new RegistrationsWindow();
            registrationsWindow.Show();
        }

        private void ServicesWindow_Click(object sender, RoutedEventArgs e)
        {
            servicesWindow = new ServicesWindow();
            servicesWindow.Show();
        }

        private void ExitAppButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ReportsButton_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Program was created by Medvedskii Evgenii. Group 10701222 FITR. BNTU. 2025");
        }

        private void AboutProgramButton_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This program about registry office.");
        }
    }
}