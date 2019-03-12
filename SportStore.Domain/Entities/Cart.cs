using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.Entities
{
    public class CartLine   //класс для предоставления товара выбранного покупателем и количества этого товара.
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity) //метод добавляет товар в корзину
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();

            if(line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product) //удалить ранее добавленый товар из корзины
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue() // рассчитываем общую стоимость товаров в корзине
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear() // очистить корзину, удалив все выбранное
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines  //свойство котороае дает доступ к содержимому корзины
        {
            get { return lineCollection; }
        }
    }
}
