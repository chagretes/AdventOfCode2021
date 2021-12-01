using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Rule{
        public int min;
        public int max;
        public char letter;
    }
    public class Day_02 : BaseDay
    {
        private readonly List<string> passwords = new List<string>();
        private readonly List<Rule> rules = new List<Rule>();

        public Day_02()
        {
            var lines = File.ReadAllText(InputFilePath).Split('\n');
            foreach (var line in lines)
            {
                var line_ = line.Split(": ");
                passwords.Add(line_[1]);

                var minMaxLetter = line_[0].Split(" ");

                rules.Add(new Rule{
                    min = int.Parse(minMaxLetter[0].Split('-')[0]),
                    max = int.Parse(minMaxLetter[0].Split('-')[1]),
                    letter = minMaxLetter[1][0]
                });
            }
        }

        public override ValueTask<string> Solve_1() {
            int i = 0;
            int a = 0;
            foreach(var password in passwords)
            {
                var rule = rules[i];
                var count = password.Count(x => x == rule.letter);
                if (count >= rule.min && count <= rule.max)
                {
                    a++;
                }
                i++;
            } 
            return new ValueTask<string>(a.ToString());
        }
        

        public override ValueTask<string> Solve_2(){
            int i = 0;
            int a = 0;
            foreach(var password in passwords)
            {
                var rule = rules[i];
                
                var count = 0;
                if (password[rule.min - 1] == rule.letter)
                {
                    count++;
                }
                if (password[rule.max - 1] == rule.letter)
                {
                    count++;
                }
                if (count == 1)
                {
                    a++;
                }
                i++;
            } 
            return new ValueTask<string>(a.ToString());
        }
    }
}
