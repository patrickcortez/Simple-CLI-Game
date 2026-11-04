using CLI_Game.Source.Definition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CLI_Game.Source.Core
{

    internal class Render
    {
        private Point ScreenSize { get;
        } 

        private List<Request> Requests { get; set; }

        public void WriteToScreen(Request request)
        {
            Request find = Requests.FirstOrDefault(req => req.Position == request.Position,new(new(11,11),'\0'));

            if (find.Data != '\0')
            {
                int index = Requests.IndexOf(find);
                find = request;
                Requests[index] = find;
                return;
            }

            Requests.Add(request);
        }

        public void WriteToScreen(Point pos,string Data)
        {
            int Cursor = 0;
            while(Cursor < Data.Length)
            {
                if(pos.X < ScreenSize.X)
                {
                    WriteToScreen(new(pos, Data[Cursor]));
                    pos.X++;
                }
                else
                {
                    pos.Y++;
                    pos.X = 0;
                    WriteToScreen(new(pos, Data[Cursor]));
                    pos.X++;
                }
                Cursor++;
            }
        }

        public void RenderScoreBoard(int Score)
        {
            Console.SetCursorPosition(15, 1);

            Console.Write($"Score: {Score}");
        }

        public void RenderScreen()
        {
            Console.SetCursorPosition(0, 0);
            StringBuilder Pending = new();
            for(int y = 0; y < ScreenSize.Y; y++)
            {

                for(int x=0;x < ScreenSize.X; x++)
                {
                    int R_index = 0;
                    bool found = false;
                    if(Requests.Count > 0)
                    {
                        while(R_index < Requests.Count)
                        {
                            Point Current = new(x, y);
                            if (Requests[R_index].Position == Current)
                            {
                                Pending.Append(Requests[R_index].Data);
                                found = true;
                                break;
                            }
                            R_index++;
                        }

                        if (found)
                        {
                            continue;
                        }
                    }
                    Pending.Append(' ');

                }
                Pending.Append('\n');
            }

            Console.Write(Pending.ToString());
            
        }

        public void Clear()
        {
            Console.SetCursorPosition(0, 0);
            StringBuilder Pending = new();
            for (int y = 0; y < ScreenSize.Y; y++)
            {
                for (int x = 0; x < ScreenSize.X; x++)
                {
                    Pending.Append(' ');

                }
                Pending.Append('\n');
            }
            Console.Write(Pending.ToString());
        }

        public Render(Point screenSize,IEnumerable<Request>? requests=null)
        {
            ScreenSize = screenSize;

            Requests = (requests is not null ) ? new List<Request>(requests) : new List<Request>();
        }
    }
}
