using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class AcessoModuloPerfil
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public bool StatusDeletado { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int AcessoModuloID { get; set; }
        public AcessoModulo AcessoModulo { get; set; }
        public int PerfilID { get; set; }
    }
}