namespace fundagMVC.Classes
{
    public class Projeto
    {
        public int ProjetoID { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public bool StatusDeletado { get; set; }
        public string Codigo { get; set; }
        public string CodAcompanhamento { get; set; }
        public string Descricao { get; set; }
        public string IntegracaoCodigo { get; set; }
    }
}