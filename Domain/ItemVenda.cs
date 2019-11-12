using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("ItensVenda")]
    public class ItemVenda
    {
        public ItemVenda() { CriadoEm = DateTime.Now; }

        [Key]
        public int ItemVendaId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime CriadoEm { get; set; }
        public string CarrinhoId { get; set; }
    }
}
