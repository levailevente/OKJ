using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class Sprite
    {
        protected readonly Texture2D Texture;
        public Rectangle Rectangle;

        public Sprite(Texture2D t, Rectangle r)
        {
            Texture = t;
            Rectangle = r;
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
