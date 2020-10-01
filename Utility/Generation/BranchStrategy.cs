using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ZuulTextBased.Game.World.Structures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Utility.Generation
{
    internal class BranchStrategy : FloorGenerationStrategy
    {
        public Dictionary<Queue<Point>, Direction> ActiveBranches { get; }
        public List<Point> UsedCoordinates { get; }
        private int _branchEventCounter;

        public BranchStrategy(Floor floor) : base(floor)
        {
            ActiveBranches = new Dictionary<Queue<Point>, Direction>();
            UsedCoordinates = new List<Point>();
            _branchEventCounter = 1;
        }

        public override void GenerateRooms(int amount)
        {
            Queue<Point> branch = new Queue<Point>();
            branch.Enqueue(new Point(0, 0));
            ActiveBranches.Add(branch, Directions.RandomValid());
            while(amount > 0)
            {
                amount = BranchStep(amount);
            }
            Logger.Instance.Debug(GetType(), $"Generation complete! showing map:\n\n" + $"{Floor.AsciiMap()}");
        }

        private int BranchStep(int amount)
        {
            foreach (Queue<Point> branch in ActiveBranches.Keys)
            {
                Point source = branch.Dequeue();
                UsedCoordinates.Add(source);
                Point newCoordinate = Points.Add(source, Directions.ToPoint(ActiveBranches[branch]));
                if (amount > 0)
                {
                    if (!UsedCoordinates.Contains(newCoordinate))
                    {
                        Floor.CreateRoom(newCoordinate);
                        Floor.LinkAreas(source, ActiveBranches[branch], newCoordinate);
                        branch.Enqueue(newCoordinate);
                        amount--;
                    }
                    else
                    {
                        Floor.LinkAreas(source, ActiveBranches[branch], newCoordinate);
                        ActiveBranches.Remove(branch);
                    }
                }
            }
            return amount;
        }
    }
}
