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
            foreach(var n1 in numbers)
                foreach (var n2 in numbers)
                    if (n1 + n2 == 2020)
                        return new ((n1 * n2).ToString());
            return new ValueTask<string>("");
        }
        

        public override ValueTask<string> Solve_2(){
            foreach(var n1 in numbers)
                foreach(var n2 in numbers)
                    foreach(var n3 in numbers)
                        if (n1 + n2 + n3 == 2020)
                            return new ((n1 * n2 * n3).ToString());
            return new ValueTask<string>("");
        }
    }
}
