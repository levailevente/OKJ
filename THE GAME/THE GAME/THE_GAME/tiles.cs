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
        readonly Texture2D tiles;
        readonly Texture2D objects;
        public Rectangle Rectangle;
        public readonly bool Blocked;

        public Tiles(int i, Rectangle r,bool blocked)
        {
           
            tiles = Game1.ContentMgr.Load<Texture2D>("tiles/Tile (" + i + ")");
            objects= Game1.ContentMgr.Load<Texture2D>("object/Object (" + i + ")");
            Rectangle = r;
            Blocked = blocked;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(tiles, Rectangle, Color.White);
        }

      
    }


}
