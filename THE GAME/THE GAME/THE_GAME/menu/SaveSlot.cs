using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    internal class SaveSlot : Button
    {
        DateTime date;
        bool isUsed;
        readonly Vector2 datePosition;
        public SaveSlot(Texture2D t, Rectangle r, string text) : base(t, r, text)
        {
            isUsed = false;
            datePosition = Position;
            datePosition.Y += 30;
        }

       public override void Update(MouseState mouse)
        {
            base.Update(mouse);
            if (IsClicked)
            {
                isUsed = true;
                date = DateTime.Now;
                Text = "Adott szint";

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
                sbatch.DrawString(Font, date.ToString(), datePosition, Color.GhostWhite);
                sbatch.DrawString(Font, Text, Position, Color.GhostWhite);
            }
            

           
        }

        
    }
}
