using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using System.Reflection;

namespace Project1.GameState
{
    /* GameStateManager is a singleton that manages the current state  for the game
     * using <CurrentState>. The states include the start, game play, item selection screen, 
     * game over win, game over lose, pause, game over, restart, and scrollscreen state. 
     */ 
    public sealed class GameStateManager: IGameStateManager
    {
        private static GameStateManager instance = new GameStateManager();
        public static GameStateManager Instance
        {
            get
            {
                return instance;
            }
        }
        public IGameState CurrentState { get; set; }
        public Game1 Game { get; set; }
        public SpriteFont Font { get; set; }

        private Sprite Link = SpriteFactory.Instance.GetSpriteData("PickUpItem");
        private Sprite TriForceFragment = SpriteFactory.Instance.GetSpriteData("TriforceFragment");
        private int Height = 176 * GameObjectManager.Instance.ScalingFactor;
        private int Width = 256 * GameObjectManager.Instance.ScalingFactor;
        private GameStateManager() 
        {
            // Set default room 
            CurrentState = new GameStateStart();
        }
        public void Initialize(Game1 game)
        {
            Game = game;
            Font = GameStateManager.Instance.Game.Content.Load<SpriteFont>("Fonts/TitleFont");
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            CurrentState.Draw(spriteBatch);   
        }
        public void Reset() 
        {
            // Restart the game from the beginning 
            CurrentState = CurrentState.Reset();
        }
        public void Pause() 
        {
            // Pause the game - stop all motion
            CurrentState = CurrentState.Pause();
        }
        public void Start() 
        {
            //GameSoundManager.Instance.PlaySong();
            CurrentState = CurrentState.StartGame(); 
        }
        public void ItemSelection() 
        {
            // Display the item selection screen
            CurrentState = CurrentState.ItemSelectMenu();
        }
        public void GameOverLose() 
        {
            // Game is lost, can restart the game or exit 
            GameSoundManager.Instance.PlayLinkDie();
            GameSoundManager.Instance.StopSong();
            CurrentState = CurrentState.LoseGame();
        }
        public void GameOverWin() 
        {
            // Game is won, can restart the game or exit 
            CurrentState = CurrentState.WinGame();
        }
        public void StartScroll()
        {
            // In GamePlay, trigger scroll animation
            CurrentState = CurrentState.StartScroll();
        }
        public void StopScroll()
        {
            CurrentState = CurrentState.StopScroll();
        }
        public bool CanPlayGame()
        {
            // True if <CurrentState> is of type GameStateGamePlay 
            return (CurrentState is GameStateGamePlay); 
        }
        public bool CanDrawHUD()
        {
            // True if not drawing item select screen 
            return !CanItemSelect() && !CanItemScroll();
        }
        public bool CanItemSelect()
        {
            // True if <CurrentState> is of type GameItemSelect
            return (CurrentState is GameStateItemSelect);
        }
        public bool CanItemScroll()
        {
            // True if <CurrentState> is of type GameItemScroll 
            return (CurrentState is GameStateItemScroll); 
        }
        public bool CanRoomScroll()
        {
            // True if <CurrentState> is of type GameRoomScroll
            return (CurrentState is GameStateRoomScroll);
        }
    }
}
