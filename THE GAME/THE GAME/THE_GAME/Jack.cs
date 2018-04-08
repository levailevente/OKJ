using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace THE_GAME
{
    class Jack : Zombie
    {

        int hp, elapsedX;
        public Jack(Vector2 startPos) : base(startPos)
        {
            Walk = new Texture2D[10];
            Death = new Texture2D[10];
            idle = new Texture2D[10];
            Jump = new Texture2D[10];

            const int o = 4;

            Idle = false;

            Rectanglew = new Rectangle(0, 0, 579 / o, 763 / o);
            hitbox = new Rectangle(0, 0, 70, 180);
            RectangleD = new Rectangle(0, 0, 986 / o, 796 / o);

            StartPos = Position = new Vector2(startPos.X, startPos.Y - 30);

            Elapsed = 0;

            WalkI = 0;

            JumpI = 0;

            elapsedX = 0;

            hp = 3;

            IsJumping = false;
            IsCrouching = false;
            Right = true;


            for (int i = 0; i < 10; i++)
            {
                Walk[i] = Game1.ContentMgr.Load<Texture2D>("enemy/jack/walk/Walk (" + (i + 1) + ")");
            }

            for (int i = 0; i < 10; i++)
            {
                Death[i] = Game1.ContentMgr.Load<Texture2D>("enemy/jack/death/Dead (" + (i + 1) + ")");
            }

            for (int i = 0; i < 10; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D>("enemy/jack/idle/Idle (" + (i + 1) + ")");
            }

            for (int i = 0; i < 10; i++)
            {
                Jump[i] = Game1.ContentMgr.Load<Texture2D>("enemy/jack/jump/Jump (" + (i + 1) + ")");
            }
        }

        protected override void UpdateMovement()
        {
            if (!IsDead)
            {

                if (Invulnerable)
                {
                    elapsedX++;
                    Color = Color.Red;
                    if (elapsedX > 30)
                    {
                        elapsedX = 0;
                        Invulnerable = false;
                        Color = Color.White;
                    }

                }

                if (hitbox.Intersects(Game1.Character.HitboxA) && !Invulnerable)
                {
                    if (Game1.Character.IsAttack && Game1.Character.AttackI > 2 && Game1.Character.AttackI < 5)
                    {
                        hp--;
                        if (Game1.Character.facing == Direction.Left)
                        {
                            Mvmnt -= new Vector2(20, 0);
                        }
                        else Mvmnt += new Vector2(20, 0);
                        Invulnerable = true;

                        if (hp == 0)
                        {
                            IsDead = true;
                            return;
                        }

                    }

                }

                if (hitbox.Intersects(Game1.Character.Hitbox))
                {
                    if (!Game1.Character.Invulnerable)
                    {
                        Game1.Character.Health -= 1;
                        Game1.Character.Invulnerable = true;
                    }

                    Idle = true;
                    if (Elapsed > 1)
                    {
                        Elapsed = 0;
                        IdleI++;
                        if (IdleI > 9) IdleI = 0;
                    }
                }

                else if (Game1.Character.RectangleW.X - hitbox.X >= 0 && Game1.Character.RectangleW.X - hitbox.X < 300 &&
                         Game1.Character.RectangleW.Y - hitbox.Y < 300 && Game1.Character.RectangleW.Y - hitbox.Y > -400)
                {
                    if ((NextToCliff(hitbox) == "right" && Game1.Character.Hitbox.Y < hitbox.Y) ||
                        (hitbox.X - Game1.Character.Hitbox.X < 10 && Game1.Character.Hitbox.X - hitbox.X < 10))
                    {
                        Idle = true;
                        if (Elapsed > 4)
                        {
                            Elapsed = 0;
                            IdleI++;
                            if (IdleI > 9) IdleI = 0;
                        }
                    }
                    else
                    {
                        Idle = false;
                        Right = true;
                        Mvmnt += new Vector2(0.7f, 0);
                        if (Elapsed > 4)
                        {
                            Elapsed = 0;
                            WalkI++;
                            if (WalkI > 9) WalkI = 0;
                        }

                        if ((NextToWall(hitbox) == "right" || NextToCliff(hitbox) == "right") &&
                            Game1.Character.Hitbox.Y - 100 < hitbox.Y)
                        {
                            Mvmnt -= new Vector2(0, 10);
                        }
                    }

                }

                else if (hitbox.X - Game1.Character.RectangleW.X < 300 && hitbox.X - Game1.Character.RectangleW.X >= 0 &&
                         Game1.Character.RectangleW.Y - hitbox.Y < 300 && Game1.Character.RectangleW.Y - hitbox.Y > -400)
                {
                    if ((NextToCliff(hitbox) == "left" && Game1.Character.Hitbox.Y < hitbox.Y) ||
                        (hitbox.X - Game1.Character.Hitbox.X < 10 && Game1.Character.Hitbox.X - hitbox.X < 10))
                    {
                        Idle = true;
                        if (Elapsed > 4)
                        {
                            Elapsed = 0;
                            IdleI++;
                            if (IdleI > 9) IdleI = 0;
                        }
                    }
                    else
                    {
                        Idle = false;
                        Right = false;
                        Mvmnt += new Vector2(-0.7f, 0);
                        if (Elapsed > 4)
                        {
                            Elapsed = 0;
                            WalkI++;
                            if (WalkI > 9) WalkI = 0;
                        }

                        if ((NextToWall(hitbox) == "left" || NextToCliff(hitbox) == "left") &&
                            Game1.Character.Hitbox.Y - 100 < hitbox.Y)
                        {
                            Mvmnt -= new Vector2(0, 10);
                        }
                    }

                }

                else if (Right)
                {
                    Idle = false;
                    Mvmnt += new Vector2(0.2f, 0);
                    if (Elapsed > 7)
                    {
                        Elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }

                    if (StartPos.X - Position.X < -100)
                    {
                        Right = false;
                    }

                    if (NextToWall(Hitbox) == "right" || NextToCliff(Hitbox) == "right")
                    {
                        if (StartPos.X - Position.X > 100)
                        {
                            Mvmnt -= new Vector2(0, 5);
                        }
                        else Right = false;
                    }
                }
                else if (!Right)
                {
                    Idle = false;
                    Mvmnt += new Vector2(-0.2f, 0);
                    if (Elapsed > 7)
                    {
                        Elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }

                    if (StartPos.X - Position.X > 100) Right = true;

                    if (NextToWall(Hitbox) == "left" || NextToCliff(Hitbox) == "left")
                    {
                        if (StartPos.X - Position.X < -100) Mvmnt -= new Vector2(0, 5);
                        else Right = true;
                    }
                }


            }

            else
            {
                if (Elapsed > 5 && DeadI != -1)
                {
                    Elapsed = 0;
                    DeadI++;
                    if (DeadI > 9) DeadI = -1;
                }

                //  if (deadI==-1) if (Game1.Enemies.Count>1) Game1.Enemies.Remove(this);
            }

        }

        public override void Draw(SpriteBatch sbatch)
        {
            //sbatch.Draw(Game1.szin, hitbox, Color);
            if (IsDead)
            {
                if (Right)
                {
                    if (DeadI != -1) sbatch.Draw(Death[DeadI], RectangleD, Color);
                }
                else if (!Right)
                {
                    if (DeadI != -1)
                    {
                        RectangleD.X -= 35;
                        sbatch.Draw(Death[DeadI], RectangleD, null, Color, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);
                    }
                }

                else if (DeadI == -1)
                {

                }
            }


            else if (Idle)
            {
                if (!Right)
                {
                    Rectanglew.X -= 40;
                    sbatch.Draw(idle[IdleI], RectangleW, null, Color, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    {
                        Rectanglew.X -= 20;
                        sbatch.Draw(idle[IdleI], RectangleW, Color);
                    }
                }
            }

            else if (Right)
            {

                Rectanglew.X -= 20;
                sbatch.Draw(Walk[WalkI], Rectanglew, Color);
            }

            else if (!Right)
            {
                Rectanglew.X -= 40;
                sbatch.Draw(Walk[WalkI], Rectanglew, null, Color, 0, new Vector2(0, 0),
                    SpriteEffects.FlipHorizontally, 0);
            }

        }


        protected override void UpdatePosition(GameTime gametime)
        {
            Position += Mvmnt * (float)gametime.ElapsedGameTime.TotalMilliseconds / 15;

            hitbox.X += (int)Position.X;
            hitbox.Y += (int)Position.Y;


            Position = Game1.GenerateMap.CollisionV2(PrevPosition, Position, hitbox);
            if (Position.X < 0) Position.X = 0;
            if (Position.Y > 2000)
            {
                IsDead = true;
            }

            hitbox.Location = new Point((int)Position.X, (int)Position.Y);
            Rectanglew.Location = new Point((int)Position.X, (int)Position.Y);
            RectangleA.Location = new Point((int)Position.X - 10, (int)Position.Y);
            if (IsDead) RectangleD.Location = new Point((int)Position.X, (int)Position.Y + 10);

        }
    }
}
