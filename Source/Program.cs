using CLI_Game.Source.Core;
using CLI_Game.Source.Definition;

namespace CLI_Game.Source;


public static class Program
{
    public static int Main(string[] args)
    {
        try
        {
            Console.CursorVisible = false;
            Console.TreatControlCAsInput = true;
            Request player = new(new(0, 8), '0');
            Engine engine = new(new(10, 10), [player]);
            engine.Start();
            return 0;
        }catch(Exception ex)
        {
            Console.Error.WriteLine(ex);
            return 1;
        }
    }
}