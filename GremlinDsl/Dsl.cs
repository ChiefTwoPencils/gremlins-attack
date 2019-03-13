using System;

using Gremlin;
using Gremlin.Net;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Process.Traversal.Strategy.Decoration;
using Gremlin.Net.Structure;
using Gremlin.Net.Structure.IO.GraphSON;
using static Gremlin.Net.Process.Traversal.AnonymousTraversalSource;
using static Gremlin.Net.Process.Traversal.__;
using static Gremlin.Net.Process.Traversal.P;
using static Gremlin.Net.Process.Traversal.Order;
using static Gremlin.Net.Process.Traversal.Operator;
using static Gremlin.Net.Process.Traversal.Order;
using static Gremlin.Net.Process.Traversal.Pop;
using static Gremlin.Net.Process.Traversal.Scope;
using static Gremlin.Net.Process.Traversal.TextP;
using static Gremlin.Net.Process.Traversal.Column;
using static Gremlin.Net.Process.Traversal.Direction;
using static Gremlin.Net.Process.Traversal.T;

namespace GremlinDsl
{
    public static class SocialTraversalExtensions
    {
        public static GraphTraversal<Vertex, Vertex> Knows(this GraphTraversal<Vertex, Vertex> traversal,
            string personName)
        {
            return traversal.Out("knows").HasLabel("person").Has("name", personName);
        }

        public static GraphTraversal<Vertex, int> YoungestFriendsAge(this GraphTraversal<Vertex, Vertex> traversal)
        {
            return traversal.Out("knows").HasLabel("person").Values<int>("age").Min<int>();
        }

        public static GraphTraversal<Vertex, long> CreatedAtLeast(this GraphTraversal<Vertex, Vertex> traversal,
            long number)
        {
            return traversal.OutE("created").Count().Is(P.Gte(number));
        }
    }

    public static class __Social
    {
        public static GraphTraversal<object, Vertex> Knows(string personName)
        {
            return __.Out("knows").HasLabel("person").Has("name", personName);
        }
        
        public static GraphTraversal<object, int> YoungestFriendsAge()
        {
            return __.Out("knows").HasLabel("person").Values<int>("age").Min<int>();
        }

        public static GraphTraversal<object, long> CreatedAtLeast(long number)
        {
            return __.OutE("created").Count().Is(P.Gte(number));
        }
    }

    public static class SocialTraversalSourceExtensions
    {
        public static GraphTraversal<Vertex, Vertex> Persons(this GraphTraversalSource source,
            params string[] personNames)
        {
            var traversal = source.V().HasLabel("person");
            if (personNames.Length > 0)
            {
                traversal = traversal.Has("name", P.Within(personNames));
            }

            return traversal;
        }
    }
}