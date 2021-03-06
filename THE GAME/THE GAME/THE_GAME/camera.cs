﻿
using Microsoft.Xna.Framework;

namespace THE_GAME
{
    public class Camera
    {
        public Matrix Transform;
        public Vector2 Centre;

        public void Update(Character player)
        {
            if (Game1.CurrentGameState == Game1.Gamestates.Playing)
            {

                Centre.Y = 720;

                if (player.RectangleW.X > Game1.Swidth / 2 && player.RectangleW.X < 6560)
                    Centre.X = player.RectangleW.X - Game1.Swidth / 2;

                if (player.RectangleW.Y < Game1.Sheight / 3 + 720 && player.RectangleW.Y > 250)
                    Centre.Y = (player.RectangleW.Y - Game1.Sheight / 3);

                if (player.RectangleW.Y > 1550)
                    Centre.X = 0;

                if (player.RectangleW.Y < 250)
                    Centre.Y = 0;


                Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                            Matrix.CreateTranslation(new Vector3(-Centre.X, -Centre.Y, 0));
            }


            else
            {
                Centre.Y = 0;
                Centre.X = 0;
                Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                            Matrix.CreateTranslation(new Vector3(-Centre.X, -Centre.Y, 0));
            }

        }

    }
}
