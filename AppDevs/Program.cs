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
            Server.DisposeConnection();
        }

        static GraphTraversal<Vertex, Vertex> GetTraversal() 
            => GraphBuilder.GetOrBuildAppDev().V();

        static GraphTraversal<Vertex, Vertex> Devs()
            => VertsByLabel("dev");

        static GraphTraversal<Vertex, string> Names(GraphTraversal<Vertex, Vertex> traversal)
            => traversal.Values<string>("name");

        static GraphTraversal<Vertex, Vertex> Apps()
            => VertsByLabel("app");

        static GraphTraversal<Vertex, Vertex> Skills()
            => VertsByLabel("skill");

        static GraphTraversal<Vertex, Vertex> VertsByLabel(string label)
            => GetTraversal().HasLabel(label);

        static void PrintDevs()
        {
            var devs = Devs()
                .Values<string>("name");
            PrintAll(devs);
        }
        
        static void PrintTrackerDevs()
        {
            var trDevs = Apps()
                .Has("name", "tracker")
                .InE()
                .HasLabel("assignedTo")
                .OutV()
                .Values<string>("name");
            PrintAll(trDevs);
        }
        
        static void PrintAllSafDevs()
        {
            var safDevs = Apps()
                .Has("name", "saf")
                .InE()
                .OutV()
                .Values<string>("name");
            PrintAll(safDevs);
        }

        static void PrintApps()
        {
            var apps = Apps()
                .Values<string>("name");
            PrintAll(apps);
        }

        static void PrintSkills()
        {
            var skills = Skills()
                .Values<string>("name");
            PrintAll(skills);
        }

        static void PrintAll(GraphTraversal<Vertex, string> traversal)
        {
            Console.WriteLine();
            traversal.ToList().ToList().ForEach(Console.WriteLine);
        }
    }
}