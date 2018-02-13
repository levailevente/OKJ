﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class Background :Sprite
    {
        
        Rectangle rectangle2;

        public Background(Texture2D t, Rectangle r) : base(t, r)
        {
            
            rectangle2 = new Rectangle(r.X+1280,0,1280,720);
           
        }

        public override void Draw(SpriteBatch sbatch)
        {
            base.Draw(sbatch);
            sbatch.Draw(texture,rectangle2,Color.White);

            UpdatePositions();
           

        }

        void UpdatePositions()
        {
            if (Game1.karakter.Hitbox.X - rectangle.X> 1880) rectangle.X += 1280 * 2;

            if (Game1.karakter.Hitbox.X - rectangle2.X >1880) rectangle2.X += 1280 * 2;

            if ( rectangle.X- Game1.karakter.Hitbox.X  > 690) rectangle.X -= 1280 * 2;

            if (rectangle2.X- Game1.karakter.Hitbox.X  > 690) rectangle2.X -= 1280 * 2;


        }
    }
}
