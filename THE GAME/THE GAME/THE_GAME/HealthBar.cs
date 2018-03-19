using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    static class HealthBar
    {
        static Texture2D fullH;
        static Texture2D halfH;
        static Texture2D emptyH;

        static Rectangle rectangle1;
        static Rectangle rectangle2;
        static Rectangle rectangle3;

        static HealthBar()
        {
            fullH = Game1.ContentMgr.Load<Texture2D>("heart/fullheart");    
            halfH = Game1.ContentMgr.Load<Texture2D>("heart/halfheart");   
            emptyH = Game1.ContentMgr.Load<Texture2D>("heart/emptyheart");  

           
        }


      public static void Draw(SpriteBatch sbatch,int hp)
        {
            rectangle1 = new Rectangle((int)Game1.kamera.centre.X+20, (int)Game1.kamera.centre.Y+20, 170 / 3, 150 / 3);
            rectangle2 = new Rectangle((int)Game1.kamera.centre.X + 70, (int)Game1.kamera.centre.Y + 20, 170 / 3, 150 / 3);
            rectangle3 = new Rectangle((int)Game1.kamera.centre.X + 120, (int)Game1.kamera.centre.Y + 20, 170 / 3, 150 / 3);

            switch (hp)
            {
                case 0:
                    sbatch.Draw(emptyH,rectangle1,Color.White);
                    sbatch.Draw(emptyH, rectangle2, Color.White);
                    sbatch.Draw(emptyH, rectangle3, Color.White);
                    break; 
                case 1:
                    sbatch.Draw(halfH, rectangle1, Color.White);
                    sbatch.Draw(emptyH, rectangle2, Color.White);
                    sbatch.Draw(emptyH, rectangle3, Color.White);
                    break; 
                case 2:
                    sbatch.Draw(fullH, rectangle1, Color.White);
                    sbatch.Draw(emptyH, rectangle2, Color.White);
                    sbatch.Draw(emptyH, rectangle3, Color.White);
                    break;
                case 3:
                    sbatch.Draw(fullH, rectangle1, Color.White);
                    sbatch.Draw(halfH, rectangle2, Color.White);
                    sbatch.Draw(emptyH, rectangle3, Color.White);
                    break;
                case 4:
                    sbatch.Draw(fullH, rectangle1, Color.White);
                    sbatch.Draw(fullH, rectangle2, Color.White);
                    sbatch.Draw(emptyH, rectangle3, Color.White);
                    break;
                case 5:
                    sbatch.Draw(fullH, rectangle1, Color.White);
                    sbatch.Draw(fullH, rectangle2, Color.White);
                    sbatch.Draw(halfH, rectangle3, Color.White);
                    break;
                case 6:
                    sbatch.Draw(fullH, rectangle1, Color.White);
                    sbatch.Draw(fullH, rectangle2, Color.White);
                    sbatch.Draw(fullH, rectangle3, Color.White);
                    break;
            }
        }
    }
}
