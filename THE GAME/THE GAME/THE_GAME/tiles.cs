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
        public Rectangle Rectangle;
        public readonly bool Blocked;

        public Tiles(int i, Rectangle r,bool blocked,bool isObject)
        {
           
          if (!isObject)  tiles = Game1.ContentMgr.Load<Texture2D>("tiles/Tile (" + i + ")");
            else tiles= Game1.ContentMgr.Load<Texture2D>("objects/Object (" + i + ")");
            Rectangle = r;
            Blocked = blocked;
            
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(tiles, Rectangle, Color.White);         
        }

      
    }


}
