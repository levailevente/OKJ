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
        Viewport view;
        private Vector2 centre;

        public Camera(Viewport newview)
        {
            view = newview;
        }

        public void Update(karakter player)
        {
            if(player.Hitbox.X>600)
            centre.X = player.Hitbox.X + (player.Hitbox.Width / 2) - Game1.Swidth / 2;

            if (player.Hitbox.Y > 150)
                centre.Y = player.Hitbox.Y + (player.Hitbox.Height / 2) - Game1.Sheight / 2 - 100;

            Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
