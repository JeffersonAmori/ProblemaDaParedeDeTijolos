using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProblemaDaParedeDeTijolos.Services
{
    public class BrickWallService
    {
        public static int CalculateNumberOfMinBrokenBricks(List<List<int>> input)
        {
            var edgesOfBricksInWall = new ConcurrentDictionary<int, IEnumerable<int>>();
            var bricksEdges = new ConcurrentBag<List<int>>();

            Parallel.For(0, input.Count, i =>
            {
                var brickLine = input[i];

                bricksEdges
                    .Add(Accumulate(brickLine.Take(brickLine.Count - 1))
                    .ToList());
            });

            var maxOccurrencesOfBrickEdgeInSameColumn = bricksEdges
                                                           .SelectMany(b => b)
                                                           .GroupBy(b => b)
                                                           .Max(g => g.Count());

            return input.Count - maxOccurrencesOfBrickEdgeInSameColumn;
        }

        private static IEnumerable<int> Accumulate(IEnumerable<int> list)
        {
            int total = 0;
            foreach (var n in list)
            {
                total += n;
                yield return total;
            }
        }
    }
}
