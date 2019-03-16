using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using static Gremlin.Net.Process.Traversal.__;
using static Gremlin.Net.Process.Traversal.P;

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
            return traversal.OutE("created").Count().Is(Gte(number));
        }
    }

    public static class __Social
    {
        public static GraphTraversal<object, Vertex> Knows(string personName)
        {
            return Out("knows").HasLabel("person").Has("name", personName);
        }
        
        public static GraphTraversal<object, int> YoungestFriendsAge()
        {
            return Out("knows").HasLabel("person").Values<int>("age").Min<int>();
        }

        public static GraphTraversal<object, long> CreatedAtLeast(long number)
        {
            return OutE("created").Count().Is(Gte(number));
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
                traversal = traversal.Has("name", Within(personNames));
            }

            return traversal;
        }
    }
}