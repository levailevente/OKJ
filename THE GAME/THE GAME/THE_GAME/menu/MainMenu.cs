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
        static readonly Button LoadGame;
        static readonly Button Exit;
        static readonly Sprite Hatter;

        static MainMenu()
        {
            Texture2D gomb = Game1.ContentMgr.Load<Texture2D>("gomb");
            Newgame = new Button(gomb,new Rectangle(560,200,170,70),"New Game");
            LoadGame = new Button(gomb, new Rectangle(560, 300, 170, 70), "Load Game");
            Exit = new Button(gomb, new Rectangle(560, 400, 170, 70), "Exit");
            Hatter =new Sprite(Game1.ContentMgr.Load<Texture2D>("menubg"),new Rectangle(0,0,1280,800));
            Exit.position.X += 30;
        }


        public static void Draw(SpriteBatch sbatch)
        {
            Hatter.Draw(sbatch);
            Newgame.Draw(sbatch);
            LoadGame.Draw(sbatch);
            Exit.Draw(sbatch);
        }

        public static void Update(MouseState mouse)
        {
            Newgame.Update(mouse);
            LoadGame.Update(mouse);
            Exit.Update(mouse);
            if (Newgame.isClicked)
            {
                Game1.CurrentGameState = Game1.Gamestates.Playing;
            }

            if (Exit.isClicked)
            {
                Game1.exit = true;
            }
  
        }
    }


    
}
