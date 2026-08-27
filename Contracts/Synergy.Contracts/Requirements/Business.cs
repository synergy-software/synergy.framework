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
        /// <param name="title">Business rule title.</param>
        /// <returns>Rule container that can be detailed and evaluated.</returns>
        [MustUseReturnValue]
        public static Principle Rule(string title)
            => new Principle(title);
        
        /// <summary>
        /// Starts requirement evaluation with a precondition.
        /// </summary>
        /// <param name="preCondition">Precondition value that controls whether requirement should be evaluated.</param>
        /// <param name="expression">Captured caller expression of <paramref name="preCondition" />.</param>
        /// <returns>Precondition builder.</returns>
        [MustUseReturnValue]
        public static Precondition When(bool preCondition, [System.Runtime.CompilerServices.CallerArgumentExpression("preCondition")] string? expression = null) 
            => new Precondition(preCondition);

        // TODO: Marcin Celej [from: Marcin Celej on: 27-05-2026]: add here [CallerArgumentExpression]
        
        /// <summary>
        /// Creates requirement without precondition.
        /// </summary>
        /// <param name="condition">Requirement condition.</param>
        /// <returns>Requirement builder.</returns>
        [MustUseReturnValue]
        public static Requirement Requires(bool condition)
            => new Requirement(condition);

        /// <summary>
        /// Represents precondition chain for business requirements.
        /// </summary>
        public readonly struct Precondition : IPrecondition
        {
            private readonly IPrecondition? previous;
            private readonly bool met;
            /// <summary>
            /// Gets a value indicating whether all preconditions in the chain are met.
            /// </summary>
            public bool Met => (this.previous?.Met ?? true) && this.met;

            /// <summary>
            /// Gets precondition description.
            /// </summary>
            [CanBeNull]
            public string Comment { get; }

            /// <summary>
            /// Initializes precondition.
            /// </summary>
            /// <param name="preCondition">Current precondition result.</param>
            /// <param name="previous">Previous precondition in chain.</param>
            /// <param name="comment">Precondition description.</param>
            public Precondition(bool preCondition, [CanBeNull] IPrecondition previous = null, [CanBeNull] string comment = null)
            {
                this.met = preCondition;
                this.previous = previous;
                this.Comment = comment;
            }

            // ReSharper disable once HeapView.BoxingAllocation
            /// <summary>
            /// Adds next precondition to the chain.
            /// </summary>
            /// <param name="preCondition">Next precondition result.</param>
            /// <returns>Updated precondition chain.</returns>
            [MustUseReturnValue]
            public Precondition And(bool preCondition)
                => new Precondition(preCondition, this, this.Comment);

            /// <summary>
            /// Sets description for current precondition chain.
            /// </summary>
            /// <param name="when">Precondition description.</param>
            /// <returns>Precondition chain with updated description.</returns>
            public Precondition this[[NotNull] string when] 
                => new Precondition(this.Met, this.previous, when.OrFailIfWhiteSpace(nameof(when)));

            // TODO: Marcin Celej [from: Marcin Celej on: 27-05-2026]: add here [CallerArgumentExpression]
            
            /// <summary>
            /// Creates requirement under this precondition.
            /// </summary>
            /// <param name="condition">Requirement condition.</param>
            /// <returns>Requirement builder.</returns>
            [MustUseReturnValue]
            public Requirement Requires(bool condition)
            {
                return new Requirement(this, condition);
            }

            /// <summary>
            /// Creates lazy requirement under this precondition.
            /// </summary>
            /// <param name="condition">Requirement predicate.</param>
            /// <returns>Requirement builder.</returns>
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

        /// <summary>
        /// Represents precondition state.
        /// </summary>
        public interface IPrecondition
        {
            /// <summary>
            /// Gets a value indicating whether precondition is met.
            /// </summary>
            bool Met { get; }
        }
        
        /// <summary>
        /// Represents business requirement with optional precondition.
        /// </summary>
        public readonly struct Requirement
        {
            private readonly Precondition? _precondition;
            private readonly Func<bool> _condition;

            /// <summary>
            /// Gets requirement description.
            /// </summary>
            [CanBeNull]
            public string Comment { get; }
            
            /// <summary>
            /// Gets a value indicating whether requirement is met.
            /// </summary>
            public bool Met
            {
                get
                {
                    if (this._precondition?.Met == false)
                        return true;

                    return this._condition.Invoke();
                }
            }

            /// <summary>
            /// Initializes requirement without precondition.
            /// </summary>
            /// <param name="condition">Requirement condition.</param>
            public Requirement(bool condition)
                : this(null, condition)
            {
            }

            /// <summary>
            /// Initializes requirement with precondition and eager condition.
            /// </summary>
            /// <param name="precondition">Precondition for requirement evaluation.</param>
            /// <param name="condition">Requirement condition.</param>
            public Requirement(Precondition? precondition, bool condition)
                : this(precondition, () => condition)
            {
            }

            /// <summary>
            /// Initializes requirement with precondition and lazy condition.
            /// </summary>
            /// <param name="precondition">Precondition for requirement evaluation.</param>
            /// <param name="condition">Requirement predicate.</param>
            /// <param name="comment">Requirement description.</param>
            public Requirement(Precondition? precondition, Func<bool> condition, [CanBeNull] string comment = null)
            {
                this._precondition = precondition;
                this._condition = condition;
                this.Comment = comment;
            }

            /// <summary>
            /// Sets description for requirement.
            /// </summary>
            /// <param name="rule">Requirement description.</param>
            /// <returns>Requirement with updated description.</returns>
            public Requirement this[[NotNull] string rule]
                => new Requirement(this._precondition, this._condition, rule.OrFailIfWhiteSpace(nameof(rule)));

            /// <summary>
            /// Throws <see cref="BusinessRuleViolationException" /> with specified message when requirement is not met.
            /// </summary>
            /// <param name="message">Exception message.</param>
            public void Throws(string message)
                => Throws(new BusinessRuleViolationException(message, this));

            /// <summary>
            /// Throws specified exception when requirement is not met.
            /// </summary>
            /// <param name="exception">Exception to throw.</param>
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
        
        /// <summary>
        /// Represents business rule metadata and entry points for checks.
        /// </summary>
        public readonly struct Principle
        {
            /// <summary>
            /// Gets rule title.
            /// </summary>
            public string Title { get; }

            /// <summary>
            /// Gets optional rule description.
            /// </summary>
            public string? Description { get; }

            /// <summary>
            /// Initializes rule metadata.
            /// </summary>
            /// <param name="title">Rule title.</param>
            /// <param name="description">Optional rule description.</param>
            public Principle(string title, string? description = null)
            {
                this.Title = title;
                this.Description = description;
            }

            /// <inheritdoc />
            public override string ToString() 
                => this.Title;

            // TODO: Make it accessible via indexer - instead of calling Details() method
            /// <summary>
            /// Adds rule details.
            /// </summary>
            /// <param name="description">Rule description.</param>
            /// <returns>Updated rule metadata.</returns>
            public Principle Details(string description)
                => new Principle(Title, description.OrFailIfWhiteSpace(nameof(description)));

            /// <summary>
            /// Starts requirement evaluation with a precondition.
            /// </summary>
            /// <param name="preCondition">Precondition value.</param>
            /// <returns>Precondition builder.</returns>
            [MustUseReturnValue]
            public Precondition When(bool preCondition) 
                => new Precondition(preCondition);

            // TODO: Marcin Celej [from: Marcin Celej on: 27-05-2026]: add here [CallerArgumentExpression]
            /// <summary>
            /// Creates requirement without precondition.
            /// </summary>
            /// <param name="condition">Requirement condition.</param>
            /// <returns>Requirement builder.</returns>
            [MustUseReturnValue]
            public Requirement Requires(bool condition)
                => new Requirement(condition);
            
            /// <summary>
            /// Always throws the specified exception.
            /// </summary>
            /// <param name="exception">Exception to throw.</param>
            public void Throws(Exception exception) 
                => throw exception;

            // TODO: Marcin Celej [from: Marcin Celej on: 28-05-2026]: Add Throws<TException>() method
        }
    }
}