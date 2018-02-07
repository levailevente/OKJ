using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
  public  class karakter
    {
        readonly Texture2D[] idle= new Texture2D[9];
        readonly Texture2D[] walk=new Texture2D[9];
        readonly Texture2D[] jump = new Texture2D[9];
        Rectangle rectanglei= new Rectangle(0, 0, 58, 110);
        Rectangle rectanglew = new Rectangle(0, 0, 91, 115);
        Rectangle hitbox = new Rectangle(0, 0, 60, 105);
        Rectangle rectanglejump = new Rectangle(0, 0, 91, 121);
        public Rectangle Hitbox => hitbox;
        double elapsed=0;
        int idleI = 0;
        int walkI = 0;
        int jumpI = 0;
        int jumpint = 0;
        private enum Direction { Left,Right,Forward,Back};

        private Direction facing=Direction.Right;
        Vector2 mvmnt,prevPosition,position,velocity;
        
       



        public karakter()
        {
            idle = new Texture2D[9];
            walk = new Texture2D[9];
            jump = new Texture2D[9];
            rectanglei = new Rectangle(0, 0, 58, 110);
            rectanglew = new Rectangle(0, 0, 91, 115);
            hitbox = new Rectangle(0, 0, 60, 105);
            rectanglejump = new Rectangle(0, 0, 91, 121);
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

            for (int i = 0; i < 9; i++)
            {
                jump[i] = Game1.ContentMgr.Load<Texture2D>("bob/jump/jump__00" + i);
            }

        }
        
        public void Update(GameTime gameTime)
        {
        
            elapsed += 1;


            prevPosition = position;

            

                if (Game1.Newkey.IsKeyDown(Keys.Right) && Game1.Newkey.IsKeyUp(Keys.Left))
                {

                    mvmnt += new Vector2(2, 0);
                    if ( elapsed > 3)
                    {
                        elapsed = 0;
                        walkI++;
                        if (walkI > 8) walkI = 0;
                    }

                    facing = Direction.Right;
                }

                if (Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right))
                {


                    mvmnt += new Vector2(-2, 0);
                    if (  elapsed > 3)
                    {
                        elapsed = 0;
                        walkI++;
                        if (walkI > 8) walkI = 0;
                    }
                    facing = Direction.Left;
                }

            
            if (Game1.Newkey.IsKeyDown(Keys.Up) && OnGround())
            {
                jumpint = 0;
      
                    velocity.Y = -5;

                if (elapsed > 0)
                {
                    elapsed = 0;
                    jumpI++;
                    if (jumpI > 8) jumpI = 0;
                }
                facing = Direction.Forward;
                }

            
            jumpint += 1;
            if (jumpint>10)
                velocity.Y = 0;
          


            if (Game1.Newkey.IsKeyDown(Keys.Down) && rectanglei.Y + rectanglei.Height <= Game1.Sheight)
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


                if (Game1.Newkey.IsKeyDown(Keys.Down) && Game1.Newkey.IsKeyDown(Keys.Up) || Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyDown(Keys.Right))
                {
                    if (elapsed > 6)
                    {
                        elapsed = 0;
                        idleI++;
                        if (idleI > 8) idleI = 0;

                    }
                }

            }


            if (!OnGround()) mvmnt += Vector2.UnitY * 2.5f;


            mvmnt.X *= 0.8f;
            mvmnt.Y *= 0.9f;

           
            mvmnt += velocity;
           
            position += mvmnt;
            

            hitbox.X += (int) position.X;
            hitbox.Y += (int) position.Y;


                position = Game1.Map.CollisionV2(prevPosition, position, hitbox);
            if (position.X < 0) position.X = 0;

            hitbox.X = (int) position.X;
            hitbox.Y = (int) position.Y;
            rectanglei.X = (int) position.X;
            rectanglei.Y = (int) position.Y;
            rectanglew.X = (int) position.X;
            rectanglew.Y = (int) position.Y;
            rectanglejump.X = (int) position.X;
            rectanglejump.Y = (int) position.Y;




            Vector2 lastMovement = position - prevPosition;
                if (lastMovement.X == 0) mvmnt *= Vector2.UnitY;
                if (lastMovement.Y == 0) mvmnt *= Vector2.UnitX;


            


        }


        public void Draw(SpriteBatch sbatch)
        {
            if (Game1.Newkey.IsKeyDown(Keys.Right) && Game1.Newkey.IsKeyUp(Keys.Left) && OnGround())
            {
                
                sbatch.Draw(walk[walkI], rectanglew, Color.White);
            }

            else if (Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right) && OnGround() )
            {
                
                sbatch.Draw(walk[walkI],rectanglew,null,Color.White,0,new Vector2(0,0),SpriteEffects.FlipHorizontally,0);

            }

            else if (Game1.Newkey.IsKeyDown(Keys.Up) && rectanglei.Y >= 0)
            {
                    if (facing == Direction.Left)
                        sbatch.Draw(jump[jumpI], rectanglejump, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                else sbatch.Draw(jump[jumpI], rectanglejump, Color.White);
            }

            else if (Game1.Newkey.IsKeyDown(Keys.Down))
            {

            }

            else 
            {
               if (facing==Direction.Left ) { sbatch.Draw(idle[idleI], rectanglei, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0); }
                
               else
                { sbatch.Draw(idle[idleI], rectanglei, Color.White); }
                
            }


        }

        bool OnGround()
        {
            Rectangle ground = hitbox;
            ground.Offset(0,1);
            return Game1.Map.Collision(ground);

        }
    }
}
