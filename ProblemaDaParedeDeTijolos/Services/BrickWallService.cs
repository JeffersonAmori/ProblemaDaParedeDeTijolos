using System;
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
            object _lock1 = new object();
            int minNumberOfBrokenBricks = int.MaxValue;
            var edgesOfBricksInWall = new Dictionary<int, IEnumerable<int>>();

            int wallLenght = input.FirstOrDefault().Sum();

            Parallel.For(1, wallLenght, (i, state) =>
             {
                 var numberOfBrokenBricksInColumn = default(int);
                 Parallel.For(0, input.Count, j =>
                 {
                     var brickLine = input[j];

                     lock (_lock1)
                     {
                         if (!edgesOfBricksInWall.ContainsKey(j))
                             edgesOfBricksInWall.Add(j, Accumulate(brickLine));
                     }

                     var bricksEdges = edgesOfBricksInWall[j];

                     if (!bricksEdges.Contains(i))
                         Interlocked.Add(ref numberOfBrokenBricksInColumn, 1);
                 });

                 Interlocked.Exchange(ref minNumberOfBrokenBricks, Math.Min(minNumberOfBrokenBricks, numberOfBrokenBricksInColumn));

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
