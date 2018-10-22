using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class LogAplicacao
    {
        public int Id { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string MensagemErro { get; set; }
        public string StackTracer { get; set; }
    }
}