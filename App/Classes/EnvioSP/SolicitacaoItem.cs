using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes.EnvioSP
{
    public class SolicitacaoItem
    {
        public int TipoDepesaID;
        public int MoedaID;
        public int LocalidadeID;
        public int AtividadeID;
        public int VeiculoID;
        public DateTime DataDespesa;
        public string HoraDespesa;
        public string Posicicao;
        public string Comentario;
        public decimal ValorTotal;
        public decimal QuantidadePessoa;
        public decimal QuantidadeDiarias;
        public decimal Quantidade;
    }
}