using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
     static class Pause
     {
         static readonly Button Resume, Save;
      static  Pause()
        {
            Resume = new Button(MainMenu.Gomb, new Rectangle(560, 250, 170, 70), "Resume");
            Save = new Button(MainMenu.Gomb, new Rectangle(560, 350, 170, 70), "Save");

            Resume.Position.X += 10;
            Save.Position.X += 25;
        }

        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);
            MainMenu.Logo.DrawC(sbatch,Color.Beige);
            MainMenu.Karakter.DrawC(sbatch, Color.DarkCyan);
            MainMenu.Options.Draw(sbatch);
            Resume.Draw(sbatch);
            Save.Draw(sbatch);
        }

        public static void Update(MouseState mouse)
        {
            Resume.Update(mouse);
            Save.Update(mouse);
            MainMenu.Options.Update(mouse);
            if (Resume.IsClicked)
            {
                Game1.CurrentGameState = Game1.Gamestates.Playing;
            }
        }
    }
}
