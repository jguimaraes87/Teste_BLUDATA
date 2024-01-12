using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface ITelefonesServices
    {
        Task AdicionarTelefone(Telefones telefones);
        Task AtualizarTelefones(Telefones telefones);
    }
}
