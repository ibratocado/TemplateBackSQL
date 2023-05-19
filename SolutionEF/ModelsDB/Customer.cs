using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTemplate
{
    public partial class Customer
    {
        public Customer()
        {
            CuatomerArticles = new HashSet<CuatomerArticle>();
        }

        public Guid Id { get; set; }
        public Guid? Account { get; set; }
        public string? Name { get; set; }
        public string? LastNames { get; set; }
        public string? Addres { get; set; }
        public bool Active { get; set; }

        public virtual Account? AccountNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<CuatomerArticle> CuatomerArticles { get; set; }
    }
}
