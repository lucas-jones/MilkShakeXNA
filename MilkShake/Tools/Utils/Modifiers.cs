using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;

namespace MilkShakeFramework.Tools.Utils
{
    /*
     * Just a rough idea
    */
    public class Modifier : GameEntity
    {
        public GameEntity Target { get { return (GameEntity)Parent; } }
    }

    public class Modifiers<T> where T : Modifier
    {
        private List<T> _modifiers;

        public Modifiers()
        {
            _modifiers = new List<T>();
        }

        public void AddModifier(T modifier)
        {
            _modifiers.Add(modifier);
        }
    }

    public class AwesomeModifier : Modifier { }

    public class Pow
    {
        // Only allow AwesomeModifiers!
        Modifiers<AwesomeModifier> _modifiers;

        public Pow()
        {
            _modifiers = new Modifiers<AwesomeModifier>();
        }
    }
}
