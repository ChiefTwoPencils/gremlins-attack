using System;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using static Gremlin.Net.Process.Traversal.__;
using static Gremlin.Net.Process.Traversal.P;

namespace AppDevsDsl
{
    internal static class EdgeNames
    {
        internal const string AssignedTo = "assignedTo";
        internal const string WorkedOn = "workedOn";
    }
    
    internal static class VertexNames
    {
        internal const string App = "app";
    }
    
    internal static class Properties
    {
        internal const string Name = "name";
    }
    
    public static class AppDevExtensions
    {
        public static GraphTraversal<Vertex, Vertex> AssignedTo(this GraphTraversal<Vertex, Vertex> traversal,
            string appName) => traversal.Out(EdgeNames.AssignedTo)
                .HasLabel(VertexNames.App)
                .Has(Properties.Name, appName);

        public static GraphTraversal<Vertex, Vertex> WorkedOn(this GraphTraversal<Vertex, Vertex> traversal,
            string appName) => traversal.Out(EdgeNames.WorkedOn)
                .HasLabel(VertexNames.App)
                .Has(Properties.Name, appName);

        public static GraphTraversal<Vertex, string> Names(this GraphTraversal<Vertex, Vertex> traversal)
            => traversal.Values<string>("name");
    }
    
    public static class __AppDev
    {
        public static GraphTraversal<object, Vertex> AssignedTo(string appName)
            => Out(EdgeNames.AssignedTo)
                .HasLabel(VertexNames.App)
                .Has(Properties.Name, appName);

        public static GraphTraversal<object, Vertex> WorkedOn(string appName)
            => Out(EdgeNames.WorkedOn)
                .HasLabel(VertexNames.App)
                .Has(Properties.Name, appName);
    }
    
    public static class AppDevTraversalSourceExtensions
    {
        public static GraphTraversal<Vertex, Vertex> Developers(this GraphTraversalSource source, 
            params object[] devNames) => source.VerticesWithNames("dev", devNames);

        public static GraphTraversal<Vertex, Vertex> Apps(this GraphTraversalSource source,
            params object[] appNames) => source.VerticesWithNames("app", appNames);

        public static GraphTraversal<Vertex, Vertex> Skills(this GraphTraversalSource source,
            params object[] skillNames) => source.VerticesWithNames("skill", skillNames);

        private static GraphTraversal<Vertex, Vertex> VerticesWithNames(this GraphTraversalSource source,
            string vertexLabel, params object[] names)
        {
            var t = source.V().HasLabel(vertexLabel);
            return names.Length > 0
                ? t.Has("name", Within(names))
                : t;
        }
    }
}    
