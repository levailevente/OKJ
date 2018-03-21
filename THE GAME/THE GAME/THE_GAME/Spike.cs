using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
  public class Spike
    {
        Sprite spike;
        Vector2 position;
        public Spike(Vector2 position)
        {
                spike=new Sprite(Game1.ContentMgr.Load<Texture2D>("tiles/Tile (17)"),new Rectangle((int)position.X, (int)position.Y+72,72,72));
            this.position = position;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sbatch)
        {

        }
    }
}
