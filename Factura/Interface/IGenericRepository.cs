using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Interface
{
    interface IGenericRepository<T>
    {
        public  Task<IEnumerable<T>> GetAll();
        public  Task<T> GetById(int id);
        public  Task<T> Post(T input);
        public  Task<T> Update(T input, int id);
        public  Task<T> Delete(int id);
    }
}
