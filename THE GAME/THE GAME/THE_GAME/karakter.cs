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
        Rectangle rectanglei= new Rectangle(100, 100, 77, 146);
        Rectangle rectanglew = new Rectangle(100, 100, 121, 153);
        Rectangle hitbox = new Rectangle(100, 100, 75, 140);
        public Rectangle Hitbox
        {
            get
            {
                return hitbox;
            }
        }
        double elapsed=0;
        int idleI = 0;
        int walkI = 0;
        public enum direction { left,right,forward,back};
        public direction facing=direction.right;
        Vector2 mvmnt,prevPosition,position;

      

        public karakter()
        {
            
        }
        
        public void loadKarakter()
        {
            for (int i = 0; i <9; i++)
            {
                idle[i] = Game1.contentMgr.Load<Texture2D> ( "bob/idle/Idle__00" + i );
            }

            for (int i = 0; i <9; i++)
            {
                walk[i] = Game1.contentMgr.Load<Texture2D>("bob/walk/Run__00" + i);
            }

        }
        
        public void update()
        {
        
            elapsed += 1;

           

            prevPosition = new Vector2(hitbox.X, hitbox.Y);

        
            
                if (Game1.newkey.IsKeyDown(Keys.Right) && Game1.newkey.IsKeyUp(Keys.Left))
                {

                    mvmnt += new Vector2(1, 0);
                    if (elapsed > 3)
                    {
                        elapsed = 0;
                        walkI++;
                        if (walkI > 8) walkI = 0;
                }
                    
                    facing = direction.right;
                }

                if (Game1.newkey.IsKeyDown(Keys.Left) && Game1.newkey.IsKeyUp(Keys.Right) && rectanglei.X >= 0)
                {


                    mvmnt += new Vector2(-1, 0);
                    if (elapsed > 3)
                    {
                        elapsed = 0;
                        walkI++;
                        if (walkI > 8) walkI = 0;
                    }
                    facing = direction.left;
                }

                if (Game1.newkey.IsKeyDown(Keys.Up) && rectanglei.Y >= 0)
                {


                    facing = direction.forward;
                }
                if (Game1.newkey.IsKeyDown(Keys.Down) && rectanglei.Y + rectanglei.Height <= Game1.sheight)
                {

                    facing = direction.back;
                }


                else
                {

                    if (elapsed > 6)
                    {
                        elapsed = 0;
                        idleI++;
                        if (idleI > 8) idleI = 0;

                    }


                    if (Game1.newkey.IsKeyDown(Keys.Down) && Game1.newkey.IsKeyDown(Keys.Up) || Game1.newkey.IsKeyDown(Keys.Left) && Game1.newkey.IsKeyDown(Keys.Right))
                    {
                        if (elapsed > 6)
                        {
                            elapsed = 0;
                            idleI++;
                            if (idleI > 8) idleI = 0;

                        }
                    }



                 if (!Game1.map.Collision(hitbox)) mvmnt += Vector2.UnitY * 1.5f; 

                mvmnt -= mvmnt * new Vector2(.1f, .1f);

                hitbox.X += (int)mvmnt.X;
                hitbox.Y += (int)mvmnt.Y;
               

                position = new Vector2(hitbox.X, hitbox.Y);
               position = Game1.map.CollisionV2(prevPosition, position, hitbox);

                hitbox.X = (int)position.X;
                hitbox.Y = (int)position.Y;
                rectanglei.X= (int)position.X;
                rectanglei.Y = (int)position.Y;
                rectanglew.X= (int)position.X;
                rectanglew.Y= (int)position.Y;  

               
             


            }


        }


        public void Draw(SpriteBatch sbatch)
        {
            if (Game1.newkey.IsKeyDown(Keys.Right) && Game1.newkey.IsKeyUp(Keys.Left))
            {
                
                sbatch.Draw(walk[walkI], rectanglew, Color.White);
            }

            else if (Game1.newkey.IsKeyDown(Keys.Left) && Game1.newkey.IsKeyUp(Keys.Right) )
            {
                
                sbatch.Draw(walk[walkI],rectanglew,null,Color.White,0,new Vector2(0,0),SpriteEffects.FlipHorizontally,0);

            }

            else if (Game1.newkey.IsKeyDown(Keys.Up) && rectanglei.Y >= 0)
            {

            }

            else if (Game1.newkey.IsKeyDown(Keys.Down))
            {

            }

            else 
            {
               if (facing==direction.left ) { sbatch.Draw(idle[idleI], rectanglei, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0); }
                
               else
                { sbatch.Draw(idle[idleI], rectanglei, Color.White); }
                
            }

      
           


        }
    }
}
