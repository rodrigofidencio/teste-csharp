using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class Reembolso
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public List<Solicitacao> Solicitacao { get; set; }
        public List<Solicitacao> Adiantamento { get; set; }
        public Projeto Projeto { get; set; }
        public Favorecido Favorecido { get; set; }
    }
}