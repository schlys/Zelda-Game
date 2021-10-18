using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.CollisionComponents;
using Project1.DirectionState;
using System.Reflection;

namespace Project1.EnemyComponents
{
    class Enemy : IEnemy, ICollidable 
    {
        // Properties from IEnemy 
        public IEnemyState EnemyState { get; set; }
        public EnemyHealth Health { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }      // Updated by EnemyState 
        public String TypeID { get; set; }

        // Other Properties 
        private double Step = .1;
        private double counter = 0.0;
        private bool IsDead = false;
        private string[] EnemyTypeKeys = { "Moblin" , "Keese", "Stalfos", "Aquamentus", "Gel", "Goriya", "OldMan"};

        public Enemy(Vector2 position, string type)
        {
            // TODO: switch to jump table /
           
            Assembly assem = typeof(IEnemyState).Assembly;
            Type enemyType = assem.GetType("Project1.EnemyComponents.EnemyState" + type);
            ConstructorInfo enemyConstructor = enemyType.GetConstructor(new[] { typeof(IEnemy) });    

            object enemyState = enemyConstructor.Invoke(new object[] { this});
            EnemyState = (IEnemyState)enemyState;
           



            Health = new EnemyHealth(3, 30);                     // default health is 3 of 3 hearts (change to 30 b.c. for testing death)
            Position = position;
            InitialPosition = Position;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox, EnemyState.Size);
            IsMoving = true;
            TypeID = this.GetType().Name.ToString()+type;
        }

        public void TakeDamage(int damage)
        {
            // TODO: need determine value to decrease by  
            EnemyState.Sprite.Color = Color.Red;
            EnemyState.TakeDamage(damage);
            //Health.DecreaseHealth(0.5);
            IsDead = Health.Dead();
        }

        public void PreviousEnemy()
        {
            ResetPosition();
            SetEnemyState((int)counter);
            IncrementCounter(-Step);
        }

        public void NextEnemy()
        {
            ResetPosition();
            SetEnemyState((int)counter);
            IncrementCounter(Step);
        }
        public void SetEnemyState(int i)
        {
            // TODO: switch to jump table 
            switch (EnemyTypeKeys[i])
            {
                case "Moblin":
                    EnemyState = new EnemyStateMoblin(this);
                    break;
                case "Stalfos":
                    EnemyState = new EnemyStateStalfos(this);
                    break;
                case "Keese":
                    EnemyState = new EnemyStateKeese(this);
                    break;
                case "Aquamentus":
                    EnemyState = new EnemyStateAquamentus(this);
                    break;
                case "Gel":
                    EnemyState = new EnemyStateGel(this);
                    break;
                case "Goriya":
                    EnemyState = new EnemyStateGoriya(this);
                    break;
                case "OldMan":
                    EnemyState = new EnemyStateOldMan(this);
                    break;
            }
        }
        public void IncrementCounter(double i)
        {
            counter += i;
            if (counter > (EnemyTypeKeys.Length - Step / 2))
            {
                counter = 0;
            }
            else if (counter < -Step / 2)
            {
                counter = EnemyTypeKeys.Length - 1;
            }
        }

        public void Reset()
        {
            ResetPosition();
            //EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin -Removed
            Health = new EnemyHealth(3, 30);                  // default health is 3 of 3 hearts 
            IsMoving = true;
            IsDead = false;
            EnemyState.Sprite.Color = Color.White;
            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox, EnemyState.Size);
            CollisionManager.Instance.AddObject(this);
        }
        public void ResetPosition()
        {
            Position = InitialPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!IsDead)
                EnemyState.Draw(spriteBatch, Position);
        }

        public void Update()
        {
            if (!IsDead)
            {
                IsMoving = true;
                EnemyState.Update();
                // Update Hitbox for collisions 
                Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox, EnemyState.Size);
            }
            else
            {
                CollisionManager.Instance.RemoveObject(this);
            }
            
        }
    }
}