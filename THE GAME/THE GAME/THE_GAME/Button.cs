using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    internal class Button : Sprite
    {
        public bool IsClicked;
        public string Text;
        Rectangle mouseRectangle;
        protected static SpriteFont Font;
        public Vector2 Position;
        static int elapsed;
        protected Color Hover;

        public Button(Texture2D t, Rectangle r, string text) : base(t, r)
        {
            elapsed = 0;
            IsClicked = false;
            Text = text;
            Font = Game1.ContentMgr.Load<SpriteFont>("font");
            Position = new Vector2(Rectangle.X + 40, Rectangle.Y + 20);
            Hover = Color.White;
        }

        public virtual void Update(MouseState mouse)
        {
            elapsed++;
            IsClicked = false;
            mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (!mouseRectangle.Intersects(Rectangle))
            {
                Hover = Color.White;

            }
            else
            {
                Hover = Color.Gold;
                if (mouse.LeftButton != ButtonState.Pressed || Game1.Prevmouse.LeftButton != ButtonState.Released ||
                    elapsed <= 60) return;
                IsClicked = true;
                elapsed = 0;
            }

        }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(Texture, Rectangle, Hover);
            sbatch.DrawString(Font, Text, Position, Color.GhostWhite);
        }

        public void Draw2(SpriteBatch sbatch, float f)
        {
            sbatch.Draw(Texture, Rectangle, Hover * f);
            sbatch.DrawString(Font, Text, Position, Color.White);
        }
    }

}
