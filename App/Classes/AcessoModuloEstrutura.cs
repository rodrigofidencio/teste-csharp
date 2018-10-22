using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class AcessoModuloEstrutura
    {
        public int Id { get; set; }
        public int EmpresaID { get; set; }
        public int StatusDeletado { get; set; }
        public DateTime DataAtualizacao { get; set; }       
        public Usuario Usuario { get; set; }
        public AcessoModulo AcessoModulo { get; set; }
        public AcessoModulo AcessoModuloPai { get; set; }
    }
}