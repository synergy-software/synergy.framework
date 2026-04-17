using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Synergy.Web.Api.Testing.Json
{
    public class Ignore
    {
        public ReadOnlyCollection<string> Nodes { get; private set; }

        public Ignore(params string[] nodes)
        {
            Nodes = nodes.ToList().AsReadOnly();
        }

        public void Append(IEnumerable<string> ignores)
        {
            var nodes = Nodes.ToList();
            nodes.AddRange(ignores);
            Nodes = nodes.Distinct().ToList().AsReadOnly();
        }

        public Ignore And(Ignore ignore)
        {
            this.Append(ignore.Nodes);
            return this;
        }
        
        public static Ignore RequestBody(params string[] nodes)
        {
            var ignore = new Ignore();
            if (nodes.Any() == false)
                ignore.Append(new[] {"$.request.body"});
            ignore.Append(nodes.Select(node=> $"$.request.body.{node}"));
            return ignore;
        }
        
        public static Ignore RequestMethod()
        {
            return new Ignore("$.request.method");
        }

        public static Ignore RequestHeaders(params string[] headers)
        {
            var ignore = new Ignore();
            if (headers.Any() == false)
                ignore.Append(new[] {"$.request.headers"});
            ignore.Append(headers.Select(node=> $"$.request.headers.{node}"));
            return ignore;
        }
        
        public static Ignore RequestDescription()
        {
            return new Ignore("$.request.description");
        }

        public static Ignore ResponseBody(params string[] nodes)
        {
            var ignore = new Ignore();
            if (nodes.Any() == false)
                ignore.Append(new[] {"$.response.content.body"});
            ignore.Append(nodes.Select(node=> $"$.response.content.body.{node}"));
            return ignore;
        }
        
        public static Ignore ResponseHeaders(params string[] headers)
        {
            var ignore = new Ignore();
            if (headers.Any() == false)
                ignore.Append(new[] {"$.response.headers"});
            ignore.Append(headers.Select(node=> $"$.response.headers.{node}"));
            return ignore;
        }
        
        public static Ignore ResponseLocationHeader()
        {
            return ResponseHeaders("Location");
        }

        public static Ignore ResponseContentHeaders(params string[] headers)
        {
            var ignore = new Ignore();
            if (headers.Any() == false)
                ignore.Append(new[] {"$.response.content.headers"});
            ignore.Append(headers.Select(node=> $"$.response.content.headers.{node}"));
            return ignore;
        }
        
        public static Ignore ResponseContentLength()
        {
            return ResponseContentHeaders("Content-Length");
        }
    }
}