using System;
using System.Data;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Synergy.NHibernate
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class ConnectToAttribute : Attribute
    {
        [NotNull] 
        public Type Database { get; }

        public IsolationLevel IsolationLevel { get; set; }

        public bool Transactional { get; set; }

        public ConnectToAttribute([NotNull] Type databaseType)
        {
            this.Database = databaseType;
            this.IsolationLevel = IsolationLevel.ReadCommitted;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[AutoTransaction(typeof({this.Database}), " +
                   $"{nameof(ConnectToAttribute.IsolationLevel)} = {this.IsolationLevel}, " +
                   $"{nameof(ConnectToAttribute.Transactional)} = {this.Transactional})]";
        }
    }
}