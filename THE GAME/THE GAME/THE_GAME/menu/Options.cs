using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
  static class Options
    {
        static Button apply, back, fullsc, lang;
        static SpriteFont Font;
        static Options()
        {
            Font= Game1.ContentMgr.Load<SpriteFont>("font2");
            apply = new Button(MainMenu.Gomb, new Rectangle(470,620,150,55), "Apply");
            back = new Button(MainMenu.Gomb, new Rectangle(670, 620, 150, 55), "Back");

            fullsc = new Button(Save.gomb, new Rectangle(630, 195, 170, 45), "Off");
            lang = new Button(Save.gomb, new Rectangle(630, 295, 170, 45), "English");

            fullsc.Position.X += 25;
            fullsc.Position.Y -= 8;

            lang.Position.X += 10;
            lang.Position.Y -= 8;

            apply.Position.X += 10;
            apply.Position.Y -= 7;

            back.Position.X += 10;
            back.Position.Y -= 7;
        }

        public static void Update(MouseState mouse)
        {
            apply.Update(mouse);
            back.Update(mouse);
            fullsc.Update(mouse);
            lang.Update(mouse);
            if (back.IsClicked)
                Game1.CurrentGameState = Game1.CurrentGameState == Game1.Gamestates.Options
                    ? Game1.Gamestates.Mainmenu
                    : Game1.Gamestates.Pause;


            if (fullsc.IsClicked)
            {
                Game1.fullscreen = true;
                if (!Game1.fullscreen) Game1.fullscreen = false;
            }
        }


        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);
            apply.Draw(sbatch);
            back.Draw(sbatch);
            sbatch.DrawString(Font, "Fullscreen:", new Vector2(460,200), Color.White);
            sbatch.DrawString(Font, "Language:", new Vector2(460, 300), Color.White);
            fullsc.Draw2(sbatch,0.5f);
            lang.Draw2(sbatch, 0.5f);
        }
    }
}
