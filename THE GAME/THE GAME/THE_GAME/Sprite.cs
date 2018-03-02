using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THE_GAME
{
    public class Sprite
    {
        protected readonly Texture2D Texture;
        protected Rectangle Rectangle;

        public Sprite(Texture2D t, Rectangle r)
        {
            Texture = t;
            Rectangle = r;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch sbatch )
        {
            sbatch.Draw(Texture, Rectangle, Color.White);
        }

        public virtual void DrawC(SpriteBatch sbatch,Color color)
        {
            sbatch.Draw(Texture, Rectangle, color);
        }
    }


}
