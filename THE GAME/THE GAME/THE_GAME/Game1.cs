using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using THE_GAME.menu;

namespace THE_GAME
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static  ContentManager ContentMgr;
        public static Karakter Karakter;
        public static Camera kamera;
        public static  int Swidth, Sheight;
        public static KeyboardState Newkey;
        public static KeyboardState Prevkey;
        public static Gamestates PrevGameState;
        Background bg;
        static int tileSize;
        public static int TileSize => tileSize;
        public static bool exit;
        public static Gamestates CurrentGameState { get; set; }

        public static int Lvl
        {
            get
            {
                if (lvl > 1) return -1;
                return lvl;
            }
            set { lvl = value; }
        }

        public static List<Karakter> Enemies=new List<Karakter>();
        public static List<Spike> Spikes = new List<Spike>();
        public static List<Items> Items = new List<Items>();
        public static bool Fullscreen;
        public static Database db;
        Texture2D szin;
        static int lvl;
        public enum Gamestates
        {
            Mainmenu,
            Playing,
            Options,
            OptionsIG,
            Pause,
            Save,
            Load,
            GameOver,
            EndScreen,
            EndSave
        };

        public static GenerateMap GenerateMap;

        public static  MouseState newmouse, prevmouse;

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
            kamera = new Camera(graphics.GraphicsDevice.Viewport);

            Karakter = new Karakter();

            Swidth = GraphicsDevice.Viewport.Width;
            Sheight = GraphicsDevice.Viewport.Height;

            Fullscreen = false;

            db=new Database();

            Lvl = 1;

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
            Newkey=Keyboard.GetState();
            newmouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                case Gamestates.Mainmenu:
                    MainMenu.Update(newmouse);
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
                case Gamestates.Options:Options.Update(newmouse);
                    
                    if (Options.apply.IsClicked&& Fullscreen!=graphics.IsFullScreen)
                    {
                      graphics.ToggleFullScreen();
                    }
                    
                    break;
                case Gamestates.OptionsIG:
                    Options.Update(newmouse);
                    if (Options.apply.IsClicked && Fullscreen != graphics.IsFullScreen)
                    {
                        graphics.ToggleFullScreen();
                    }
                    break;
                case Gamestates.Pause:
                    Pause.Update(newmouse);
                    break;
                case Gamestates.Save:
                    menu.Save.Update(newmouse);
                    break;
                case Gamestates.Load:
                    menu.Save.Update(newmouse);
                    break;
                case Gamestates.GameOver:
                    menu.Gameover.Update(newmouse);
                    break;
                case Gamestates.EndScreen:
                    menu.Endscreen.Update(newmouse);
                    break;
                case Gamestates.EndSave:
                    menu.Save.Update(newmouse);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            kamera.Update(Karakter);

         /*   if (Karakter.IsDead)
            {
                Karakter = new Karakter();
                kamera = new Camera(graphics.GraphicsDevice.Viewport);
            }*/

            Prevkey = Newkey;
            prevmouse = newmouse;
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(26,29,30));

            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,kamera.Transform);
            switch (CurrentGameState)
            {
                case Gamestates.Mainmenu:
                    MainMenu.Draw(spriteBatch);
                    break;
                case Gamestates.Playing:
                    bg.Draw(spriteBatch);
                    // spriteBatch.Draw(szin, Karakter.HitboxA, Color.White);

                    foreach (Spike k in Spikes)
                    {
                        k.Draw(spriteBatch);
                    }
                    GenerateMap.Draw(spriteBatch, Karakter);
                    Karakter.Draw(spriteBatch);

                    foreach (Items k in Items)
                    {
                        k.Draw(spriteBatch);
                    }

                    foreach (Karakter k in Enemies)
                    {
                        k.Draw(spriteBatch);
                    }


                    HealthBar.Draw(spriteBatch,Karakter.Health);
                    break;
                case Gamestates.Options: Options.Draw(spriteBatch);
                    break;
                case Gamestates.OptionsIG:
                    Options.Draw(spriteBatch);
                    break;
                case Gamestates.Pause:
                    Pause.Draw(spriteBatch);
                    break;
                case Gamestates.Save:
                    menu.Save.Draw(spriteBatch);
                    break;
                case Gamestates.Load:
                    menu.Save.Draw(spriteBatch);
                    break;
                case Gamestates.GameOver:
                    menu.Gameover.Draw(spriteBatch);
                    break;
                case Gamestates.EndScreen:
                    menu.Endscreen.Draw(spriteBatch);
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
