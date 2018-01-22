using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
   public class tiles
    {
        protected Texture2D texture;
        public bool blocked;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }


        public tiles(int i, Rectangle r, bool b)
        {
            texture = Game1.contentMgr.Load<Texture2D>("tiles/Tile (" + i + ")");
            Rectangle = r;
            blocked = b;
        }
        public void draw(SpriteBatch spritebatch)
        {
            if (blocked) spritebatch.Draw(texture, rectangle, Color.White);
        }

      
    }


}
