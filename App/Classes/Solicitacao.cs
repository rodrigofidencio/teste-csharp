namespace fundagMVC.Classes
{
    public class Solicitacao
    {
        public int SolicitacaoID { get; set; }
        public string TipoSolicitacaoDescricao { get; set; }
        public string Codigo { get; set; }
        public int SolicitanteID { get; set; }
        public string DescricaoSolicitante { get; set; }
        public int MoedaID { get; set; }
        public string DescricaoMoeda { get; set; }
        public int SituacaoID { get; set; }
        public string SituacaoDescricao { get; set; }
        public int ProjetoID { get; set; }
        public string ProjetoDescricao { get; set; }      
        public string ValorTotal { get; set; }
        public string ValorTotalBase { get; set; }
        public string Data { get; set; }

    }
}