using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProblemaDaParedeDeTijolos.Services
{
    public class BrickWallService
    {
        public static int CalculateNumberOfMinBrokenBricks(List<List<int>> input)
        {
            int minNumberOfBrokenBricks = int.MaxValue;

            int wallLenght = input.FirstOrDefault().Sum();

            var edgesOfBricksInWall = new Dictionary<int, IEnumerable<int>>();

            for (int i = 1; i < wallLenght; i++)
            {
                var numberOfBrokenBricksInColumn = default(int);
                for (int j = 0; j < input.Count; j++)
                {
                    var brickLine = input[j];

                    if (!edgesOfBricksInWall.ContainsKey(j))
                        edgesOfBricksInWall.Add(j, Accumulate(brickLine));

                    var bricksEdges = edgesOfBricksInWall[j];

                    if (!bricksEdges.Contains(i))
                        numberOfBrokenBricksInColumn++;
                }

                minNumberOfBrokenBricks = Math.Min(minNumberOfBrokenBricks, numberOfBrokenBricksInColumn);
                if (minNumberOfBrokenBricks == 0)
                    return minNumberOfBrokenBricks;
            }

            return minNumberOfBrokenBricks;
        }

        public static int CalculateNumberOfMinBrokenBricksFlatten(List<List<int>> input)
        {
            int minNumberOfBrokenBricks = int.MaxValue;
            int wallLenght = input.FirstOrDefault().Sum();
            var allLineOfBricks = input.SelectMany(w => w);
            var allLineOfBricksAsList = allLineOfBricks.ToList();

            var edgesOfBricksInWall = new Dictionary<int, IEnumerable<int>>();

            for (int i = 0; i < allLineOfBricksAsList.Count();)
            {

                var numberOfBrokenBricksInColumn = default(int);
                var sumOfBriksOnWall = default(int);
                var maxPossibleBrickLine = allLineOfBricksAsList.Skip(i).Take(wallLenght);

                List<int> brickLine = new List<int>();
                int total = 0;
                while (total < wallLenght)
                {
                    var brick = allLineOfBricksAsList[i];
                    if(brick > 1)


                    brickLine.Add(brick);
                    total += brick;
                    i++;
                }

                if (!edgesOfBricksInWall.ContainsKey(i))
                    edgesOfBricksInWall.Add(i, Accumulate(brickLine));

                var bricksEdges = edgesOfBricksInWall[i];
                numberOfBrokenBricksInColumn = brickLine.Count() - bricksEdges.Count();

                if (!bricksEdges.Contains(i))
                    numberOfBrokenBricksInColumn++;

                minNumberOfBrokenBricks = Math.Min(minNumberOfBrokenBricks, numberOfBrokenBricksInColumn);
                if (minNumberOfBrokenBricks == 0)
                    return minNumberOfBrokenBricks;
            }

            return minNumberOfBrokenBricks;

            void GetBricks()
            {

            }
        }

        public static int CalculateNumberOfMinBrokenBricksParallel(List<List<int>> input)
        {
            object _lock1 = new object();
            object _lock2 = new object();
            int minNumberOfBrokenBricks = int.MaxValue;

            int wallLenght = input.FirstOrDefault().Sum();

            var edgesOfBricksInWall = new Dictionary<int, IEnumerable<int>>();

            Parallel.For(1, wallLenght, i =>
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
                         numberOfBrokenBricksInColumn++;
                 });
                 lock (_lock2)
                 {
                     minNumberOfBrokenBricks = Math.Min(minNumberOfBrokenBricks, numberOfBrokenBricksInColumn);
                 }
                 //if (minNumberOfBrokenBricks == 0)
                 //    return minNumberOfBrokenBricks;
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
