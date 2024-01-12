using Domain.Interfaces.IUsuarioEmpresa;
using Entities.Entidades;
using Infra.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioUsuarioEmpresa : RepositoryGenerics<UsuarioEmpresa> , InterfaceUsuarioEmpresa
    {

        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositorioUsuarioEmpresa()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<IList<UsuarioEmpresa>> ListarUsuariosEmpresa(int IdEmpresa)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    banco.UsuarioEmpresa.Where(e => e.IdEmpresa == IdEmpresa).AsNoTracking().ToListAsync();
            }
        }

        public async Task<UsuarioEmpresa> ObterUsuarioPorEmail(string emailUsuario)
        {
           using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                banco.UsuarioEmpresa.AsNoTracking().FirstOrDefaultAsync(x => x.EmailUsuario.Equals(emailUsuario));
            }
        }

        public async Task RemoverUsuarioEmpresa(List<UsuarioEmpresa> usuario)
        {
           using (var banco = new ContextBase(_OptionsBuilder))
            {
                banco.UsuarioEmpresa.RemoveRange(usuario);
                await banco.SaveChangesAsync();
            }
        }
    }
}
