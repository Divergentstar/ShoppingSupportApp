﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    public class CategoryData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageData Image { get; set; }
        public CategoryData mainCategory { get; set; }

        public CategoryData(){}
    }
}