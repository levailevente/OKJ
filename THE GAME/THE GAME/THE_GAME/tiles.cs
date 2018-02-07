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
        public Rectangle Rectangle,Rectangle2;
        public readonly bool Blocked,Blocked2;

        public Tiles(int i,int j, Rectangle r,Rectangle r2,bool blocked,bool blocked2)
        {
           
            tiles = Game1.ContentMgr.Load<Texture2D>("tiles/Tile (" + i + ")");
            objects= Game1.ContentMgr.Load<Texture2D>("objects/Object (" + j + ")");
            Rectangle = r;
            Blocked = blocked;
            Blocked2 = blocked2;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(tiles, Rectangle, Color.White);
            spritebatch.Draw(objects, Rectangle, Color.White);
        }

      
    }


}
