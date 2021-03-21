using System;
using System.Linq;
using System.Collections.Generic;

namespace ProblemaDaParedeDeTijolos
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> input = new List<List<int>>()
            {
                new List<int>(){ 1, 2, 2, 1 },
                new List<int>(){ 3, 1, 2},
                new List<int>(){ 1, 3, 2 },
                new List<int>(){ 2, 4 },
                new List<int>(){ 3, 1, 2 },
                new List<int>(){ 1, 3, 1, 1 }
            };

            int minNumberOfBrokenBricks = CalculateNumberOfMinBrokenBricks(input);

            Console.WriteLine(minNumberOfBrokenBricks);
        }

        private static int CalculateNumberOfMinBrokenBricks(List<List<int>> input)
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

        private static IEnumerable<int> Accumulate(IEnumerable<int> list)
        {
            int total = 0;
            foreach (var n in list)
            {
                total += n;
                yield return total;
            }
        }
    }}
