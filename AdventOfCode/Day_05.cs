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
        public int x1;
        public int y1;
        public int x2;
        public int y2;
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
                    x1 = int.Parse(line.Split(" -> ")[0].Split(',')[0]),
                    y1 = int.Parse(line.Split(" -> ")[0].Split(',')[1]),
                    x2 = int.Parse(line.Split(" -> ")[1].Split(',')[0]),
                    y2 = int.Parse(line.Split(" -> ")[1].Split(',')[1]),
                });
            }
            
        }

        public override ValueTask<string> Solve_1()
        {
            result = 0;
            foreach(var line in _lines) {
                //if((line.ex == line.sx)||(line.sy == line.ey))
                if(line.x2 == line.x1) {
                    int x = line.x2;
                    int maxy = line.y1 > line.y2 ? line.y1 : line.y2;
                    int miny = maxy == line.y1 ? line.y2: line.y1 ;
                    for(int y = miny;y<=maxy;y++){
                        points[x,y] = points[x,y] + 1;
                        if(points[x,y]==2) 
                            result++;
                    }
                }
                else if (line.y2 == line.y1) {
                    int y = line.y2;
                    int maxx = line.x1 > line.x2 ? line.x1 : line.x2;
                    int minx = maxx == line.x1 ? line.x2 : line.x1;
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
                if(line.x2 != line.x1 && line.y2 != line.y1)
                {
                    int x = line.x1;
                    int y = line.y1;
                    int var_x = line.x1 < line.x2 ? 1 : -1;
                    int var_y = line.y1 < line.y2 ? 1 : -1;
                    do {
                        points[x,y] = points[x,y] + 1;
                        if(points[x,y]==2) 
                            result++;
                        x = x + var_x;
                        y = y + var_y;
                    }while(x!=line.x2&&y!=line.y2);

                }
            }

            return new ValueTask<string>(result.ToString());
        }
    }
}
