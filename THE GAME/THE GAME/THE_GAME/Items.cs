﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    public class Items
    {
        readonly Sprite texture;
        readonly string type;
        bool visible, on;
        int elapsed;

        public Items(string type, Vector2 position)
        {
            visible = true;
            elapsed = 0;
            on = false;
            this.type = type;
            switch (type)
            {
                case "heart":
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("heart/fullheart"),
                        new Rectangle((int) position.X, (int) position.Y, 170 / 4, 150 / 4));
                    break;
                case "boots":
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("heart/boots"),
                        new Rectangle((int) position.X, (int) position.Y, 400 / 4, 400 / 4));
                    break;
                case "jump":
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("heart/rocket"),
                        new Rectangle((int) position.X, (int) position.Y, 260 / 4, 240 / 4));
                    break;
                case "end":
                    texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("objects/Object (12)"),
                        new Rectangle((int) position.X, (int) position.Y, 72, 72));
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
                        if (Game1.Karakter.Hitbox.Intersects(new Rectangle(texture.Rectangle.X, texture.Rectangle.Y,
                            texture.Rectangle.Width, texture.Rectangle.Height)))
                        {
                            Game1.Karakter.Health++;
                            visible = false;
                        }

                        break;
                    case "boots":
                        if (Game1.Karakter.Hitbox.Intersects(new Rectangle(texture.Rectangle.X, texture.Rectangle.Y,
                            texture.Rectangle.Width, texture.Rectangle.Height)))
                        {
                            Game1.Karakter.Speed = 3;
                            on = true;
                            visible = false;
                            Game1.Karakter.Color = Color.Cyan;
                        }

                        break;
                    case "jump":

                        if (Game1.Karakter.Hitbox.Intersects(new Rectangle(texture.Rectangle.X, texture.Rectangle.Y,
                            texture.Rectangle.Width, texture.Rectangle.Height)))
                        {
                            Game1.Karakter.JumpHeight = 22;
                            on = true;
                            visible = false;
                            Game1.Karakter.Color = Color.LightGreen;
                        }

                        break;
                    case "end":
                        if (Game1.Karakter.Hitbox.Intersects(new Rectangle(texture.Rectangle.X, texture.Rectangle.Y,
                            texture.Rectangle.Width, texture.Rectangle.Height)))
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
                    Game1.Karakter.Speed = 1.9f;
                    on = false;
                    Game1.Karakter.Color = Color.White;
                }

                if (type == "jump" && elapsed > 250)
                {
                    Game1.Karakter.JumpHeight = 12.5f;
                    on = false;
                    Game1.Karakter.Color = Color.White;
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
