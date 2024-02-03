using System;
using JetBrains.Annotations;

namespace Synergy.Contracts.Samples.Domain
{
    public class Contractor
    {
        [NotNull, Pure]
        public static Contractor CreateCompany([NotNull] string name)
        {
            Fail.IfArgumentEmpty(name);

            return new Contractor
            {
                Id = Guid.NewGuid(),
                Type = ContractorType.Company,
                CompanyName = name
            };
        }

        [NotNull, Pure]
        public static Contractor CreatePerson(string firstName, string lastName)
        {
            Fail.IfArgumentEmpty(firstName);
            Fail.IfArgumentWhiteSpace(lastName);

            return new Contractor()
            {
                FirstName = firstName,
                LastName = lastName
            };
        }

        public Guid Id { get; set; }

        public ContractorType Type { get; set; }

        [CanBeNull]
        public string CompanyName { get; set; }

        [CanBeNull]
        public string LastName { get; set; }

        [CanBeNull]
        public string FirstName { get; set; }

        [CanBeNull, Pure]
        public string GetName()
        {
            switch (this.Type)
            {
                case ContractorType.Company:
                    return this.CompanyName;
                case ContractorType.Person:
                    return this.FirstName + " " + this.LastName;
                default:
                    throw Fail.BecauseEnumOutOfRange(this.Type);
            }
        }

        [CanBeNull]
        public Address Address { get; set; }

        [NotNull]
        public string GetCity()
        {
            return this.Address.FailIfNull()
                       .City.FailIfNull();
        }

        public void SetPersonName([NotNull] string firstName, [NotNull] string lastName)
        {
            Fail.IfArgumentEmpty(firstName);
            Fail.IfArgumentEmpty(lastName);

            throw Fail.Because("Not implemented yet");
        }
    }
}