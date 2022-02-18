using Newtonsoft.Json;

namespace Synergy.Catalogue
{
    public struct SnapshotOf<T>
    {
        private readonly string snapshot;

        public SnapshotOf(T source)
        {
            this.snapshot = Serialize(source);
        }

        private static string Serialize(T source)
        {
            if (source == null)
                return null;

            return JsonConvert.SerializeObject(source);
        }

        public bool HasChanged(T changed)
        {
            if (this.snapshot == null && changed == null)
                return false;

            if (this.snapshot == null || changed == null)
                return true;
            
            var newSnapshot = Serialize(changed);
            return this.snapshot != newSnapshot;
        }
    }
}