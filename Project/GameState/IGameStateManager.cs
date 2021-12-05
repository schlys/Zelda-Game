using Microsoft.Xna.Framework.Graphics;

namespace Project1.GameState
{
    public interface IGameStateManager
    {
        static IGameState Instance { get; set; }
        IGameState CurrentState { get; set; }
        Game1 Game { get; set; }
        SpriteFont TitleFont { get; set; }
        SpriteFont BodyFont { get; set; }
        void Initialize(Game1 game);
        void Reset(); 
        void Pause();
        void Start();
        void ItemSelection();
        void GameOverLose();
        void GameOverWin();
        void SetLinkCount(int n);
        void StartScroll();
        void StopScroll();
        void EnterStoreMenu();
        void ExitStoreMenu();
        bool CanPlayGame();
        bool CanDrawHUD();
        bool CanItemSelect();
        bool CanItemScroll();
        bool CanRoomScroll();
        bool CanStoreMenu(); 
    }
}
