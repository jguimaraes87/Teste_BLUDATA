using Domain.Interfaces.IFornecedor;
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
    public class FornecedorController : ControllerBase
    {
        private readonly InterfaceFornecedor _interfaceFornecedor;
        private readonly IFornecedorServices _iFornecedorServices;

        public FornecedorController(InterfaceFornecedor interfaceFornecedor, IFornecedorServices iFornecedorServices)
        {
            _interfaceFornecedor = interfaceFornecedor;
            _iFornecedorServices = iFornecedorServices;
        }

        [HttpGet("/api/ListarFornecedorEmpresa")]
        [Produces("application/json")]
        public async Task<object> ListarFornecedorEmpresa(int IdEmpresa, string emailUsuario)
        {
            return await _interfaceFornecedor.ListarFornecedorEmpresa(IdEmpresa, emailUsuario);
        }

        [HttpGet("/api/ContarFornecedoresEmpresa")]
        [Produces("application/json")]
        public async Task<int> ContarFornecedoresEmpresa (int IdEmpresa, string emailUsuario)
        {
            return await _interfaceFornecedor.ContarFornecedoresEmpresa(IdEmpresa, emailUsuario);
        }

        [HttpPost("/api/AdicionarFornecedor")]
        [Produces("application/json")]
        public async Task<object> AdicionarFornecedor(Fornecedor fornecedor)
        {
            await _iFornecedorServices.AdicionarFornecedor(fornecedor);

            return fornecedor;
        }

        [HttpPut("/api/AtualizarFornecedor")]
        [Produces("application/json")]
        public async Task<object> AtualizarFornecedor(Fornecedor fornecedor)
        {
            await _iFornecedorServices.AtualizarFornecedor(fornecedor);

            return Task.FromResult(fornecedor);
        }

        [HttpDelete("/api/RemoverFornecedor")]
        [Produces("application/json")]
        public async Task<object> RemoverFornecedor(int id)
        {
            try
            {
                var fornecedor = await _interfaceFornecedor.GetEntityById(id);
                await _interfaceFornecedor.Delete(fornecedor);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        [HttpGet("/api/ObterFornecedor")]
        [Produces("application/json")]
        public async Task<object> ObterFornecedor(int id)
        {
            return await _interfaceFornecedor.GetEntityById(id);

        }

        [HttpGet("/api/FiltrarFornecedor")]
        [Produces("application/json")]
        public async Task<object> FiltrarFornecedor (string? nome, string? cpf, string? cnpj,
                                                     DateTime? dataDE, DateTime? dataATE, int idEmpresa, string email)
        {
            return await _interfaceFornecedor.FiltrarFornecedor(nome, cpf, cnpj, dataDE, dataATE, idEmpresa, email);
        }
    }
}
