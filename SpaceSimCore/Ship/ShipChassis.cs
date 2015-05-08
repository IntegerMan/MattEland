using System.Collections.Generic;
using System.Linq;

namespace MattEland.SpaceSim.Ship
{
    public class ShipChassis
    {
        private readonly List<ShipAttachment> _attachments = new List<ShipAttachment>();
        private readonly List<ShipArea> _areas = new List<ShipArea>();

        public ICollection<ShipArea> Areas => _areas;

        public ICollection<ShipAttachment> Attachments => _attachments;

        public IEnumerable<PowerPlant> PowerPlants => GetTypedAttachments<PowerPlant>();

        public IEnumerable<ShipEngine> Engines => GetTypedAttachments<ShipEngine>();

        private IEnumerable<T> GetTypedAttachments<T>()
        {
            return _attachments.Where(a => a.GetType() == typeof (T)).Cast<T>();
        }

    }
}
