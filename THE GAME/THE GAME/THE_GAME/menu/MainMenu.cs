using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    internal static class MainMenu
   {
       public static readonly Button Newgame;
       public static readonly Button LoadGame;
       public static readonly Button Options;
       public static readonly Button Exit;
       public static readonly Sprite Hatter, Logo, Karakter;
       public static readonly Texture2D Gomb;
        static MainMenu()
        {
            
            Gomb = Game1.ContentMgr.Load<Texture2D>("gomb");
            Newgame = new Button(Gomb,new Rectangle(560,250,170,65),"New Game");
            LoadGame = new Button(Gomb, new Rectangle(560, 350, 170, 65), "Load Game");
            Options = new Button(Gomb, new Rectangle(560, 450, 170, 65), "Options");
            Exit = new Button(Gomb, new Rectangle(560, 550, 170, 65), "Exit");
            Hatter =new Sprite(Game1.ContentMgr.Load<Texture2D>("menubg"),new Rectangle(0,0,1280,800));
            Logo=new Sprite(Game1.ContentMgr.Load<Texture2D>("logo"),new Rectangle(360,10,550,220));
            Karakter = new Sprite(Game1.ContentMgr.Load<Texture2D>("bob/slide/Slide__000"), new Rectangle(430, 110, 130, 120));

            Exit.Position.X += 25;
            Options.Position.X += 10;
            Newgame.Position.X -= 0;
            LoadGame.Position.X -= 5;
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
                Game1.Karakter = new Karakter();
                Game1.Enemies.Clear();
                Game1.GenerateMap = new GenerateMap(Game1.Lvl, Game1.TileSize);
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
            if (LoadGame.IsClicked)
            {
               Game1.CurrentGameState = Game1.Gamestates.Load;
            }
            if (Options.IsClicked)
            {
                Game1.CurrentGameState = Game1.Gamestates.Options;
            }


        }
    }
    
}
