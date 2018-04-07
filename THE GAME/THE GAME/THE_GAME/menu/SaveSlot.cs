using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    internal class SaveSlot : Button
    {
        public string Date;
        public bool IsUsed;
        public bool NameInput;
        readonly Vector2 datePosition;
        public readonly Button Save, Back, Textbox;
        readonly Sprite box;
        public string Name;
        Keys[] letter;
        public int Hp;
        public int Lvl;
        public string PositionString;
        readonly Vector2 namePos;
        Regex r = new Regex("^[a-zA-Z0-9]*$");

        public SaveSlot(Texture2D t, Rectangle r, string text) : base(t, r, text)
        {
            Texture2D asd = Game1.ContentMgr.Load<Texture2D>("textbox");
            box = new Sprite(Game1.ContentMgr.Load<Texture2D>("popup"), new Rectangle(420, 200, 450, 300));
            IsUsed = false;
            datePosition = Position;
            datePosition.Y += 30;
            Save = new Button(MainMenu.Gomb, new Rectangle(490, 400, 145, 60), "Save");
            Back = new Button(MainMenu.Gomb, new Rectangle(670, 400, 145, 60), "Back");
            Textbox = new Button(asd, new Rectangle(570, 280, 372 / 2, 110 / 2), "Name: ");
            Textbox.Position.X -= 110;
            Textbox.Position.Y -= 3;
            Save.Position.Y -= 3;
            Save.Position.X += 5;
            Back.Position.Y -= 3;
            Back.Position.X += 8;
            NameInput = false;
            Name = "";
            namePos = new Vector2(Textbox.Position.X, Textbox.Position.Y);
            namePos.X += 80;

        }

        public override void Update(MouseState mouse)
        {

            base.Update(mouse);
            if (IsClicked && Game1.CurrentGameState == Game1.Gamestates.Save)
            {
                NameInput = true;
            }
        }

        public override void Draw(SpriteBatch sbatch)
        {

            sbatch.Draw(Texture, Rectangle, Hover * 0.5f);
            if (IsUsed) sbatch.DrawString(Font, Date, datePosition, Color.GhostWhite);
            sbatch.DrawString(Font, Text, Position, Color.GhostWhite);


        }

        void NameUpdate()
        {
            letter = Game1.Newkey.GetPressedKeys();


        if (letter.Length > 0 && Name.Length < 15 && Game1.Prevkey.GetPressedKeys().Length == 0 || Game1.Newkey.IsKeyDown(Keys.Back) && Game1.Prevkey.GetPressedKeys().Length == 0)
            {

              string value = letter[0].ToString();
                if (value.Length == 1) Name += value.ToLower();
                if (Game1.Newkey.IsKeyDown(Keys.Back))
                {
                    if (Name.Length > 0) Name = Name.Remove(Name.Length - 1, 1);
                }

            }

        }

        public void TextboxUpdate(MouseState mouse)
        {
            Save.Update(mouse);
            Back.Update(mouse);
            NameUpdate();
            if (Save.IsClicked)
            {
                NameInput = false;
                IsUsed = true;
                Date = DateTime.Now.ToString(CultureInfo.CurrentCulture);
                Text = Name + "  " + Game1.Lvl + ". lvl";

            }

            if (Back.IsClicked)
            {
                NameInput = false;
                Name = "";
            }
        }

        public void DrawTextbox(SpriteBatch sbatch)
        {
            box.DrawC(sbatch, Color.White);
            Save.Draw(sbatch);
            Back.Draw(sbatch);
            Textbox.Draw(sbatch);
            sbatch.DrawString(Font, Name, namePos, Color.Black);
        }

    }

}

