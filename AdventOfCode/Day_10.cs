using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_10 : BaseDay
    {
        public List<string> lines = new List<string>();

        public Day_10()
        {
           lines = File.ReadAllText(InputFilePath).Split('\n').ToList();
        }

        bool OpenChunk(char c) => new List<char>{'(','[','{','<'}.Contains(c);

        public override ValueTask<string> Solve_1()
        {
            int syntaxErrorScore = 0;
            
            lines.ForEach(line => {
                Stack<char> stack = new Stack<char>();
                foreach(var c in line) {
                    if(OpenChunk(c)){
                        stack.Push(c);
                    } else if(stack.Count()>0){
                        if (IsPair(stack.First(),c)){
                            stack.Pop();
                        }
                        else {
                            switch (c)
                            {
                                case ')':
                                    syntaxErrorScore+=3;
                                    break;
                                case ']':
                                    syntaxErrorScore+=57;
                                    break;
                                case '}':
                                    syntaxErrorScore+=1197;
                                    break;
                                case '>':
                                    syntaxErrorScore+=25137;
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }
                    }
                };
            });


            return new ValueTask<string>(syntaxErrorScore.ToString());
        }

        private bool IsPair(char first, char last)
        {
            return ((first=='('&& last==')') || (first+2==last+0));
        }

        public override ValueTask<string> Solve_2()
        {
            List<long> scores = new List<long>();
            
            lines.ForEach(line => {
                long syntaxErrorScore = 0;
                Stack<char> stack = new Stack<char>();
                for(int i = 0; i<line.Count();i++) {
                    var c = line[i];
                    if(OpenChunk(c)){
                        stack.Push(c);
                    } else if(stack.Count()>0){
                        if (IsPair(stack.First(),c)){
                            stack.Pop();
                        }
                        else {
                            break;
                        }
                    }

                    if ((i+1==line.Count())&&(stack.Count()>0)){
                        Console.WriteLine("");
                        foreach(var s in stack){
                            Console.Write(s);
                            switch (s)
                            {
                                case '(':
                                    syntaxErrorScore=syntaxErrorScore*5+1;
                                    break;
                                case '[':
                                    syntaxErrorScore=syntaxErrorScore*5+2;
                                    break;
                                case '{':
                                    syntaxErrorScore=syntaxErrorScore*5+3;
                                    break;
                                case '<':
                                    syntaxErrorScore=syntaxErrorScore*5+4;
                                    break;

                                default:
                                    break;
                            }
                        }
                        Console.Write("SCORE: "+syntaxErrorScore);
                        scores.Add(syntaxErrorScore);
                    }
                };
            });

            var median = Median(scores.ToArray());


            return new ValueTask<string>(median.ToString());
        }
        long Median(long[] xs) {
            var ys = xs.OrderBy(x => x).ToList();
            double mid = (ys.Count - 1) / 2.0;
            return ys[(int)(mid)];
        }

    }
}
