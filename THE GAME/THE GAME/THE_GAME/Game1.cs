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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static  ContentManager contentMgr;
        public static karakter karakter;
        public static Sprite   hatter;
        public static  int swidth, sheight;
        public static KeyboardState newkey = Keyboard.GetState();
        public static KeyboardState prevkey = newkey;

 
       public static map map;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight =  720;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            contentMgr = Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            karakter = new karakter();
            hatter = new Sprite(Content.Load<Texture2D>("BG"), new Rectangle(0, 0, 1280, 720));

            swidth = GraphicsDevice.Viewport.Width;
            sheight = GraphicsDevice.Viewport.Height;

           map = new map();

            base.Initialize();
        }

      
        protected override void LoadContent()
        {

            map.Generate(mapok.palya, 64);
          

            karakter.loadKarakter();
           
        }

       
        protected override void UnloadContent()
        {
        
        }

        
        protected override void Update(GameTime gameTime)
        {
            newkey=Keyboard.GetState();
            if (newkey.IsKeyDown(Keys.Escape))
                Exit();






            karakter.update();




            prevkey = newkey;
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            hatter.draw(spriteBatch);
            karakter.Draw(spriteBatch);
            map.draw(spriteBatch);

            spriteBatch.End();

           

           
        }
    }
}
