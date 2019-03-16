using System;
using System.Collections.Generic;
using System.Linq;
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

using GremlinDsl;
using static GremlinUtils.Server;

namespace GremlinNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assumes the modern graph is loaded...
            var g = SubG(new SubgraphStrategy(
                HasLabel("name"), 
                Has("weight", Gt(0.5))));
            
            var names = g.V()
                .Values<string>("name")
                .ToList()
                .ToList();
            names.ForEach(Console.WriteLine);
            
            foreach (var vertex in g.V().Values<object>().ToList())
            {
                Console.WriteLine(vertex);
            }

            var markoKnowsJosh = g.Persons("marko").Knows("josh");
            Console.WriteLine(markoKnowsJosh.Next());
            
            var markosYoungestFriend = g.Persons("marko").YoungestFriendsAge();
           Console.WriteLine(markosYoungestFriend.Next());
            
            var createdAtLeastTwo = g.Persons().CreatedAtLeast(2).Count();
            Console.WriteLine(createdAtLeastTwo.Next());
            
            DisposeConnection();
        }
    }
}