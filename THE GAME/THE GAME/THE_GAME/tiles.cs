using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class Tiles
    {
        readonly Texture2D tiles;
        public Rectangle Rectangle;
        public readonly bool Blocked;
        public readonly int Tile;

        public Tiles(int i, Rectangle r, bool blocked, bool isObject)
        {
            Tile = i;
            tiles = !isObject ? Game1.ContentMgr.Load<Texture2D>("tiles/Tile (" + i + ")") : Game1.ContentMgr.Load<Texture2D>("objects/Object (" + i + ")");
            Rectangle = r;
            Blocked = blocked;

        }



        public void Draw(SpriteBatch sbatch)
        {
            if (Tile == 8 && Rectangle.Intersects(Game1.Character.HitboxA) && Game1.Character.IsAttack &&
                Game1.Character.AttackI > 2 && Game1.Character.AttackI < 5)
            {
                Rectangle = new Rectangle(0, 0, 0, 0);
            }

            sbatch.Draw(tiles, Rectangle, Color.White);
        }


    }

}
