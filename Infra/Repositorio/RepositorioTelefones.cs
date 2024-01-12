using Domain.Interfaces.ITelefones;
using Entities.Entidades;
using Infra.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioTelefones : RepositoryGenerics<Telefones> , InterfaceTelefones
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositorioTelefones()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<IList<Telefones>> ListarTelefonesFornecedor(int IdFornecedor , string emailUsuario , int IdEmpresa)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    (from e in banco.Empresa
                     join f in banco.Fornecedor on e.Id equals f.IdEmpresa
                     join t in banco.Telefones on f.Id equals t.IdFornecedor
                     join ue in banco.UsuarioEmpresa on e.Id equals ue.IdEmpresa
                     where f.Id == IdFornecedor && ue.EmailUsuario.Equals(emailUsuario) && e.Id == IdEmpresa && ue.EmpresaAtual
                     select t).AsNoTracking().ToListAsync();
            }
        }
    }
}
