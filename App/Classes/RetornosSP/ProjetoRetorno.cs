using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes.RetornosSP
{
    public class ProjetoRetorno
    {
        public int ProjetoID ;
        public int EmpresaID ;
        public Empresa Empresa ;
        public bool StatusDeletado ;
        public string Codigo ;
        public string CodAcompanhamento ;
        public string Descricao ;
        public string IntegracaoCodigo ;
    }
}