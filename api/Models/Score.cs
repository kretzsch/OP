using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int Value { get; set; }
        public DateTime DateTime { get; set; }
        public List<Comment> Comments { get; set; }
    }
}