
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
   public class Camera
    {
        public Matrix Transform;
        public Vector2 centre;

        Viewport view;

        public Camera(Viewport newview)
        {
            view = newview;
        }

        public void Update(Karakter player)
        {
            if (Game1.CurrentGameState == Game1.Gamestates.Playing)
            {

            
            if (player.RectangleW.X>Game1.Swidth/2)
                centre.X = player.RectangleW.X  - Game1.Swidth / 2;

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
