using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THE_GAME
{
    class Button:Sprite
    {
       
            bool isClicked;
            string text;
            public Button(Texture2D t, Rectangle r, string text) : base(t, r)
            {
                isClicked = false;
                this.text = text;
            }

            public void Update(Rectangle mouse)
            {

            }
    }
    
}
