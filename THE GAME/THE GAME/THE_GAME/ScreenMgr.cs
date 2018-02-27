using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    class ScreenMgr
    {
        class Button:Sprite
        {
            bool isClicked;
            string text;
            protected Button(Texture2D t, Rectangle r,string text) : base(t, r)
            {
                isClicked = false;
                this.text = text;
            }

            public override void Update()
            {
                
            }
        }
        enum Gamestates
        {
            Mainmenu,
            Playing,
            Options
     };

        Gamestates currentGameState = Gamestates.Mainmenu;

        public void Update()
        {
            switch (currentGameState)
            {
                    
            }
        }

       
    }
}
