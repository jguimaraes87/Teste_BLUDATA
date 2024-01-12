using Domain.Interfaces.IEmpresa;
using Domain.Interfaces.InterfaceServices;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly InterfaceEmpresa _interfaceEmpresa;
        private readonly IEmpresaServices _IEmpresaServices;

        public EmpresaController(InterfaceEmpresa interfaceEmpresa , IEmpresaServices iEmpresaServices)
        {
            _interfaceEmpresa = interfaceEmpresa;
            _IEmpresaServices = iEmpresaServices;
        }

        [HttpGet("/api/ListarEmpresasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarEmpresasUsuario (string emailUsuario)
        {
            return await _interfaceEmpresa.ListarEmpresasUsuario(emailUsuario);
        }
        [HttpPost("/api/AdicionarEmpresa")]
        [Produces("application/json")]
        public async Task<object> AdicionarEmpresa(Empresa empresa)
        {
            await _IEmpresaServices.AdicionarEmpresa(empresa);

            return Task.FromResult(empresa);
        }

        [HttpPut("/api/AtualizarEmpresa")]
        [Produces("application/json")]
        public async Task<object> AtualizarEmpresa(Empresa empresa)
        {
            await _IEmpresaServices.AtualizarEmpresa(empresa);

            return Task.FromResult(empresa);
        }

        [HttpGet("/api/ObterEmpresa")]
        [Produces("application/json")]
        public async Task<object> ObterEmpresa (int id)
        {
            return await _interfaceEmpresa.GetEntityById(id);
        }
    }
}
