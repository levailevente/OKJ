using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
  public  class Karakter
    {
        readonly Texture2D[] idle;
        readonly Texture2D[] walk;
        readonly Texture2D[] jump;
        readonly Texture2D[] attack;
        readonly Texture2D[] jumpA;
        readonly Texture2D crouch;
        Rectangle rectanglei, rectanglew, hitbox, rectanglejump, rectangleA, rectanglejumpA;

        public Rectangle Hitbox => hitbox;
        double elapsed;
        int idleI, walkI, jumpI, attackI;
        int jumpint;
        int o;
        private enum Direction { Left,Right,Forward,Back};

        private Direction facing=Direction.Right;
        Vector2 mvmnt,prevPosition,position,velocity,lastMovement;
        public Vector2 Position => position;

        public bool IsCrouching => isCrouching;

        public bool IsDead => isDead;

        bool isJumping, isCrouching, isAttack;
        bool   isDead;

        public Karakter()
        {
            idle = new Texture2D[10];
            walk = new Texture2D[10];
            jump = new Texture2D[6];
            attack = new Texture2D[10];
            jumpA=new Texture2D[10];

            o = 4;
            rectanglei = new Rectangle(0, 0, 232/o, 439/o);
            rectanglew = new Rectangle(0, 0,363/o , 458/o);
            hitbox = new Rectangle(0, 0, 60, 108);            
            rectanglejump = new Rectangle(0, 0, 362/o, 483/o);
            rectangleA = new Rectangle(0, 0, 536 / o, 495 / o);
            rectanglejumpA = new Rectangle(0, 0, 504 / o, 522 / o);
            position =new Vector2(0,300);

             elapsed = 0;
             idleI = 0;
             walkI = 0;
             jumpI = 0;
             jumpint = 0;
             attackI = 0;


            isJumping = false;
            isCrouching = false;


            for (int i = 0; i < 10; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D>("bob/idle/Idle__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                walk[i] = Game1.ContentMgr.Load<Texture2D>("bob/walk/Run__00" + i);
            }

            for (int i = 0; i < 6; i++)
            {
                jump[i] = Game1.ContentMgr.Load<Texture2D>("bob/jump/jump__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                attack[i] = Game1.ContentMgr.Load<Texture2D>("bob/attack/Attack__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                jumpA[i] = Game1.ContentMgr.Load<Texture2D>("bob/jumpAttack/Jump_Attack__00" + i);
            }

            crouch = Game1.ContentMgr.Load<Texture2D>("bob/crouch");

        }
        
        
        public void Update(GameTime gameTime)
        {
            isDead = false;
            elapsed += 1;
            prevPosition = position;

            UpdateMovement();

            Gravity();

            UpdatePosition(gameTime);

            lastMovement = position - prevPosition;
            StopMoving();

        }


        public void Draw(SpriteBatch sbatch)
        {

            if (isJumping)
            {
                if (lastMovement.Y == 0)
                {
                    velocity.Y = 0;
                    jumpI = 0;
                    isJumping = false;
                }

                jumpint++;
                if (jumpint > 10)
                    velocity.Y = 0;
            }

            if (isAttack)
            {
                elapsed++;
                if (elapsed > 3 && attackI < 9) attackI++;

                rectangleA.Y -= 2;
                if (facing == Direction.Left)
                {
                    rectangleA.X -= 70;
                    sbatch.Draw(attack[attackI], rectangleA, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    sbatch.Draw(attack[attackI], rectangleA, Color.White);
                }

                if (attackI == 9)
                {
                    attackI = 0;
                    isAttack = false;
                }

            }

           else if (isJumping)
            {
                if (jumpI < 5) jumpI++;


                if (facing == Direction.Left)
                {
                    rectanglejump.X -= 15;
                    sbatch.Draw(jump[jumpI], rectanglejump, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);

                }
                else
                {
                    rectanglejump.X -= 15;
                    sbatch.Draw(jump[jumpI], rectanglejump, Color.White);
                }

               
            }
            



            else if (Game1.Newkey.IsKeyDown(Keys.Right) && Game1.Newkey.IsKeyUp(Keys.Left) && OnGround() && NextToWall(Hitbox) != "right")
            {
                if (isCrouching) sbatch.Draw(crouch, rectanglei, Color.White);
                else sbatch.Draw(walk[walkI], rectanglew, Color.White);

            }

            else if (Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right) && OnGround() && NextToWall(Hitbox) != "left")
            {
                rectanglew.X -= 35;

                if (!isCrouching) sbatch.Draw(walk[walkI], rectanglew, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                else sbatch.Draw(crouch, rectanglei, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);

            }

            else if (IsCrouching)
            {
                if (facing == Direction.Left)
                {

                    sbatch.Draw(crouch, rectanglei, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    sbatch.Draw(crouch, rectanglei, Color.White);
                }

            }

            else if (lastMovement.Y > 0)
            {
                if (facing == Direction.Left)
                {
                    rectanglejump.X -= 30;
                    sbatch.Draw(jump[5], rectanglejump, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    sbatch.Draw(jump[5], rectanglejump, Color.White);
                }

            }
           
            else 
            {
                if (facing == Direction.Left)
                {
                    
                    sbatch.Draw(idle[idleI], rectanglei, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                }

               else
               {
                   sbatch.Draw(idle[idleI], rectanglei, Color.White);
               }
                
            }
        }

        void UpdateMovement()
        {
           
            if (Game1.Newkey.IsKeyDown(Keys.Right) && Game1.Newkey.IsKeyUp(Keys.Left) && NextToWall(Hitbox)!="right" && !isAttack)
            {
                if (!isCrouching) {
                    mvmnt += new Vector2(2, 0);
                    if (elapsed > 3)
                    {
                        elapsed = 0;
                        walkI++;
                        if (walkI > 9) walkI = 0;
                    }
                }

                else mvmnt += new Vector2(1, 0);

                facing = Direction.Right;
                 
            }

            if (Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right) && NextToWall(Hitbox)!="left" && !isAttack)
            {

                if (!isCrouching)
                {
                    mvmnt += new Vector2(-2, 0);
                    if (elapsed > 3)
                    {
                        elapsed = 0;
                        walkI++;
                        if (walkI > 9) walkI = 0;
                    }
                }

                else mvmnt += new Vector2(-1, 0);
                facing = Direction.Left;
            }


            if (Game1.Newkey.IsKeyDown(Keys.Up) && Game1.Newkey.IsKeyUp(Keys.Down) && OnGround() && Game1.Prevkey.IsKeyUp((Keys.Up)))
            {
                isJumping = true;
                jumpint = 0;
                velocity.Y = -5;

            }

            

            if (Game1.Newkey.IsKeyDown(Keys.Space) && Game1.Prevkey.IsKeyUp(Keys.Space) && Game1.Newkey.IsKeyUp(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right))
            {
                isAttack = true;
            }


            if (Game1.Newkey.IsKeyDown(Keys.Down) && Game1.Newkey.IsKeyUp(Keys.Up) &&Game1.Prevkey.IsKeyUp(Keys.Down) && OnGround() && !isCrouching)
            {
                isCrouching = true;
                hitbox.Height -= 30;

            }

            if (Game1.Prevkey.IsKeyDown(Keys.Down) && Game1.Newkey.IsKeyUp(Keys.Down) && OnGround() && isCrouching)
            {
                hitbox.Height += 30;
                isCrouching = false;
            }

            else
            {
                if (!(elapsed > 6)) return;
                elapsed = 0;
                idleI++;
                if (idleI > 9) idleI = 0;

            }
        }

        void UpdatePosition(GameTime gametime)
        {
            position += mvmnt*(float)gametime.ElapsedGameTime.TotalMilliseconds/15;


            hitbox.X += (int)position.X;
            hitbox.Y += (int)position.Y;
   
            position = Game1.GenerateMap.CollisionV2(prevPosition, position, hitbox);
            if (position.X < 0) position.X = 0;
            if (position.Y > 1500)
            {
                isDead = true;
                position = new Vector2(0, 300);
            }    
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            rectanglei.X = (int)position.X;
            rectanglei.Y = (int)position.Y;
            rectanglew.X = (int)position.X;
            rectanglew.Y = (int)position.Y;
            rectanglejump.X = (int)position.X;
            rectanglejump.Y = (int)position.Y;
            rectangleA.X = (int)position.X;
            rectangleA.Y = (int)position.Y;
            rectanglejumpA.X = (int)position.X;
            rectanglejumpA.Y = (int)position.Y;

        }

        bool OnGround()
        {
            Rectangle ground = hitbox;
            ground.Offset(0,1);
            return Game1.GenerateMap.Collision(ground);

        }

        string NextToWall(Rectangle movingRectangle)
        {
            Rectangle wall = movingRectangle;
            Rectangle wallLeft = movingRectangle;
            wall.Offset(1,0);
            wallLeft.Offset(-1,0);
            if (Game1.GenerateMap.Collision(wall)) return "right";
            return Game1.GenerateMap.Collision(wallLeft) ? "left" : "no";
        }

        void Gravity()
        {
            if (!OnGround()) mvmnt += Vector2.UnitY * 2.5f;

            mvmnt.X *= 0.8f;
            mvmnt.Y *= 0.9f;

            mvmnt += velocity;
        }

        void StopMoving()
        {          
            if (lastMovement.X == 0) mvmnt *= Vector2.UnitY;
            if (lastMovement.Y == 0) mvmnt *= Vector2.UnitX;

        }
    }
}
