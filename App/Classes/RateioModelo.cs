using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class RateioModelo
    {
        public int Id { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public bool StatusDeletado { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Posicao { get; set; }
        public int CentroDeCustoID { get; set; }
        public int ProjetoID { get; set; }
        public Projeto Projeto { get; set; }
        public Decimal ValorPercentual { get; set; }
    }
}