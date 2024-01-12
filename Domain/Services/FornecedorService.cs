using Domain.Interfaces.Generics;
using Domain.Interfaces.IEmpresa;
using Domain.Interfaces.IFornecedor;
using Domain.Interfaces.InterfaceServices;
using Entities.Entidades;
using Entities.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Services
{
    public class FornecedorService : IFornecedorServices
    {
        private readonly InterfaceFornecedor _interfaceFornecedor;
        private readonly InterfaceEmpresa _interfaceEmpresa;
        

        public FornecedorService(InterfaceFornecedor interfaceFornecedor , InterfaceEmpresa interfaceEmpresa)
        {
            _interfaceFornecedor = interfaceFornecedor;
            _interfaceEmpresa = interfaceEmpresa;
           
        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Today.Year - dataNascimento.Year;

            if(dataNascimento > DateTime.Today.AddYears(-idade))
            {
                idade--;
            }
            return idade;
        }

        public async Task AdicionarFornecedor(Fornecedor fornecedor)
        {
            
            var validarNome = fornecedor.ValidarPropriedadeString(fornecedor.Nome, "Nome");
            var data = DateTime.Now;

            if (validarNome)
            {
                if (fornecedor.TipoFornecedor == EnumTipoFornecedor.CNPJ) //TIPO FORNECEDOR CNPJ = 1 , CPF =2
                {
                    fornecedor.RG = null;
                    fornecedor.CPF = null;
                    fornecedor.DataNascimento = null;
                    fornecedor.DataAlteracao = null;
                    fornecedor.DataCadastro = data;

                    await _interfaceFornecedor.Add(fornecedor);
                }
                else //SE FOR CPF - PESSOA FÍSICA, VERIFICAR SE É DO PARANÁ PARA VERIFICAR IDADE.
                {
                    Empresa empresa = await _interfaceEmpresa.GetEntityById(fornecedor.IdEmpresa);

                    if (empresa.UF == "PR")
                    {
                        if(fornecedor.DataNascimento == null || CalcularIdade(fornecedor.DataNascimento.Value)<18)
                        {
                            throw new Exception("Fornecedor menor de idade, ou data de nascimento inválida.");
                        }
                    }

                    fornecedor.CNPJ = null;
                    fornecedor.DataAlteracao = null;
                    fornecedor.DataCadastro = data;

                    await _interfaceFornecedor.Add(fornecedor);
                }
            }            
        }

        public async Task AtualizarFornecedor(Fornecedor fornecedor)
        {
            var valido = fornecedor.ValidarPropriedadeString(fornecedor.Nome, "Nome");
            var data = DateTime.Now;

            fornecedor.DataAlteracao = data;
            await _interfaceFornecedor.UpDate(fornecedor);
        }
    }
}
