using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Emit;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using GremlinUtils;

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

        static GraphTraversal<Vertex, Vertex> GetTraversal() 
            => GraphBuilder.GetOrBuildAppDev().V();

        static GraphTraversal<Vertex, Vertex> Devs()
            => VertsByLabel("dev");
        
        static GraphTraversal<Vertex, Vertex> Apps()
            => VertsByLabel("app");

        static GraphTraversal<Vertex, Vertex> Skills()
            => VertsByLabel("skill");

        private static Func<GraphTraversal<Vertex, string>> DevNames = () => NamesFor(Devs());

        private static Func<GraphTraversal<Vertex, string>> AppNames = () => NamesFor(Apps());

        private static Func<GraphTraversal<Vertex, string>> SkillNames = () => NamesFor(Skills());

        static GraphTraversal<Vertex, string> NamesFor(GraphTraversal<Vertex, Vertex> traversal)
            => traversal.Values<string>("name");

        static GraphTraversal<Vertex, Vertex> VertsByLabel(string label)
            => GetTraversal().HasLabel(label);

        static void PrintDevs() => PrintAll(DevNames());
        
        static void PrintTrackerDevs() => PrintAll(NamesFor(DevsAssignedTo("tracker")));

        static void PrintApps() => PrintAll(AppNames());

        static void PrintSkills() => PrintAll(SkillNames());

        static GraphTraversal<Vertex, Vertex> DevsAssignedTo(string appName)
        {
            return Apps()
                .Has("name", appName)
                .InE("assignedTo")
                .OutV();
        }
        
        static void PrintAllSafDevs()
        {
            var safDevs = Apps()
                .Has("name", "saf")
                .InE()
                .OutV();
            PrintAll(NamesFor(safDevs));
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