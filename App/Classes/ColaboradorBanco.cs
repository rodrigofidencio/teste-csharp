using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class ColaboradorBanco
    {
        public int BancoID { get; set; }
        public string BancoCodigo { get; set; }
        public string BancoNome { get; set; }
        public string BancoAgencia { get; set; }
        public string BancoAgenciaDigito { get; set; }
        public string BancoContaCorrente { get; set; }
        public string BancoContaCorrenteDigito { get; set; }
    }
}