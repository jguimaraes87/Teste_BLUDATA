using Domain.Interfaces.IEmpresa;
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
    public class RepositorioEmpresa : RepositoryGenerics<Empresa> , InterfaceEmpresa
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositorioEmpresa()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<IList<Empresa>> ListarEmpresasUsuario(string emailUsuario)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    (from e in banco.Empresa
                     join ue in banco.UsuarioEmpresa on e.Id equals ue.IdEmpresa
                     where ue.EmailUsuario.Equals(emailUsuario)
                     orderby e.NomeFantasia
                     select e).AsNoTracking().ToListAsync();
            }
        }

        public async Task<Empresa> ObterEmpresaPorId(int Id)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    banco.Empresa.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(Id));
            }
        }
    }
}
