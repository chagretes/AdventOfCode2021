using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Display {
        private List<string> uniqueSignals;
        public List<string> UniqueSignals {
            get{
                return uniqueSignals;
            }
            set{
            uniqueSignals = value.Select(s => SortString(s)).ToList();
        }}
        private List<string> input;
        
        public List<string> Input {
            get{
                return input;
            }
            set{
            input = value.Select(s => SortString(s)).ToList();
        }}
        
        static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }
    }
    public class Day_08 : BaseDay
    {
        public List<Display> displays = new List<Display>();
        public int[] magicNumbers = {2,4,3,7};

        public Day_08()
        {
           File.ReadAllText(InputFilePath).Split('\n').ToList().ForEach(line => {
               displays.Add(new Display{
                   UniqueSignals = line.Split(" | ")[0].Split(" ").ToList(),
                   Input = line.Split(" | ")[1].Split(" ").ToList(),
               });
           });
        }

        public override ValueTask<string> Solve_1()
        {
            var result = displays.Sum(d => d.Input.Sum(x => magicNumbers.Contains(x.Count())? 1:0));

            return new ValueTask<string>(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var result = displays.Sum(display => {
                string[] n = new string[10];
                var signals = display.UniqueSignals;
                n[1] = signals.Find(s => s.Count()==2); signals.Remove(n[1]);
                n[4] = signals.Find(s => s.Count()==4); signals.Remove(n[4]);
                n[7] = signals.Find(s => s.Count()==3); signals.Remove(n[7]);
                n[8] = signals.Find(s => s.Count()==7); signals.Remove(n[8]);
                n[2] = signals.Find(s => s.Count()==5 && s.Count(c => n[4].Contains(c))==2); signals.Remove(n[2]);
                n[5] = signals.Find(s => s.Count()==5 && s.Count(c => n[1].Contains(c))==1); signals.Remove(n[5]);
                n[3] = signals.Find(s => s.Count()==5); signals.Remove(n[3]);
                n[6] = signals.Find(s => s.Count()==6 && s.Count(c => n[1].Contains(c))==1); signals.Remove(n[6]);
                n[9] = signals.Find(s => s.Count()==6 && s.Count(c => n[5].Contains(c))==5); signals.Remove(n[9]);
                n[0] = signals.First();

                var value = Enumerable.Range(0,4).ToList().Sum(i=>
                    n.ToList().IndexOf(display.Input[i]) * Math.Pow(10,(3-i))
                ); 

                Console.WriteLine(value);
                return value;
            });

            return new ValueTask<string>(result.ToString());
        }
    }
}
