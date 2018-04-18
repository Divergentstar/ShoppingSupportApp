using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    public class ShopData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ShopNetworkData ShopNetwork { get; set; }

        public ShopData(){}
    }
}
