using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Week5.Universita.Entities;

namespace Test.Week5.Universita.Repositories
{
    public class RepositoryStudenteEsame : Repository
    {
        const string entity = "StudenteEsame";

        public static void Select()
        {
            Repository.Select(entity);
        }


        public static void RegisterExam(int studenteID, int esameID)
        {
            bool uscire = false; 

            if (! RepositoryEsame.ExistsByID(esameID) )
            {
                Console.WriteLine("Errore, esameID non riconosciuto.");
                uscire = true;
            }
            if ( ! RepositoryStudente.ExistsByID(studenteID) )
            {
                Console.WriteLine("Errore, studenteID non riconosciuto.");
                uscire = true;
            }

            if (uscire)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM " + entity
                };

                SqlCommand insertCommand = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "INSERT INTO " + entity + " VALUES" +
                    "(@StudenteID,@EsameID)"
                };
                insertCommand.Parameters.AddWithValue("@StudenteID", studenteID);
                insertCommand.Parameters.AddWithValue("@EsameID", esameID);

                DataSet dataset = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;

                try
                {
                    conn.Open();
                    adapter.Fill(dataset, entity);

                    DataRow row = dataset.Tables[entity].NewRow();
                    row["StudenteID"] = studenteID;
                    row["EsameID"] = esameID;

                    dataset.Tables[entity].Rows.Add(row);

                    adapter.Update(dataset, entity);
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        public IList<StudenteEsame> GetAll()
        {
            IList<StudenteEsame> se = new List<StudenteEsame>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                SqlCommand command = new SqlCommand()
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM " + entity
                };

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    se.Add
                        (
                            new StudenteEsame()
                            {
                                EsameID = Int32.Parse(reader["EsameID"].ToString()),
                                StudenteID = Int32.Parse(reader["StudenteID"].ToString()),
                            }
                        );
                }
                reader.Close();
            }
            return se;
        }

    }
}
