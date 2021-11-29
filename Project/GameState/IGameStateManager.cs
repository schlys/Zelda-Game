﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public interface IGameStateManager
    {
        static IGameState Instance { get; set; }
        IGameState CurrentState { get; set; }
        Game1 Game { get; set; }
        SpriteFont Font { get; set; }
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
        void StoreMenu(); 
        bool CanPlayGame();
        bool CanDrawHUD();
        bool CanItemSelect();
        bool CanItemScroll();
        bool CanRoomScroll(); 

    }
}
