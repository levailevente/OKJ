using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME.menu
{
     class Pause
    {
        public Pause()
        {
                
        }

        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Karakter.DrawC(sbatch,Color.Cyan);
            MainMenu.Hatter.Draw(sbatch);
            MainMenu.Logo.DrawC(sbatch,Color.Beige);
        }

        public static void Update()
        {

        }
    }
}
