using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    class ZombieGirl:Zombie
    {
        public ZombieGirl(Vector2 startPos) : base(startPos)
        {
            walk = new Texture2D[10];
            death = new Texture2D[12];
            attack = new Texture2D[8];
            idle=new Texture2D[15];

            const int o = 5;

            Rectanglew = new Rectangle(0, 0, 430 / o, 519 / o);
            hitbox = new Rectangle(0, 0, 45, 100);
            rectangleA = new Rectangle(0, 0, 430 / o, 519 / o);
            RectangleD = new Rectangle(0, 0, 609 / o, 546 / o);

            this.StartPos = position = new Vector2(startPos.X, startPos.Y - 50);

            elapsed = 0;

            WalkI = 1;

            attackI = 0;


            isJumping = false;
            isCrouching = false;
            Right = true;

            Idle = false;

            for (int i = 0; i < 10; i++)
            {
                walk[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/walk/Walk (" + (i) + ")");
            }

            for (int i = 0; i < 12; i++)
            {
                death[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/death/Dead (" + (i+1) + ")");
            }
            for (int i = 0; i < 8; i++)
            {
                attack[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/attack/Attack (" + (i + 1) + ")");
            }
            for (int i = 0; i < 15; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/idle/Idle (" + (i + 1) + ")");
            }
        }
    }
}
