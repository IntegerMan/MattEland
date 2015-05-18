using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ani.Core.Models.Weather
{
    public class WeatherHistoryModel
    {

        public int ZipCode { get; set; }

        public IList<WeatherPrediction> Predeictions { get; set; } = new List<WeatherPrediction>();

    }
}
