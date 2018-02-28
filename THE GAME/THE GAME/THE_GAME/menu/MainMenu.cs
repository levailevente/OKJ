using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME.menu
{
    class MainMenu
    {
        Button newgame;
        Sprite hatter;
        Texture2D gomb;
        public MainMenu()
        {
            gomb= Game1.ContentMgr.Load<Texture2D>("gomb");
            newgame = new Button(gomb,new Rectangle(100,100,100,50),"Új játék");
        }





        public void Draw(SpriteBatch sbatch)
        {

        }
    }


    
}
