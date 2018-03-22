using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
    public static class Gameover
    {
        static Sprite gameover;
        static Button restart;
        static Gameover()
        {
                gameover=new Sprite(Game1.ContentMgr.Load<Texture2D>("Game_Over"),new Rectangle(400,300,3508/10,2480/10));
            restart = MainMenu.Newgame;
            restart.Text = "Restart";
        }

        public static void Update(MouseState mouse)
        {
            Pause.ExitM.Update(mouse);
            restart.Update(mouse);


        }

        public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);
            gameover.Draw(sbatch);
            restart.Draw(sbatch);
            Pause.ExitM.Draw(sbatch);
        }
    }
}
