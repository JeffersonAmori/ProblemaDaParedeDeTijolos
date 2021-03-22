using ProblemaDaParedeDeTijolos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProblemaDaParedeDeTijolos.Tests
{
    public class BrickWallServiceTests
    {
        [Fact]
        public void With_Example_Input_Should_Return_2()
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

            int minNumberOfBrokenBricks = BrickWallService.CalculateNumberOfMinBrokenBricks(input);

            Assert.Equal(2, minNumberOfBrokenBricks);
        }

        [Fact]
        public void With_Input_Of_Any_Size()
        {
            Random rand = new Random();

            List<List<int>> input = Enumerable.Range(0, 300)
                .Select(n => new List<int>(Enumerable.Range(0, 200).Select(l => rand.Next(10))))
                .ToList();

            int minNumberOfBrokenBricks = BrickWallService.CalculateNumberOfMinBrokenBricks(input);
        }
    }
}
