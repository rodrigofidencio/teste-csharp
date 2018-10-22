using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class CredencialAcesso
    {
        public int Id { get; set; }
        public Empresa Empresa { get; set; }
        public int PerfilDespesaID { get; set; }
        public string UserName { get; set; }
        public string Senha { get; set; }
        public DateTime DataUltimoAcesso { get; set; }
        public bool BloqueadoTentativa { get; set; }
    }
}