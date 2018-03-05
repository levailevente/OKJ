using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    internal class Button:Sprite
    {
        public bool IsClicked;
        readonly string text;
        Rectangle mouseRectangle;
        readonly SpriteFont font;
        public Vector2 Position;
        
        Color hover;
            public Button(Texture2D t, Rectangle r, string text) : base(t, r)
            {
                IsClicked = false;
                this.text = text;
             font = Game1.ContentMgr.Load<SpriteFont>("font");
            Position = new Vector2(Rectangle.X+40, Rectangle.Y+20);
            hover = Color.White;
        }

            public void Update(MouseState mouse)
            {
               IsClicked = false;
               mouseRectangle=new Rectangle(mouse.X,mouse.Y,1,1);
             
                if (!mouseRectangle.Intersects(Rectangle))
            {
                hover = Color.White;

            }
           else
                {
                    hover = Color.Gold;
                    if (mouse.LeftButton == ButtonState.Pressed) IsClicked = true;
                }

            }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(Texture, Rectangle, hover);
            sbatch.DrawString(font, text, Position, Color.GhostWhite);
        }
    }
    
}
