using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class Colaborador
    {
        public int ColaboradorID { get; set; }       
        public bool StatusDeletado { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int CodigoDepartamento { get; set; }
        public int CodInstituicao { get; set; }
        public string IntegracaoCodigo { get; set; }
        public ColaboradorBanco ColaboradorBanco { get; set; }
        public ColaboradorAtributo ColaboradorAtributo { get; set; }
        //public PerfilAcesso PerfilAcesso { get; set; }
        public List<CredencialAcesso> CredencialAcesso { get; set; }

    }
}