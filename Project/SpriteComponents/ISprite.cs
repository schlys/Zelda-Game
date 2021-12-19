/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Project1.SpriteComponents
{
    public interface ISprite
    {

        Texture2D Texture { get; set; }
        Color Color { get; set; }
        int TotalFrames { get; set; }
        int StartFrame { get; set; }
        int CurrentFrame { get; set; }
        Vector2 HitBox { get; set; }
        int OriginalSize { get; set; }
         int MaxDelay { get; set; }
        double DelayRate { get; set; }
        int StartDelay { get; set; }
        double Delay { get; set; }
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void Update(); 
    }
}
