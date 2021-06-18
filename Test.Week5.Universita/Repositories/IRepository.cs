using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Week5.Universita.Repositories
{
    public interface IRepository<T>
    {
        public IList<T> GetAll();
    }
}
