using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    class Zombie:Karakter
    {
        public Zombie()
        {
            idle = new Texture2D[10];
            walk = new Texture2D[10];
            jump = new Texture2D[6];
            attack = new Texture2D[10];
            jumpA = new Texture2D[10];

            const int o = 4;
            rectanglei = new Rectangle(0, 0, 232 / o, 439 / o);
            rectanglew = new Rectangle(0, 0, 363 / o, 458 / o);
            hitbox = new Rectangle(0, 0, 60, 108);
            rectanglejump = new Rectangle(0, 0, 362 / o, 483 / o);
            rectangleA = new Rectangle(0, 0, 536 / o, 495 / o);
            rectanglejumpA = new Rectangle(0, 0, 504 / o, 522 / o);
            position = new Vector2(500, 300);

            elapsed = 0;
            idleI = 0;
            walkI = 0;
            jumpI = 0;
            jumpint = 0;
            attackI = 0;


            isJumping = false;
            isCrouching = false;


            for (int i = 1; i < 15; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D>("enemy/idle/Idle (" + i+")");
            }

            for (int i = 1; i < 11; i++)
            {
                walk[i] = Game1.ContentMgr.Load<Texture2D>("enemy/walk/Walk (" + i + ")");
            }


        }


        public override void Update(GameTime gameTime)
        {

        }
        public override void  Draw(SpriteBatch sbatch)
        {

        }


       
    }
}
