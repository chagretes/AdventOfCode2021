using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_06 : BaseDay
    {
        private List<int> Fishes;

        public Day_06()
        {
           Fishes = File.ReadAllText(InputFilePath).Split(',').Select(int.Parse).ToList<int>();
        }

        public override ValueTask<string> Solve_1()
        {
            var fishes = Fishes;
            for(int day = 0; day<80;day++){
                List<int> newFishes = new List<int>();
                fishes.ForEach(fish =>{
                    fish--;
                    if(fish == -1){
                        newFishes.Add(8);
                        fish = 6;
                    }
                    newFishes.Add(fish);
                });
                fishes= newFishes;
                // Console.Write("\nAfter "+(day+1)+" day :");
                // fishes.ForEach(x=>Console.Write(x+","));
            }
            
            var result = fishes.Count;

            return new ValueTask<string>(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            long[] fishPerTimer = new long[9];
            for (int i = 0; i<9; i++) {
                fishPerTimer[i] = Fishes.Count(x => x == i);
            }
            
            for(int day = 0; day<256;day++){
                var newFishes = fishPerTimer[0];
                fishPerTimer[0] = fishPerTimer[1];
                fishPerTimer[1] = fishPerTimer[2];
                fishPerTimer[2] = fishPerTimer[3];
                fishPerTimer[3] = fishPerTimer[4];
                fishPerTimer[4] = fishPerTimer[5];
                fishPerTimer[5] = fishPerTimer[6];
                fishPerTimer[6] = fishPerTimer[7]+newFishes;
                fishPerTimer[7] = fishPerTimer[8];
                fishPerTimer[8] = newFishes;
            }
            
            long result = 0;
            foreach(var fishCount in fishPerTimer)
            {
                result += fishCount;
            }

            return new ValueTask<string>(result.ToString());
        }
    }
}
