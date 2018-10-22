using System.Collections.Generic;

namespace fundagMVC.Classes
{
    public class Usuario : MensagemResponse
    {
        public string SessaoID { get; set; }
        public virtual IList<Menu> Menu { get; set; }
        public virtual IList<Solicitacao> SolicitacoesNaoEnviada { get; set; }
        public virtual IList<Solicitacao> SolicitacoesPendente { get; set; }
        public virtual IList<Solicitacao> SolicitacoesFinalizadas { get; set; }
        public virtual IList<Projeto> Projetos { get; set; }
    }
}