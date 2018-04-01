using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using THE_GAME.menu;

namespace THE_GAME
{

    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static ContentManager ContentMgr;
        public static Karakter Karakter;
        public static Camera Kamera;
        public static int Swidth, Sheight;
        public static KeyboardState Newkey;
        public static KeyboardState Prevkey;
        Background bg;
        static int tileSize;
        public static int TileSize => tileSize;
        public static bool exit;
        public static Gamestates CurrentGameState { get; set; }
        public static Texture2D szin;
        public static bool Godmode;

        public static int Lvl
        {
            get
            {
                if (lvl > 1) return -1;
                return lvl;
            }
            set { lvl = value; }
        }

        public static readonly List<Karakter> Enemies = new List<Karakter>();
        public static readonly List<Spike> Spikes = new List<Spike>();
        public static readonly List<Items> Items = new List<Items>();
        public static bool Fullscreen;
        static int lvl;

        public enum Gamestates
        {
            Mainmenu,
            Playing,
            Options,
            OptionsIg,
            Pause,
            Save,
            Load,
            GameOver,
            EndScreen,
            EndSave
        };

        public static GenerateMap GenerateMap;

        public static MouseState Newmouse, Prevmouse;

        public Game1()
        {
            IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this)


            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };



            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            tileSize = 72;

            ContentMgr = Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Kamera = new Camera();

            Karakter = new Karakter();

            Swidth = GraphicsDevice.Viewport.Width;
            Sheight = GraphicsDevice.Viewport.Height;

            Fullscreen = false;

            Lvl = 1;

            Godmode = false;

            CurrentGameState = Gamestates.Mainmenu;

            base.Initialize();
        }


        protected override void LoadContent()
        {
            szin = Content.Load<Texture2D>("grey");

            bg = new Background(Content.Load<Texture2D>("BG"), new Rectangle(0, 720, 1280, 720));

        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (exit) Exit();
            Newkey = Keyboard.GetState();
            Newmouse = Mouse.GetState();

            if (Newkey.IsKeyDown(Keys.F1) && Prevkey.IsKeyUp(Keys.F1))
            {
                Godmode = !Godmode;
            } 

            switch (CurrentGameState)
            {
                case Gamestates.Mainmenu:
                    MainMenu.Update(Newmouse);
                    break;
                case Gamestates.Playing:
                    if (Newkey.IsKeyDown(Keys.Escape)) CurrentGameState = Gamestates.Pause;

                    foreach (Spike k in Spikes)
                    {
                        k.Update();
                    }

                    Karakter.Update(gameTime);
                    foreach (Karakter k in Enemies)
                    {
                        k.Update(gameTime);
                    }

                    foreach (Items k in Items)
                    {
                        k.Update();
                    }

                    break;
                case Gamestates.Options:
                    Options.Update(Newmouse);

                    if (Options.Apply.IsClicked && Fullscreen != graphics.IsFullScreen)
                    {
                        graphics.ToggleFullScreen();
                    }

                    break;
                case Gamestates.OptionsIg:
                    Options.Update(Newmouse);
                    if (Options.Apply.IsClicked && Fullscreen != graphics.IsFullScreen)
                    {
                        graphics.ToggleFullScreen();
                    }

                    break;
                case Gamestates.Pause:
                    Pause.Update(Newmouse);
                    break;
                case Gamestates.Save:
                    menu.Save.Update(Newmouse);
                    break;
                case Gamestates.Load:
                    menu.Save.Update(Newmouse);
                    break;
                case Gamestates.GameOver:
                    menu.Gameover.Update(Newmouse);
                    break;
                case Gamestates.EndScreen:
                    menu.EndScreen.Update(Newmouse);
                    break;
                case Gamestates.EndSave:
                    menu.Save.Update(Newmouse);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            Kamera.Update(Karakter);


            Prevkey = Newkey;
            Prevmouse = Newmouse;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(26, 29, 30));

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Kamera.Transform);
            switch (CurrentGameState)
            {
                case Gamestates.Mainmenu:
                    MainMenu.Draw(spriteBatch);
                    IsMouseVisible = true;
                    break;
                case Gamestates.Playing:
                    bg.Draw(spriteBatch);

                   // spriteBatch.Draw(szin, Karakter.Hitbox, Color.White);
                    IsMouseVisible = false;
                    foreach (Spike k in Spikes)
                    {
                        k.Draw(spriteBatch);
                    }

                    GenerateMap.Draw(spriteBatch, Karakter);
                    Karakter.Draw(spriteBatch);
                    spriteBatch.DrawString(Options.Font, "Level " + lvl,
                        new Vector2(Kamera.Centre.X + 1150, Kamera.Centre.Y + 10), Color.White * 0.5f);
                    foreach (Items k in Items)
                    {
                        k.Draw(spriteBatch);
                    }

                    foreach (Karakter k in Enemies)
                    {
                        k.Draw(spriteBatch);
                    }

                    HealthBar.Draw(spriteBatch, Karakter.Health);
                    break;
                case Gamestates.Options:
                    Options.Draw(spriteBatch);
                    break;
                case Gamestates.OptionsIg:
                    Options.Draw(spriteBatch);
                    break;
                case Gamestates.Pause:
                    IsMouseVisible = true;
                    Pause.Draw(spriteBatch);
                    break;
                case Gamestates.Save:
                    menu.Save.Draw(spriteBatch);
                    break;
                case Gamestates.Load:
                    menu.Save.Draw(spriteBatch);
                    break;
                case Gamestates.GameOver:
                    IsMouseVisible = true;
                    menu.Gameover.Draw(spriteBatch);
                    break;
                case Gamestates.EndScreen:
                    IsMouseVisible = true;
                    menu.EndScreen.Draw(spriteBatch);
                    break;
                case Gamestates.EndSave:
                    menu.Save.Draw(spriteBatch);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            spriteBatch.End();


        }
    }
}
