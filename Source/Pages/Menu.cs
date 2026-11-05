using CLI_Game.Source.Definition;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLI_Game.Source.Pages
{
    internal class Menu
    {
        private Point ScreenSize { get; init {

                if (value.X < 0 || value.Y < 0) throw new InvalidDataException("Cannot set screensize dimension to negative");

                field = value;    
            } 
        }



        public Menu(Point S_Size)
        {
            ScreenSize = S_Size;
            Button b = new("btn_menu", "Start");
            b.OnEnter += (s, e) =>
            {
               
            };
        }
    }
}
