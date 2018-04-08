using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    static class HealthBar
    {
        static readonly Texture2D FullH;
        static readonly Texture2D HalfH;
        static readonly Texture2D EmptyH;

        static Rectangle rectangle1;
        static Rectangle rectangle2;
        static Rectangle rectangle3;

        static HealthBar()
        {
            FullH = Game1.ContentMgr.Load<Texture2D>("heart/fullheart");
            HalfH = Game1.ContentMgr.Load<Texture2D>("heart/halfheart");
            EmptyH = Game1.ContentMgr.Load<Texture2D>("heart/emptyheart");


        }


        public static void Draw(SpriteBatch sbatch, int hp)
        {
            rectangle1 = new Rectangle((int)Game1.Kamera.Centre.X + 20, (int)Game1.Kamera.Centre.Y + 20, 170 / 3,
                150 / 3);
            rectangle2 = new Rectangle((int)Game1.Kamera.Centre.X + 70, (int)Game1.Kamera.Centre.Y + 20, 170 / 3,
                150 / 3);
            rectangle3 = new Rectangle((int)Game1.Kamera.Centre.X + 120, (int)Game1.Kamera.Centre.Y + 20, 170 / 3,
                150 / 3);

            switch (hp)
            {
                case 0:
                    sbatch.Draw(EmptyH, rectangle1, Color.White);
                    sbatch.Draw(EmptyH, rectangle2, Color.White);
                    sbatch.Draw(EmptyH, rectangle3, Color.White);
                    break;
                case 1:
                    sbatch.Draw(HalfH, rectangle1, Color.White);
                    sbatch.Draw(EmptyH, rectangle2, Color.White);
                    sbatch.Draw(EmptyH, rectangle3, Color.White);
                    break;
                case 2:
                    sbatch.Draw(FullH, rectangle1, Color.White);
                    sbatch.Draw(EmptyH, rectangle2, Color.White);
                    sbatch.Draw(EmptyH, rectangle3, Color.White);
                    break;
                case 3:
                    sbatch.Draw(FullH, rectangle1, Color.White);
                    sbatch.Draw(HalfH, rectangle2, Color.White);
                    sbatch.Draw(EmptyH, rectangle3, Color.White);
                    break;
                case 4:
                    sbatch.Draw(FullH, rectangle1, Color.White);
                    sbatch.Draw(FullH, rectangle2, Color.White);
                    sbatch.Draw(EmptyH, rectangle3, Color.White);
                    break;
                case 5:
                    sbatch.Draw(FullH, rectangle1, Color.White);
                    sbatch.Draw(FullH, rectangle2, Color.White);
                    sbatch.Draw(HalfH, rectangle3, Color.White);
                    break;
                case 6:
                    sbatch.Draw(FullH, rectangle1, Color.White);
                    sbatch.Draw(FullH, rectangle2, Color.White);
                    sbatch.Draw(FullH, rectangle3, Color.White);
                    break;
            }
        }
    }
}
