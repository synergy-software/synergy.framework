using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Reflection
{
    public class EnumSpecifics
    { 
        [NotNull, Pure]
        public static Concrete<T> Of<T>()
        {
            return new Concrete<T>();
        }

        [NotNull, Pure]
        public static Weak Of([NotNull] Type type)
        {
            return new Weak(type);
        }

        public class Concrete<TEnum> : EnumSpecifics
        {
            [NotNull]
            private readonly Weak weak;

            internal Concrete()
            {
                this.weak = new Weak(typeof(TEnum));
            }

            [NotNull, Pure]
            public string GetEnumName(TEnum @enum)
            {
                return this.weak.GetEnumName(@enum.CastOrFail<Enum>());
            }

            [CanBeNull, Pure]
            public string GetEnumDisplayName(TEnum @enum)
            {
                return this.weak.GetEnumDisplayName(@enum.CastOrFail<Enum>());
            }

            [NotNull, Pure]
            public TEnum[] GetValues()
            {
                return this.weak
                    .GetValues()
                    .Cast<TEnum>()
                    .ToArray();
            }
        }

        public class Weak
        {
            [NotNull]
            private readonly Type type;

            internal Weak([NotNull] Type type)
            {
                Fail.IfArgumentNull(type, nameof(type));
                Fail.IfFalse(type.IsEnum, "Type '{0}' is not an enum", type);

                this.type = type;
            }

            [NotNull, Pure]
            public string GetEnumName([NotNull] Enum @enum)
            {
                Fail.IfArgumentNull(@enum, nameof(@enum));

                var field = this.GetFieldInfo(@enum);
                var enumMemberAttribute = field.GetCustomAttribute<EnumMemberAttribute>();
                var name = enumMemberAttribute?.Value ?? @enum.ToString();

                return name;
            }

            [CanBeNull, Pure]
            public string GetEnumDisplayName([NotNull] Enum @enum)
            {
                Fail.IfArgumentNull(@enum, nameof(@enum));

                var field = this.GetFieldInfo(@enum);
                var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
                return displayAttribute?.Name;
            }

            [NotNull, Pure]
            public FieldInfo GetFieldInfo([NotNull] Enum @enum)
            {
                Fail.IfArgumentNull(@enum, nameof(@enum));

                string name = Enum.GetName(this.type, @enum);
                Fail.IfNull(name, "Enum value {0} not found in enum {1}", @enum, this.type);
                FieldInfo field = this.type.GetField(name);
                return field.OrFail(nameof(field));
            }

            [NotNull, Pure]
            public Enum[] GetValues()
            {
                return Enum
                    .GetValues(this.type)
                    .Cast<Enum>()
                    .ToArray();
            }
        }
    }
}
