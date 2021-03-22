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

        [Theory]
        [InlineData(50, 20)]
        [InlineData(300, 200)]
        [InlineData(1000, 1000)]
        [InlineData(5000, 4000)]
        public void With_Input_Of_Varius_Sizes_For_Preformance_Porposes_N(int columns, int lines)
        {
            Random rand = new Random();

            List<List<int>> input = Enumerable.Range(0, columns)
                .Select(n => new List<int>(Enumerable.Range(0, lines).Select(l => rand.Next(10))))
                .ToList();

            int minNumberOfBrokenBricks = BrickWallService.CalculateNumberOfMinBrokenBricks(input);
        }
    }
}
