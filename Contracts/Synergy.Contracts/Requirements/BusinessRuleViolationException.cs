using System;

namespace Synergy.Contracts.Requirements
{
    public class BusinessRuleViolationException : Exception
    {
        public Business.Requirement Requirement { get; }

        public BusinessRuleViolationException(string message, Business.Requirement requirement) : base(message)
        {
            this.Requirement = requirement;
        }
    }
}