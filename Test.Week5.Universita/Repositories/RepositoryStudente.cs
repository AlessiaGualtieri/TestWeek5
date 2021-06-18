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

    public class RepositoryStudente : Repository, IRepositoryStudente
    {
        const string entity = "Studente";

        public static void Select()
        {
            Select(entity);
        }

        public static bool ExistsByID(int ID)
        {
            return ExistsByID(ID, entity);
        }

        public IList<Studente> GetAll()
        {
            IList<Studente> studenti = new List<Studente>();
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
                    studenti.Add
                        (
                            new Entities.Studente()
                            {
                                ID = Int32.Parse(reader["ID"].ToString()),
                                Nome = reader["Nome"].ToString(),
                                Cognome = reader["Cognome"].ToString(),
                                AnnoNascita = Int32.Parse(reader["AnnoNascita"].ToString())
                            }
                        );
                }
                reader.Close();
            }
            return studenti;
        }

        public static void ShowExamsByStudentOrdered(int studenteID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT e.Nome, e.CFU, e.DataEsame, e.Votazione, e.Passato " +
                    "FROM Studente s " +
                    "INNER JOIN StudenteEsame se " +
                    "ON s.ID = se.StudenteID " +
                    "INNER JOIN Esame e " +
                    "ON e.ID = se.EsameID " +
                    "WHERE s.ID = @studenteID " +
                    "ORDER BY e.Votazione, e.DataEsame "
                };

                selectCommand.Parameters.AddWithValue("@StudenteID", studenteID);

                DataSet dataset = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;

                try
                {
                    conn.Open();
                    adapter.Fill(dataset, "Entity");

                    foreach (DataRow row in dataset.Tables["Entity"].Rows)
                        Console.WriteLine($"{row[0]}, {row[1]}, {row[2]}, {row[3]}, {row[4]}");


                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }


        public static void Add(Studente studente)
        {
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
                    CommandText = "INSERT INTO " + entity +
                        " VALUES (@Nome,@Cognome,@AnnoNascita) "
                };

                insertCommand.Parameters.AddWithValue("@Nome", studente.Nome);
                insertCommand.Parameters.AddWithValue("@Cognome", studente.Cognome);
                insertCommand.Parameters.AddWithValue("@AnnoNascita", studente.AnnoNascita);

                DataSet dataset = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;

                try
                {
                    conn.Open();
                    adapter.Fill(dataset, entity);

                    DataRow row = dataset.Tables[entity].NewRow();
                    row["Nome"] = studente.Nome;
                    row["Cognome"] = studente.Cognome;
                    row["AnnoNascita"] = studente.AnnoNascita;

                    dataset.Tables[entity].Rows.Add(row);

                    adapter.Update(dataset, entity);

                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
