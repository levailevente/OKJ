using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace THE_GAME
{
    class Zombie : Character
    {
        protected bool Right;
        protected Vector2 StartPos;
        protected bool Idle;

        public Zombie(Vector2 startPos)
        {

            Walk = new Texture2D[10];
            Death = new Texture2D[12];
            Attack = new Texture2D[8];
            idle = new Texture2D[15];
            const int o = 5;

            Idle = false;

            Rectanglew = new Rectangle(0, 0, 430 / o, 519 / o);
            hitbox = new Rectangle(0, 0, 45, 100);
            RectangleA = new Rectangle(0, 0, 430 / o, 519 / o);
            RectangleD = new Rectangle(0, 0, 629 / o, 526 / o);

            StartPos = Position = new Vector2(startPos.X, startPos.Y - 50);

            Elapsed = 0;

            WalkI = 0;

            AttackI = 0;

            DeadI = 0;

            IdleI = 0;

            IsJumping = false;
            IsCrouching = false;
            Right = true;


            for (int i = 0; i < 10; i++)
            {
                Walk[i] = Game1.ContentMgr.Load<Texture2D>("enemy/walk/Walk (" + (i) + ")");
            }

            for (int i = 0; i < 12; i++)
            {
                Death[i] = Game1.ContentMgr.Load<Texture2D>("enemy/death/Dead (" + (i) + ")");
            }

            for (int i = 0; i < 8; i++)
            {
                Attack[i] = Game1.ContentMgr.Load<Texture2D>("enemy/attack/Attack (" + (i + 1) + ")");
            }

            for (int i = 0; i < 15; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D>("enemy/idle/Idle (" + (i + 1) + ")");
            }
        }

        protected override void UpdateMovement()
        {
            if (!IsDead)
            {

                if (hitbox.Intersects(Game1.Character.HitboxA))
                {
                    if (Game1.Character.IsAttack && Game1.Character.AttackI > 2 && Game1.Character.AttackI < 5)
                    {
                        IsDead = true;
                        return;
                    }


                 
                }

                if (hitbox.Intersects(Game1.Character.Hitbox))
                {
                    if (!Game1.Character.Invulnerable)
                    {
                        Game1.Character.Health -= 1;
                        Game1.Character.Invulnerable = true;
                    }

                    IsAttack = true;
                    if (Elapsed > 5)
                    {
                        Elapsed = 0;
                        AttackI++;
                        if (AttackI > 7) AttackI = 0;
                    }
                }

                else if (Game1.Character.RectangleW.X - hitbox.X >= 0 && Game1.Character.RectangleW.X - hitbox.X < 500 &&
                         Game1.Character.RectangleW.Y - hitbox.Y < 600 && Game1.Character.RectangleW.Y - hitbox.Y > -50)
                {
                    if ((NextToCliff(hitbox) == "right" && Game1.Character.Hitbox.Y < hitbox.Y) ||
                        (hitbox.X - Game1.Character.Hitbox.X < 10 && Game1.Character.Hitbox.X - hitbox.X < 10))
                    {
                        Idle = true;
                        if (Elapsed > 4)
                        {
                            Elapsed = 0;
                            IdleI++;
                            if (IdleI > 14) IdleI = 0;
                        }
                    }
                    else
                    {
                        Idle = false;

                        IsAttack = false;
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
                            Mvmnt -= new Vector2(0, 8);
                        }
                    }

                }

                else if (hitbox.X - Game1.Character.RectangleW.X < 500 && hitbox.X - Game1.Character.RectangleW.X >= 0 &&
                         Game1.Character.RectangleW.Y - hitbox.Y < 600 && Game1.Character.RectangleW.Y - hitbox.Y > -50)
                {
                    if ((NextToCliff(hitbox) == "left" && Game1.Character.Hitbox.Y < hitbox.Y) ||
                        (hitbox.X - Game1.Character.Hitbox.X < 10 && Game1.Character.Hitbox.X - hitbox.X < 10))
                    {
                        Idle = true;
                        if (Elapsed > 4)
                        {
                            Elapsed = 0;
                            IdleI++;
                            if (IdleI > 14) IdleI = 0;
                        }
                    }
                    else
                    {
                        Idle = false;

                        IsAttack = false;
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
                            Mvmnt -= new Vector2(0, 8);
                        }
                    }

                }

                else if (Right)
                {
                    Idle = false;
                    IsAttack = false;
                    Mvmnt += new Vector2(0.2f, 0);
                    if (Elapsed > 6)
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
                    IsAttack = false;
                    Mvmnt += new Vector2(-0.2f, 0);
                    if (Elapsed > 6)
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
                    if (DeadI > 11) DeadI = -1;
                }

            }

        }

        public override void Draw(SpriteBatch sbatch)
        {
            //sbatch.Draw(Game1.szin, hitbox, Color.White);
            if (IsDead)
            {
                if (Right)
                {
                    if (DeadI != -1) sbatch.Draw(Death[DeadI], RectangleD, Color.White);
                }
                else if (!Right)
                {
                    if (DeadI != -1)
                    {
                        RectangleD.X -= 25;
                        sbatch.Draw(Death[DeadI], RectangleD, null, Color.White, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);
                    }
                }


            }

            else if (IsAttack)
            {
                if (!Right)
                {
                    RectangleA.X -= 20;
                    sbatch.Draw(Attack[AttackI], RectangleA, null, Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    RectangleA.X -= 20;
                    sbatch.Draw(Attack[AttackI], RectangleA, Color.White);
                }
            }

            else if (Idle)
            {
                if (!Right)
                {
                    Rectanglew.X -= 20;
                    sbatch.Draw(idle[IdleI], RectangleW, null, Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    {
                        Rectanglew.X -= 20;
                        sbatch.Draw(idle[IdleI], RectangleW, Color.White);
                    }
                }
            }

            else if (Right)
            {

                Rectanglew.X -= 20;
                sbatch.Draw(Walk[WalkI], Rectanglew, Color.White);
            }

            else if (!Right)
            {
                Rectanglew.X -= 20;
                sbatch.Draw(Walk[WalkI], Rectanglew, null, Color.White, 0, new Vector2(0, 0),
                    SpriteEffects.FlipHorizontally, 0);
            }

        }

        protected override void UpdatePosition(GameTime gametime)
        {
            Position += Mvmnt * (float) gametime.ElapsedGameTime.TotalMilliseconds / 15;

            hitbox.X += (int) Position.X;
            hitbox.Y += (int) Position.Y;


            Position = Game1.GenerateMap.CollisionV2(PrevPosition, Position, hitbox);
            if (Position.X < 0) Position.X = 0;
            if (Position.Y > 2000)
            {
                IsDead = true;
            }

            hitbox.Location = new Point((int) Position.X, (int) Position.Y);
            Rectanglew.Location = new Point((int) Position.X, (int) Position.Y);
            RectangleA.Location = new Point((int) Position.X - 10, (int) Position.Y);
            if (IsDead) RectangleD.Location = new Point((int) Position.X, (int) Position.Y + 10);

        }

        protected string NextToCliff(Rectangle movingRectangle)
        {
            if (OnGround(movingRectangle))
            {
                Rectangle cliffLeft;
                Rectangle cliff = cliffLeft = movingRectangle;
                cliff.Offset(60, 2);
                cliffLeft.Offset(-60, 2);
                if (!Game1.GenerateMap.Collision(cliff)) return "right";
                return !Game1.GenerateMap.Collision(cliffLeft) ? "left" : "no";
            }

            return "inair";
        }
    }
}
