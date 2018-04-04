using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    public static class Gameover
    {
        static Sprite gameover;
        static Button restart;

        static Gameover()
        {
            gameover = new Sprite(Game1.ContentMgr.Load<Texture2D>("Game_Over"),
                new Rectangle(390, 100, 3508 / 7, 2480 / 7));
            restart = new Button(MainMenu.Gomb, new Rectangle(560, 250, 170, 65), "Restart");

            restart.Position.X += 12;
            restart.Position.Y += 180;
            restart.Rectangle.Y += 180;
        }

        public static void Update(MouseState mouse)
        {
            Pause.ExitM.Update(mouse);
            restart.Update(mouse);

            if (restart.IsClicked)
            {
                Game1.Character = new Character();
                Game1.Enemies.Clear();
                Game1.GenerateMap = new GenerateMap(Game1.Lvl, Game1.TileSize);
                Game1.CurrentGameState = Game1.Gamestates.Playing;
            }

            if (Pause.ExitM.IsClicked)
            {

                Game1.CurrentGameState = Game1.Gamestates.Mainmenu;
            }

        }

        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);
            gameover.Draw(sbatch);
            restart.Draw(sbatch);
            Pause.ExitM.Draw(sbatch);
        }
    }
}
