using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    public class Command {
        public string direction;
        public int value;
    }
    public class Day_02 : BaseDay
    {
        private readonly List<Command> commands;

        public Day_02()
        {
            commands = File.ReadAllText(InputFilePath).Split('\n').Select(x => new Command{
                direction = x.Split(" ")[0],
                value = int.Parse(x.Split(" ")[1])
            }).ToList();
        }

        public override ValueTask<string> Solve_1() {
            int horizontal=0;
            int depth=0;
            foreach(var command in commands) {
                switch (command.direction)  {
                    case "forward":
                        horizontal+= command.value;
                        break;
                    case "down":
                        depth+= command.value;
                        break;
                    default:
                        depth-= command.value;
                        break;
                };
            }

            var result = horizontal*depth;

            return new ValueTask<string>(result.ToString());
        }
        

        public override ValueTask<string> Solve_2(){
            int horizontal=0,depth=0,aim=0;

            foreach(var command in commands) {
                switch (command.direction)  {
                    case "forward":
                        horizontal+= command.value;
                        depth += aim*command.value;
                        break;
                    case "down":
                        aim+= command.value;
                        break;
                    default:
                        aim-= command.value;
                        break;
                };
            }

            var result = horizontal*depth;

            return new ValueTask<string>(result.ToString());
        }
    }
}
