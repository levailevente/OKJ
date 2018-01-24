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
        public readonly bool Blocked;
        public Rectangle Rectangle { get; }


        public Tiles(int i, Rectangle r, bool b)
        {
            texture = Game1.ContentMgr.Load<Texture2D>("tiles/Tile (" + i + ")");
            Rectangle = r;
            Blocked = b;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (Blocked) spritebatch.Draw(texture, Rectangle, Color.White);
        }

      
    }


}
