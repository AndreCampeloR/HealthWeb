using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoPloomes.Models
{
    public class Operadora
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }

        public string Nome { get; set; }
        public Empresa Empresa { get; set; }

        public List<Medico> MedicosAssociados { get; set; }
    }
}