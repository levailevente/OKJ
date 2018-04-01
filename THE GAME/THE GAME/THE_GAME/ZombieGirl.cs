using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    class ZombieGirl : Zombie
    {
        public ZombieGirl(Vector2 startPos) : base(startPos)
        {
            Walk = new Texture2D[10];
            Death = new Texture2D[12];
            Attack = new Texture2D[8];
            idle = new Texture2D[15];

            const int o = 5;

            Rectanglew = new Rectangle(0, 0, 430 / o, 519 / o);
            hitbox = new Rectangle(0, 0, 45, 100);
            RectangleA = new Rectangle(0, 0, 430 / o, 519 / o);
            RectangleD = new Rectangle(0, 0, 609 / o, 546 / o);

            StartPos = Position = new Vector2(startPos.X, startPos.Y - 50);

            Elapsed = 0;

            WalkI = 0;

            AttackI = 0;

            DeadI = 0;

            IdleI = 0;

            IsJumping = false;
            IsCrouching = false;
            Right = true;

            Idle = false;

            for (int i = 0; i < 10; i++)
            {
                Walk[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/walk/Walk (" + (i) + ")");
            }

            for (int i = 0; i < 12; i++)
            {
                Death[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/death/Dead (" + (i + 1) + ")");
            }

            for (int i = 0; i < 8; i++)
            {
                Attack[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/attack/Attack (" + (i + 1) + ")");
            }

            for (int i = 0; i < 15; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/idle/Idle (" + (i + 1) + ")");
            }
        }
    }
}
