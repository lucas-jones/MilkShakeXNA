using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Filters.Presets
{
    public class InvertFilter : Filter
    {
        public InvertFilter() : base("Scene//Levels//Effects//Invert")
        {
            Name = "Invert";
        }
    }
}
