using System.Timers;

namespace CLI_Game.Source.Definition
{


    public delegate void ClickEventHandler(object sender, ClickEventArgs e);

    public sealed class ClickEventArgs : EventArgs;
    
    
    internal sealed class Button : IControl
    {
        public string ID { get; init
            {
                field = value.ToLower();
            } 
        }

        public string Text { get; 
            set {
                field = (value != string.Empty) ? value : ID;
            } 
        }

        public event ClickEventHandler OnEnter;


        public Button(string id,string text = "")
        {
            Text = text;
            ID = id;

            
        }
    }
}
