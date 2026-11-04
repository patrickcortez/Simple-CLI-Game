using CLI_Game.Source.Definition;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CLI_Game.Source.Core
{
    internal class Engine
    {
         private Point ScreenSize { get; }
        private Entity Player { get; set; }
        private Render render { get; set; }

        private List<Entity> Debrees { get; set; }

        public Engine(Point screenSize,IEnumerable<Request> Init)
        {
            ScreenSize = screenSize;
            render = new(ScreenSize,Init);
            Player = new(Init.ToArray()[0].Position, Init.ToArray()[0].Data);
        }

        private void UpdatePlayer()
        {
            if (!Console.KeyAvailable) return; 

            ConsoleKeyInfo Current = Console.ReadKey();

            if(Current.Key is ConsoleKey.LeftArrow && Player.Position.X > 0)
            {
                render.WriteToScreen(new(Player.Position, ' '));
                Player.Position.X--;
                render.WriteToScreen(new(Player.Position, Player.Data));
            }else if(Current.Key is ConsoleKey.RightArrow && Player.Position.X < ScreenSize.X-1)
            {
                render.WriteToScreen(new(Player.Position, ' '));
                Player.Position.X++;
                render.WriteToScreen(new(Player.Position, Player.Data));
            }
        }

        private void SpawnDebree()
        {
            // Handle spawning of debree
            Random rand = new();

            int Capacity = rand.Next(1, 4);

            Debrees = new(Capacity);

            for (int i = 0; i < Capacity;i++)
            {
                Random randpos = new();
                Point pos = new(randpos.Next(0, ScreenSize.X), 0);
                Entity debree = new(pos, 'X');
                Debrees.Add(debree);
                render.WriteToScreen(new(debree.Position,debree.Data));
            }
        }

        private void UpdateDebree(ref int Score)
        {
            // Update Debree

            List<Entity> Removal = new();
            int tmp = Score;
            bool hasHit = false,hasLanded=false;
            
            Debrees.ForEach(item =>
            {
                if (item.Position.Y == 8)
                {
                    if(Player.Position == item.Position)
                    {
                        hasHit = true;

                    }
                    hasLanded = true;
                    render.WriteToScreen(new(item.Position, ' '));
                    Removal.Add(item);
                }
                else
                {
                    render.WriteToScreen(new(item.Position, ' '));
                    item.Position.Y++;
                    render.WriteToScreen(new(item.Position, item.Data));
                }
            });

            if(Removal.Count > 0)
            {
                for(int i=0; i < Debrees.Count; i++)
                {
                    for(int j = 0; j < Removal.Count; j++)
                    {
                        if (Debrees[i] == Removal[j])
                        {
                            Debrees.Remove(Removal[j]);
                            break;
                        }
                    }    
                }
            }

            if (!hasHit && hasLanded)
            {
                tmp++;
            }
            Score = tmp;
        }

        private bool isGameOver()
        {
            Point PlayerPosition = Player.Position;
            bool isOver = false;

            Debrees.ForEach(debree =>
            {
                if (Player.Position == debree.Position)
                {
                    isOver = true;
                }

            });

            return isOver;
        }

        private void Init()
        {

            render.WriteToScreen(new(0, 9), "==========");
        }

        public void Start()
        {
            Init();
            SpawnDebree();
            int Score = 0;
            while (true)
            {
                //    render.Clear();

                if (Debrees.Count == 0)
                {
                    SpawnDebree();
                }

                if (isGameOver())
                {
                    break;
                }

                UpdateDebree(ref Score);
                UpdatePlayer();
                render.RenderScoreBoard(Score);

                render.RenderScreen();
                Thread.Sleep(100);
            }

            render.WriteToScreen(new(0, 5), "Game Over");
            render.WriteToScreen(new(0, 6), $"Score:{Score}");
            render.RenderScreen();
        }
    }
}
