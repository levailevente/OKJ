using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME.menu
{
   static class Endscreen
   {
     public  static Button nextlvl, save;
        static Sprite gj;
       public static SpriteFont font;
       static bool end;
        static Endscreen()
        {
            nextlvl = new Button(MainMenu.Gomb, new Rectangle(560, 350, 170, 65), "Next Level");
            gj=new Sprite(Game1.ContentMgr.Load<Texture2D>("gj"),new Rectangle(570,50,392/3,512/3));
            save= new Button(MainMenu.Gomb, new Rectangle(560, 450, 170, 65), "Save");
            font = Game1.ContentMgr.Load<SpriteFont>("font2");
            save.Position.X += 22;
        }


       public static void Update(MouseState mouse)
       {
           if (end) return;
           Pause.ExitM.Update(mouse);
           nextlvl.Update(mouse);
           save.Update(mouse);

           if (Pause.ExitM.IsClicked)
           {
               Game1.CurrentGameState = Game1.Gamestates.Mainmenu;
           }

           if (save.IsClicked)
           {
               menu.Save.Update(mouse);
           }

           if (nextlvl.IsClicked)
           {
               Game1.Lvl++;
               if (Game1.Lvl != -1) Game1.GenerateMap = new GenerateMap(Game1.Lvl, 72);

               else
               {
                   end = true;
               }
           }
       }


       public static void Draw(SpriteBatch sbatch)
        {
            MainMenu.Hatter.Draw(sbatch);
            Pause.ExitM.Draw(sbatch);
            save.Draw(sbatch);
            nextlvl.Draw(sbatch);
            sbatch.DrawString(font,"Level "+Game1.Lvl+" Completed",new Vector2(520,250),Color.White);
            gj.Draw(sbatch);

            if (end)
            {
                MainMenu.Hatter.Draw(sbatch);
                sbatch.DrawString(font, "  Created by \n Lévai Levente", new Vector2(550, 300), Color.White);
            }
        }
    }
}
