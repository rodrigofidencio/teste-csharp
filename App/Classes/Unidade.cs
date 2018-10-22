using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class Unidade
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string TipoPessoa { get; set; }
        public string IntegracaoCodigo { get; set; }
    }
}