using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppmwebapp.repo.Interfaces
{
    public interface IRepository<T> where T : class
    {
        
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T obj);
       T Edit(T obj);
       T Delete(int id);
    }
}
