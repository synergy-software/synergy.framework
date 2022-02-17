using System;
using JetBrains.Annotations;

namespace Synergy.Contracts.Requirements
{
    /// <summary>
    /// Allows to create business requirement verification conditions and checks.
    /// </summary>
    public static class Business
    {
        /// <summary>
        /// Gets rule with description only.
        /// </summary>
        [MustUseReturnValue]
        public static Principle Rule(string description)
            => new Principle(description);
        
        [MustUseReturnValue]
        public static Precondition When(bool preCondition) 
            => new Precondition(preCondition);

        [MustUseReturnValue]
        public static Requirement Requires(bool condition)
            => new Requirement(condition);

        public readonly struct Precondition : IPrecondition
        {
            private readonly IPrecondition? previous;
            private readonly bool met;
            public bool Met => (this.previous?.Met ?? true) && this.met;

            [CanBeNull]
            public string Comment { get; }

            public Precondition(bool preCondition, [CanBeNull] IPrecondition previous = null, [CanBeNull] string comment = null)
            {
                this.met = preCondition;
                this.previous = previous;
                this.Comment = comment;
            }

            // ReSharper disable once HeapView.BoxingAllocation
            [MustUseReturnValue]
            public Precondition And(bool preCondition)
                => new Precondition(preCondition, this, this.Comment);

            public Precondition this[[NotNull] string when] 
                => new Precondition(this.Met, this.previous, when.OrFailIfWhiteSpace(nameof(when)));

            [MustUseReturnValue]
            public Requirement Requires(bool condition)
            {
                return new Requirement(this, condition);
            }

            [MustUseReturnValue]
            public Requirement Requires(Func<bool> condition)
            {
                return new Requirement(this, condition);
            }

            /// <inheritdoc />
            [NotNull]
            public override string ToString()
            {
                string precondition = this.Comment ?? "__PRECONDITION__";

                if (this.previous == null)
                    return $"WHEN {precondition}";

                return $"{this.previous} AND {precondition}";
            }
        }

        public interface IPrecondition
        {
            bool Met { get; }
        }
        
        public readonly struct Requirement
        {
            private readonly Precondition? _precondition;
            private readonly Func<bool> _condition;

            [CanBeNull]
            public string Comment { get; }
            
            public bool Met
            {
                get
                {
                    if (this._precondition?.Met == false)
                        return true;

                    return this._condition.Invoke();
                }
            }

            public Requirement(bool condition)
                : this(null, condition)
            {
            }

            public Requirement(Precondition? precondition, bool condition)
                : this(precondition, () => condition)
            {
            }

            public Requirement(Precondition? precondition, Func<bool> condition, [CanBeNull] string comment = null)
            {
                this._precondition = precondition;
                this._condition = condition;
                this.Comment = comment;
            }

            public Requirement this[[NotNull] string rule]
                => new Requirement(this._precondition, this._condition, rule.OrFailIfWhiteSpace(nameof(rule)));

            public void Throws(string message)
                => Throws(new BusinessRuleViolationException(message, this));

            public void Throws(Exception exception)
            {
                if (this.Met == false)
                    throw exception;
            }

            /// <inheritdoc />
            [NotNull]
            public override string ToString()
            {
                string requirement = this.Comment ?? "__REQUIREMENT__";

                if (this._precondition != null)
                    return $"{this._precondition.ToString()} THEN {requirement}";

                return requirement;
            }
        }
        
        public readonly struct Principle
        {
            public string Description { get; }

            public Principle(string description)
            {
                this.Description = description;
            }

            /// <inheritdoc />
            public override string ToString() 
                => this.Description;

            [MustUseReturnValue]
            public Precondition When(bool preCondition) 
                => new Precondition(preCondition);

            [MustUseReturnValue]
            public Requirement Requires(bool condition)
                => new Requirement(condition);
            
            /// <summary>
            /// Always throws the specified exception.
            /// </summary>
            public void Throws(Exception exception) 
                => throw exception;
        }
    }
}