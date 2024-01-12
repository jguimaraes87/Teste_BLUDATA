using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.ITelefones;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TelefoneController : ControllerBase
    {
        private readonly InterfaceTelefones _interfaceTelefones;
        private readonly ITelefonesServices _iTelefonesServices;

        public TelefoneController(InterfaceTelefones interfaceTelefones, ITelefonesServices iTelefonesServices)
        {
            _interfaceTelefones = interfaceTelefones;
            _iTelefonesServices = iTelefonesServices;
        }

        [HttpGet("/api/ListarTelefones")]
        [Produces("application/json")]
        public async Task<object> ListarTelefones(int IdFornecedor, string emailUsuario, int IdEmpresa)
        {
            return await _interfaceTelefones.ListarTelefonesFornecedor(IdFornecedor, emailUsuario, IdEmpresa);
        }

        [HttpPost("/api/AdicionarTelefone")]
        [Produces("application/json")]
        public async Task<object> AdicionarTelefone(List<Telefones> telefones)
        {
            foreach(var telefone in telefones)
            {
                await _iTelefonesServices.AdicionarTelefone(telefone);
            }
            return Task.FromResult(telefones);
        }

        [HttpPut("/api/AtualizarTelefones")]
        [Produces("application/json")]
        public async Task<object> AtualizarTelefones(List<Telefones> telefones)
        {
            foreach (var telefone in telefones)
            {
                await _iTelefonesServices.AtualizarTelefones(telefone);
            }

            return Task.FromResult(telefones);
        }
    }
}
