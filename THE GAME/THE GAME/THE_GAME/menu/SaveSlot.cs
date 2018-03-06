using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME.menu
{
    class SaveSlot : Button
    {
        DateTime date;
        public SaveSlot(Texture2D t, Rectangle r, string text) : base(t, r, text)
        {
            date = new DateTime();
        }


        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.DrawString(font, date.ToString(), Position, Color.GhostWhite);
            base.Draw(sbatch);
        }
    }
}
