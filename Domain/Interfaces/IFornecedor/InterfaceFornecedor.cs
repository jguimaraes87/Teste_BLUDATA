using Domain.Interfaces.Generics;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IFornecedor
{
    public interface InterfaceFornecedor : RepositoryGenerics<Fornecedor>
    {
        Task<IList<Fornecedor>> ListarFornecedorEmpresa(int IdEmpresa , string emailUsuario);


        Task<IList<Fornecedor>> FiltrarFornecedor(string? nome, string? cpf, string? cnpj,
                                                                    DateTime? dataDE, DateTime? dataATE, int idEmpresa, string email);

        Task<int> ContarFornecedoresEmpresa(int IdEmpresa, string emailUsuario);

    }
}
