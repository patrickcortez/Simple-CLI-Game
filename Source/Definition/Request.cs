using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CLI_Game.Source.Definition
{
    struct Point
    {
       public int X
        {
            get; set;
        }
       public int Y
        {
#pragma warning disable CS9266
            get; set;
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
            
        }

        public override bool Equals(object? O)
        {
            if(O is Point p)
            {
                return this == p;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    internal struct Request
    {
       public Point Position
        {
            get; set
            {
                if(value.X < 0 || value.Y < 0)
                {
                    throw new InvalidDataException("Coordinate for a request cannot be negative"); // for now
                }
                field = value;
            }
        }

        public char Data
        {
            get; set;
        }

        public Request(Point position, char data)
        {
            Position = position;
            Data = data;
        }

    }
}
