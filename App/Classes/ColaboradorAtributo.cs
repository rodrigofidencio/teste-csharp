using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class ColaboradorAtributo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public bool StatusDeletado { get; set; }
        public string Atributos { get; set; }
        public string Valor { get; set; }
    }
}