using System;
using System.Collections.Generic;
using ApprovalTests;
using Xunit;

namespace Synergy.Catalogue.Reflection.Tests
{
    public class ReflectionExtensionTest
    {
        [Fact]
        public void Test()
        {
            var types = new List<Type>()
            {
                typeof(object),
                typeof(string),
                
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(char),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(bool),
                
                typeof(byte?),
                typeof(sbyte?),
                typeof(short?),
                typeof(ushort?),
                typeof(int?),
                typeof(uint?),
                typeof(long?),
                typeof(ulong?),
                typeof(char?),
                typeof(float?),
                typeof(double?),
                typeof(decimal?),
                typeof(bool?),
                
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(List<int>),
                typeof(int[]),
                typeof(Dictionary<string, long>),
            };

            Approvals.VerifyAll(types, type => $"{type} => {type.GetFriendlyTypeName()}");
        }
    }
}