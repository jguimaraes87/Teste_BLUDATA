using Domain.Interfaces.Generics;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ITelefones
{
    public interface InterfaceTelefones : RepositoryGenerics<Telefones>
    {
        Task<IList<Telefones>> ListarTelefonesFornecedor(int IdFornecedor , string emailUsuario , int IdEmpresa);
    }
}
