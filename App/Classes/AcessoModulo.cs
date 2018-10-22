using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes
{
    public class AcessoModulo
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public bool StatusDeletado { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int UsuarioAtualizacao { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public bool Ativo { get; set; }
    }
}