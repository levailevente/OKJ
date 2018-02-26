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
            tileSize = 72;
            
            ContentMgr = Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            kamera = new Camera(graphics.GraphicsDevice.Viewport);
            Karakter = new Karakter();
      
           
            Swidth = GraphicsDevice.Viewport.Width;
            Sheight = GraphicsDevice.Viewport.Height;

           GenerateMap = new GenerateMap(Mapok.Palya,Mapok.Objects,tileSize);


            
            base.Initialize();
        }

      
        protected override void LoadContent()
        {

          
         bg=new Background(Content.Load<Texture2D>("BG"), new Rectangle(0, 0, 1280, 720));

         szin = Content.Load<Texture2D>("grey");


        }

       
        protected override void UnloadContent()
        {
        
        }

        
        protected override void Update(GameTime gameTime)
        {
            Newkey=Keyboard.GetState();
            if (Newkey.IsKeyDown(Keys.Escape))
                Exit();




            Karakter.Update(gameTime);
            kamera.Update(Karakter);



            Prevkey = Newkey;
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(13,21,22));

            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,kamera.Transform);

           
            bg.Draw(spriteBatch);
            spriteBatch.Draw(szin,Karakter.Hitbox,Color.White);
           
            GenerateMap.Draw(spriteBatch,Karakter);
            Karakter.Draw(spriteBatch);

            spriteBatch.End();

           

           
        }
    }
}
