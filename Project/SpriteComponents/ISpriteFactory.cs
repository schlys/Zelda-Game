using Microsoft.Xna.Framework.Content;

namespace Project1.SpriteComponents
{
    public interface ISpriteFactory
    {
        public static ISpriteFactory Instance { get; }
        void LoadAllTextures(ContentManager content);
    }
}
