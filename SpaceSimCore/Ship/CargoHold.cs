using System.Collections.Generic;

namespace MattEland.SpaceSim.Ship
{
    public class CargoHold : ShipArea
    {
        public ICollection<CargoItem> Items { get; } = new List<CargoItem>();
    }
}