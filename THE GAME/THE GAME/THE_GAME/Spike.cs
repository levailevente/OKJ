using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class Spike
    {
        Sprite spike;
        Point position;
        Rectangle rectangle;
        Rectangle hitbox;
        bool up, visible;

        public Spike(Vector2 position)
        {

            rectangle = new Rectangle((int) position.X, (int) position.Y + 55, 62, 55);
            this.position = rectangle.Location;
            spike = new Sprite(Game1.ContentMgr.Load<Texture2D>("tiles/Tile (17)"), rectangle);
            hitbox = new Rectangle(rectangle.X + 8, rectangle.Y + 30, 45, 22);

            up = false;
            visible = false;
        }

        public void Update()
        {
            if (hitbox.Intersects(Game1.Character.Hitbox))
            {
                if (!Game1.Character.Invulnerable)
                {
                    Game1.Character.Health -= 1;
                    Game1.Character.Invulnerable = true;
                    Game1.Character.Position.Y -= 30;
                }
            }

            foreach (Zombie z in Game1.Enemies)
            {
                if (hitbox.Intersects(z.Hitbox))
                {
                    z.IsDead = true;
                }
            }

            if (up)
            {
                visible = true;
                if (position.Y < spike.Rectangle.Y + 30)
                {

                    spike.Rectangle.Y -= 10;
                    hitbox.Y -= 10;
                }

                if ((Game1.Character.Hitbox.X - rectangle.X > 150 || rectangle.X - Game1.Character.Hitbox.X > 150))
                {
                    up = false;
                }
            }

            if (!up)
            {

                if (spike.Rectangle.Y < position.Y)
                {
                    spike.Rectangle.Y += 15;
                    hitbox.Y += 15;
                }
                else visible = false;

                if ((rectangle.X - Game1.Character.Hitbox.X < 150 && Game1.Character.Hitbox.X - rectangle.X < 150) &&
                    Game1.Character.RectangleW.Y - hitbox.Y < 1 && Game1.Character.RectangleW.Y - hitbox.Y > -200)
                {
                    up = true;
                }
            }
        }

        public void Draw(SpriteBatch sbatch)
        {
            if (visible) spike.Draw(sbatch);

        }
    }
}
