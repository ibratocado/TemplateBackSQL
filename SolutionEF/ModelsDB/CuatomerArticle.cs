using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTemplate
{
    public partial class CuatomerArticle
    {
        public Guid Id { get; set; }
        public Guid? Cuatomer { get; set; }
        public Guid? Article { get; set; }
        public DateTime? Date { get; set; }

        public virtual Article? ArticleNavigation { get; set; }
        [JsonIgnore]
        public virtual Customer? CuatomerNavigation { get; set; }
    }
}
