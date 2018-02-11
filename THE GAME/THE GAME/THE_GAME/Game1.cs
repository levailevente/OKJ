using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace THE_GAME
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static  ContentManager ContentMgr;
        static Karakter karakter;
        static Sprite   hatter;
        Camera kamera;
        public static  int Swidth, Sheight;
        public static KeyboardState Newkey = Keyboard.GetState();
        public static KeyboardState prevkey;

 
       public static Map Map;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };


            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            ContentMgr = Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            kamera = new Camera(graphics.GraphicsDevice.Viewport);
            karakter = new Karakter();
            hatter = new Sprite(Content.Load<Texture2D>("BG"), new Rectangle(0, 0, 1280, 720));
           
            Swidth = GraphicsDevice.Viewport.Width;
            Sheight = GraphicsDevice.Viewport.Height;

           Map = new Map(Mapok.Palya,Mapok.Objects,72);

            base.Initialize();
        }

      
        protected override void LoadContent()
        {

          
          

            karakter.LoadKarakter();
           
        }

       
        protected override void UnloadContent()
        {
        
        }

        
        protected override void Update(GameTime gameTime)
        {
            Newkey=Keyboard.GetState();
            if (Newkey.IsKeyDown(Keys.Escape))
                Exit();

           // if (hatter.rectangle.X + hatter.texture.Width < kamera)
          //      hatter.rectangle.X = hatter.rectangle.X + hatter.texture.Width;






            karakter.Update(gameTime);
            kamera.Update(karakter);



            prevkey = Newkey;
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,kamera.Transform);

            hatter.Draw(spriteBatch);
            karakter.Draw(spriteBatch);
            Map.Draw(spriteBatch);

            spriteBatch.End();

           

           
        }
    }
}
