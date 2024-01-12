using Domain.Interfaces.IFornecedor;
using Entities.Entidades;
using Infra.Config;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioFornecedor : RepositoryGenerics<Fornecedor> , InterfaceFornecedor
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositorioFornecedor()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<IList<Fornecedor>> ListarFornecedorEmpresa(int IdEmpresa , string emailUsuario)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    (from e in banco.Empresa
                     join f in banco.Fornecedor on e.Id equals f.IdEmpresa
                     join ue in banco.UsuarioEmpresa on e.Id equals ue.IdEmpresa
                     where e.Id == IdEmpresa && ue.EmailUsuario.Equals(emailUsuario) && ue.EmpresaAtual
                     orderby f.Nome
                     select f).AsNoTracking().ToListAsync();
            }
        }

        public async Task<IList<Fornecedor>> FiltrarFornecedor(string? nome , string? cpf, string? cnpj ,
                                                                    DateTime? dataDE , DateTime? dataATE , int idEmpresa , string email)
        {
            var filtro = await ListarFornecedorEmpresa(idEmpresa, email);
            { 
                if(!string.IsNullOrEmpty(nome))
                {
                    filtro = filtro.Where(f => f.Nome.ToLower().Contains(nome.ToLower())).ToList();
                }

                if(!string.IsNullOrEmpty(cpf))
                {
                    filtro = filtro.Where(f => f.CPF == cpf).ToList();
                }

                if(!string.IsNullOrEmpty(cnpj))
                {
                    filtro = filtro.Where(f => f.CNPJ == cnpj).ToList();
                }

                if (dataDE.HasValue && dataATE.HasValue)
                {
                    filtro = filtro.Where(f => f.DataCadastro.Date >= dataDE.Value.Date && f.DataCadastro <= dataATE.Value.Date || f.DataCadastro.Date == dataATE.Value.Date).ToList();
                }

                else if(dataDE.HasValue)
                {
                    filtro = filtro.Where(f => f.DataCadastro.Date == dataDE.Value.Date).ToList();
                }

                Console.WriteLine($"Parâmetros de filtro - Nome: {nome}, CPF: {cpf}, CNPJ: {cnpj}, Data DE: {dataDE}, Data ATÉ: {dataATE}, ID Empresa: {idEmpresa}, Email: {email}");


                return filtro;
            }            
        }

        public async Task<int> ContarFornecedoresEmpresa (int IdEmpresa, string emailUsuario)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    (from e in banco.Empresa
                     join f in banco.Fornecedor on e.Id equals f.IdEmpresa
                     join ue in banco.UsuarioEmpresa on e.Id equals ue.IdEmpresa
                     where e.Id == IdEmpresa && ue.EmailUsuario.Equals(emailUsuario) && ue.EmpresaAtual
                     select f).AsNoTracking().CountAsync();
            }
        }

        public Task<IEnumerable<Fornecedor>> FiltrarFornecedor(string? nome, string? cpf, string? cnpj, DateTime? dataCadastro, int idEmpresa, string email)
        {
            throw new NotImplementedException();
        }
    }
}
