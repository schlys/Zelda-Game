/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Project1.LinkComponents; 

namespace Project1.Controller
{
    public interface IController
    {
        Game1 Game { get; set; }
        void InitializeGameCommands();
        void InitializeLinkCommands(ILink Link, int player); 
        void Update();
        void Reset();
    }
    
}
