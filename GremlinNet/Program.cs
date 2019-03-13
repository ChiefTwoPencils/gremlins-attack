using System;

using Gremlin;
using Gremlin.Net;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
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

namespace GremlinNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = Traversal().WithRemote(LocalRemoteConn);
            Console.WriteLine(g.ToString());
        }

        private static DriverRemoteConnection LocalRemoteConn { get; } 
            = new DriverRemoteConnection(LocalClient);

        private static GremlinClient LocalClient => new GremlinClient(
            new GremlinServer("localhost", 8182),
            new GraphSON3Reader(),
            new GraphSON3Writer());
    }
}