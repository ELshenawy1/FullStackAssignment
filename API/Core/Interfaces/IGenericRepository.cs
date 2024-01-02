using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetByID(int id);
        T GetByID(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetEntityWithSpecifications(ISpecification<T> spec);
        List<T> List(ISpecification<T> spec);
        int Count(ISpecification<T> spec);


    }
}
