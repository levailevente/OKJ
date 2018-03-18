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
            rectangleA = new Rectangle(0, 100, 430 / o, 519 / o);
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
                 if (hitbox.X ==Game1.Karakter.Hitbox.X && Game1.Karakter.RectangleW.Y - hitbox.Y < 72 &&
                         Game1.Karakter.RectangleW.Y - hitbox.Y > -72)
                 {
                      
                 }
               else if (Game1.Karakter.RectangleW.X - hitbox.X   >= 0 &&  Game1.Karakter.RectangleW.X- hitbox.X < 150 && Game1.Karakter.RectangleW.Y-hitbox.Y<72 && Game1.Karakter.RectangleW.Y - hitbox.Y > -72)
                 {
                     Right = true;
                    mvmnt += new Vector2(0.5f, 0);
                    if (elapsed > 4)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }
                }

                else if (hitbox.X-Game1.Karakter.RectangleW.X  < 150 && hitbox.X - Game1.Karakter.RectangleW.X >=0 && Game1.Karakter.RectangleW.Y - hitbox.Y < 72 && Game1.Karakter.RectangleW.Y - hitbox.Y > -72)
                 {
                     Right = false;
                    mvmnt += new Vector2(-0.5f, 0);
                    if (elapsed > 4)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }
                }

                
               else if (Right)
                {
                    mvmnt += new Vector2(0.5f, 0);
                    if (elapsed > 4)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }

                    if (StartPos.X - position.X < -100 || NextToWall(Hitbox) == "right") Right = false;
                }
                else if (!Right)
                {
                    mvmnt += new Vector2(-0.5f, 0);
                    if (elapsed > 4)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }

                    if (StartPos.X - position.X > 100 || NextToWall(Hitbox) == "left") Right = true;
                }



                if (hitbox.Intersects(Game1.Karakter.Hitbox))
                {
                    if (Game1.Karakter.isAttack) isDead = true;

                    isAttack = true;

                }

            }



            else
            {
                if (elapsed > 4 && deadI!=-1)
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

            if (isDead)
            {
                if (Facing == Direction.Right)
                {
                    if (deadI != -1) sbatch.Draw(death[deadI], RectangleD, Color.White);
                }
                else if (Facing == Direction.Left)
                {
                    if (deadI!=-1 )sbatch.Draw(death[deadI], RectangleD, null, Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);
                }

                else if (deadI == -1)
                {

                }
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
                isDead = true;
                position = new Vector2(0, 300);
            }
           hitbox.Location = new Point((int)position.X, (int)position.Y);

            Rectanglew.Location = new Point((int)position.X, (int)position.Y);
            rectangleA.Location = new Point((int)position.X, (int)position.Y);
            if (isDead) RectangleD.Location = new Point((int)position.X, (int)position.Y+10);

        }




    }
}
