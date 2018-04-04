using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class Items
    {
        readonly Sprite texture;
        readonly string type;
        bool visible, on, open;
        int elapsed;
        Rectangle hitbox;
        public Items(string type, Vector2 position)
        {
            visible = true;
            elapsed = 0;
            on = false;
            open = false;
            this.type = type;
            switch (type)
            {
                case "box":
                    hitbox= new Rectangle((int)position.X, (int)position.Y, 72, 72);
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("heart/Object (8)"), hitbox);
                    visible = false;
                    break;
                case "heart":
                    hitbox = new Rectangle((int)position.X, (int)position.Y, 170 / 4, 150 / 4);
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

                    case "box":
                        if (hitbox.Intersects(Game1.Character.HitboxA) && Game1.Character.IsAttack && Game1.Character.AttackI > 2 && Game1.Character.AttackI < 5)
                        {
                            
                        }

                        break;

                    case "heart":
                        if (Game1.Character.Hitbox.Intersects(hitbox))
                        {
                            Game1.Character.Health++;
                            visible = false;
                        }

                        break;
                    case "boots":
                        if (Game1.Character.Hitbox.Intersects(hitbox))
                        {
                            Game1.Character.Speed = 3;
                            on = true;
                            visible = false;
                            Game1.Character.Color = Color.Cyan;
                        }

                        break;
                    case "jump":

                        if (Game1.Character.Hitbox.Intersects(hitbox))
                        {
                            Game1.Character.JumpHeight = 22;
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

                            if (i != Game1.Enemies.Count) Game1.CurrentGameState = Game1.Gamestates.EndScreen;
                        }

                        break;
                }
            }

            if (on)
            {
                elapsed++;
                if (type == "boots" && elapsed > 250)
                {
                    Game1.Character.Speed = 1.9f;
                    on = false;
                    Game1.Character.Color = Color.White;
                }

                if (type == "jump" && elapsed > 250)
                {
                    Game1.Character.JumpHeight = 12.5f;
                    on = false;
                    Game1.Character.Color = Color.White;
                }
            }

            if (elapsed > 1000) visible = true;



        }

        public void Draw(SpriteBatch sbatch)
        {
            if (visible) texture.Draw(sbatch);
        }
    }
}
