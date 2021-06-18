using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Week5.Universita.Entities
{
    public enum SiNo
    {
        Si,
        No
    }

    public class Esame
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int CFU { get; set; }
        public DateTime DataEsame { get; set; }
        public int Votazione { get; set; }
        public SiNo Passato { get; set; }
    }
}
