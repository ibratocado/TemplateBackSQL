using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTemplate
{
    public partial class Article
    {
        public Article()
        {
            CuatomerArticles = new HashSet<CuatomerArticle>();
            StoreArticles = new HashSet<StoreArticle>();
        }

        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Image { get; set; }
        public int? Stock { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        public virtual ICollection<CuatomerArticle> CuatomerArticles { get; set; }
        [JsonIgnore]
        public virtual ICollection<StoreArticle> StoreArticles { get; set; }
    }
}
