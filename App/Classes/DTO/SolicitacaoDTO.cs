using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes.DTO
{
    public class SolicitacaoDTO
    {
        public DateTime DataHotaInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public List<string> TipoSolicitacao { get; set; }
        public List<string> Solicitante { get; set; }
        public List<string> Projeto { get; set; }
        public List<string> Situacao { get; set; }
        public List<string> Pesquisador { get; set; }
        public List<string> Coordenador { get; set; }
        public List<string> UnidadeContratante { get; set; }
        public List<string> UnidadeSolicitadora { get; set; }
        public List<string> UnidadeRealizadora { get; set; }
    }
}