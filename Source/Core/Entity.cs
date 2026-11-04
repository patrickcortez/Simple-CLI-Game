using System;
using System.Collections.Generic;
using CLI_Game.Source.Definition;
using System.Text;

namespace CLI_Game.Source.Core
{
    internal class Entity
    {
        public Point Position;
        public char Data { get; }

        public Entity(Point Pos,char Rep)
        {
            Position = Pos;
            Data = Rep;
        }
    }
}
