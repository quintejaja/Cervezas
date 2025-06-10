using System;
using System.Data.SqlClient;

namespace Cervezas
{
    public class CervezaBD
    {
        private string connectionString = "Data Source=SQLSERVER\\SQLSERVER;Initial Catalog=base2;User ID=Base2;Password=Base2;Encrypt=True;TrustServerCertificate=True;";

        public void InsertarBebida(Bebida bebida)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Bebidas (Nombre, Marca, Alcohol, Cantidad, Tipo) VALUES (@Nombre, @Marca, @Alcohol, @Cantidad, @Tipo)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Nombre", bebida.Nombre);
                command.Parameters.AddWithValue("@Cantidad", bebida.Cantidad);

                if (bebida is Cerveza cerveza)
                {
                    command.Parameters.AddWithValue("@Marca", cerveza.Marca);
                    command.Parameters.AddWithValue("@Alcohol", cerveza.Alcohol);
                    command.Parameters.AddWithValue("@Tipo", "Cerveza");
                }
                else if (bebida is Vino vino)
                {
                    command.Parameters.AddWithValue("@Marca", DBNull.Value);
                    command.Parameters.AddWithValue("@Alcohol", vino.Alcohol);
                    command.Parameters.AddWithValue("@Tipo", "Vino");
                }
                else if (bebida is Gaseosa)
                {
                    command.Parameters.AddWithValue("@Marca", DBNull.Value);
                    command.Parameters.AddWithValue("@Alcohol", DBNull.Value);
                    command.Parameters.AddWithValue("@Tipo", "Gaseosa");
                }

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
