using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes.RetornoJSON
{
    public class Item
    {
        public string codigo { get; set; }
        public string Descricao { get; set; }
    }

    public class ListaItem
    {
        public List<Item> Itens { get; set; }
    }
}