using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.IUsuarioEmpresa;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UsuarioEmpresaService : IUsuarioEmpresaServices
    {
        private readonly InterfaceUsuarioEmpresa _interfaceUsuarioEmpresa;

        public UsuarioEmpresaService(InterfaceUsuarioEmpresa interfaceUsuarioEmpresa)
        {
            _interfaceUsuarioEmpresa = interfaceUsuarioEmpresa;
        }
        public async Task CadastrarUsuarioNaEmpresa(UsuarioEmpresa usuarioEmpresa)
        {
            await _interfaceUsuarioEmpresa.Add(usuarioEmpresa);
        }
    }
}
