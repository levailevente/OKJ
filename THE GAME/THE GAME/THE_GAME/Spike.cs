using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
  public class Spike
    {
        Sprite spike;
        Vector2 position;
        Rectangle rectangle;
        Rectangle hitbox;
        bool up;

        public Spike(Vector2 position)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y + 72, 72, 72);
                spike =new Sprite(Game1.ContentMgr.Load<Texture2D>("tiles/Tile (17)"),rectangle);
            hitbox = new Rectangle(rectangle.X+7, rectangle.Y+30, 65, 42);
            this.position = position;
            up = false;
        }

        public void Update()
        {
            if (hitbox.Intersects(Game1.Karakter.Hitbox))
            {
                if (!Game1.Karakter.invulnerable)
                {
                    Game1.Karakter.health -= 1;
                    Game1.Karakter.invulnerable = true;
                    Game1.Karakter.damaged = true;
                    Game1.Karakter.position.Y -= 50;
                }
                
            }

            if (up)
            {
                if (position.Y < spike.Rectangle.Y && Game1.Karakter.RectangleW.Y - spike.Rectangle.Y < 90)
                {

                    spike.Rectangle.Y -= 12;
                    hitbox.Y -= 12;
                }

                if ((Game1.Karakter.Hitbox.X -rectangle.X  > 110 ||  rectangle.X - Game1.Karakter.Hitbox.X > 110) && Game1.Karakter.RectangleW.Y - spike.Rectangle.Y < 150 && Game1.Karakter.RectangleW.Y - spike.Rectangle.Y > -150 || Game1.Karakter.Hitbox.Y>720)
                {
                    up = false;
                }
            }

            if (!up)
            {

                if (spike.Rectangle.Y < position.Y+72 )
                {
                    spike.Rectangle.Y += 24;
                    hitbox.Y+= 24;
                }

                if ((rectangle.X-Game1.Karakter.Hitbox.X<100 ||  Game1.Karakter.Hitbox.X - rectangle.X  < 100) && Game1.Karakter.RectangleW.Y - spike.Rectangle.Y < 150 && Game1.Karakter.RectangleW.Y - spike.Rectangle.Y > -150)
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
