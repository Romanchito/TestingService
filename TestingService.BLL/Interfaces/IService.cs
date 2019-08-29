using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
