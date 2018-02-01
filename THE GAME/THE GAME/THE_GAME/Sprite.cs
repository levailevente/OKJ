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
       public  Texture2D texture;
       public   Rectangle rectangle;

        public Sprite(Texture2D t, Rectangle r)
        {
            texture = t;
            rectangle = r;
        }

        public virtual void Update()
        {

        }

        public void Draw(SpriteBatch sbatch )
        {
            sbatch.Draw(texture, rectangle, Color.White);
        }
    }


}
