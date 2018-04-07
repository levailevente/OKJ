using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    static class EndScreen
    {
        public static readonly Button nextlvl;
        public static readonly Button Save;
        static readonly Sprite gj;
        static readonly SpriteFont font;
        static bool end;

        static EndScreen()
        {
            nextlvl = new Button(MainMenu.Gomb, new Rectangle(560, 350, 170, 65), "Next Level");
            gj = new Sprite(Game1.ContentMgr.Load<Texture2D>("gj"), new Rectangle(570, 50, 392 / 3, 512 / 3));
            Save = new Button(MainMenu.Gomb, new Rectangle(560, 450, 170, 65), "Save");
            font = Game1.ContentMgr.Load<SpriteFont>("font2");
            Save.Position.X += 22;
        }


        public static void Update(MouseState mouse)
        {
            if (end) return;
            Pause.ExitM.Update(mouse);
            nextlvl.Update(mouse);
            Save.Update(mouse);

            if (Pause.ExitM.IsClicked)
            {
                Game1.CurrentGameState = Game1.Gamestates.Mainmenu;
            }

            if (Save.IsClicked)
            {
                menu.Save.Update(mouse);
            }

            if (nextlvl.IsClicked)
            {
                Game1.Lvl++;
                if (Game1.Lvl != -1)
                {
                    Game1.GenerateMap = new GenerateMap(Game1.Lvl, 72);
                    Game1.Character=new Character();
                    Game1.CurrentGameState = Game1.Gamestates.Playing;
                }

                else
                {
                    end = true;
                }
            }
        }


        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);
            Pause.ExitM.Draw(sbatch);
            Save.Draw(sbatch);
            nextlvl.Draw(sbatch);
            sbatch.DrawString(font, "Level " + Game1.Lvl + " Completed", new Vector2(520, 250), Color.White);
            gj.Draw(sbatch);

            if (end)
            {
                MainMenu.Hatter.Draw(sbatch);
                sbatch.DrawString(font, "  Created by \n Lévai Levente", new Vector2(550, 300), Color.White);
            }
        }
    }
}
