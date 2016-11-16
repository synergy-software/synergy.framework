using JetBrains.Annotations;

namespace Synergy.Contracts.Samples.Domain
{
    public class Address
    {
        public Address([NotNull] string city)
        {
            this.City = city;
        }

        [NotNull]
        public string City { get; set; }
    }
}