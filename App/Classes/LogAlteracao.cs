using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class LogAlteracao
    {
        public int Id { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int UsuarioAtualizacao { get; set; }
        public Colaborador Colaborador { get; set; }
        public string Objeto { get; set; }
        public string Descricao { get; set; }
    }
}