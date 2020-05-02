using System.Collections.Generic;

namespace SSU.Reception.Models
{
    public class RatingViewModel
    {
        public IEnumerable<Enrollee> Enrollees { get; set; }
        public Dictionary<Direction, IList<Enrollee>> SortedDirections { get; set; }
    }
}