using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTemplate
{
    public partial class Store
    {
        public Store()
        {
            StoreArticles = new HashSet<StoreArticle>();
        }

        public Guid Id { get; set; }
        public string? Branch { get; set; }
        public string? Addres { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        public virtual ICollection<StoreArticle> StoreArticles { get; set; }
    }
}
