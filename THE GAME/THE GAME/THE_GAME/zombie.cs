using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    class Zombie:Karakter
    {
        bool right;
        Vector2 startPos;
        public Zombie(Vector2 startPos)
        {

            walk = new Texture2D[10];
          
            attack = new Texture2D[10];


            const int o = 4;

            rectanglew = new Rectangle(0, 0, 363 / o, 458 / o);
            hitbox = new Rectangle(0, 0, 60, 108);

            rectangleA = new Rectangle(0, 0, 536 / o, 495 / o);

            this.startPos=position = startPos;

            elapsed = 0;

            walkI = 1;
            
            attackI = 0;


            isJumping = false;
            isCrouching = false;
            right = true;



            for (int i = 0; i < 10; i++)
            {
                walk[i] = Game1.ContentMgr.Load<Texture2D>("enemy/walk/Walk (" + (i) + ")");
            }


        }


        protected override void UpdateMovement()
        {
            if (right)
            {
                mvmnt += new Vector2(1, 0);
                if (elapsed > 3)
                {
                    elapsed = 0;
                    walkI++;
                    if (walkI > 8) walkI = 0;
                }
                if (position.X - startPos.X > 100 || NextToWall(Hitbox)=="right" ) right = false;
            }
            else
            {
                mvmnt += new Vector2(-1, 0);
                if (elapsed > 3)
                {
                    elapsed = 0;
                    walkI++;
                    if (walkI > 8) walkI = 0;
                }
                if (position.X - startPos.X < 100 || NextToWall(Hitbox) == "left") right = true;
            }
        }
        public override void  Draw(SpriteBatch sbatch)
        {
            if (right)
                sbatch.Draw(walk[walkI], rectanglew, Color.White);

            else
            {
                rectanglew.X -= 35;
                sbatch.Draw(walk[walkI], rectanglew, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
        }


       
    }
}
