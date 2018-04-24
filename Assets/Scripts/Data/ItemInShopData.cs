using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    public class ItemInShopData
    {
        public int Id { get; set; }
        public ItemData Item { get; set; }
        public ShopData Shop { get; set; }
        public double Price { get; set; }

        public ItemInShopData() { }
    }
}
