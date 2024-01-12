using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.IUsuarioEmpresa;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioEmpresaController : ControllerBase
    {
        private readonly InterfaceUsuarioEmpresa _InterfaceUsuarioEmpresa;
        private readonly IUsuarioEmpresaServices _IUsuarioEmpresaServices;

        public UsuarioEmpresaController(InterfaceUsuarioEmpresa interfaceUsuarioEmpresa, IUsuarioEmpresaServices iUsuarioEmpresaServices)
        {
            _InterfaceUsuarioEmpresa = interfaceUsuarioEmpresa;
            _IUsuarioEmpresaServices = iUsuarioEmpresaServices;
        }

        [HttpGet("/api/ListarUsuariosEmpresa")]
        [Produces("application/json")]
        public async Task<object> ListarUsuariosEmpresa (int idEmpresa)
        {
            return await _InterfaceUsuarioEmpresa.ListarUsuariosEmpresa(idEmpresa);
        }

        [HttpPost("/api/CadastrarUsuarioNaEmpresa")]
        [Produces("application/json")]
        public async Task<object> CadastrarUsuarioNaEmpresa(int idEmpresa , string emailUsuario)
        {
            try
            {
                await _IUsuarioEmpresaServices.CadastrarUsuarioNaEmpresa(
                    new UsuarioEmpresa
                    {
                        IdEmpresa = idEmpresa,
                        EmailUsuario = emailUsuario,
                        Administrador = true ,
                        EmpresaAtual = true
                    });
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        [HttpDelete("/api/RemoverUsuarioEmpresa")]
        [Produces("application/json")]
        public async Task<object> RemoverUsuarioEmpresa (int id)
        {
            try
            {
                var usuarioEmpresa = await _InterfaceUsuarioEmpresa.GetEntityById(id);
                await _InterfaceUsuarioEmpresa.Delete(usuarioEmpresa);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
