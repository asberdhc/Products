using System;
using System.Collections.Generic;

namespace Products.Models
{
    public partial class ChangesOnProduct
    {
        public int IdLog { get; set; }
        public int IdProduct { get; set; }
        public int ActionMade { get; set; }
    }
}
