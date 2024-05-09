using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoPloomes.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        
        public string Senha { get; set; }

    }
}