using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class Menu
    {
        public int Codigo { get; set; }
        public int codigoPai { get; set; }
        public string Descricao { get; set; }
        public string DescricaoPai { get; set; }
        public string Url { get; set; }
        public string UrlPai { get; set; }
        public bool Ativo { get; set; }     
    }
}