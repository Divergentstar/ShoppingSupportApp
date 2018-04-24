using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    public class ItemData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageData Image { get; set; }
        public CategoryData Category { get; set; }

        public ItemData() { }
    }
}
