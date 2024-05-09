using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoPloomes.Models
{
    public class OperadoraMedico
    {
            public int Id { get; set; }
            public int MedicoId { get; set; } 
            public int OperadoraId { get; set; }                                                    
    }
}