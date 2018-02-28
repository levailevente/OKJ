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
            public Button(Texture2D t, Rectangle r, string text) : base(t, r)
            {
                isClicked = false;
                this.text = text;
            }

            public void Update(MouseState mouse)
            {
               mouseRectangle=new Rectangle(mouse.X,mouse.Y,1,1);

                if (!mouseRectangle.Intersects(Rectangle)) return;
                if (mouse.LeftButton==ButtonState.Pressed) isClicked = true;

            }
    }
    
}
