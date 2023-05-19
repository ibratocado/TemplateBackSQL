using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTemplate
{
    public partial class StoreArticle
    {
        public Guid Id { get; set; }
        public Guid? Store { get; set; }
        public Guid? Article { get; set; }
        public DateTime? Date { get; set; }

        public virtual Article? ArticleNavigation { get; set; }

        [JsonIgnore]
        public virtual Store? StoreNavigation { get; set; }
    }
}
