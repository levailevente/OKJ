using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    internal class SaveSlot : Button
    {
        DateTime date;
        bool isUsed;
        public bool nameInput;
        readonly Vector2 datePosition;
        readonly Button save, back, textbox;
        readonly Sprite box;
        string name;
         Keys[] letter;
        Vector2 namePos;
        Regex r= new Regex("^[a-zA-Z0-9]*$");

        public SaveSlot(Texture2D t, Rectangle r, string text) : base(t, r, text)
        {
            Texture2D asd = Game1.ContentMgr.Load<Texture2D>("textbox");
            box = new Sprite(Game1.ContentMgr.Load<Texture2D>("box"), new Rectangle(420, 200, 450, 300));
            isUsed = false;
            datePosition = Position;
            datePosition.Y += 30;
            save = new Button(MainMenu.Gomb, new Rectangle(485, 400, 145, 63), "Save");
            back = new Button(MainMenu.Gomb, new Rectangle(675, 400, 145, 63), "Back");
            textbox = new Button(asd, new Rectangle(570, 280, 372 / 2, 110 / 2), "Name: ");
            textbox.Position.X -= 110;
            textbox.Position.Y -= 3;
            save.Position.Y -= 3;
            save.Position.X += 5;
            back.Position.Y -= 3;
            back.Position.X += 5;
            nameInput = false;
            name = "";
            namePos = new Vector2(textbox.Position.X, textbox.Position.Y);
            namePos.X += 80;
        }

        public override void Update(MouseState mouse)
        {

            switch (Game1.CurrentGameState)
            {
                case Game1.Gamestates.Load:
                    if (IsClicked) Game1.CurrentGameState = Game1.Gamestates.Textbox;
                    base.Update(mouse);
                    nameInput = false;
                    break;
                case Game1.Gamestates.Save:
                    if (IsClicked) Game1.CurrentGameState = Game1.Gamestates.Textbox;
                    base.Update(mouse);
                    nameInput = false;
                    break;
                case Game1.Gamestates.Textbox:
                    nameInput = true;
                    save.Update(mouse);
                    back.Update(mouse);
                    NameUpdate();
                    if (save.IsClicked)
                    {
                        isUsed = true;
                        date = DateTime.Now;
                        Text = "Adott szint";
                        name = "";
                        Game1.CurrentGameState = Game1.Gamestates.Save;
                    }

                    if (back.IsClicked)
                    {
                        name = "";
                        Game1.CurrentGameState = Game1.Gamestates.Save;
                    }

                    break;
            }

        }

        public override void Draw(SpriteBatch sbatch)
        {

            if (!isUsed)
            {

                sbatch.Draw(Texture, Rectangle, Hover * 0.5f);
                sbatch.DrawString(Font, "Empty Slot", Position, Color.GhostWhite);

            }
            else
            {
                sbatch.Draw(Texture, Rectangle, Hover * 0.5f);
                sbatch.DrawString(Font, date.ToString(CultureInfo.CurrentCulture), datePosition, Color.GhostWhite);
                sbatch.DrawString(Font, Text, Position, Color.GhostWhite);
            }

            if (Game1.CurrentGameState == Game1.Gamestates.Textbox)
            {
                box.DrawC(sbatch, Color.DarkSeaGreen);
                save.Draw(sbatch);
                back.Draw(sbatch);
                textbox.Draw(sbatch);
                sbatch.DrawString(Font,name,namePos, Color.Black);
            }
        }

        void NameUpdate()
        {
          letter= Game1.Newkey.GetPressedKeys();
            if (letter.Length > 0 && Game1.Prevkey.GetPressedKeys().Length == 0 && name.Length<10)
            {
                string value = letter[0].ToString();
                if (value.Length==1) name += value.ToLower();
                
            }
        }

}

}

