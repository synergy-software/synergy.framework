using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Convention.Testing.Rules
{
    public struct Deficit
    {
        public MemberInfo Member { get; }
        public string Description { get; }

        private Type DeclaringType => Member.DeclaringType.OrFail();
        public Assembly Assembly => DeclaringType.Assembly;
        public string MemberName => FullNameOfMember();
        
        public Deficit(MemberInfo member, string description)
        {
            this.Member = member.OrFail();
            this.Description = description.OrFail();
        }

        public override string ToString()
        {
            return $"{MemberName}{Environment.NewLine}- {this.Description}";
        }

        [NotNull, System.Diagnostics.Contracts.Pure]
        private string FullNameOfMember()
        {
            if (Member is Type type)
                return type.FullName.OrFailIfWhiteSpace(nameof(type.FullName));

            var fullName = DeclaringType.FullName;

            if (Member is PropertyInfo)
                return $"{fullName}.{Member.Name}";

            return $"{fullName}.{Member.Name}()";
        }
    }
}