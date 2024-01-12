using Domain.Interfaces.IEmpresa;
using Domain.Interfaces.InterfaceServices;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class EmpresaService : IEmpresaServices
    {
        private readonly InterfaceEmpresa _interfaceEmpresa;

        public EmpresaService(InterfaceEmpresa interfaceEmpresa)
        {
            _interfaceEmpresa = interfaceEmpresa;
        }
        public async Task AdicionarEmpresa(Empresa empresa)
        {
            //IMPLEMENTAR A VALIDAÇÃO E VERIFICAÇÃO AQUI - ANTES DE ADICIONAR.
            var validarNomeFantasia = empresa.ValidarPropriedadeString(empresa.NomeFantasia, "NomeFantasia");
            var validarCNPJ = empresa.ValidarPropriedadeString(empresa.CNPJ, "CNPJ");
            var validarNome = empresa.ValidarPropriedadeString(empresa.Nome, "Nome");
            var validarUF = empresa.ValidarPropriedadeString(empresa.UF, "UF");


            if (validarNomeFantasia && validarCNPJ && validarNome && validarUF)
            {
                await _interfaceEmpresa.Add(empresa);
            }
        }

        public async Task AtualizarEmpresa(Empresa empresa)
        {
            var validarNomeFantasia = empresa.ValidarPropriedadeString(empresa.NomeFantasia, "NomeFantasia");
            var validarCNPJ = empresa.ValidarPropriedadeString(empresa.CNPJ, "CNPJ");
            var validarNome = empresa.ValidarPropriedadeString(empresa.Nome, "Nome");
            var validarUF = empresa.ValidarPropriedadeString(empresa.UF, "UF");


            if (validarNomeFantasia && validarCNPJ && validarNome && validarUF)
            {
                await _interfaceEmpresa.UpDate(empresa);
            }
        }
    }
}
