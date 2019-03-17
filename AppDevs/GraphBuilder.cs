using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using static GremlinUtils.Server;
using VertTraversal =
    Gremlin.Net.Process.Traversal.GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex>;
using EdgeTraversal =
    Gremlin.Net.Process.Traversal.GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Edge>;

namespace AppDevs
{
    /// <summary>
    /// Assumes a Gremlin server is running on localhost and listening
    /// on 8182. It builds the server with an empty graph for building
    /// examples easily.
    /// </summary>
    public static class GraphBuilder
    {
        
        public static GraphTraversalSource BuildEmpty() => G();

        public static EdgeTraversal GetOrBuildAppDev()
        {
            var g = BuildEmpty();
            g.V().Drop().Iterate();
            return BuildAppDev(g);
        }

        private static EdgeTraversal BuildAppDev(GraphTraversalSource source)
        {
            var vees = source
                .AddV("dev").Property("name", "srivani").As("dsm")
                .AddV("dev").Property("name", "sumit").As("dss")
                .AddV("dev").Property("name", "jansson").As("djs")
                .AddV("dev").Property("name", "rob").As("drw")
                .AddV("app").Property("name", "saf").As("as")
                .AddV("app").Property("name", "las").As("al")
                .AddV("app").Property("name", "eievs").As("ae")
                .AddV("app").Property("name", "tab").As("at")
                .AddV("app").Property("name", "tracker").As("atr")
                .AddV("app").Property("name", "awesomeness").As("aa")
                .AddV("skill").Property("name", "c#").As("sc")
                .AddV("skill").Property("name", "webapi").As("sw")
                .AddV("skill").Property("name", "ngJS").As("snj")
                .AddV("skill").Property("name", "ng").As("sn")
                .AddV("skill").Property("name", "rx").As("sr")
                .AddV("skill").Property("name", "mstest").As("sm")
                .AddV("skill").Property("name", "xunit").As("sx")
                .AddE("requires").From("as").To("sc")
                .AddE("requires").From("as").To("sw")
                .AddE("requires").From("as").To("snj")
                .AddE("requires").From("as").To("sm")
                .AddE("requires").From("al").To("sc")
                .AddE("requires").From("al").To("sw")
                .AddE("requires").From("al").To("snj")
                .AddE("requires").From("al").To("sm")
                .AddE("requires").From("ae").To("sc")
                .AddE("requires").From("ae").To("sw")
                .AddE("requires").From("ae").To("sn")
                .AddE("requires").From("ae").To("sr")
                .AddE("requires").From("ae").To("sm")
                .AddE("requires").From("at").To("sc")
                .AddE("requires").From("atr").To("sc")
                .AddE("requires").From("atr").To("sw")
                .AddE("requires").From("atr").To("sn")
                .AddE("requires").From("atr").To("sr")
                .AddE("requires").From("atr").To("sm")
                .AddE("requires").From("aa").To("sc")
                .AddE("requires").From("aa").To("sw")
                .AddE("requires").From("aa").To("sn")
                .AddE("requires").From("aa").To("sr")
                .AddE("requires").From("aa").To("sx")
                .AddE("knows").From("dsm").To("sc")
                .AddE("knows").From("dsm").To("sw")
                .AddE("knows").From("dsm").To("snj")
                .AddE("knows").From("dsm").To("sn")
                .AddE("knows").From("dsm").To("sm")
                .AddE("knows").From("dss").To("sc")
                .AddE("knows").From("dss").To("sw")
                .AddE("knows").From("dss").To("snj")
                .AddE("knows").From("dss").To("sn")
                .AddE("knows").From("dss").To("sm")
                .AddE("knows").From("djs").To("sc")
                .AddE("knows").From("djs").To("sw")
                .AddE("knows").From("djs").To("snj")
                .AddE("knows").From("djs").To("sn")
                .AddE("knows").From("djs").To("sm")
                .AddE("knows").From("djs").To("sr")
                .AddE("knows").From("drw").To("sc")
                .AddE("knows").From("drw").To("sw")
                .AddE("knows").From("drw").To("snj")
                .AddE("knows").From("drw").To("sn")
                .AddE("knows").From("drw").To("sm")
                .AddE("knows").From("drw").To("sr")
                .AddE("knows").From("drw").To("sx")
                .AddE("assignedTo").From("dsm").To("al")
                .AddE("assignedTo").From("dss").To("at")
                .AddE("assignedTo").From("djs").To("atr")
                .AddE("assignedTo").From("djs").To("ae")
                .AddE("assignedTo").From("drw").To("as")
                .AddE("assignedTo").From("drw").To("atr")
                .AddE("workedOn").From("dsm").To("as")
                .AddE("workedOn").From("dsm").To("ae")
                .AddE("workedOn").From("dss").To("as")
                .AddE("workedOn").From("dss").To("al")
                .AddE("workedOn").From("djs").To("as")
                .AddE("workedOn").From("djs").To("al");
            return vees;
        }
    }
}