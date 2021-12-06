using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using Project1.CollisionComponents;
using Project1.ItemComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateOldMan : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Step { get; set; }
        public IItem DropItem { get; set; }

        public EnemyStateOldMan(IEnemy enemy, string type)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateNotMoving();
            ((ICollidable)Enemy).IsMoving = false;
            ID = type;
            Sprite = SpriteFactory.Instance.GetSpriteData(type);
            Step = 0;   // no movement
            DropItem = new NullItem();
        }

        public void TakeDamage(double damage)
        {
            Enemy.Health.DecreaseHealth(damage);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position);
        }

        public void Update()
        {
            Sprite.Update();
        }
    }
}
