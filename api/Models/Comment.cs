using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? PlayerId { get; set; }
        //navigation property
        public Player? Player { get; set; }

        public int? ScoreId { get; set; }
        //navigation property  
        public Score? Score { get; set; }
    }
}