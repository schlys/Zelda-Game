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
