using System;
using System.Data;

namespace Synergy.NHibernate.Transactions
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class AutoTransactionAttribute : Attribute
    {
        //[NotNull] 
        public Type On { get; set; }

        public IsolationLevel IsolationLevel { get; set; }

        public bool Disabled { get; set; }

        public AutoTransactionAttribute()
        {
            this.IsolationLevel = IsolationLevel.ReadCommitted;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[AutoTransaction({nameof(AutoTransactionAttribute.On)} = typeof({this.On}), " +
                   $"{nameof(AutoTransactionAttribute.IsolationLevel)} = {this.IsolationLevel}, " +
                   $"{nameof(AutoTransactionAttribute.Disabled)} = {this.Disabled})]";
        }
    }
}