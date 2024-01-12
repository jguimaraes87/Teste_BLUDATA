using Domain.Interfaces.Generics;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IUsuarioEmpresa
{
    public interface InterfaceUsuarioEmpresa : RepositoryGenerics<UsuarioEmpresa>
    {
        Task<IList<UsuarioEmpresa>> ListarUsuariosEmpresa(int IdEmpresa);
        Task RemoverUsuarioEmpresa(List<UsuarioEmpresa> usuario);
        Task<UsuarioEmpresa> ObterUsuarioPorEmail(string emailUsuario);

    }
}
