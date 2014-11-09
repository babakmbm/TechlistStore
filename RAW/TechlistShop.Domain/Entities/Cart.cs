using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechlistShop.Domain.Entities
{
    public class Cart
    {
        private List<CartItem> itemCollection = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            CartItem Item = itemCollection
                            .Where(p => p.Product.ProductID == product.ProductID)
                            .FirstOrDefault();

            if (Item == null)
            {
                itemCollection.Add(new CartItem 
                { Product = product, Quantity = quantity });
            }
            else
            {
                Item.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
            itemCollection.RemoveAll(i => i.Product.ProductID == product.ProductID);
        }

        public decimal ComputeValue()
        {
            return itemCollection.Sum(p => p.Product.Price * p.Quantity);
        }

        public void Clear()
        {
            itemCollection.Clear();
        }

        public IEnumerable<CartItem> Items { get { return itemCollection; } }
    }

    //The Product in the Cart  // CartLine in page 219 of the book
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
