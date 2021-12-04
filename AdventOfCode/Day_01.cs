using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    
    public class Day_01 : BaseDay
    {
        private readonly List<int> numbers;

        public Day_01()
        {
            numbers = File.ReadAllText(InputFilePath).Split('\n').Select(int.Parse).ToList<int>();

        }

        public override ValueTask<string> Solve_1() {
            var lastNumber = 99999999;
            var counter = 0;
            foreach(var n1 in numbers) {
                if (n1 > lastNumber) counter++;
                lastNumber = n1;
            }

            return new ValueTask<string>(counter.ToString()); 
        }
        

        public override ValueTask<string> Solve_2(){

            int counter =0;
            for (int i = 3; i <numbers.Count; i++) {
                var A = numbers[i-3] + numbers[i-2] + numbers[i-1];
                var B = numbers[i-2] + numbers[i-1] + numbers[i];
                if (B > A) counter ++;
            }

            return new ValueTask<string>(counter.ToString());
        }
    }
}
