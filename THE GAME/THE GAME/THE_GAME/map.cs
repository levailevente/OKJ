﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace THE_GAME
{
    public  class map
    {
        private List<Tiles> Tiles { get; } = new List<Tiles>();

        private int width, height;

        public map()
        {

        }

        public void Generate(int[,]map,int size)
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    int n = map[j, i];

                    if (n > 0 )
                        Tiles.Add(new Tiles(n, new Rectangle(i * size, j * size, size, size),true));

                      width = (i + 1) * size;
                    height = (j + 1) * size;
                }

                
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Tiles tile in Tiles)
            {
                tile.Draw(spritebatch);
            }
        }


        public bool Collision (Rectangle movingRectangle)
        {
            foreach (Tiles tile in Tiles)
            {
                if (tile.Blocked&&tile.Rectangle.Intersects(movingRectangle))
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
