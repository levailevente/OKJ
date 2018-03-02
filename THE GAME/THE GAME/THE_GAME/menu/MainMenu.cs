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
        static readonly Button Options;
        static readonly Button Exit;
        public static readonly Sprite Hatter;
        public static readonly Sprite Logo;
        public static readonly Sprite Karakter;
        static MainMenu()
        {
            
            Texture2D gomb = Game1.ContentMgr.Load<Texture2D>("gomb");
            Newgame = new Button(gomb,new Rectangle(560,250,170,70),"New Game");
            LoadGame = new Button(gomb, new Rectangle(560, 350, 170, 70), "Load Game");
            Options = new Button(gomb, new Rectangle(560, 450, 170, 70), "Options");
            Exit = new Button(gomb, new Rectangle(560, 550, 170, 70), "Exit");
            Hatter =new Sprite(Game1.ContentMgr.Load<Texture2D>("menubg"),new Rectangle(0,0,1280,800));
            Logo=new Sprite(Game1.ContentMgr.Load<Texture2D>("logo"),new Rectangle(350,20,550,220));
            Karakter = new Sprite(Game1.ContentMgr.Load<Texture2D>("bob/slide/Slide__000"), new Rectangle(420, 120, 130, 120));

            Exit.Position.X += 30;
            Options.Position.X += 15;
        }


        public static void Draw(SpriteBatch sbatch)
        {
            Hatter.Draw(sbatch);
            Newgame.Draw(sbatch);
            LoadGame.Draw(sbatch);
            Karakter.DrawC(sbatch,Color.Cyan);
            Options.Draw(sbatch);
            Exit.Draw(sbatch);
            Logo.DrawC(sbatch,Color.Beige);
        }

        public static void Update(MouseState mouse)
        {
            Newgame.Update(mouse);
            LoadGame.Update(mouse);
            Exit.Update(mouse);
            Options.Update(mouse);
            if (Newgame.IsClicked)
            {
                Game1.CurrentGameState = Game1.Gamestates.Playing;
            }

            if (Exit.IsClicked)
            {
                Game1.exit = true;
            }

            if (Game1.Newkey.IsKeyDown(Keys.Escape))
            {
                Game1.exit = true;
            }

           
        }
    }
    
}
