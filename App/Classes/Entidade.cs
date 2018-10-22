using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class Entidade
    {
        public int Id { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public bool StatusDeletado { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string BancoCodigo { get; set; }
        public string BancoDigito { get; set; }
        public string BancoContaCorrente { get; set; }
        public string BancoContaCorrenteDigito { get; set; }
        public string Email { get; set; }
        public string TipoPessoa { get; set; }
        public string CodigoIntegracao { get; set; }
    }
}