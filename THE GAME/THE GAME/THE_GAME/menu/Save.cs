using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    static class Save
    {
        //static readonly SaveSlot[] Saves;
        static readonly Button Back;
       static SaveSlot Save1, Save2, Save3, Save4;
        
        static Save()
        {
            Texture2D gomb = Game1.ContentMgr.Load<Texture2D>("button");
            //Saves = new SaveSlot[4];

             Save1 = new SaveSlot(gomb, new Rectangle(400, 50, 500, 100), "sadfdsfsdlot");
             Save2 = new SaveSlot(gomb, new Rectangle(400, 200, 500, 100), "Emkkkpty slot");
             Save3 = new SaveSlot(gomb, new Rectangle(400, 350, 500, 100), "Empty slot");
             Save4 = new SaveSlot(gomb, new Rectangle(400, 500, 500, 100), "Empkkkkty slot");

            Back=new Button(MainMenu.Gomb,new Rectangle(575,625,150,60),"Back" );
            Back.Position.Y -= 4;
            Back.Position.X += 13;
        }

        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);

            Save1.Draw(sbatch);
            Save2.Draw(sbatch);
            Save3.Draw(sbatch);
            Save4.Draw(sbatch);

            Back.Draw(sbatch);
        }

        public static void Update(MouseState mouse)
        {

       

                Save1.Update(mouse);
                Save2.Update(mouse);
                Save3.Update(mouse);
                Save4.Update(mouse);



                Back.Update(mouse);
                    if (Back.IsClicked) Game1.CurrentGameState = Game1.CurrentGameState == Game1.Gamestates.Load ? Game1.Gamestates.Mainmenu : Game1.Gamestates.Pause;
                
            

       
        }
    }
}
