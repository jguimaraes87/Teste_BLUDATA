using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades
{
    [Table("Telefones")]
    public class Telefones
    {
        public EnumTipoTelefone TipoTelefone { get; set; }
        public int Id { get; set; }
        public int DDD { get; set; }
        public string NumerTelefone { get; set; }

        [ForeignKey("Fornecedor")]
        [Column(Order = 1)]
        public int IdFornecedor { get; set; }
        //public virtual Fornecedor Fornecedor { get; set; }
    }
}
