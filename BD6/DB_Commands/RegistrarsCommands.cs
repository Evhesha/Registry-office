using Microsoft.Data.SqlClient;
using System.Data;

namespace BD6.DB_Commands
{
    public class RegistrarsCommands
    {
        private SqlDataAdapter registrarsAdapter;

        public RegistrarsCommands(SqlDataAdapter registrarsAdapter)
        {
            this.registrarsAdapter = registrarsAdapter;
        }

        public void ConfigureCommands(SqlConnection connection)
        {
            registrarsAdapter.InsertCommand = new SqlCommand(
                "INSERT INTO Registrars (FirstName, LastName, FathersName) VALUES (@FirstName, @LastName, @FathersName)",
                connection);
            registrarsAdapter.InsertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255, "FirstName");
            registrarsAdapter.InsertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 255, "LastName");
            registrarsAdapter.InsertCommand.Parameters.Add("@FathersName", SqlDbType.NVarChar, 255, "FathersName");

            registrarsAdapter.UpdateCommand = new SqlCommand(
                "UPDATE Registrars SET FirstName = @FirstName, LastName = @LastName, FathersName = @FathersName WHERE RegistrarId = @RegistrarId",
                connection);
            registrarsAdapter.UpdateCommand.Parameters.Add("@RegistrarId", SqlDbType.Int, 0, "RegistrarId");
            registrarsAdapter.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255, "FirstName");
            registrarsAdapter.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 255, "LastName");
            registrarsAdapter.UpdateCommand.Parameters.Add("@FathersName", SqlDbType.NVarChar, 255, "FathersName");

            registrarsAdapter.DeleteCommand = new SqlCommand(
                "DELETE FROM Registrars WHERE RegistrarId = @RegistrarId",
                connection);
            registrarsAdapter.DeleteCommand.Parameters.Add("@RegistrarId", SqlDbType.Int, 0, "RegistrarId");
        }
    }
}