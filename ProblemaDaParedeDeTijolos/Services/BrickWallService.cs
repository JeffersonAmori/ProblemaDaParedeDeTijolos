using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProblemaDaParedeDeTijolos.Services
{
    public class BrickWallService
    {
        public static int CalculateNumberOfMinBrokenBricks(List<List<int>> input)
        {
            int minNumberOfBrokenBricks = int.MaxValue;
            var edgesOfBricksInWall = new ConcurrentDictionary<int, IEnumerable<int>>();

            int wallLength = input.FirstOrDefault().Sum();

            // Iterate over the columns of the wall
            Parallel.For(1, wallLength, (i, state) =>
             {
                 // For each column iterate over each line/row of the wall
                 var numberOfBrokenBricksInColumn = default(int);
                 Parallel.For(0, input.Count, j =>
                 {
                     var brickLine = input[j];

                     edgesOfBricksInWall.TryAdd(j, Accumulate(brickLine));

                     var bricksEdges = edgesOfBricksInWall[j];

                     if (!bricksEdges.Contains(i))
                         Interlocked.Add(ref numberOfBrokenBricksInColumn, 1);
                 });

                 Interlocked.Exchange(ref minNumberOfBrokenBricks, Math.Min(minNumberOfBrokenBricks, numberOfBrokenBricksInColumn));

                 // If there is a line that would break no bricks end finish processing
                 if (minNumberOfBrokenBricks == 0)
                     state.Break();
             });

            return minNumberOfBrokenBricks;
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
