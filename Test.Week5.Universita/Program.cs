using System;
using Test.Week5.Universita.Repositories;
using Test.Week5.Universita.Entities;
using System.Collections.Generic;

namespace Test.Week5.Universita
{
    class Program
    {
        static void Main(string[] args)
        {
            //RepositoryStudente.Select();
            //RepositoryEsame.Select();

            //RepositoryStudenteEsame.RegisterExam(1,1);
            //RepositoryStudenteEsame.RegisterExam(1,2);



            //RepositoryStudente.ShowExamsByStudentOrdered(1);

            RepositoryStudente.Add(new Studente()
                {
                    Nome = "Elisa",
                     Cognome = "Mauri",
                     AnnoNascita = 1995
                }
            );
            RepositoryStudente.Select();

            RepositoryEsame e = new RepositoryEsame();
            
        }
    }
}
