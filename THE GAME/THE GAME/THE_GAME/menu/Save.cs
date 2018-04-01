using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    static class Save
    {
        public static readonly SaveSlot[] Saves;

        public static readonly Button Back;
        public static readonly Texture2D Gomb;

        static Save()
        {
            Gomb = Game1.ContentMgr.Load<Texture2D>("button");
            Saves = new SaveSlot[4];

            Saves[0] = new SaveSlot(Gomb, new Rectangle(400, 50, 500, 100), "Empty slot");
            Saves[1] = new SaveSlot(Gomb, new Rectangle(400, 200, 500, 100), "Empty slot");
            Saves[2] = new SaveSlot(Gomb, new Rectangle(400, 350, 500, 100), "Empty slot");
            Saves[3] = new SaveSlot(Gomb, new Rectangle(400, 500, 500, 100), "Empty slot");

            Back = new Button(MainMenu.Gomb, new Rectangle(575, 625, 150, 60), "Back");
            Back.Position.Y -= 2;
            Back.Position.X += 15;
        }

        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);

            foreach (SaveSlot s in Saves)
            {
                s.Draw(sbatch);
            }

            foreach (SaveSlot s in Saves)
            {
                if (s.NameInput) s.DrawTextbox(sbatch);
            }

            Back.Draw(sbatch);
        }

        public static void Update(MouseState mouse)
        {

            if (!Saves[0].NameInput && !Saves[1].NameInput && !Saves[2].NameInput && !Saves[3].NameInput)
            {
                foreach (SaveSlot s in Saves)
                {
                    s.Update(mouse);
                    Back.Update(mouse);
                    if (Back.IsClicked)
                    {
                        if (Game1.CurrentGameState == Game1.Gamestates.EndSave)
                            Game1.CurrentGameState = Game1.Gamestates.EndScreen;

                        if (Game1.CurrentGameState == Game1.Gamestates.Load)
                            Game1.CurrentGameState = Game1.Gamestates.Mainmenu;

                        if (Game1.CurrentGameState == Game1.Gamestates.Save)
                            Game1.CurrentGameState = Game1.Gamestates.Pause;
                    }

                }

            }

            else
            {
                foreach (SaveSlot s in Saves)
                {
                    if (s.NameInput) s.TextboxUpdate(mouse);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (Saves[i].Save.IsClicked)
                    {
                        Database.Save(i + 1, Saves[i].Name, Saves[i].Date, Game1.Lvl, Game1.Karakter.PositionPoint,
                            Game1.Karakter.Health);
                    }
                }
            }

            if (MainMenu.LoadGame.IsClicked || Pause.Save.IsClicked || EndScreen.Save.IsClicked)
            {
                for (int i = 0; i < 4; i++)
                {
                    string[] save = Database.Load(i + 1);
                    Saves[i].Name = save[0];
                    Saves[i].Date = save[1];
                    Saves[i].Lvl = int.Parse(save[2]);
                    if (Saves[i].Lvl > 0)
                    {
                        Saves[i].PositionString = save[3];
                        Saves[i].Hp = int.Parse(save[4]);
                        Saves[i].IsUsed = true;
                        Saves[i].Text = Saves[i].Name + "  " + Saves[i].Lvl + ". lvl";
                    }
                }

                MainMenu.LoadGame.IsClicked = false;
                Pause.Save.IsClicked = false;
                EndScreen.Save.IsClicked = false;
            }

            if (Game1.CurrentGameState == Game1.Gamestates.Load)
            {
                foreach (SaveSlot s in Saves)
                {
                    if (s.IsUsed && s.IsClicked)
                    {
                        Game1.GenerateMap = new GenerateMap(s.Lvl, 72);
                        Game1.Karakter.Health = s.Hp;
                        string[] positions = s.PositionString.Split(',');
                        int x = int.Parse(positions[0]);
                        int y = int.Parse(positions[1]);
                        Game1.Karakter.Position = new Vector2(x, y);
                        Game1.CurrentGameState = Game1.Gamestates.Playing;
                    }
                }
            }
        }
    }
}
