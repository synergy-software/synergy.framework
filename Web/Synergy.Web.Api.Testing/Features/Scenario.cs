using System.Collections.Generic;
using JetBrains.Annotations;

namespace Synergy.Web.Api.Testing.Features
{
    public class Scenario
    {
        public int No { get; }

        [NotNull]
        public string Title { get; }

        public List<Step> Steps { get; }

        public Scenario([NotNull] string title, int no)
        {
            No = no;
            Title = title;
            Steps = new List<Step>(5);
        }

        [NotNull]
        public Step Step([NotNull] string title)
        {
            var step = new Step(title, Steps.Count + 1);
            Steps.Add(step);
            return step;
        }
    }
}