using System.Collections.Generic;

namespace Ani.Core.Models.Traffic
{
    public class TrafficModel
    {
        public List<ActiveTrafficIncidentInfoSelect_Result> Accidents { get; set; }
        public List<ActiveTrafficIncidentInfoSelect_Result> ConstructionEvents { get; set; }
    }
}