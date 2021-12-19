/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using Project1.DirectionState;
using Project1.ItemComponents; 

namespace Project1.EnemyComponents
{
    public interface IEnemyState
    {
        IEnemy Enemy { get; set; }
        IDirectionState DirectionState {get;set;}
        Sprite Sprite { get; set; }
        string ID { get; set; }
        int Step { get; set; }
        IItem DropItem { get; set; }
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void Update();
        void TakeDamage(double damage);
    }
}
