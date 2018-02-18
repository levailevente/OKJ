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
        public static Karakter karakter;
         Camera kamera;
        public static  int Swidth, Sheight;
        public static KeyboardState Newkey = Keyboard.GetState();
        public static KeyboardState Prevkey;
        Background bg;

        Texture2D szin;
 
       public static GenerateMap GenerateMap;

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
      
           
            Swidth = GraphicsDevice.Viewport.Width;
            Sheight = GraphicsDevice.Viewport.Height;

           GenerateMap = new GenerateMap(Mapok.Palya,Mapok.Objects,72);


            szin = Content.Load<Texture2D>("grey");
            base.Initialize();
        }

      
        protected override void LoadContent()
        {

          
         bg=new Background(Content.Load<Texture2D>("BG"), new Rectangle(0, 0, 1280, 720)); 

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




            karakter.Update(gameTime);
            kamera.Update(karakter);



            Prevkey = Newkey;
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(13,21,22));

            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,kamera.Transform);

           
            bg.Draw(spriteBatch);
            spriteBatch.Draw(szin,karakter.Hitbox,Color.White);
           
            GenerateMap.Draw(spriteBatch);
            karakter.Draw(spriteBatch);

            spriteBatch.End();

           

           
        }
    }
}
