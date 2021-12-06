using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Line{
        public int sx;
        public int sy;
        public int ex;
        public int ey;
    }
    public class Day_05 : BaseDay
    {
        private readonly List<Line> _lines = new List<Line>();
        private int[,] points = new int[999,999];
        private int result;

        public Day_05()
        {
            var input = File.ReadAllText(InputFilePath).Split('\n').ToList();
            foreach(var line in input) {
                _lines.Add(new Line {
                    sx = int.Parse(line.Split(" -> ")[0].Split(',')[0]),
                    sy = int.Parse(line.Split(" -> ")[0].Split(',')[1]),
                    ex = int.Parse(line.Split(" -> ")[1].Split(',')[0]),
                    ey = int.Parse(line.Split(" -> ")[1].Split(',')[1]),
                });
            }
            
        }

        public override ValueTask<string> Solve_1()
        {
            result = 0;
            foreach(var line in _lines) {
                //if((line.ex == line.sx)||(line.sy == line.ey))
                if(line.ex == line.sx) {
                    int x = line.ex;
                    int maxy = line.sy > line.ey ? line.sy : line.ey;
                    int miny = maxy == line.sy ? line.ey: line.sy ;
                    for(int y = miny;y<=maxy;y++){
                        points[x,y] = points[x,y] + 1;
                        if(points[x,y]==2) 
                            result++;
                    }
                }
                else if (line.ey == line.sy) {
                    int y = line.ey;
                    int maxx = line.sx > line.ex ? line.sx : line.ex;
                    int minx = maxx == line.sx ? line.ex : line.sx;
                    for(int x = minx;x<=maxx;x++){
                        points[x,y] = points[x,y] + 1;
                        if(points[x,y]==2) 
                            result++;
                    }
                }
            }

            return new ValueTask<string>(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            foreach(var line in _lines) {
                if(line.ex != line.sx && line.ey != line.sy)
                {
                    int x = line.sx;
                    int y = line.sy;
                    int var_x = line.sx < line.ex ? 1 : -1;
                    int var_y = line.sy < line.ey ? 1 : -1;
                    do {
                        points[x,y] = points[x,y] + 1;
                        if(points[x,y]==2) 
                            result++;
                        x = x + var_x;
                        y = y + var_y;
                    }while(x!=line.ex&&y!=line.ey);

                } else if (line.ex == line.sx && line.ey==line.sy)
                {
                    Console.WriteLine("asd");
                }
                
            }

            return new ValueTask<string>(result.ToString());
        }
    }
}
