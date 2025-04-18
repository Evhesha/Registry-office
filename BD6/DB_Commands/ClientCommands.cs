using Microsoft.Data.SqlClient;
using System.Data;

namespace BD6.DB_Commands
{
    public class ClientCommands
    {
        private SqlDataAdapter clientsAdapter;

        public ClientCommands(SqlDataAdapter clientsAdapter)
        {
            this.clientsAdapter = clientsAdapter;
        }

        public void ConfigureCommands(SqlConnection connection)
        {
            clientsAdapter.InsertCommand = new SqlCommand("INSERT INTO Clients (FirstName, LastName, FathersName, Gender, PassportData) VALUES (@FirstName, @LastName, @FathersName, @Gender, @PassportData)", connection);
            clientsAdapter.InsertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255, "FirstName");
            clientsAdapter.InsertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 255, "LastName");
            clientsAdapter.InsertCommand.Parameters.Add("@FathersName", SqlDbType.NVarChar, 255, "FathersName");
            clientsAdapter.InsertCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 1, "Gender");
            clientsAdapter.InsertCommand.Parameters.Add("@PassportData", SqlDbType.NVarChar, 255, "PassportData");

            clientsAdapter.UpdateCommand = new SqlCommand("UPDATE Clients SET FirstName = @FirstName, LastName = @LastName, FathersName = @FathersName, Gender = @Gender, PassportData = @PassportData WHERE ClientId = @ClientId", connection);
            clientsAdapter.UpdateCommand.Parameters.Add("@ClientId", SqlDbType.Int, 0, "ClientId");
            clientsAdapter.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255, "FirstName");
            clientsAdapter.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 255, "LastName");
            clientsAdapter.UpdateCommand.Parameters.Add("@FathersName", SqlDbType.NVarChar, 255, "FathersName");
            clientsAdapter.UpdateCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 1, "Gender");
            clientsAdapter.UpdateCommand.Parameters.Add("@PassportData", SqlDbType.NVarChar, 255, "PassportData");

            clientsAdapter.DeleteCommand = new SqlCommand("DELETE FROM Clients WHERE ClientId = @ClientId", connection);
            clientsAdapter.DeleteCommand.Parameters.Add("@ClientId", SqlDbType.Int, 0, "ClientId");
        }
    }
}