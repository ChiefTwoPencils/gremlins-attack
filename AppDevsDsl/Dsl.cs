using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using static Gremlin.Net.Process.Traversal.__;
using static Gremlin.Net.Process.Traversal.P;

namespace AppDevsDsl
{
    internal static class Names
    {
        internal static class Edges
        {
            internal const string AssignedTo = "assignedTo";
            internal const string WorkedOn = "workedOn";
        }
        
        internal static class Vertices
        {
            internal const string App = "app";
        }
        
        internal static class Properties
        {
            internal const string Name = "name";
        }
    }
    public static class AppDevExtensions
    {
        public static GraphTraversal<Vertex, Vertex> AssignedTo(this GraphTraversal<Vertex, Vertex> traversal,
            string appName) => traversal.Out(Names.Edges.AssignedTo)
                .HasLabel(Names.Vertices.App)
                .Has(Names.Properties.Name, appName);

        public static GraphTraversal<Vertex, Vertex> WorkedOn(this GraphTraversal<Vertex, Vertex> traversal,
            string appName) => traversal.Out(Names.Edges.WorkedOn)
                .HasLabel(Names.Vertices.App)
                .Has(Names.Properties.Name, appName);
    }
    
    public static class __AppDev
    {
        public static GraphTraversal<object, Vertex> AssignedTo(string appName)
            => Out(Names.Edges.AssignedTo)
                .HasLabel(Names.Vertices.App)
                .Has(Names.Properties.Name, appName);

        public static GraphTraversal<object, Vertex> WorkedOn(string appName)
            => Out(Names.Edges.WorkedOn)
                .HasLabel(Names.Vertices.App)
                .Has(Names.Properties.Name, appName);
    }
    
    public static class AppDevTraversalSourceExtensions
    {
        public static GraphTraversal<Vertex, Vertex> Developers(this GraphTraversalSource source, params object[] devNames)
        {
            var t = source.V().HasLabel("dev");
            return devNames.Length > 0
                ? t.Has("name", Within(devNames))
                : t;
        }
    }
}    
