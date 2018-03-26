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
       public string date;
        public bool isUsed;
        public bool NameInput;
        readonly Vector2 datePosition;
        public readonly Button save, back, textbox;
        readonly Sprite box;
        public string name;
        Keys[] letter;
        public int hp;
        public int lvl;
        public string position;
        readonly Vector2 namePos;
        Regex r= new Regex("^[a-zA-Z0-9]*$");

        public SaveSlot(Texture2D t, Rectangle r, string text) : base(t, r, text)
        {
            Texture2D asd = Game1.ContentMgr.Load<Texture2D>("textbox");
            box = new Sprite(Game1.ContentMgr.Load<Texture2D>("popup"), new Rectangle(420, 200, 450, 300));
            isUsed = false;
            datePosition = Position;
            datePosition.Y += 30;
            save = new Button(MainMenu.Gomb, new Rectangle(490, 400, 145, 60), "Save");
            back = new Button(MainMenu.Gomb, new Rectangle(670, 400, 145, 60), "Back");
            textbox = new Button(asd, new Rectangle(570, 280, 372 / 2, 110 / 2), "Name: ");
            textbox.Position.X -= 110;
            textbox.Position.Y -= 3;
            save.Position.Y -= 3;
            save.Position.X += 5;
            back.Position.Y -= 3;
            back.Position.X += 8;
            NameInput = false;
            name = "";
            namePos = new Vector2(textbox.Position.X, textbox.Position.Y);
            namePos.X += 80;
            
        }

        public override void Update(MouseState mouse)
        {

            base.Update(mouse);
            if (IsClicked && Game1.CurrentGameState==Game1.Gamestates.Save)
            {
                NameInput = true;
            }
        }

        public override void Draw(SpriteBatch sbatch)
        {

                sbatch.Draw(Texture, Rectangle, Hover * 0.5f);
     if (isUsed)sbatch.DrawString(Font, date, datePosition, Color.GhostWhite);
                sbatch.DrawString(Font, Text, Position, Color.GhostWhite);
            

        }

        void NameUpdate()
        {
            letter= Game1.Newkey.GetPressedKeys();
            if (letter.Length > 0  && name.Length<15 && Game1.Prevkey.GetPressedKeys().Length == 0 || Game1.Newkey.IsKeyDown(Keys.Back) && Game1.Prevkey.GetPressedKeys().Length == 0)
            {
                                   
                       
                        string value = letter[0].ToString();
                        if (value.Length == 1) name += value.ToLower();
                        if (Game1.Newkey.IsKeyDown(Keys.Back))
                        {
                            if (name.Length > 0) name = name.Remove(name.Length - 1, 1);
                        }
                               
            }
        }

      public  void Textbox(MouseState mouse)
      {
            save.Update(mouse);
            back.Update(mouse);
            NameUpdate();
            if (save.IsClicked)
            {
                NameInput = false;
                isUsed = true;
                date = DateTime.Now.ToString();
                Text = name+"  "+Game1.Lvl+". lvl";              
                
            }

            if (back.IsClicked)
            {
                NameInput = false;
                name = "";              
            }
        }
        public void DrawTextbox(SpriteBatch sbatch)
        {
            box.DrawC(sbatch, Color.White);
            save.Draw(sbatch);
            back.Draw(sbatch);
            textbox.Draw(sbatch);
            sbatch.DrawString(Font, name, namePos, Color.Black);
        }

    }

}

