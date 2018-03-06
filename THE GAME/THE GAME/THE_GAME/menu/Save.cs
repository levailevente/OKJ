using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    internal static class Save
    {
         static readonly SaveSlot[] Saves;
       static Texture2D gomb;
        static readonly Button Back;

        static Save()
        {
            gomb = Game1.ContentMgr.Load<Texture2D>("button");
            Saves = new SaveSlot[4];

            Saves[0] = new SaveSlot(gomb, new Rectangle(400, 50, 500, 100), "Empty slot");
            Saves[1] = new SaveSlot(gomb, new Rectangle(400, 200, 500, 100), "Empty slot");
            Saves[2] = new SaveSlot(gomb, new Rectangle(400, 350, 500, 100), "Empty slot");
            Saves[3] = new SaveSlot(gomb, new Rectangle(400, 500, 500, 100), "Empty slot");

            Back=new Button(MainMenu.Gomb,new Rectangle(575,625,150,60),"Back" );
            Back.Position.Y -= 4;
            Back.Position.X += 13;

        }

        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);

            foreach (SaveSlot s in Saves )
            {
                s.Draw(sbatch);
            }

            Back.Draw(sbatch);
        }

        public static void Update(MouseState mouse)
        {
            foreach (SaveSlot s in Saves)
            {
                s.Update(mouse);
            }

            Back.Update(mouse);

            if (Back.IsClicked)
            {
                Game1.CurrentGameState = Game1.Gamestates.Pause;
            }
        }
    }
}
