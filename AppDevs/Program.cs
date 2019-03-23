using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Emit;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using GremlinUtils;

using AppDevsDsl;

namespace AppDevs
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintDevs();
            PrintApps();
            PrintSkills();
            PrintTrackerDevs();
            PrintAllSafDevs();
            PrintDevSkillsForAwesomeness();
            Server.DisposeConnection();
        }

        static GraphTraversalSource GetTraversalSource() 
            => GraphBuilder.GetOrBuildAppDev();

        static GraphTraversal<Vertex, Vertex> Devs()
            => GetTraversalSource().Developers();

        static GraphTraversal<Vertex, Vertex> Apps()
            => GetTraversalSource().Apps();

        static GraphTraversal<Vertex, Vertex> Skills()
            => GetTraversalSource().Skills();

        static void PrintDevs() => PrintAll(Devs().Names());
        
        static void PrintTrackerDevs() => PrintAll(Devs().AssignedTo("tracker").Names());

        static void PrintApps() => PrintAll(Apps().Names());

        static void PrintSkills() => PrintAll(Skills().Names());
        
        static void PrintAllSafDevs()
        {
            PrintAll(Devs()
                .Or(__AppDev.WorkedOn("saf"), 
                    __AppDev.AssignedTo("saf"))
                .Names());
        }
        
        static void PrintDevSkillsForAwesomeness()
        {
            Console.WriteLine();
            Apps()
                .Has("name", "awesomeness")
                .OutE("requires")
                .InV()
                .InE("knows")
                .GroupCount<string>()
                .By(__.OutV().Values<string>("name"))
                .Next()
                .Select(pair => $"{pair.Key}: {pair.Value}")
                .ToList()
                .ForEach(Console.WriteLine);
        }

        static void PrintAll(GraphTraversal<Vertex, string> traversal)
        {
            Console.WriteLine();
            traversal.ToList().ToList().ForEach(Console.WriteLine);
        }
    }
}