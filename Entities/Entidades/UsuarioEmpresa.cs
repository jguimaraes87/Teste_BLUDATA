using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades
{
    [Table("UsuarioEmpresa")]
    public class UsuarioEmpresa
    {
        public int Id { get; set; }
        public string EmailUsuario { get; set; }
        public bool Administrador { get; set; }
        public bool EmpresaAtual { get; set; }

        [ForeignKey("Empresa")]
        [Column(Order = 1)]
        public int IdEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
