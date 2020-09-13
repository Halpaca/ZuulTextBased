using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZuulTextBased.Game.World.Structures;

namespace ZuulTextBased.Utility.Generation
{
    internal class BlobGenerationStrategy : FloorGenerationStrategy
    {
        public BlobGenerationStrategy(Floor floor) : base(floor)
        {
        }

        public override void GenerateRooms(int amount)
        {
            while (amount > 0)
            {
                Point source = RandomExistingCoordinate();
                Direction randomDirection = Directions.RandomValid();
                Point destination = Points.Add(source, Directions.ToAdditivePoint(randomDirection));
                if (!Floor.CreateRoom(destination).Equals(Limbo.Instance))
                {
                    Floor.LinkAreas(source, randomDirection, destination);
                    amount--;
                }
            }
        }

        public Point RandomExistingCoordinate()
        {
            return Floor.Areas.Keys.ElementAt(Random.Next(0, Floor.Areas.Count));
        }
    }
}
