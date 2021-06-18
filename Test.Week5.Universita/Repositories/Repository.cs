using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Week5.Universita.Repositories
{
    public class Repository
    {
        public const string connectionString = @"Server = .\SQLEXPRESS; 
                Persist Security Info = False;
                Integrated Security = True;
                Initial Catalog = Universita;";

        public static void Select(string entity)
        {
            if (!(entity.Equals("Esame") || entity.Equals("Studente") || entity.Equals("StudenteEsame")))
            {
                Console.WriteLine("Inserire una enità opportuna da visualizzare.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM " + entity
                };

                DataSet dataset = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;

                try
                {
                    conn.Open();
                    adapter.Fill(dataset, "Entity");

                    switch (entity)
                    {
                        case "Esame":
                            foreach (DataRow row in dataset.Tables["Entity"].Rows)
                                Console.WriteLine($"{row[0]} - {row[1]}, {row[2]}, {row[3]}, {row[4]}, {row[5]}");
                            break;
                        case "Studente":
                            foreach (DataRow row in dataset.Tables["Entity"].Rows)
                                Console.WriteLine($"{row[0]} - {row[1]}, {row[2]}, {row[3]}");
                            break;
                        case "StudenteEsame":
                            foreach (DataRow row in dataset.Tables["Entity"].Rows)
                                Console.WriteLine($"( {row[0]} - {row[1]} )");
                            break;
                    }


                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static bool ExistsByID(int ID, string entity)
        {
            bool res = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT ID FROM " + entity + " WHERE ID = @ID"
                };

                selectCommand.Parameters.AddWithValue("@ID", ID);

                DataSet dataset = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;

                try
                {
                    conn.Open();
                    adapter.Fill(dataset, "Entity");
                    int nrRows = dataset.Tables["Entity"].Rows.Count;

                    if (nrRows == 1)
                        res = true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return res;
        }
    }
}
