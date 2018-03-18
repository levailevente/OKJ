﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    class ZombieGirl:Zombie
    {
        public ZombieGirl(Vector2 startPos) : base(startPos)
        {
            walk = new Texture2D[10];
            death = new Texture2D[12];

            const int o = 5;

            Rectanglew = new Rectangle(0, 0, 430 / o, 519 / o);
            hitbox = new Rectangle(0, 0, 50, 100);
            rectangleA = new Rectangle(0, 100, 536 / o, 495 / o);
            RectangleD = new Rectangle(0, 0, 629 / o, 526 / o);

            this.StartPos = position = startPos;

            elapsed = 0;

            WalkI = 1;

            attackI = 0;


            isJumping = false;
            isCrouching = false;
            Right = true;



            for (int i = 0; i < 10; i++)
            {
                walk[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/walk/Walk (" + (i) + ")");
            }

            for (int i = 0; i < 12; i++)
            {
                death[i] = Game1.ContentMgr.Load<Texture2D>("enemy/girl/death/Dead (" + (i+1) + ")");
            }
        }
    }
}
