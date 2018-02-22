using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace THE_GAME
{
    public  class GenerateMap
    {

        Tiles[,] Tiles2 { get; }
        Tiles[,] Objects2 { get; }
        int width, height;
        int x, y;
        
        public GenerateMap(int[,] map, int[,] objects,int size)
        {
            x = map.GetLength(0);
            y = map.GetLength(1);
            Tiles2 = new Tiles[x, y];
            Objects2 = new Tiles[x, y];
            for (int i = 0; i < map.GetLength(0); i++)
            {

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int n = map[i, j];
                    int m = objects[i, j];

                    if (n > 0)
                        Tiles2[i,j]=new Tiles(n, new Rectangle(j * size, i * size, size, size), true, false);
                    else if (n < 0) Tiles2[i, j] = new Tiles(n*-1, new Rectangle(j * size, i * size, size, size), false, false);

                    bool blocked = m < 0;
                    if (blocked) m *= -1;
                    if (m > 0)

                        switch (m)
                        {

                            
                            default:Objects2[i, j] = new Tiles(m, new Rectangle(j * size, i * size, size, size), blocked, true);
                                break;
                        }


                    width = (i + 1) * size;
                    height = (j + 1) * size;
                }


            }

        }

        public void Draw(SpriteBatch spritebatch, Karakter karakter)
        {
            int ystart = (karakter.Hitbox.X-Game1.Swidth)/Game1.TileSize;
            if (ystart < 0) ystart = 0;
            int yend = (karakter.Hitbox.X + Game1.Swidth ) / Game1.TileSize;
            if (yend > y) yend = y;

            int xstart = (karakter.Hitbox.Y - Game1.Sheight) / Game1.TileSize;
            if (xstart < 0) xstart = 0;
            int xend = (karakter.Hitbox.Y + Game1.Sheight) / Game1.TileSize;
            if (xend > x) xend = x;
            for (int i = xstart; i <xend; i++)
            {
                for (int j = ystart; j < yend; j++)
                {
                  if (Tiles2[i,j]!=null)
                    Tiles2[i, j].Draw(spritebatch);

                    if (Objects2[i, j] != null)
                        Objects2[i, j].Draw(spritebatch);
                }
            }

        }


        public bool Collision (Rectangle movingRectangle)
        {
            int xstart = movingRectangle.Y  / Game1.TileSize-2;
            if (xstart < 0) xstart = 0;
            int xend = movingRectangle.Y  / Game1.TileSize+4;
            if (xend > x) xend = x;

            int ystart = movingRectangle.X  / Game1.TileSize-2;
            if (ystart < 0) ystart = 0;
            int yend = movingRectangle.X  / Game1.TileSize+4;
            if (yend > y) yend = y;


            for (int i =xstart; i < xend; i++)
            {
                
                for (int j = ystart; j <yend ; j++)
                {
                  
                    if (Tiles2[i, j] != null && (Tiles2[i, j].tile == 14 || Tiles2[i, j].tile == 15 || Tiles2[i, j].tile == 16))
                    {
                        Rectangle hitbox = new Rectangle(movingRectangle.X, movingRectangle.Y+30, 72,78);
                       
                        if (Tiles2[i, j] != null && Tiles2[i, j].Blocked && Tiles2[i, j].Rectangle.Intersects(hitbox))
                        {
                            return true;
                        }
                       
                    }
                   else if (Tiles2[i, j] != null && Tiles2[i, j].Blocked && Tiles2[i, j].Rectangle.Intersects(movingRectangle))
                    {
                        return true;
                    }

                    if (Objects2[i, j] != null && Objects2[i, j].Blocked && Objects2[i, j].Rectangle.Intersects(movingRectangle))
                    {
                        return true;
                    }
                }
            }
   
            return false;

        }

        public Vector2 CollisionV2 (Vector2 ogPosition, Vector2 destination, Rectangle hitbox)
        {

            Vector2 movement = destination - ogPosition;

            Vector2 furthestLocation = ogPosition;

            int breaks = (int)(movement.Length() * 2) + 1;

            Vector2 oneStep = movement / breaks;

            

            for (int i = 1 ; i <= breaks; i++)
            {
                Vector2 newPosition = ogPosition + oneStep * i;
                Rectangle newHitbox = new Rectangle((int)newPosition.X, (int)newPosition.Y, hitbox.Width, hitbox.Height);
                if(!Collision(newHitbox)) { furthestLocation = newPosition; }
                else
                {
                    bool isDiagonalMove = movement.X != 0 && movement.Y != 0;
                    if (isDiagonalMove)
                    {
                        int stepsLeft = breaks - (i - 1);

                        Vector2 horizontalM = oneStep.X * Vector2.UnitX * stepsLeft;
                        Vector2 horizonPosition = furthestLocation + horizontalM;
                        furthestLocation = CollisionV2(furthestLocation, horizonPosition, hitbox);

                        Vector2 verticalM = oneStep.Y * Vector2.UnitY * stepsLeft;
                        Vector2 verticalPosition = furthestLocation + verticalM;
                        furthestLocation = CollisionV2(furthestLocation, verticalPosition, hitbox);


                    }
                    break;
                }
            }
            return furthestLocation;
        }

    }

   
}
