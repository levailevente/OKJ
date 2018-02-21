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
        Rectangle rectanglei;
        Rectangle rectanglew;
        Rectangle hitbox ;
        Rectangle rectanglejump;
        public Rectangle Hitbox => hitbox;
        double elapsed;
        int idleI, walkI, jumpI;
        int jumpint;
        private enum Direction { Left,Right,Forward,Back};

        private Direction facing=Direction.Right;
        Vector2 mvmnt,prevPosition,position,velocity,lastMovement;
        public Vector2 Position => position;

        bool isJumping;
        public bool   isDead;

        public Karakter()
        {
            idle = new Texture2D[9];
            walk = new Texture2D[9];
            jump = new Texture2D[6];
           
            rectanglei = new Rectangle(0, 0, 58, 110);
            rectanglew = new Rectangle(0, 0, 91, 115);
            hitbox = new Rectangle(0, 0, 60, 110);
            
            rectanglejump = new Rectangle(0, 0, 91, 121);
            position=new Vector2(0,300);

             elapsed = 0;
             idleI = 0;
             walkI = 0;
             jumpI = 0;
             jumpint = 0;

            isJumping = false;

        }
        
        public void LoadKarakter()
        {
            for (int i = 0; i <9; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D> ( "bob/idle/Idle__00" + i );
            }

            for (int i = 0; i <9; i++)
            {
                walk[i] = Game1.ContentMgr.Load<Texture2D>("bob/walk/Run__00" + i);
            }

            for (int i = 0; i < 6; i++)
            {
                jump[i] = Game1.ContentMgr.Load<Texture2D>("bob/jump/jump__00" + i);
            }

        }
        
        public void Update(GameTime gameTime)
        {
            isDead = false;
            elapsed += 1;
            prevPosition = position;

            UpdateMovement();

            Gravity();

            UpdatePosition();

            lastMovement = position - prevPosition;
            StopMoving();

        }


        public void Draw(SpriteBatch sbatch)
        {
             if (isJumping)
             {
                 
                if ( jumpI < 5) jumpI++;


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

                if (lastMovement.Y == 0)
                {
                    velocity.Y = 0;
                    jumpI = 0;
                    isJumping = false;
                }

                 jumpint += 1;
                 if (jumpint > 10)
                     velocity.Y = 0;
            }
           else if (Game1.Newkey.IsKeyDown(Keys.Right) && Game1.Newkey.IsKeyUp(Keys.Left) && OnGround() && NextToWall(Hitbox)!="right")
            {
                
                sbatch.Draw(walk[walkI], rectanglew, Color.White);
            }

            else if (Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right) && OnGround() && NextToWall(Hitbox)!="left")
            {
                rectanglew.X -= 35;
                
                sbatch.Draw(walk[walkI],rectanglew,null,Color.White,0,new Vector2(0,0),SpriteEffects.FlipHorizontally,0);

            }

            else if (Game1.Newkey.IsKeyDown(Keys.Down) && Game1.Newkey.IsKeyUp(Keys.Up))
            {
                
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
           
            if (Game1.Newkey.IsKeyDown(Keys.Right) && Game1.Newkey.IsKeyUp(Keys.Left) && NextToWall(Hitbox)!="right")
            {
                
                mvmnt += new Vector2(2, 0);
                if (elapsed > 3)
                {
                    elapsed = 0;
                    walkI++;
                    if (walkI > 8) walkI = 0;
                }

                facing = Direction.Right;
            }


            if (Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right) && NextToWall(Hitbox)!="left")
            {

                
                mvmnt += new Vector2(-2, 0);
                if (elapsed > 3)
                {
                    elapsed = 0;
                    walkI++;
                    if (walkI > 8) walkI = 0;
                }
                facing = Direction.Left;
            }


            if ( Game1.Newkey.IsKeyDown(Keys.Up) && Game1.Newkey.IsKeyUp(Keys.Down) && OnGround() && Game1.Prevkey.IsKeyUp((Keys.Up)))
            {

                isJumping = true;
                jumpint = 0;

                velocity.Y = -5;

            }


            if (Game1.Newkey.IsKeyDown(Keys.Down) && Game1.Newkey.IsKeyUp(Keys.Up) && rectanglei.Y + rectanglei.Height <= Game1.Sheight)
            {

                facing = Direction.Back;
            }


            else
            {

                if (elapsed > 6)
                {
                    elapsed = 0;
                    idleI++;
                    if (idleI > 8) idleI = 0;

                }
      
            }
        }

        void UpdatePosition()
        {
            position += mvmnt;


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
            if (Game1.GenerateMap.Collision(wallLeft)) return "left";
            else return "no";
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

        public int getMatrixPosX(int i, int length)
        {
            int position = Game1.Swidth / Game1.TileSize + i;
            if (position < 0) return 0;
            if (position > length) return length;
            else return position;

        }

        public int getMatrixPosY(int i, int length)
        {
            int position = Game1.Sheight / Game1.TileSize + i;
            if (position < 0) return 0;
            if (position > length) return length;
            else return position;

        }
    }
}
