using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    /// <summary>
    /// Singleton area used as a special case object. 
    /// Its intention is to prevent the use of null or with error handling when an entity doesn't move correctly
    /// </summary>
    internal class Limbo : Area
    {
        public static Limbo Instance { get; } = new Limbo();

        private Limbo() { }
    }
}
