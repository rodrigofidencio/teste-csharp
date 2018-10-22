using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes.DTO
{
    public class ReembolsoDTO
    {        
        public int SolicitanteID { get; set; }       
        public int ProjetoID { get; set; }
        public string Titulo { get; set; }
        public List<string> Adiantamentos { get; set; }
        public List<Favorecido> Favorecido { get; set; }
    }
}