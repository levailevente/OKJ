using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class MovingTile:Tiles
    {
        Sprite left, right;
        bool vertical;
        Vector2 startPos;


        bool RightOrDown;
        public MovingTile(int i,Rectangle r, bool blocked,bool isObject,bool vertical,bool RoD):base(i,r,blocked,isObject)
        {
            left=new Sprite(Game1.ContentMgr.Load<Texture2D>("tiles/Tile (14)"),new Rectangle(r.X,r.Y,72,72));
            right = new Sprite(Game1.ContentMgr.Load<Texture2D>("tiles/Tile (16)"), new Rectangle(r.X+72, r.Y, 72, 72));
            this.vertical = vertical;
            startPos = new Vector2(r.X, r.Y);
            RightOrDown = RoD;        
        }

       

        public  override void   Draw(SpriteBatch sbatch)
        {
            if (!vertical)
            {
                if (RightOrDown)
                {
                    Rectangle.X += 1;
                    left.Rectangle.X += 1;
                    right.Rectangle.X += 1;

                    if (Rectangle.Intersects(Game1.Karakter.Hitbox))

                    if (Rectangle.X - startPos.X > 100) RightOrDown = false;
                }

                else
                {
                    Rectangle.X -= 1;
                    left.Rectangle.X -= 1;
                    right.Rectangle.X -= 1;
                    if (Rectangle.X - startPos.X < -100) RightOrDown = true;
                }
            }

            left.Draw(sbatch);
            right.Draw(sbatch);
        }
    }
}
