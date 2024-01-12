using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generics
{
    public interface RepositoryGenerics<T> where T : class
    {
        Task Add(T Objeto);
        Task UpDate(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
        Task<T> GetEntityByName(string Name);
        Task<T> GetEntityByDate(DateTime DataCadastro);
    }
}
