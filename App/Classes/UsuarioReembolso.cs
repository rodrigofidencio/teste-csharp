using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class UsuarioReembolso
    {
        public List<Colaborador> Colaborador { get; set; }
        public List<Solicitacao> Solicitacaos { get; set; }
        public List<Solicitacao> Adiantamentos { get; set; }
    }
}