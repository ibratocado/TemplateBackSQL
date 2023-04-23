using System;
using System.Collections.Generic;

namespace ApiTemplate
{
    public partial class Articulo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
    }
}
