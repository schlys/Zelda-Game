using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;

namespace Project1.BlockComponents
{
    public interface IBlock
    {
        Vector2 Position { get; set; }
        Sprite Sprite { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Reset();
        void Change(string type);
    }
}
