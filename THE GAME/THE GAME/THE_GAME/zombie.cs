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
            hitbox = new Rectangle(0, 0, 30, 100);
            rectangleA = new Rectangle(0, 0, 430 / o, 519 / o);
            RectangleD=new Rectangle(0, 0, 629 / o, 526 / o);

            this.StartPos = position = new Vector2(startPos.X, startPos.Y - 50);

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
                    if (Game1.Karakter.isAttack && Game1.Karakter.attackI > 2 && Game1.Karakter.attackI < 5)
                    {
                        IsDead = true;
                        return;
                    }


                    if (hitbox.Intersects(Game1.Karakter.Hitbox))
                    {
                        if (!Game1.Karakter.invulnerable)
                        {
                            Game1.Karakter.Health -= 1;
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
                }

                else if (Game1.Karakter.RectangleW.X - hitbox.X >= 0 && Game1.Karakter.RectangleW.X - hitbox.X < 300 && Game1.Karakter.RectangleW.Y - hitbox.Y < 300 && Game1.Karakter.RectangleW.Y - hitbox.Y > -400)
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
                    if ((NextToWall(hitbox) == "right" || NextToCliff(hitbox) == "right") && Game1.Karakter.Hitbox.Y + 100 > hitbox.Y)
                    {
                        mvmnt -= new Vector2(0, 5);
                    }
                    else if ((NextToCliff(hitbox) == "right") && Game1.Karakter.Hitbox.Y < hitbox.Y)
                    {
                        mvmnt += new Vector2(-0.7f, 0);
                    }

                }

                else if (hitbox.X - Game1.Karakter.RectangleW.X < 300 && hitbox.X - Game1.Karakter.RectangleW.X >= 0 && Game1.Karakter.RectangleW.Y - hitbox.Y < 300 && Game1.Karakter.RectangleW.Y - hitbox.Y > -400)
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

                    if ((NextToWall(hitbox) == "left" || NextToCliff(hitbox) == "left") && Game1.Karakter.Hitbox.Y + 100 > hitbox.Y)
                    {
                        mvmnt -= new Vector2(0, 5);
                    }
                    else if ((NextToCliff(hitbox) == "left") && Game1.Karakter.Hitbox.Y < hitbox.Y)
                    {
                        mvmnt += new Vector2(0.7f, 0);
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

                    if (NextToWall(Hitbox) == "right" || NextToCliff(Hitbox) == "right")
                    {
                        if (StartPos.X - position.X > 100)
                        {
                            mvmnt -= new Vector2(0, 5);
                        }
                        else Right = false;
                    }
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

                    if (NextToWall(Hitbox) == "left" || NextToCliff(Hitbox) == "left")
                    {
                        if (StartPos.X - position.X < -100) mvmnt -= new Vector2(0, 5);
                        else Right = true;
                    }
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
            if (position.Y > 2000)
            {
                IsDead = true;
            }

            hitbox.Location = new Point((int)position.X, (int)position.Y);
            Rectanglew.Location = new Point((int)position.X, (int)position.Y);
            rectangleA.Location = new Point((int)position.X-10, (int)position.Y);
            if (IsDead) RectangleD.Location = new Point((int)position.X, (int)position.Y+10);

        }

        protected string NextToCliff(Rectangle movingRectangle)
        {
            if (OnGround(movingRectangle))
            {
                Rectangle cliffLeft;
                Rectangle cliff = cliffLeft = movingRectangle;
                cliff.Offset(30, 2);
                cliffLeft.Offset(-30, 2);
                if (!Game1.GenerateMap.Collision(cliff)) return "right";
                return !Game1.GenerateMap.Collision(cliffLeft) ? "left" : "no";
            }

                return "inair";         
        }
   


    }
}
