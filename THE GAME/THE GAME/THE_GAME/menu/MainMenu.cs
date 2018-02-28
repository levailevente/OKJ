using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
   static class MainMenu
    {
        static readonly Button Newgame;
        static readonly Sprite Hatter;

        static MainMenu()
        {
            Texture2D gomb = Game1.ContentMgr.Load<Texture2D>("gomb");
            Newgame = new Button(gomb,new Rectangle(100,100,100,50),"Új játék");
            Hatter=new Sprite(Game1.ContentMgr.Load<Texture2D>("menubg"),new Rectangle(0,0,1280,800));
        }


        public static void Draw(SpriteBatch sbatch)
        {
            Hatter.Draw(sbatch);
            Newgame.Draw(sbatch);
        }

        public static void Update(MouseState mouse)
        {
            Newgame.Update(mouse);
            if (Newgame.isClicked)
            {
                Game1.CurrentGameState = Game1.Gamestates.Playing;
            }
  
        }
    }


    
}
