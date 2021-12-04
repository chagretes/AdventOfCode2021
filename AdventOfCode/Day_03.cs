using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Command2 {
        public string direction;
        public int value;
    }
    public class Day_03 : BaseDay
    {
        private readonly List<string> numbers;

        public Day_03()
        {
            numbers = File.ReadAllText(InputFilePath).Split('\n').ToList();
        }

        public override ValueTask<string> Solve_1() {
            // int[] vector1 = new int[]{0,0,0,0,0,0,0,0,0,0,0,0};
            // foreach (var number in numbers) {
            //     for(int i = 0; i<12;i++){
            //         if (number[i]=='1') vector1[i]++;
            //     }
            // }
            // bool[] gama = new bool[12];
            // bool[] epsilon= new bool[12];
            // int[] gamai = new int[12];
            // for(int i = 0; i<12;i++){
            //     if(numbers.Count-vector1[i]<vector1[i]){
            //         gama[i] = true;
            //         epsilon[i] = false;
            //         gamai[i] = 1;
            //     } else {
            //         gama[i] = false;
            //         epsilon[i] = true;
            //     }
            // }

            // if (BitConverter.IsLittleEndian){
            //     Array.Reverse(gama);
            //     Array.Reverse(epsilon);
            // }

            // int result = getIntFromBitArray(new BitArray(gama))*getIntFromBitArray(new BitArray(epsilon));

            return new ValueTask<string>("result".ToString());
        }

        private int getIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }
        

        public override ValueTask<string> Solve_2(){
            int[] vector1 = new int[]{0,0,0,0,0,0,0,0,0,0,0,0};
            foreach (var number in numbers) {
                for(int i = 0; i<5;i++){
                    if (number[i]=='1') vector1[i]++;
                }
            }
            var originalVector = vector1;
            string oxygen = "";
            var filter = numbers;
            var filterValue = '0';
            for(int i=0; i<12;i++){
                if(filter.Count-vector1[i]<=vector1[i]) 
                    filterValue = '1';
                else
                    filterValue = '0';
                filter = filter.FindAll(x => x[i]==filterValue);
                if(filter.Count == 1) {
                    oxygen = filter[0];
                    break;
                }
                vector1 = new int[]{0,0,0,0,0,0,0,0,0,0,0,0};
                foreach (var number in filter) {
                    for(int j = i+1; j<12;j++){
                        if (number[j]=='1') vector1[j]++;
                    }
                }
            }

            string c02 = "";
            filter = numbers;
            vector1=originalVector;
            for(int i=0; i<12;i++){
                if(filter.Count-vector1[i]>vector1[i]) 
                    filterValue = '1';
                else
                    filterValue = '0';
                filter = filter.FindAll(x => x[i]==filterValue);
                if(filter.Count == 1) {
                    c02 = filter[0];
                    break;
                }
                vector1 = new int[]{0,0,0,0,0,0,0,0,0,0,0,0};
                foreach (var number in filter) {
                    for(int j = i+1; j<12;j++){
                        if (number[j]=='1') vector1[j]++;
                    }
                }
            }
            

            return new ValueTask<string>(c02+ " "+oxygen);
        }
    }
}
