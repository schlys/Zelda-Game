using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Project1.SpriteComponents
{
    public class Sprite : ISprite 
    {

        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public int TotalFrames { get; set; }
        public int CurrentFrame { get; set; }
        public int Row { get; set; }
        public Vector2 HitBox { get; set; }
        public int OriginalSize { get; set; }
        public int MaxDelay { get; set; }
        public double DelayRate { get; set; }
        public int StartDelay { get; set; }
        public double Delay { get; set; }
        public int StartFrame { get; set; }

        public Sprite(Texture2D texture, int totalFrames, int currentFrame, int row, int s, int hitx, int hity)
        {
            Texture = texture;
            TotalFrames = totalFrames;
            CurrentFrame = currentFrame;
            StartFrame = currentFrame;
            Color = Color.White; 
            Row = row;
            OriginalSize = s;
            MaxDelay = GameVar.SpriteMaxDelay;
            DelayRate = GameVar.SpriteDelayRate;
            StartDelay = MaxDelay;
            HitBox = new Vector2(hitx, hity); 
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            /* Draw the <CurrentFrame> at the given <position>, factor by <scalingFactor> 
             */ 

            int scalingFactor = GameVar.ScalingFactor;
            int universalSize = SpriteFactory.Instance.UniversalSize; 
            Rectangle sourceRectangle = new Rectangle((CurrentFrame - 1) * universalSize, Row * universalSize, universalSize, universalSize);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, universalSize * scalingFactor, universalSize * scalingFactor);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color);
        }

        public void Update()
        {
            /* Update <CurrentFrame> at the defined rate to animate the Sprite. 
             */ 
            
            Delay+=DelayRate;
            if (Delay > MaxDelay)
            {
                if (CurrentFrame < TotalFrames)
                {
                    CurrentFrame++;
                }
                else
                {
                    CurrentFrame=StartFrame;
                }
                Delay = 0;
            }
        }

    }
}
