using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
   public class Camera
    {
        public Matrix Transform;
        private Vector2 centre;

        Viewport view;

        public Camera(Viewport newview)
        {
            view = newview;
        }

        public void Update(Karakter player)
        {
            if (Game1.CurrentGameState == Game1.Gamestates.Playing)
            {

            
            if (player.RectangleW.X>600)
                centre.X = player.RectangleW.X + (player.RectangleW.Width / 2) - Game1.Swidth / 2+10;

            if (player.RectangleW.Y > 600)
                centre.Y = player.RectangleW.Y + (player.RectangleW.Height / 2) - Game1.Sheight / 2 - 100;

            if (player.IsDead)
            {
                centre.Y = player.RectangleW.Y + (player.RectangleW.Height / 2) - Game1.Sheight / 2 +5;
                centre.X = player.RectangleW.X + (player.RectangleW.Width / 2) - Game1.Swidth / 2 + 610;
            }
                Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }

            else
            {
                centre.Y = 0;
                centre.X = 0;
                Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }
            
        }
       
    }
}
