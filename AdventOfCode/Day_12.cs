using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using AoCHelper;


namespace AdventOfCode
{
    public class Node {
        public string Id;
        public List<Node> Conections = new List<Node>();
        public int Visits = 0;
        
        public bool isBig => Id.Aggregate(true,(x1,x2)=>(x1&&(char.IsUpper(x2))));
    }

    public class Day_12 : BaseDay
    {

        List<Node> cave = new List<Node>();

        public Day_12()
        {

            File.ReadAllText(base.InputFilePath).Split('\n').ToList().ForEach(line =>
           {
               var strNodeA = line.Split('-')[0];
               var strNodeB = line.Split('-')[1];
               AddIfNotExists(strNodeA);
               AddIfNotExists(strNodeB);
               var nodeA = cave.Find(x=>x.Id==strNodeA);
               var nodeB = cave.Find(x=>x.Id==strNodeB);
               nodeA.Conections.Add(nodeB);
               nodeB.Conections.Add(nodeA);
           });
           
        }

        private void AddIfNotExists(string nodeA)
        {
            if (cave.Find(x => x.Id == nodeA) == null)
            {
                cave.Add(new Node { Id = nodeA });
            }
        }

        public override ValueTask<string> Solve_1()
        {
            // return new ValueTask<string>("sdf".ToString());
            var start = cave.Find(x => x.Id == "start");
            var path = new List<Node>();
            var paths = new List<List<Node>>();
            AddToPath(start, path,paths);
            //PrintPaths(paths);
            return new ValueTask<string>(paths.Count().ToString());
        }

        private void PrintPaths(List<List<Node>> paths)
        {
            paths.ForEach(path => {
                Console.Write('\n');
                path.ForEach(p=>{
                    Console.Write(p.Id+",");
                });
            });
        }

        private static void AddToPath(Node nextNode, List<Node> path, List<List<Node>> paths, bool smallTwice=false)
        {
            path.Add(nextNode);
            int repetitions = smallTwice?1:0;
            if(nextNode.Id!="end"){
                for (int i=0;i<nextNode.Conections.Count();i++){
                    var node = nextNode.Conections[i];
                    if ((node.isBig) || (((path.FindAll(x => x.Id == node.Id).Count()) <= repetitions) && (node.Id!="start")))
                    {
                        if((smallTwice)&&(!node.isBig)) {
                            var stillTwice=!path.Any(x => x.Id == node.Id);
                            List<Node> newBranch = new List<Node>();
                            newBranch.AddRange(path);
                            AddToPath(node,newBranch,paths,stillTwice);

                        }
                        else {
                            List<Node> newBranch = new List<Node>();
                            newBranch.AddRange(path);
                            AddToPath(node,newBranch,paths,smallTwice);
                        }
                    }
                }
            }
            else{
                paths.Add(path);
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var start = cave.Find(x => x.Id == "start");
            var path = new List<Node>();
            var paths = new List<List<Node>>();
            AddToPath(start, path,paths, true);
            PrintPaths(paths);
            return new ValueTask<string>(paths.Count().ToString()); 
        }
    }
}
