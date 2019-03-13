using System;
using System.Collections.Generic;
using System.Linq;
using Gremlin;
using Gremlin.Net;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Process.Traversal.Strategy.Decoration;
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

using GremlinDsl;

namespace GremlinNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = Traversal().WithRemote(LocalRemoteConn);
            g = g.WithStrategies(new SubgraphStrategy(
                HasLabel("person"), 
                Has("weight", Gt(0.5))));
            g.V()
                .Values<string>("name")
                .ToList()
                .ToList()
                .ForEach(Console.WriteLine);

            var markoKnowsJosh = g.Persons("marko").Knows("josh");
            Console.WriteLine(markoKnowsJosh.Next());
            
            var markosYoungestFriend = g.Persons("marko").YoungestFriendsAge();
            Console.WriteLine(markosYoungestFriend.Next());
            
            var createdAtLeastTwo = g.Persons().CreatedAtLeast(2).Count();
            Console.WriteLine(createdAtLeastTwo.Next());
            
            LocalRemoteConn.Dispose();
        }

        private static DriverRemoteConnection LocalRemoteConn { get; } 
            = new DriverRemoteConnection(LocalClient);

        private static GremlinClient LocalClient => new GremlinClient(
            new GremlinServer("localhost", 8182),
            new GraphSON3Reader(),
            new GraphSON3Writer());
    }
}