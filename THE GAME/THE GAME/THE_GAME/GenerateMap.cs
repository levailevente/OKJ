using System;
using System.Collections.Generic;
using System.IO;
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
       public Tiles[,] Objects2 { get; }
        int width, height;
        readonly int x;
        readonly int y;
        int id;
        public GenerateMap(int id, int size)
        {
            string tilestring = Game1.db.GetTiles(id);
            string objectstring = Game1.db.GetObjects(id);


            int[,] map = GetMatrix(tilestring);
            int[,] objects = GetMatrix(objectstring);

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


                    bool blocked = n < -1;
                    if (blocked) n *= -1;

                   if (n>0)
                       
                        Tiles2[i,j]=new Tiles(n, new Rectangle(j * size, i * size, size, size), !blocked, false);
                   
                    bool blockedO = m < -1;
                    if (blockedO) m *= -1;
                    if (m > 0)

                        switch (m)
                        {
                            case 6:
                                Objects2[i, j] = new Tiles(m, new Rectangle(j * size-20, i * size+15, (int)(182/1.5), (int)(90/1.5)), blockedO,
                                    true); break;
                            case 7:
                                Objects2[i, j] = new Tiles(m, new Rectangle(j * size - 20, i * size + 32, (int)(100 / 1.5), (int)(64 / 1.5)), blockedO,
                                    true); break;
                            case 9:
                                Objects2[i, j] = new Tiles(m, new Rectangle(j * size - 20, i * size+20, (int)(132 /1.4), (int)(74 / 1.4)), blockedO,
                                    true); break;
                            case 10:
                                Objects2[i, j] = new Tiles(m, new Rectangle(j * size - 100, i * size - 97, (int)(286/1.4 ), (int)(239 / 1.4)), blockedO,
                                    true); break;
                            case 11:
                                Objects2[i, j] = new Tiles(m, new Rectangle(j * size - 20, i * size + 40, (int)(102 / 1.5), (int)(50 / 1.5)), blockedO,
                                    true); break;
                            case 13:
                                Objects2[i, j] = new Tiles(m, new Rectangle(j * size + 15, i * size + 36, (int)(54 / 1.5), (int)(55 / 1.5)), blockedO,
                                    true); break;
                            case 14:
                                Objects2[i, j] = new Tiles(m, new Rectangle(j * size + 45, i * size + 22, (int)(53 / 1.5), (int)(76 / 1.5)), blockedO,
                                    true); break;
                            case 15:
                                Game1.Enemies.Add(new Zombie(new Vector2(j * size, i * size)));
                                break;
                            case 16:
                                Game1.Enemies.Add(new ZombieGirl(new Vector2(j * size, i * size)));
                                break;                          
                            case 17:
                                Game1.Spikes.Add(new Spike(new Vector2(j * size, i * size)));
                                break;
                            case 18:
                                Game1.Items.Add(new Items("heart", new Vector2(j * size, i * size+30)));
                                break;
                            case 19:
                                Game1.Items.Add(new Items("boots", new Vector2(j * size, i * size)));
                                break;
                            case 20:
                                Game1.Items.Add(new Items("jump", new Vector2(j * size, i * size)));
                                break;
                            case 21:
                                Tiles2[i, j] = new MovingTile(14, new Rectangle(j * size, i * size, size, size), true, true, false, true);
                                break;

                            default:Objects2[i, j] = new Tiles(m, new Rectangle(j * size, i * size, size, size), blockedO, true);  break;
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
            int yend = (karakter.Hitbox.X + Game1.Swidth ) / Game1.TileSize+10;
            if (yend > y) yend = y;

            /*int xstart = (karakter.Hitbox.Y - Game1.Sheight) / Game1.TileSize;
            if (xstart < 0) xstart = 0;
            int xend = ((karakter.Hitbox.Y + Game1.Sheight) / Game1.TileSize);
            if (xend > x) xend = x; */

            for (int i = 0; i <x; i++)
            {
                for (int j = ystart; j < yend; j++)
                {
                  if (Tiles2[i,j]!=null)
                    Tiles2[i, j].Draw(spritebatch);

                    if (Objects2[i, j] != null)
                    {
                        Objects2[i, j].Draw(spritebatch);
                    }
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
                   
                    if (Tiles2[i, j] != null && (Tiles2[i, j].Tile == 14 || Tiles2[i, j].Tile == 15 || Tiles2[i, j].Tile == 16))
                    {
                        Rectangle hitbox = new Rectangle(movingRectangle.X, movingRectangle.Y+30, movingRectangle.Width,movingRectangle.Height-30);
                       
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


        int[,] GetMatrix(string m)
        {
            string map = m;

            int[,] matrix=new int[20,100];

            for (int i = 0; i < 20; i++)
            {
                    
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string[] sorok = map.Split('\n');
                string[] help = sorok[i].Split(',');
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = int.Parse(help[j]);
                }
            }

            return matrix;
        }

    }

   
}
