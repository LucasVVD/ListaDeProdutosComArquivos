using ListaDeProdutosComArquivos.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeProdutosComArquivos.Entities
{
    internal class Products
    {
        public string ProdName { get; set; }
        public double ProdPrice { get; set; }
        public int Quantity { get; set; }
        public Category ProdCategory { get; set; }
        public DateTime AddLog { get; set; } = DateTime.Now;

        public Products(string? prodName, double prodPrice, int quantity, Category prodCategory)
        {
            ProdName = prodName;
            ProdPrice = prodPrice;
            Quantity = quantity;
            ProdCategory = prodCategory;
        }

        public double TotalPrice()
        {
            return (double)ProdPrice * Quantity;
        }

        public string RandomId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
