using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    class Zombie:Karakter
    {
       protected bool Right;
       protected Vector2 StartPos;
     

        public Zombie(Vector2 startPos)
        {

            walk = new Texture2D[10];
            death=new Texture2D[12];
            attack = new Texture2D[8];
            const int o = 5;

            Rectanglew = new Rectangle(0, 0, 430 / o, 519 / o);
            hitbox = new Rectangle(0, 0, 50, 100);
            rectangleA = new Rectangle(0, 0, 430 / o, 519 / o);
            RectangleD=new Rectangle(0, 0, 629 / o, 526 / o);

            this.StartPos=position = startPos;

            elapsed = 0;
            
            WalkI = 1;
            
            attackI = 0;

            isJumping = false;
            isCrouching = false;
            Right = true;

            

            for (int i = 0; i < 10; i++)
            {
                walk[i] = Game1.ContentMgr.Load<Texture2D>("enemy/walk/Walk (" + (i) + ")");
            }

            for (int i = 0; i < 12; i++)
            {
                death[i] = Game1.ContentMgr.Load<Texture2D>("enemy/death/Dead (" + (i) + ")");
            }

            for (int i = 0; i < 8; i++)
            {
                attack[i] = Game1.ContentMgr.Load<Texture2D>("enemy/attack/Attack (" + (i+1) + ")");
            }
        }

        protected override void UpdateMovement()
        {
            if (!IsDead)
            {

                if (hitbox.Intersects(Game1.Karakter.HitboxA))
                {
                    if (Game1.Karakter.isAttack && Game1.Karakter.attackI>1)
                    {
                        IsDead = true;
                        return;
                    }

                    if (!Game1.Karakter.invulnerable)
                    {
                        Game1.Karakter.health -= 1;
                        Game1.Karakter.invulnerable = true;
                    }


                    isAttack = true;
                    if (elapsed > 5)
                    {
                        elapsed = 0;
                        attackI++;
                        if (attackI > 7) attackI = 0;
                    }
                }

               else if (Game1.Karakter.RectangleW.X - hitbox.X >= 0 && Game1.Karakter.RectangleW.X - hitbox.X < 250 && Game1.Karakter.RectangleW.Y - hitbox.Y < 250 && Game1.Karakter.RectangleW.Y - hitbox.Y > -250)
                {
                    isAttack = false;
                    Right = true;
                    mvmnt += new Vector2(0.7f, 0);
                    if (elapsed > 4)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }
                    if (NextToWall(hitbox) == "right")
                    {
                        mvmnt -= new Vector2(0, 5);
                    }


                }

                else if (hitbox.X - Game1.Karakter.RectangleW.X < 250 && hitbox.X - Game1.Karakter.RectangleW.X >= 0 && Game1.Karakter.RectangleW.Y - hitbox.Y < 250 && Game1.Karakter.RectangleW.Y - hitbox.Y > -250)
                {
                    isAttack = false;
                    Right = false;
                    mvmnt += new Vector2(-0.7f, 0);
                    if (elapsed > 4)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }

                    if (NextToWall(hitbox) == "left")
                    {
                        mvmnt -= new Vector2(0, 5);
                    }


                }

                else if (Right)
                {
                    isAttack = false;
                    mvmnt += new Vector2(0.2f, 0);
                    if (elapsed > 6)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }

                    if (StartPos.X - position.X < -100)
                    { 
                        Right = false;
                    }

                    if (NextToWall(Hitbox) == "right") mvmnt -= new Vector2(0, 5);
                }
                else if (!Right)
                {
                    isAttack = false;
                    mvmnt += new Vector2(-0.2f, 0);
                    if (elapsed > 6)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }

                    if (StartPos.X - position.X > 100) Right = true;

                    if (NextToWall(Hitbox) == "left") mvmnt -= new Vector2(0, 5);
                }

              
            }

            else
            {
                if (elapsed > 5 && deadI!=-1)
                {
                    elapsed = 0;
                    deadI++;
                    if (deadI > 11) deadI = -1;
                }

              //  if (deadI==-1) if (Game1.Enemies.Count>1) Game1.Enemies.Remove(this);
            }

        }
        public override void  Draw(SpriteBatch sbatch)
        {

            if (IsDead)
            {
                if (Right)
                {
                    if (deadI != -1) sbatch.Draw(death[deadI], RectangleD, Color.White);
                }
                else if (!Right)
                {
                    if (deadI != -1)
                    {
                        RectangleD.X -= 25;
                        sbatch.Draw(death[deadI], RectangleD, null, Color.White, 0, new Vector2(0, 0),
          SpriteEffects.FlipHorizontally, 0);
                    }
                }

                else if (deadI == -1)
                {

                }
            }

            else if (isAttack)
            {
                if (!Right)
                {
                    Rectanglew.X -= 35;
                    sbatch.Draw(attack[attackI], rectangleA, null, Color.White, 0, new Vector2(0, 0),
               SpriteEffects.FlipHorizontally, 0);
                }
                else sbatch.Draw(attack[attackI], rectangleA, Color.White);
            }

          else  if (Right)
                sbatch.Draw(walk[WalkI], Rectanglew, Color.White);

           else if (!Right)
            {
                Rectanglew.X -= 35;
                sbatch.Draw(walk[WalkI], Rectanglew, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
      
        }

        protected override void UpdatePosition(GameTime gametime)
        {
            position += mvmnt * (float)gametime.ElapsedGameTime.TotalMilliseconds / 15;

            hitbox.X += (int)position.X;
            hitbox.Y += (int)position.Y;

            position = Game1.GenerateMap.CollisionV2(prevPosition, position, hitbox);
            if (position.X < 0) position.X = 0;
            if (position.Y > 1500)
            {
                IsDead = true;
            }
            hitbox.Location = new Point((int)position.X, (int)position.Y);
             hitbox.Location = new Point((int)position.X, (int)position.Y);
            Rectanglew.Location = new Point((int)position.X, (int)position.Y);
            rectangleA.Location = new Point((int)position.X-10, (int)position.Y);
            if (IsDead) RectangleD.Location = new Point((int)position.X, (int)position.Y+10);

        }

        protected string NextToCliff(Rectangle movingRectangle)
        {
            Rectangle cliff, cliffLeft;
            if (OnGround(movingRectangle))
            {
                cliff = cliffLeft = movingRectangle;
                cliff.Offset(2, 2);
                cliffLeft.Offset(-2, 1);
                if (!Game1.GenerateMap.Collision(cliff)) return "right";
                return !Game1.GenerateMap.Collision(cliffLeft) ? "left" : "no";
            }

                return "inair";
            
        }
   


    }
}
