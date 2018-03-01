using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    class Button:Sprite
    {
       
         public  bool isClicked;
            string text;
        Rectangle mouseRectangle;
        SpriteFont font;
       public  Vector2 position;
        
        Color hover;
            public Button(Texture2D t, Rectangle r, string text) : base(t, r)
            {
                isClicked = false;
                this.text = text;
             font = Game1.ContentMgr.Load<SpriteFont>("font");
            position = new Vector2(Rectangle.X+40, Rectangle.Y+20);
            hover = Color.White;
        }

            public void Update(MouseState mouse)
            {
               mouseRectangle=new Rectangle(mouse.X,mouse.Y,1,1);
             
                if (!mouseRectangle.Intersects(Rectangle))
            {
                hover = Color.White;
                return;
            }
           else {
                hover = Color.Gold;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
               

            }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(Texture, Rectangle, hover);
            sbatch.DrawString(font, text, position, Color.GhostWhite);
        }
    }
    
}
