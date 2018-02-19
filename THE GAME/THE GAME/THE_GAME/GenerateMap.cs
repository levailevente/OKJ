﻿using System;
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
        int n = Mapok.Palya.GetLength(0);
        int m = Mapok.Palya.GetLength(1);
        List<Tiles> Tiles { get; } = new List<Tiles>();
        List<Tiles> Objects { get; } = new List<Tiles>();
        Tiles[,] Tiles2 { get; }
        Tiles[,] Objects2 { get; }
        int width, height;

        public GenerateMap(int[,] map, int[,] objects,int size)
        {
            //for (int i = 0; i < map.GetLength(1); i++)
            // {

            //for (int j = 0; j < map.GetLength(0); j++)
            //{
            //    int n = map[j, i];
            //    int m = objects[j, i];

            //    if (n > 0)
            //        Tiles.Add(new Tiles(n, new Rectangle(i * size, j * size, size, size),true, false));
            //    else if (n<0)Tiles.Add(new Tiles(n * -1, new Rectangle(i * size, j * size, size, size),false, false));

            //    if (m > 0)
            //        Objects.Add(new Tiles(m, new Rectangle(i * size, j * size, size, size),false, true));
            //    else if (m<0) Objects.Add(new Tiles(m * -1, new Rectangle(i * size, j * size, size, size),true, true));

            //    width = (i + 1) * size;
            //    height = (j + 1) * size;
            //}


            // }

            n = map.GetLength(1);
            m = map.GetLength(0);
            Tiles2 = new Tiles[n, m];
            Objects2 = new Tiles[n, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; i < n; j++)
                {
                    int t = map[j,i];
                    int o = objects[i, j];
                    if (t>0)
                        Tiles2[i,j]=new Tiles(t, new Rectangle(i * size, j * size, size, size), true, false);
                    else if (t<0) Tiles2[i, j] = new Tiles(t*-1, new Rectangle(i * size, j * size, size, size), false, false);
                }
            }
        }


        public void Draw(SpriteBatch spritebatch)
        {
            //foreach (Tiles tile in Tiles)
            //{
            //    tile.Draw(spritebatch);

            //}
            //foreach (Tiles tile in Objects)
            //{
            //    tile.Draw(spritebatch);

            //}

            for (int i = (int)(Game1.karakter.Position.X/64)-2; i < (int)(Game1.karakter.Position.X / 64) + 2; i++)
            {
                for (int j = (int)(Game1.karakter.Position.Y / 64) - 2; j < (int)(Game1.karakter.Position.Y / 64) + 2; j++)
                {
                    Tiles2[i, j].Draw(spritebatch);
                }
            }

        }


        public bool Collision (Rectangle movingRectangle)
        {


          
           
            for (int i = 0; i < Tiles.Count; i++)
            {
               // if (i == 14 || i == 15 || i == 16) movingRectangle.Height -= 50;
                if (Tiles[i].Blocked && Tiles[i].Rectangle.Intersects(movingRectangle))
                {
                    return true;
                }

            }

            foreach (Tiles objects in Objects)
            {
                if (objects.Blocked && objects.Rectangle.Intersects(movingRectangle))
                {
                    return true;
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
