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
         Camera kamera;
        public static  int Swidth, Sheight;
        public static KeyboardState Newkey = Keyboard.GetState();
        public static KeyboardState Prevkey;
        Background bg;
        static int tileSize;
        public static int TileSize => tileSize;
        public static bool exit;
        public static Gamestates CurrentGameState { get; set; }

        Texture2D szin;
        public enum Gamestates
        {
            Mainmenu,
            Playing,
            Options,
            Pause
        };

        public static GenerateMap GenerateMap;

        MouseState mouse;

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


            CurrentGameState = Gamestates.Mainmenu;

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
               szin = Content.Load<Texture2D>("grey");
         
                GenerateMap = new GenerateMap(Mapok.Palya, Mapok.Objects, tileSize);
                
                bg = new Background(Content.Load<Texture2D>("BG"), new Rectangle(0, 0, 1280, 720));

        }

       
        protected override void UnloadContent()
        {
        
        }

        
        protected override void Update(GameTime gameTime)
        {
             if (exit) Exit();
            Newkey=Keyboard.GetState();
            mouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                case Gamestates.Mainmenu:
                    MainMenu.Update(mouse);
                    break;
                case Gamestates.Playing:                    
                    if (Newkey.IsKeyDown(Keys.Escape)) CurrentGameState = Gamestates.Pause;
                    Karakter.Update(gameTime);
                    break;
                case Gamestates.Options:
                    break;
                case Gamestates.Pause: Pause.Update(mouse);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
          
        
            kamera.Update(Karakter);


            Prevkey = Newkey;
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(13,21,22));

            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,kamera.Transform);
            switch (CurrentGameState)
            {
                case Gamestates.Mainmenu:
                    MainMenu.Draw(spriteBatch);
                    break;
                case Gamestates.Playing:
                    bg.Draw(spriteBatch);
                    spriteBatch.Draw(szin, Karakter.Hitbox, Color.White);

                    GenerateMap.Draw(spriteBatch, Karakter);
                    Karakter.Draw(spriteBatch);
                    break;
                case Gamestates.Options:
                    break;
                case Gamestates.Pause: Pause.Draw(spriteBatch);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
           

            spriteBatch.End();

           
        }
    }
}
