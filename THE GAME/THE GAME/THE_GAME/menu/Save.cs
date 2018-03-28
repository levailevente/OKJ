using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    static class Save
    {
        public static readonly SaveSlot[] Saves;

        public static readonly Button Back;
        public static Texture2D Gomb;

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
              if (s.NameInput)  s.DrawTextbox(sbatch);
            }



            Back.Draw(sbatch);
        }

        public static void Update(MouseState mouse)
        {

            if (!Saves[0].NameInput&& !Saves[1].NameInput && !Saves[2].NameInput && !Saves[3].NameInput)
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
                    if (s.NameInput) s.Textbox(mouse);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (Saves[i].save.IsClicked)
                    {
                        Game1.Db.Save(i+1,Saves[i].name,Saves[i].date,Game1.Lvl,Game1.Karakter.Position,Game1.Karakter.Health);
                    }
                }
            }

            if (MainMenu.LoadGame.IsClicked || Pause.Save.IsClicked || Endscreen.save.IsClicked)
            {
                for (int i = 0; i < 4; i++)
                {
                    string[] save = Game1.Db.Load(i + 1);
                    Saves[i].name = save[0];
                    Saves[i].date = save[1];
                    Saves[i].lvl = int.Parse(save[2]);
                    if (Saves[i].lvl > 0)
                    {
                        Saves[i].position = save[3];
                        Saves[i].hp = int.Parse(save[4]);
                        Saves[i].isUsed = true;
                        Saves[i].Text = Saves[i].name + "  " + Saves[i].lvl + ". lvl";
                    }


                }
            }

            if (Game1.CurrentGameState == Game1.Gamestates.Load)
            {
                foreach (SaveSlot s in Saves)
                {
                    if (s.isUsed && s.IsClicked)
                    {
                        Game1.GenerateMap=new GenerateMap(s.lvl,72);
                        Game1.Karakter.Health = s.hp;
                        string[] positions = s.position.Split(',');
                        int x = int.Parse(positions[0]);
                        int y = int.Parse(positions[1]);
                        Game1.Karakter.position = new Vector2(x, y);
                        Game1.CurrentGameState = Game1.Gamestates.Playing;
                    }
                }
            }
        }
    }
}
