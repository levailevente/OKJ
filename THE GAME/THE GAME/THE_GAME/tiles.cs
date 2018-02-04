using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
   public class Tiles
    {
        private readonly Texture2D texture;
        public Rectangle Rectangle { get; }
        

        public Tiles(int i, Rectangle r,bool Blocked)
        {
           
                texture = Game1.ContentMgr.Load<Texture2D>("tiles/Tile (" + i + ")");
                Rectangle = r;
      
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, Rectangle, Color.White);
        }

      
    }


}
