using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Octopus {
        public bool flashed = false;
        public int energy;
    }
    public class Day_11 : BaseDay
    {
        public Octopus[,] octopuses;

        public int X_SIZE { get; }
        public int Y_SIZE { get; }
        public int flashCount = 0;

        public Day_11()
        {
           var lines = File.ReadAllText(InputFilePath).Split('\n').ToList();
           X_SIZE = lines[0].Count();
           Y_SIZE = lines.Count();
           octopuses = new Octopus[X_SIZE,Y_SIZE];
           for(int y = 0; y < Y_SIZE; y++)
           {
               var line = lines[y];
               for (int x = 0; x < X_SIZE; x++){
                   octopuses[x,y] = new Octopus{energy=int.Parse(line[x].ToString())};
               }
           }
        }

        public override ValueTask<string> Solve_1()
        {
            return new ValueTask<string>("asd".ToString());
            for(int step=0;step<10;step++){
                // PrintStep();
                ClearFlash();
                for(int y = 0; y < Y_SIZE; y++){
                    for (int x = 0; x < X_SIZE; x++){
                        octopuses[x,y].energy++;
                    }
                }
                for(int y = 0; y < Y_SIZE; y++){
                    for (int x = 0; x < X_SIZE; x++){
                        CheckFlash(x,y);
                    }
                }
            }
            

            return new ValueTask<string>(flashCount.ToString());
        }

        private void PrintStep()
        {
            Console.WriteLine();
            Console.WriteLine();
            for(int y = 0; y < Y_SIZE; y++){
                Console.Write('\n');
                for (int x = 0; x < X_SIZE; x++){
                    if(octopuses[x,y].flashed){
                        Console.ForegroundColor = ConsoleColor.Green;
                    } else {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(octopuses[x,y].energy);
                }
            }
        }

        private void ClearFlash()
        {
            for(int y = 0; y < Y_SIZE; y++){
                for (int x = 0; x < X_SIZE; x++){
                    octopuses[x,y].flashed=false;
                }
            }
        }

        private void CheckFlash(int x, int y)
        {
            if(!octopuses[x,y].flashed)
            {
                if(octopuses[x,y].energy>9)
                {
                    flashCount++;
                    octopuses[x,y].energy = 0;
                    octopuses[x,y].flashed = true;
                    if (x!=0)       {
                        if (!octopuses[x-1,y].flashed) octopuses[x-1,y].energy++;
                        CheckFlash(x-1,y);
                        if (y!=0)       {
                            if (!octopuses[x-1,y-1].flashed) octopuses[x-1,y-1].energy++;
                            CheckFlash(x-1,y-1);
                        }
                        if (y!=Y_SIZE-1){
                            if (!octopuses[x-1,y+1].flashed) octopuses[x-1,y+1].energy++;
                            CheckFlash(x-1,y+1);
                        }
                    }
                    if (x!=X_SIZE-1){
                        if (!octopuses[x+1,y].flashed) octopuses[x+1,y].energy++;
                        CheckFlash(x+1,y);
                        if (y!=0)       {
                            if (!octopuses[x+1,y-1].flashed) octopuses[x+1,y-1].energy++;
                            CheckFlash(x+1,y-1);
                        }
                        if (y!=Y_SIZE-1){
                            if (!octopuses[x+1,y+1].flashed) octopuses[x+1,y+1].energy++;
                            CheckFlash(x+1,y+1);
                        }
                    }
                    if (y!=0)       {
                        if (!octopuses[x,y-1].flashed) octopuses[x,y-1].energy++;
                        CheckFlash(x,y-1);
                    }
                    if (y!=Y_SIZE-1){
                        if (!octopuses[x,y+1].flashed) octopuses[x,y+1].energy++;
                        CheckFlash(x,y+1);
                    }
                }
            }
        }
        private bool Flash(int x, int y)
        {
            if(!octopuses[x,y].flashed)
            {
                if(octopuses[x,y].energy>9)
                {
                    flashCount++;
                    octopuses[x,y].flashed = true;
                    if (x!=0)       {
                        if (!octopuses[x-1,y].flashed) octopuses[x-1,y].energy++;
                        if (y!=0)       {
                            if (!octopuses[x-1,y-1].flashed) octopuses[x-1,y-1].energy++;
                        }
                        if (y!=Y_SIZE-1){
                            if (!octopuses[x-1,y+1].flashed) octopuses[x-1,y+1].energy++;
                        }
                    }
                    if (x!=X_SIZE-1){
                        if (!octopuses[x+1,y].flashed) octopuses[x+1,y].energy++;
                        if (y!=0)       {
                            if (!octopuses[x+1,y-1].flashed) octopuses[x+1,y-1].energy++;
                        }
                        if (y!=Y_SIZE-1){
                            if (!octopuses[x+1,y+1].flashed) octopuses[x+1,y+1].energy++;
                        }
                    }
                    if (y!=0)       {
                        if (!octopuses[x,y-1].flashed) octopuses[x,y-1].energy++;
                    }
                    if (y!=Y_SIZE-1){
                        if (!octopuses[x,y+1].flashed) octopuses[x,y+1].energy++;
                    }
                }
            }
            return octopuses[x,y].flashed;
        }

        public override ValueTask<string> Solve_2()
        {
            // for(int step=0;true;step++){
            //     // First, the energy level of each octopus increases by 1.
            //     for(int y = 0; y < Y_SIZE; y++){
            //         for (int x = 0; x < X_SIZE; x++){
            //             octopuses[x,y].energy++;
            //         }
            //     }
            //     //Then, any octopus with an energy level greater than 9 flashes
            //     bool checkFlash = true;
            //     while(checkFlash){
            //         checkFlash = false;
            //         for(int y = 0; y < Y_SIZE; y++){
            //             for (int x = 0; x < X_SIZE; x++){
            //                 var flashed = Flash(x,y);
            //                 checkFlash = checkFlash?true:flashed;
            //             }
            //         }
            //     }
            //     // Finally, any octopus that flashed during this step has its energy level set to 0
            //     for(int y = 0; y < Y_SIZE; y++){
            //         for (int x = 0; x < X_SIZE; x++){
            //             if(octopuses[x,y].flashed) 
            //             {
            //                 octopuses[x,y].energy = 0;
            //                 octopuses[x,y].flashed = false;
            //             }
            //         }
            //     }
            for(int step=0;true;step++){
                PrintStep();
                ClearFlash();
                for(int y = 0; y < Y_SIZE; y++){
                    for (int x = 0; x < X_SIZE; x++){
                        octopuses[x,y].energy++;
                    }
                }
                for(int y = 0; y < Y_SIZE; y++){
                    for (int x = 0; x < X_SIZE; x++){
                        CheckFlash(x,y);
                    }
                }
            
                PrintStep();
                var allFlashed = true;
                for(int y = 0; y < Y_SIZE; y++){
                    for (int x = 0; x < X_SIZE; x++){
                        if(!octopuses[x,y].flashed)
                        {
                            allFlashed = false;
                            break;
                        }
                    }
                    if (!allFlashed) break;
                }
                if (allFlashed) {
                    return new ValueTask<string>((step+1).ToString()); 
                }
            }     
        }
    }
}
