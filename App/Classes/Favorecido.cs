using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class Favorecido
    {
        public int Id { get; set; }      
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string BancoCodigo { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string AgenciaDigito { get; set; }
        public string ContaCorrente { get; set; }
        public string Digito { get; set; }     
    }
}