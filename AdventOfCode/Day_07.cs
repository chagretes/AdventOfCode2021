using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_07 : BaseDay
    {
        private List<int> positions;

        public Day_07()
        {
           positions = File.ReadAllText(InputFilePath).Split(',').Select(int.Parse).ToList<int>();
        }

        public override ValueTask<string> Solve_1()
        {
            var min = positions.Min();
            var max = positions.Max();
            var fuel = int.MaxValue;
            for(int postiion = min; postiion <= max; postiion++){
                var currentFuel = positions.Sum(x => Math.Abs(x-postiion));
                fuel = Math.Min(currentFuel,fuel);
            }
            
            var result = fuel;

            return new ValueTask<string>(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var min = positions.Min();
            var max = positions.Max();
            var fuel = int.MaxValue;
            for(int postiion = min; postiion <= max; postiion++){
                var currentFuel = positions.Sum(x => {
                    var current = x;
                    var fuelTotal = 0;
                    var fuelCost = 1;
                    var increment = x < postiion?1:-1;
                    while(current!=postiion){
                        fuelTotal += fuelCost;
                        fuelCost++;
                        current+=increment;
                    }
                    return fuelTotal;
                });
                fuel = Math.Min(currentFuel,fuel);
            }
            
            var result = fuel;

            return new ValueTask<string>(result.ToString());
        }
    }
}
