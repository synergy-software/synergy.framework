using System;
using JetBrains.Annotations;

namespace Synergy.NHibernate.Domain
{
    public class Entity: IEquatable<Entity>
    {
        //public Entity()
        //{
        //    //this.CreatedOn = DateTime.Now;
        //    //this.ModifiedOn = DateTime.Now;
        //}

        public virtual long Id { get; [UsedImplicitly] protected set; }

        public virtual int Version { get; protected set; }

        //public virtual long CreatedBy { get; protected set; }

        //public virtual DateTime CreatedOn { get; protected set; }

        //public virtual long ModifiedBy { get; protected set; }

        //public virtual DateTime ModifiedOn { get; protected set; }

        [CanBeNull]
        public virtual long? TenantId { get; set; }

        //public virtual void SetCreationInformation(long createdById, DateTime createdOn)
        //{
        //    this.CreatedBy = createdById;
        //    this.CreatedOn = createdOn;
        //}

        //public virtual void SetModificationInformation(long modifiedById, DateTime modifiedOn)
        //{
        //    this.ModifiedBy = modifiedById;
        //    this.ModifiedOn = modifiedOn;
        //}

        [NotNull, Pure]
        public virtual Type GetUnproxiedType()
        {
            return this.GetType();
        }

        public static bool operator ==([CanBeNull] Entity left, [CanBeNull] Entity right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=([CanBeNull] Entity left, [CanBeNull] Entity right)
        {
            return !object.Equals(left, right);
        }

        /// <summary>
        ///     Indicates whether the current <see cref="T:FluentNHibernate.Data.Entity" /> is equal to another
        ///     <see cref="T:FluentNHibernate.Data.Entity" />.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="obj" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="obj">An Entity to compare with this object.</param>
        [Pure]
        public virtual bool Equals(Entity obj)
        {
            //if (object.ReferenceEquals(null, obj))
            //    return false;
            //if (object.ReferenceEquals(this, obj))
            //    return true;
            //if (this.GetType() != obj.GetType())
            //    return false;

            //return obj.Id == this.Id;

            return this.Equals((object) obj);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:FluentNHibernate.Data.Entity" /> is equal to the current
        ///     <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     true if the specified <see cref="T:FluentNHibernate.Data.Entity" /> is equal to the current
        ///     <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception>
        /// <filterpriority>2</filterpriority>
        [Pure]
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
                return false;
            if (object.ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;

            return ((Entity) obj).Id == this.Id;
        }

        /// <summary>
        ///     Serves as a hash function for a Entity.
        /// </summary>
        [Pure]
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return this.Id.GetHashCode()*397 ^ this.GetType().GetHashCode();
        }
    }
}