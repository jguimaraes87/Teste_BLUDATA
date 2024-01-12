using Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entidades
{
    [Table("Fornecedor")]
    public class Fornecedor : Base
    {
        public EnumTipoFornecedor TipoFornecedor { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        [ForeignKey("Empresa")]
        [Column(Order = 1)]
        public int IdEmpresa { get; set; }
        //public virtual Empresa Empresa { get; set; }
    }
}
