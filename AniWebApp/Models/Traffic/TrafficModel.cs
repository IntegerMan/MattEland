using System.Collections.Generic;
using Ani.Core;

namespace AniWebApp.Models
{
    public class TrafficModel
    {
        public List<ActiveTrafficIncidentInfoSelect_Result> Accidents { get; set; }
        public List<ActiveTrafficIncidentInfoSelect_Result> ConstructionEvents { get; set; }
    }
}