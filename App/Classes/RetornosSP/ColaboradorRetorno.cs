using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes.RetornosSP
{
    public class ColaboradorRetorno 
    {
        public int ColaboradorID;
        public bool StatusDeletado;
        public DateTime DataAtualizacao;
        public string Codigo;
        public string Nome;
        public string Email;
        public int CodigoDepartamento;
        public int CodInstituicao;
        public string IntegracaoCodigo;
    }
}