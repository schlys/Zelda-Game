/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Content;

namespace Project1.SpriteComponents
{
    public interface ISpriteFactory
    {
        public static ISpriteFactory Instance { get; }
        void LoadAllTextures(ContentManager content);
    }
}
