using System;
using System.Linq;
using System.Collections.Generic;
using ProblemaDaParedeDeTijolos.Services;

namespace ProblemaDaParedeDeTijolos
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> input = new List<List<int>>()
            {
                new List<int>(){ 1, 2, 2, 1 },
                new List<int>(){ 3, 1, 2 },
                new List<int>(){ 1, 3, 2 },
                new List<int>(){ 2, 4 },
                new List<int>(){ 3, 1, 2 },
                new List<int>(){ 1, 3, 1, 1 }
            };

            int minNumberOfBrokenBricks = BrickWallService.CalculateNumberOfMinBrokenBricks(input);

            Console.WriteLine(minNumberOfBrokenBricks);
        }
    }}
