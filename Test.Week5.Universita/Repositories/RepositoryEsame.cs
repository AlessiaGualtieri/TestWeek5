using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Week5.Universita.Entities;

namespace Test.Week5.Universita.Repositories
{
    public class RepositoryEsame : Repository, IRepositoryEsame
    {
        const string entity = "Esame";

        public static void Select()
        {
            Select(entity);
        }

        public static bool ExistsByID(int ID)
        {
            return ExistsByID(ID, entity);
        }

        public IList<Esame> GetAll()
        {
            IList<Esame> esami = new List<Esame>();
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
                    esami.Add
                        (
                            new Esame()
                            {
                                ID = Int32.Parse(reader["ID"].ToString()),
                                Nome = reader["Nome"].ToString(),
                                CFU = Int32.Parse(reader["CFU"].ToString()),
                                DataEsame = DateTime.Parse(reader["DataEsame"].ToString()),
                                Votazione = Int32.Parse(reader["Votazione"].ToString()),
                                Passato = SiNo.Si       ///////////////////////////////////////
                            }
                        );
                }
                reader.Close();
            }
            return esami;
        }

        public IList<Esame> ORderedVotazioneData()
        {
            return GetAll().OrderBy(e => e.Votazione).ThenBy(e => e.DataEsame).ToList();
        }


    }
}
