using Microsoft.Data.SqlClient;
using System.Data;

namespace BD6.DB_Commands
{
    public class ServicesCommands
    {
        private SqlDataAdapter servicesAdapter;

        public ServicesCommands(SqlDataAdapter servicesAdapter)
        {
            this.servicesAdapter = servicesAdapter;
        }

        public void ConfigureCommands(SqlConnection connection)
        {
            servicesAdapter.InsertCommand = new SqlCommand(
                "INSERT INTO Services (ServiceName, ServicePrice) VALUES (@ServiceName, @ServicePrice)",
                connection);
            servicesAdapter.InsertCommand.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 255, "ServiceName");
            servicesAdapter.InsertCommand.Parameters.Add("@ServicePrice", SqlDbType.Money, 0, "ServicePrice");

            servicesAdapter.UpdateCommand = new SqlCommand(
                "UPDATE Services SET ServiceName = @ServiceName, ServicePrice = @ServicePrice WHERE ServiceId = @ServiceId",
                connection);
            servicesAdapter.UpdateCommand.Parameters.Add("@ServiceId", SqlDbType.Int, 0, "ServiceId");
            servicesAdapter.UpdateCommand.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 255, "ServiceName");
            servicesAdapter.UpdateCommand.Parameters.Add("@ServicePrice", SqlDbType.Money, 0, "ServicePrice");

            servicesAdapter.DeleteCommand = new SqlCommand(
                "DELETE FROM Services WHERE ServiceId = @ServiceId",
                connection);
            servicesAdapter.DeleteCommand.Parameters.Add("@ServiceId", SqlDbType.Int, 0, "ServiceId");
        }
    }
}