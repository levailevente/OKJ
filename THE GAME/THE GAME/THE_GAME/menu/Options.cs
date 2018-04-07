using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    static class Options
    {
        static readonly Button Back, Fullsc, Lang;
        public static readonly Button Apply;
        public static readonly SpriteFont Font;
        static string fullscreen, language;

        enum Languages
        {
            Hu,
            En
        }

        static Languages langauges;

        static Options()
        {
            Font = Game1.ContentMgr.Load<SpriteFont>("font2");
            Apply = new Button(MainMenu.Gomb, new Rectangle(470, 620, 150, 55), "Apply");
            Back = new Button(MainMenu.Gomb, new Rectangle(670, 620, 150, 55), "Back");

            Fullsc = new Button(Save.Gomb, new Rectangle(675, 195, 170, 45), "Off");
            Lang = new Button(Save.Gomb, new Rectangle(675, 295, 170, 45), "English");

            Fullsc.Position.X += 30;
            Fullsc.Position.Y -= 8;

            Lang.Position.X += 10;
            Lang.Position.Y -= 8;

            Apply.Position.X += 10;
            Apply.Position.Y -= 5;

            Back.Position.X += 15;
            Back.Position.Y -= 5;

            langauges = Languages.Hu;

            language = "Language:";
            fullscreen = "Fullscreen:";
        }

        public static void Update(MouseState mouse)
        {
            Apply.Update(mouse);
            Back.Update(mouse);
            Fullsc.Update(mouse);
            Lang.Update(mouse);

            if (Back.IsClicked)
                Game1.CurrentGameState = Game1.CurrentGameState == Game1.Gamestates.Options
                    ? Game1.Gamestates.Mainmenu
                    : Game1.Gamestates.Pause;


            if (Fullsc.IsClicked)
            {
                if (Game1.Fullscreen)
                {
                    Game1.Fullscreen = false;
                    if (langauges == Languages.En) Fullsc.Text = "Off";
                    if (langauges == Languages.Hu) Fullsc.Text = "Ki";
                }
                else
                {
                    Game1.Fullscreen = true;
                    if (langauges == Languages.En) Fullsc.Text = "On";
                    if (langauges == Languages.Hu) Fullsc.Text = "Be";
                }
            }

            if (Lang.IsClicked)
            {
                if (langauges == Languages.Hu)
                {
                    langauges = Languages.En;
                    Lang.Text = "English";
                }
                else if (langauges == Languages.En)
                {
                    langauges = Languages.Hu;
                    Lang.Text = "Magyar";
                }

            }

            if (Apply.IsClicked)
            {
                if (langauges == Languages.Hu && MainMenu.Exit.Text != "Kilépés") ChangetoHungarian();
                if (langauges == Languages.En && MainMenu.Exit.Text != "Exit") ChangetoEnglish();
            }
        }


        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);
            Apply.Draw(sbatch);
            Back.Draw(sbatch);
            sbatch.DrawString(Font, fullscreen, new Vector2(450, 200), Color.White);
            sbatch.DrawString(Font, language, new Vector2(450, 300), Color.White);
            Fullsc.Draw2(sbatch, 0.5f);
            Lang.Draw2(sbatch, 0.5f);
        }

        static void ChangetoHungarian()
        {
            MainMenu.Newgame.Text = "Új játék";
            MainMenu.Newgame.Position.X += 15;

            MainMenu.LoadGame.Text = "Játék betöltése";
            MainMenu.LoadGame.Position.X -= 20;

            MainMenu.Options.Text = "Beállítások";
            MainMenu.Options.Position.X -= 15;

            MainMenu.Exit.Text = "Kilépés";
            MainMenu.Exit.Position.X -= 15;

            fullscreen = "Teljes képernyő:";
            language = "Nyelv:";

            Back.Text = "Vissza";
            Back.Position.X -= 10;
            Apply.Text = "Alkalmaz";
            Apply.Position.X -= 20;
            if (Game1.Fullscreen) Fullsc.Text = "Be";
            else Fullsc.Text = "Ki";
            Save.Back.Text = "Vissza";
            Save.Back.Position.X -= 10;

            Pause.Save.Text = "Mentés";
            Pause.ExitM.Text = "Kilépés a menübe";
            Pause.Resume.Text = "Folytatás";

            Pause.ExitM.Position.X -= 20;
            Pause.Save.Position.X -= 10;


            foreach (SaveSlot s in Save.Saves)
            {
                s.Back.Text = "Vissza";
                s.Back.Position.X -= 7;
                s.Save.Text = "Mentés";
                s.Save.Position.X -= 10;
                s.Textbox.Text = "Név:";
                s.Text = "Üres mentés";
            }

            EndScreen.Save.Text = "Mentés";
            EndScreen.Save.Position.X -= 10;
            EndScreen.nextlvl.Text = "Köveketkező szint";

            Gameover.restart.Text = "Újrakezdés";
            Gameover.restart.Position.X -= 12;
        }

        static void ChangetoEnglish()
        {
            MainMenu.Newgame.Text = "New Game";

            MainMenu.LoadGame.Text = "Load Game";

            MainMenu.Options.Text = "Options";

            MainMenu.Exit.Text = "Exit";

            MainMenu.Exit.Position.X += 15;
            MainMenu.Options.Position.X += 15;
            MainMenu.Newgame.Position.X -= 15;
            MainMenu.LoadGame.Position.X += 20;

            fullscreen = "Fullscreen:";
            language = "Language:";
            Back.Text = "Back";
            Apply.Text = "Apply";

            Apply.Position.X += 20;

            Back.Position.X += 10;

            if (Game1.Fullscreen) Fullsc.Text = "On";
            else Fullsc.Text = "Off";

            Save.Back.Text = "Back";
            Save.Back.Position.X += 10;

            Pause.Save.Text = "Save";
            Pause.ExitM.Text = "Exit to menu";
            Pause.Resume.Text = "Resume";

            Pause.ExitM.Position.X += 20;
            Pause.Save.Position.X += 10;

            foreach (SaveSlot s in Save.Saves)
            {
                s.Back.Text = "Back";
                s.Back.Position.X += 7;
                s.Save.Text = "Save";
                s.Save.Position.X += 10;
                s.Textbox.Text = "Name:";
                s.Text = "Empty Slot";
            }

            EndScreen.Save.Text = "Save";
            EndScreen.Save.Position.X += 10;
            EndScreen.nextlvl.Text = "Next Level";

            Gameover.restart.Text = "Restart";
            Gameover.restart.Position.X += 12;
        }
    }
}
