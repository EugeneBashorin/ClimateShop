using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateStore.Domain.Entities
{
    public class CartLine
    {
        public Product Product { get; set; }//выбранный товар
        public int Quantity { get; set; }//кол-во товаров в корзине
    }

    public class Cart
    {     
        private List<CartLine> lineCollection = new List<CartLine>();
        
        //свойство, которое дает доступ к содержимому корзины
        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if(line == null)//default value
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                     Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(i => i.Product.ProductID == product.ProductID);
        }

        public decimal CalculateTotalValue(/*Product product*/)
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }



    }
}











