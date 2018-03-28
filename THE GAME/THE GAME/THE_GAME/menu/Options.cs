using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
  static class Options
    {
        public  static Button back, fullsc, lang;
        public static Button apply;
       public static SpriteFont Font;
        static string fullscreen, language;
        enum Languages
        {
            Hu,
            En,
            De
        }

        static Languages langauges;
        static Options()
        {
            Font= Game1.ContentMgr.Load<SpriteFont>("font2");
            apply = new Button(MainMenu.Gomb, new Rectangle(470,620,150,55), "Apply");
            back = new Button(MainMenu.Gomb, new Rectangle(670, 620, 150, 55), "Back");

            fullsc = new Button(Save.Gomb, new Rectangle(675, 195, 170, 45), "Off");
            lang = new Button(Save.Gomb, new Rectangle(675, 295, 170, 45), "English");

            fullsc.Position.X += 30;
            fullsc.Position.Y -= 8;

            lang.Position.X += 10;
            lang.Position.Y -= 8;

            apply.Position.X += 10;
            apply.Position.Y -= 5;

            back.Position.X += 15;
            back.Position.Y -= 5;

            langauges = Languages.En;

            language = "Language:";
            fullscreen = "Fullscreen:";
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
                if (Game1.Fullscreen)
                {
                    Game1.Fullscreen = false;
                    if (langauges==Languages.En) fullsc.Text = "Off";
                    if (langauges == Languages.Hu) fullsc.Text = "Ki";
                }
                else
                {
                    Game1.Fullscreen = true;
                    if (langauges == Languages.En) fullsc.Text = "On";
                    if (langauges == Languages.Hu) fullsc.Text = "Be";
                }
            }

            if (lang.IsClicked)
            {
                if (langauges == Languages.Hu)
                {
                    langauges = Languages.En;
                    lang.Text = "English";
                }
               else if (langauges == Languages.En)
                {
                    langauges = Languages.Hu;
                    lang.Text = "Magyar";
                }

            }

            if (apply.IsClicked)
            {
                if (langauges==Languages.Hu && MainMenu.Exit.Text!="Kilépés") ChangetoHungarian();
                if (langauges == Languages.En && MainMenu.Exit.Text != "Exit") ChangetoEnglish();
            }
        }


        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);
            apply.Draw(sbatch);
            back.Draw(sbatch);
            sbatch.DrawString(Font, fullscreen, new Vector2(450,200), Color.White);
            sbatch.DrawString(Font, language, new Vector2(450, 300), Color.White);
            fullsc.Draw2(sbatch,0.5f);
            lang.Draw2(sbatch, 0.5f);
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

            back.Text = "Vissza";
            back.Position.X -= 10;
            apply.Text = "Alkalmaz";
            apply.Position.X -= 20;
            if (Game1.Fullscreen) fullsc.Text = "Be";
            else fullsc.Text = "Ki";
            Save.Back.Text = "Vissza";
            Save.Back.Position.X -= 10;

            Pause.Save.Text = "Mentés";
            Pause.ExitM.Text="Kilépés a menübe";
            Pause.Resume.Text = "Folytatás";

            Pause.ExitM.Position.X -= 20;
            Pause.Save.Position.X -= 10;


            foreach (SaveSlot s in Save.Saves)
            {
                s.back.Text = "Vissza";
                s.back.Position.X -= 7;
                s.save.Text = "Mentés";
                s.save.Position.X -= 10;
                s.textbox.Text = "Név:";
                s.Text = "Üres mentés";
            }
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
            back.Text = "Back";
            apply.Text = "Apply";

            apply.Position.X += 20;

            back.Position.X += 10;

            if (Game1.Fullscreen) fullsc.Text = "On";
            else fullsc.Text = "Off";

            Save.Back.Text = "Back";
            Save.Back.Position.X += 10;

            Pause.Save.Text = "Save";
            Pause.ExitM.Text = "Exit to menu";
            Pause.Resume.Text = "Resume";

            Pause.ExitM.Position.X += 20;
            Pause.Save.Position.X += 10;

            foreach (SaveSlot s in Save.Saves)
            {
                s.back.Text = "Back";
                s.back.Position.X += 7;
                s.save.Text = "Save";
                s.save.Position.X += 10;
                s.textbox.Text = "Name:";
                s.Text = "Empty Slot";
            }
        }
    }
}
