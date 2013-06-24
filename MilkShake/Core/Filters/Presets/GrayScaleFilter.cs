using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Filters.Presets
{
    public class GrayscaleFilter : Filter
    {
        public GrayscaleFilter() : base("Scene//Levels//Effects//GrayScale")
        {
            Name = "GrayScale";
        }
    }
}
