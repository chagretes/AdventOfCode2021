using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    class Square {
        public int value;
        public bool check = false;
    }
    class Board {
        public bool won = false;
        public List<List<Square>> rows = new List<List<Square>>();
        public List<Square> lineChecked = null;

        internal void WriteNumber(int number)
        {
            foreach(var row in rows) {
                for(int i = 0; i<row.Count; i++)
                {
                    if (row[i].value==number) row[i].check = true;
                    if(row.FindAll(x => x.check).Count == row.Count) {
                        lineChecked = row;
                    } else {
                        List<Square> col = new List<Square> {
                            rows[0][i],
                            rows[1][i],
                            rows[2][i],
                            rows[3][i],
                            rows[4][i],
                        };
                        if(col.FindAll(x => x.check).Count == col.Count) {
                            lineChecked = col;
                        }
                    }
                }
            }
        }
    }
    public class Day_04 : BaseDay
    {
        private readonly List<int> numbers;
        private readonly List<Board> boards = new List<Board>();

        public Day_04()
        {
            var input = File.ReadAllText(InputFilePath).Split('\n').ToList();
            numbers = input[0].Split(',').Select(int.Parse).ToList();
            for(int i=2;i<input.Count-4;i+=6) {
                var board = new Board{
                    rows = new List<List<Square>>(){
                        input[i].TrimStart().Replace("  "," ").Split(' ').Select(x => new Square{value = int.Parse(x),check = false}).ToList(),
                        input[i+1].TrimStart().Replace("  "," ").Split(' ').Select(x => new Square{value = int.Parse(x),check = false}).ToList(),
                        input[i+2].TrimStart().Replace("  "," ").Split(' ').Select(x => new Square{value = int.Parse(x),check = false}).ToList(),
                        input[i+3].TrimStart().Replace("  "," ").Split(' ').Select(x => new Square{value = int.Parse(x),check = false}).ToList(),
                        input[i+4].TrimStart().Replace("  "," ").Split(' ').Select(x => new Square{value = int.Parse(x),check = false}).ToList()
                    }
                };
                boards.Add(board);
            }
        }

        public override ValueTask<string> Solve_1() {
            int result = 0;
            foreach(var number in numbers){
                foreach(var board in boards){
                    board.WriteNumber(number);
                    if(board.lineChecked!=null) {
                        var sum = board.rows.Sum(x => x.Sum(y => y.check?0:y.value));
                        result = sum * number;
                        break;
                    }
                }
                if (result != 0) break;
            }

            return new ValueTask<string>(result.ToString());
        }

        public override ValueTask<string> Solve_2(){
            int result = 0;
            foreach(var number in numbers){
                foreach(var board in boards){
                    board.WriteNumber(number);
                    if(board.lineChecked!=null) {
                        if(board.won == false){
                            var sum = board.rows.Sum(x => x.Sum(y => y.check?0:y.value));
                            result = sum * number;
                            board.won = true;
                        }
                    }
                }
            }

            return new ValueTask<string>(result.ToString());
        }
    }
}
