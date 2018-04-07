using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class Items
    {
        Sprite texture;
        readonly string type;
        bool visible, on, respawn;
        int elapsed;
        Rectangle hitbox;
        public Items(string type, Vector2 position)
        {
            visible = true;
            elapsed = 0;
            on = false;
            respawn = false;
            this.type = type;

            
            switch (type)
            {

                case "heart":
                    hitbox = new Rectangle((int)position.X, (int)position.Y+20, 170 / 4, 150 / 4);
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("heart/fullheart"), hitbox);
                    break;
                case "boots":
                    hitbox = new Rectangle((int)position.X, (int)position.Y, 400 / 4, 400 / 4);
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("heart/boots"), hitbox);
                    break;
                case "jump":
                    hitbox = new Rectangle((int)position.X, (int)position.Y, 260 / 4, 240 / 4);
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("heart/rocket"), hitbox);
                    break; 

                case "end":
                    hitbox = new Rectangle((int)position.X, (int)position.Y, 72, 72);
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("objects/Object (12)"), hitbox);
                    break;

            }

        }

        public void Update()
        {
            if (visible)
            {
        
                    switch (type)
                    {


                        case "heart":
                           
                            if (Game1.Character.Hitbox.Intersects(hitbox))
                            {
                                Game1.Character.Health++;
                                visible = false;
                                on = true;
                            }

                            break;
                        case "boots":
                      
                            if (Game1.Character.Hitbox.Intersects(hitbox))
                            {
                                Game1.Character.Speed = 3.5f;
                                on = true;
                                visible = false;
                                Game1.Character.Color = Color.Cyan;
                            }

                            break;
                        case "jump":
                         
                            if (Game1.Character.Hitbox.Intersects(hitbox))
                            {
                                Game1.Character.JumpHeight = 30f;
                                on = true;
                                visible = false;
                                Game1.Character.Color = Color.LightGreen;
                            }

                            break;
                        case "end":

                            if (Game1.Character.Hitbox.Intersects(hitbox))
                            {
                                int i = 0;
                                while (i < Game1.Enemies.Count && Game1.Enemies[i].IsDead)
                                {
                                    i++;
                                }

                                if (i == Game1.Enemies.Count) Game1.CurrentGameState = Game1.Gamestates.EndScreen;
                            }

                            break;
                    }
                
            }

            if (on)
            {
                elapsed++;
                if (type == "boots" && elapsed > 250 &&!respawn)
                {
                    Game1.Character.Speed = 2;
                    Game1.Character.Color = Color.White;
                    respawn = true;
                }

                if (type == "jump" && elapsed > 250 && !respawn)
                {
                    Game1.Character.JumpHeight = 15f;
                    Game1.Character.Color = Color.White;
                    respawn = true;

                }

                if (type == "heart" && !respawn)
                {
                    respawn = true;
                }

                if (respawn && elapsed > 1500)
                {
                    visible = true;
                    elapsed = 0;
                    on = false;
                }
            }


        }

        public void Draw(SpriteBatch sbatch)
        {
            if (!visible) return;
                      
                texture.Draw(sbatch);
            
        }
    }
}
