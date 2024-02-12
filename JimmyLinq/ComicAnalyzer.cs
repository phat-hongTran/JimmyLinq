using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyLinq
{
    public class ComicAnalyzer
    {
        public static IEnumerable<string> GetReviews(IEnumerable<Comic> catalog, IEnumerable<Review> reviews)
        {
            //var reviewed = from comic in catalog
            //               orderby comic.Issue
            //               join review in reviews on comic.Issue equals review.Issue
            //               select $"{review.Critic} rated #{comic.Issue} '{comic.Name}' {review.Score:0.00}";

            var reviewed = catalog.OrderBy(x => x.Issue).
                Join(reviews, x => x.Issue, y => y.Issue, (x, y) => $"{y.Critic} rated #{x.Issue} '{x.Name}' {y.Score:0.00}");

            return reviewed;
        }

        public static IEnumerable<IGrouping<PriceRange, Comic>> GroupComicsByPrice(IEnumerable<Comic> catalog, IReadOnlyDictionary<int, decimal> prices)
        {
            //var grouped = from comic in catalog
            //              orderby prices[comic.Issue]
            //              group comic by CalculatePriceRange(comic, prices) into groups
            //              select groups;

            var grouped = catalog.OrderBy(x => prices[x.Issue]).GroupBy(x => CalculatePriceRange(x, prices));

            return grouped;
        }

        private static PriceRange CalculatePriceRange(Comic comic, IReadOnlyDictionary<int, decimal> prices)
        {
            if (prices[comic.Issue] > 100M)
            {
                return PriceRange.Expensive;
            }
            else
            {
                return PriceRange.Cheap;
            }
        }
    }
}
