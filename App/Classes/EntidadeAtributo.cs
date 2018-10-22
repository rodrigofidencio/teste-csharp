using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class EntidadeAtributo
    {
        public int Id { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public bool StatusDeletado { get; set; }
        public int EntidadeID { get; set; }
        public Entidade Entidade { get; set; }
        public string Atributo { get; set; }
        public string Valor { get; set; }
    }
}