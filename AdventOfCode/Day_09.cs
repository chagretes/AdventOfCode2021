using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Point {
        public int x;
        public int y;
        public int value;
    }
    public class Day_09 : BaseDay
    {
        const int SIZE_X = 100;
        const int SIZE_Y = 100;
        public List<Display> displays = new List<Display>();
        public int[,] heightmap = new int[SIZE_X,SIZE_Y];

        public Day_09()
        {
           var y = 0;
           File.ReadAllText(InputFilePath).Split('\n').ToList().ForEach(line => {
               for(int x = 0; x<SIZE_X; x++){
                heightmap[x,y] = int.Parse(line[x].ToString());
               }
               y++;
           });
        }

        public void AddIFNotNine(int x, int y, List<Point> basin){
            if (heightmap[x,y] != 9){
                if(basin.Find(p=>p.x==x&&p.y==y)==null)
                {
                    basin.Add(new Point(){
                        x = x,
                        y = y,
                        value = heightmap[x,y]
                    });
                    if (x!=0)       AddIFNotNine(x-1,y,basin);
                    if (x!=SIZE_X-1)AddIFNotNine(x+1,y,basin);
                    if (y!=0)       AddIFNotNine(x,y-1,basin);
                    if (y!=SIZE_Y-1)AddIFNotNine(x,y+1,basin);
                }
            }            
        }



        public override ValueTask<string> Solve_2()
        {
            int sum1 = 0;
            int sum2 = 0;
            int sum3 = 0;
            List<List<Point>> basins = new List<List<Point>>();
            for(int x=0; x<SIZE_X;x++){
                for(int y=0; y<SIZE_Y;y++){


                    //Novo Basin
                    if ((heightmap[x,y]!=9)&&(basins.Find(b => b.Find(p=>p.x==x&&p.y==y)!= null) == null)){
                        var basin = new List<Point>();
                        AddIFNotNine(x,y,basin);
                        basins.Add(basin);
                        var sum = basin.Count();
                        if (sum>sum1){
                            sum3=sum2;
                            sum2=sum1;
                            sum1=sum;
                        } else if (sum>sum2){
                            sum3=sum2;
                            sum2=sum;
                        } else if (sum>sum3) {
                            sum3=sum;
                        }
                    }
                }
            }

            var result = sum1*sum2*sum3;

            return new ValueTask<string>(result.ToString());
        }

        public override ValueTask<string> Solve_1()
        {
            List<int> lowHeights = new List<int>();
            for(int x=0; x<SIZE_X;x++){
                for(int y=0; y<SIZE_Y;y++){
                    if(
                        ((x==0)||(heightmap[x,y]<heightmap[x-1,y]))&&
                        ((y==0)||(heightmap[x,y]<heightmap[x,y-1]))&&
                        ((x==SIZE_X-1)||(heightmap[x,y]<heightmap[x+1,y]))&&
                        ((y==SIZE_Y-1)||(heightmap[x,y]<heightmap[x,y+1]))
                    ) {
                        lowHeights.Add(heightmap[x,y]);
                    }
                }
            }

            var result = lowHeights.Sum(x => x+1);

            return new ValueTask<string>(result.ToString());
        }
    }
}
