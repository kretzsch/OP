using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Score
{
    public class CreateScoreRequestDto
    {
        public int PlayerId { get; set; }
        public int Value { get; set; }
    }
}