using System.Collections.Generic;

namespace Synergy.Web.Api.Testing.Features 
{
    public class Step
    {
        public int No { get; }
        public string Title { get; }
        public List<HttpOperation> Operations = new List<HttpOperation>(1);

        public Step(string title, int no)
        {
            Title = title;
            No = no;
        }

        internal void Attach(HttpOperation operation)
        {
            Operations.Add(operation);
        }
    }
}