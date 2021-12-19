/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.EnemyComponents
{
    public interface IEnemy
    {
        IEnemyState EnemyState { get; set; }
        EnemyHealth Health { get; set; }
        Vector2 Position { get; set; }
        Vector2 InitialPosition { get; set; }
        void TakeDamage(double damage, string direction);
        void Knockback(string direction);
        void Draw(SpriteBatch spriteBatch);
        void Update();
        void Reset();
        void Spawn();
    }
}
