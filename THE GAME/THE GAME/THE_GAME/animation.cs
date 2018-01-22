using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    class animation
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position, origin, velocity;

        int currentf, fheight, fwidth;

        float timer, interval=75;
        
        public animation(Texture2D t, Vector2 p, int fh, int fw ) 
        {
            texture = t;
            position = p;
            fheight = fh;
            fwidth = fw;
        }


        public void update(GameTime gametime)
        {
            rectangle = new Rectangle(currentf * fwidth, 0, fwidth, fheight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position = position + velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                animeteright(gametime);
                velocity.X = 3;
            }
            else velocity = Vector2.Zero;

        }

        public void animeteright(GameTime gametime)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds / 2;

            if (timer>interval)
            {
                currentf++;
                timer = 0;
                if (currentf > 10) currentf = 0;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

    }
}
