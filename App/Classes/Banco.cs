using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class Banco
    {
        public int Id { get; set; }
        public int EmpresaID { get; set; }
        public bool StatusDeletado { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}