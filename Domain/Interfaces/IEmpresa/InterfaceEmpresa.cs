using Domain.Interfaces.Generics;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IEmpresa
{
    public interface InterfaceEmpresa : RepositoryGenerics<Empresa>
    {
        Task<IList<Empresa>> ListarEmpresasUsuario(string emailUsuario);
        Task<Empresa> ObterEmpresaPorId(int Id);
    }
}
