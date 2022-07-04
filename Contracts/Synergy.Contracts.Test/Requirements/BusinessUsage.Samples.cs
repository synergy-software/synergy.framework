using System;
using Synergy.Contracts.Requirements;
using Synergy.Contracts.Test.Documentation;

namespace Synergy.Contracts.Test.Requirements
{
    partial class BusinessUsage
    {
        private static string? Read(string method)
        {
            return ClassReader.ReadMethodBody(method);
        }
        
        public void Step1Sample()
        {
            Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
                    .Throws(new NotImplementedException("NOT IMPLEMENTED"));
        }
    }
}