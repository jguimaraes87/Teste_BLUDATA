using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.ITelefones;
using Entities.Entidades;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TelefoneService : ITelefonesServices
    {
        private readonly InterfaceTelefones _interfaceTelefones;

        public TelefoneService(InterfaceTelefones interfaceTelefones)
        {
            _interfaceTelefones = interfaceTelefones;
        }

        public async Task AdicionarTelefone(Telefones telefones)
        {
            await _interfaceTelefones.Add(telefones);
        }

        public async Task AtualizarTelefones(Telefones telefones)
        {
            await _interfaceTelefones.UpDate(telefones);
        }
    }
}
