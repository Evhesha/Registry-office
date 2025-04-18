using Microsoft.Data.SqlClient;
using System.Data;

namespace BD6.DB_Commands
{
    public class RegistrationCommands
    {
        private SqlDataAdapter registrationsAdapter = new SqlDataAdapter();

        public RegistrationCommands(SqlDataAdapter registrationsAdapter)
        {
            this.registrationsAdapter = registrationsAdapter;
        }

        public void ConfigureCommands(SqlConnection connection)
        {
            registrationsAdapter.InsertCommand = new SqlCommand("INSERT INTO Registrations (RegistrarId, ServiceId, ClientId1, ClientId2, RegistrationDate, Price) VALUES (@RegistrarId, @ServiceId, @ClientId1, @ClientId2, @RegistrationDate, @Price)", connection);
            registrationsAdapter.InsertCommand.Parameters.Add("@RegistrarId", SqlDbType.Int, 0, "RegistrarId");
            registrationsAdapter.InsertCommand.Parameters.Add("@ServiceId", SqlDbType.Int, 0, "ServiceId");
            registrationsAdapter.InsertCommand.Parameters.Add("@ClientId1", SqlDbType.Int, 0, "ClientId1");
            registrationsAdapter.InsertCommand.Parameters.Add("@ClientId2", SqlDbType.Int, 0, "ClientId2");
            registrationsAdapter.InsertCommand.Parameters.Add("@RegistrationDate", SqlDbType.DateTime, 0, "RegistrationDate");
            registrationsAdapter.InsertCommand.Parameters.Add("@Price", SqlDbType.Decimal, 0, "Price");

            registrationsAdapter.UpdateCommand = new SqlCommand("UPDATE Registrations SET RegistrarId = @RegistrarId, ServiceId = @ServiceId, ClientId1 = @ClientId1, ClientId2 = @ClientId2, RegistrationDate = @RegistrationDate, Price = @Price WHERE RegistrationId = @RegistrationId", connection);
            registrationsAdapter.UpdateCommand.Parameters.Add("@RegistrarId", SqlDbType.Int, 0, "RegistrarId");
            registrationsAdapter.UpdateCommand.Parameters.Add("@ServiceId", SqlDbType.Int, 0, "ServiceId");
            registrationsAdapter.UpdateCommand.Parameters.Add("@ClientId1", SqlDbType.Int, 0, "ClientId1");
            registrationsAdapter.UpdateCommand.Parameters.Add("@ClientId2", SqlDbType.Int, 0, "ClientId2");
            registrationsAdapter.UpdateCommand.Parameters.Add("@RegistrationDate", SqlDbType.DateTime, 0, "RegistrationDate");
            registrationsAdapter.UpdateCommand.Parameters.Add("@Price", SqlDbType.Decimal, 0, "Price");
            registrationsAdapter.UpdateCommand.Parameters.Add("@RegistrationId", SqlDbType.Int, 0, "RegistrationId");

            registrationsAdapter.DeleteCommand = new SqlCommand("DELETE FROM Registrations WHERE RegistrationId = @RegistrationId", connection);
            registrationsAdapter.DeleteCommand.Parameters.Add("@RegistrationId", SqlDbType.Int, 0, "RegistrationId");
        }

    }
}