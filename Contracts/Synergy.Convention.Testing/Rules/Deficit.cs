﻿using System;
using System.Reflection;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Convention.Testing.Rules
{
    public struct Deficit
    {
        public MemberInfo Member { get; }
        public string Description { get; }

        private Type DeclaringType => Member.DeclaringType.OrFail(nameof(Member.DeclaringType))!;
        public Assembly Assembly => DeclaringType.Assembly;
        public string MemberName => FullNameOfMember();
        
        public Deficit(MemberInfo member, string description)
        {
            this.Member = member.OrFail(nameof(member));
            this.Description = description.OrFail(nameof(description));
        }

        public override string ToString()
        {
            return $"{MemberName}{Environment.NewLine}- {this.Description}";
        }

        [NotNull, Pure]
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