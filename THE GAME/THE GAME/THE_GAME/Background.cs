﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class Background : Sprite
    {

        Rectangle rectangle2;

        public Background(Texture2D t, Rectangle r) : base(t, r)
        {

            rectangle2 = new Rectangle(r.X + 1280, 720, 1280, 720);

        }

        public override void Draw(SpriteBatch sbatch)
        {
            base.Draw(sbatch);
            sbatch.Draw(Texture, rectangle2, Color.White);

            UpdatePositions();


        }

        void UpdatePositions()
        {
            if (Game1.Character.RectangleW.X - Rectangle.X > 1880 && Game1.Character.RectangleW.X < 5920)
                Rectangle.X += 1280 * 2;

            if (Game1.Character.RectangleW.X - rectangle2.X > 1880 && Game1.Character.RectangleW.X < 5920)
                rectangle2.X += 1280 * 2;

            if (Rectangle.X - Game1.Character.RectangleW.X > 640) Rectangle.X -= 1280 * 2;

            if (rectangle2.X - Game1.Character.RectangleW.X > 640) rectangle2.X -= 1280 * 2;


        }
    }
}