﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
   public class Tiles
    {
        readonly Texture2D tiles;
        public Rectangle Rectangle;
        public readonly bool Blocked;
        public readonly int Tile;
        public  Tiles(int i, Rectangle r,bool blocked,bool isObject)
        {
            Tile = i;
          if (!isObject)  tiles = Game1.ContentMgr.Load<Texture2D>("tiles/Tile (" + i + ")");
            else tiles= Game1.ContentMgr.Load<Texture2D>("objects/Object (" + i + ")");
            Rectangle = r;
            Blocked = blocked;
            
        }

  

        public virtual void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(tiles, Rectangle, Color.White);         
        }

      
    }


}
