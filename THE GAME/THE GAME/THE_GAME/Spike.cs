using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
  public class Spike
    {
        Sprite spike;
        Vector2 position;
        Rectangle rectangle;
        bool up;
        public Spike(Vector2 position)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y + 72, 72, 72);
                spike =new Sprite(Game1.ContentMgr.Load<Texture2D>("tiles/Tile (17)"),rectangle);
            this.position = position;
            up = false;
        }

        public void Update()
        {
            if (rectangle.Intersects(Game1.Karakter.Hitbox))
            {
                Game1.Karakter.health -= 1;
                Game1.Karakter.invulnerable = true;
                Game1.Karakter.damaged = true;
            }

            if (up)
            {
              if (rectangle.Y>position.Y)  rectangle.Y -= 1;
            }

            if (!up)
            {
                if (rectangle.X-Game1.Karakter.Hitbox.X<100)
                {
                    up = true;
                }
            }
        }

        public void Draw(SpriteBatch sbatch)
        {
            spike.Draw(sbatch);
        }
    }
}
