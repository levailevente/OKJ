using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    public class Karakter
    {
        protected Texture2D[] idle;
        protected Texture2D[] Walk;
        protected Texture2D[] jump;
        protected Texture2D[] Attack;
        readonly Texture2D crouch;
        protected Texture2D[] Death;
        Rectangle rectanglei;
        protected Rectangle Rectanglew, hitbox;
        Rectangle hitboxA;
        protected Rectangle rectanglejump;
        protected Rectangle RectangleA;
        Rectangle rectanglejumpA;
        protected Rectangle RectangleD;
        int health;
        public Rectangle Hitbox => hitbox;
        public Rectangle HitboxA => hitboxA;
        public Rectangle RectangleW => Rectanglew;
        protected int Elapsed;
        protected int IdleI;
        protected int WalkI;
        protected int JumpI;
        public int AttackI;
        protected int DeadI;
        int jumpint;
        int elapsedD, elapsedA;
        public Color Color;
        Rectangle wall, wallLeft;
        public float Speed, JumpHeight;

        enum Direction
        {
            Left,
            Right
        }

        Direction facing = Direction.Right;
        protected Vector2 Mvmnt, PrevPosition;
        public Vector2 Position;
        Vector2 velocity;
        Vector2 lastMovement;

        public string PositionPoint
        {
            get
            {
                Point point = new Point((int)Position.X, (int)Position.Y);
                string help = point.X + "," + point.Y;
                return help;
            }
        }

        bool x;

        public bool IsDead { get; protected set; }

        public int Health
        {
            get { return health; }
            set
            {
                if (Game1.Godmode) health = 6;
                else if (value > -1 && value < 7) health = value;
            }
        }

        protected bool IsJumping;
        protected bool IsCrouching;
        public bool IsAttack;
        public bool Invulnerable;

        public Karakter()
        {
            idle = new Texture2D[10];
            Walk = new Texture2D[10];
            jump = new Texture2D[6];
            Attack = new Texture2D[10];
            Texture2D[] jumpA = new Texture2D[10];
            Death = new Texture2D[9];

            const int o = 4;
            rectanglei = new Rectangle(0, 0, 232 / o, 439 / o);
            Rectanglew = new Rectangle(0, 0, 363 / o, 458 / o);
            hitbox = new Rectangle(0, 0, 50, 108);
            hitboxA = new Rectangle(0, 0, 60, 108);
            rectanglejump = new Rectangle(0, 0, 362 / o, 483 / o);
            RectangleA = new Rectangle(0, 0, 536 / o, 495 / o);
            rectanglejumpA = new Rectangle(0, 0, 504 / o, 522 / o);
            RectangleD = new Rectangle(0, 0, 482 / o, 498 / o);
            Position = new Vector2(0, 1000);

            Elapsed = 0;
            IdleI = 0;
            WalkI = 0;
            JumpI = 0;
            jumpint = 0;
            AttackI = 0;
            DeadI = 0;
            elapsedD = 0;
            elapsedA = 0;

            IsJumping = false;
            IsCrouching = false;
            Invulnerable = false;

            Speed = 1.9f;
            JumpHeight = 12.5f;

            x = false;

            Health = 6;

            Color = Color.White;

            for (int i = 0; i < 10; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D>("bob/idle/Idle__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                Walk[i] = Game1.ContentMgr.Load<Texture2D>("bob/walk/Run__00" + i);
            }

            for (int i = 0; i < 6; i++)
            {
                jump[i] = Game1.ContentMgr.Load<Texture2D>("bob/jump/jump__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                Attack[i] = Game1.ContentMgr.Load<Texture2D>("bob/attack/Attack__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                jumpA[i] = Game1.ContentMgr.Load<Texture2D>("bob/jumpAttack/Jump_Attack__00" + i);
            }

            for (int i = 0; i < 9; i++)
            {
                Death[i] = Game1.ContentMgr.Load<Texture2D>("bob/death/Dead__00" + (i + 1));
            }

            crouch = Game1.ContentMgr.Load<Texture2D>("bob/crouch");

        }


        public void Update(GameTime gameTime)
        {

            Elapsed += 1;

            if (Invulnerable) Damaged();

            PrevPosition = Position;

            UpdateMovement();

            Gravity();

            UpdatePosition(gameTime);

            lastMovement = Position - PrevPosition;
            StopMoving();

        }


        protected virtual void UpdateMovement()
        {

            if (health > 0)
            {
                if (Game1.Newkey.IsKeyDown(Keys.Space) && Game1.Prevkey.IsKeyUp(Keys.Space) &&
                    !Invulnerable)
                {
                    IsAttack = true;
                }

                if (IsAttack)
                {
                    elapsedA++;
                    if (elapsedA > 1 && AttackI < 9)
                    {
                        elapsedA = 0;
                        AttackI++;
                    }

                    if (AttackI == 9)
                    {
                        AttackI = 0;
                        IsAttack = false;
                    }
                }


                if (Game1.Newkey.IsKeyDown(Keys.D) &&
                    NextToWall(Hitbox) != "right")
                {

                    Mvmnt += new Vector2(Speed, 0);
                    if (Elapsed > 3)
                    {
                        Elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }


                    facing = Direction.Right;

                }

                else if (Game1.Newkey.IsKeyDown(Keys.A) && NextToWall(Hitbox) != "left" && !IsCrouching)
                {
                    Mvmnt += new Vector2(-Speed, 0);
                    if (Elapsed > 3)
                    {
                        Elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }

                    facing = Direction.Left;
                }


                if (Game1.Newkey.IsKeyDown(Keys.W) && Game1.Newkey.IsKeyUp(Keys.S) && OnGround(hitbox) &&
                    Game1.Prevkey.IsKeyUp((Keys.W)))
                {
                    IsJumping = true;
                    jumpint = 0;
                    velocity.Y = -5;

                }


                else if (Game1.Newkey.IsKeyDown(Keys.S) && Game1.Newkey.IsKeyUp(Keys.W) &&
                         Game1.Prevkey.IsKeyUp(Keys.S) && OnGround(hitbox) && !IsCrouching)
                {
                    IsCrouching = true;

                }

                else if (Game1.Prevkey.IsKeyDown(Keys.S) && Game1.Newkey.IsKeyUp(Keys.S) && OnGround(hitbox) &&
                         IsCrouching)
                {
                    IsCrouching = false;
                }

                else
                {
                    if (!(Elapsed > 6)) return;
                    Elapsed = 0;
                    IdleI++;
                    if (IdleI > 9) IdleI = 0;

                }

            }
            else
            {
                if (Elapsed > 3)
                {
                    Elapsed = 0;
                    DeadI++;
                    if (DeadI > 8)
                    {
                        Game1.CurrentGameState = Game1.Gamestates.GameOver;
                        DeadI = 0;
                        Health++;
                    }

                }

            }
        }

        public virtual void Draw(SpriteBatch sbatch)
        {

            if (health > 0)
            {
                if (IsJumping)
                {
                    if (lastMovement.Y == 0)
                    {
                        velocity.Y = 0;
                        JumpI = 0;
                        IsJumping = false;
                    }

                    jumpint++;
                    if (jumpint > JumpHeight)
                        velocity.Y = 0;
                }

                if (IsAttack)
                {

                    RectangleA.Y -= 2;
                    if (facing == Direction.Left)
                    {
                        RectangleA.X -= 70;
                        sbatch.Draw(Attack[AttackI], RectangleA, null, Color, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);
                    }

                    else
                    {
                        RectangleA.X -= 5;
                        sbatch.Draw(Attack[AttackI], RectangleA, Color);
                    }

                }

                else if (Game1.Newkey.IsKeyDown(Keys.D) && OnGround(hitbox) && NextToWall(Hitbox) != "right")
                {
                    Rectanglew.X -= 5;
                    sbatch.Draw(Walk[WalkI], Rectanglew, Color);

                }

                else if (Game1.Newkey.IsKeyDown(Keys.A) && OnGround(hitbox) &&
                         NextToWall(Hitbox) != "left" && !IsCrouching)
                {
                    Rectanglew.X -= 35;
                    sbatch.Draw(Walk[WalkI], Rectanglew, null, Color, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);

                }

                else if (IsJumping)
                {
                    if (JumpI < 5) JumpI++;


                    if (facing == Direction.Left)
                    {
                        rectanglejump.X -= 15;
                        sbatch.Draw(jump[JumpI], rectanglejump, null, Color, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);

                    }
                    else
                    {
                        rectanglejump.X -= 15;
                        sbatch.Draw(jump[JumpI], rectanglejump, Color);
                    }


                }




                else if (IsCrouching)
                {
                    if (facing == Direction.Left)
                    {

                        sbatch.Draw(crouch, rectanglei, null, Color, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);
                    }

                    else
                    {
                        rectanglei.X -= 5;
                        sbatch.Draw(crouch, rectanglei, Color);
                    }

                }

                else if (lastMovement.Y > 0)
                {
                    if (facing == Direction.Left)
                    {
                        rectanglejump.X -= 30;
                        sbatch.Draw(jump[5], rectanglejump, null, Color, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);
                    }

                    else
                    {
                        rectanglejump.X -= 5;
                        sbatch.Draw(jump[5], rectanglejump, Color);
                    }

                }

                else
                {
                    if (facing == Direction.Left)
                    {

                        sbatch.Draw(idle[IdleI], rectanglei, null, Color, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);
                    }

                    else
                    {
                        rectanglei.X -= 5;
                        sbatch.Draw(idle[IdleI], rectanglei, Color);
                    }

                }
            }
            else
            {
                RectangleD.Y -= 10;
                if (facing == Direction.Left)
                {
                    RectangleD.X -= 30;
                    sbatch.Draw(Death[DeadI], RectangleD, null, Color, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    RectangleD.X -= 5;
                    sbatch.Draw(Death[DeadI], RectangleD, Color);
                }
            }
        }

        protected virtual void UpdatePosition(GameTime gametime)
        {
            Position += Mvmnt * (float)gametime.ElapsedGameTime.TotalMilliseconds / 17;


            hitbox.Location = new Point((int)Position.X, (int)Position.Y);

            Position = Game1.GenerateMap.CollisionV2(PrevPosition, Position, hitbox);


            if (Position.X < 0) Position.X = 0;

            if (Position.Y > 1500)
            {
                Health -= 1;
                Invulnerable = true;
                Position = new Vector2(0, 1000);
            }

            if (IsAttack)
            {
                hitboxA.Location = facing == Direction.Right
                    ? new Point((int)Position.X + 50, (int)Position.Y)
                    : new Point((int)Position.X - 50, (int)Position.Y);
            }
            else hitboxA.Location = new Point((int)Position.X, (int)Position.Y);

            hitbox.Location = IsCrouching
                ? new Point((int)Position.X, (int)Position.Y + 30)
                : new Point((int)Position.X, (int)Position.Y);



            rectanglei.Location = new Point((int)Position.X, (int)Position.Y);
            Rectanglew.Location = new Point((int)Position.X, (int)Position.Y);
            rectanglejump.Location = new Point((int)Position.X, (int)Position.Y);
            RectangleA.Location = new Point((int)Position.X, (int)Position.Y);
            rectanglejumpA.Location = new Point((int)Position.X, (int)Position.Y);
            RectangleD.Location = new Point((int)Position.X, (int)Position.Y);



        }

        protected bool OnGround(Rectangle movingRectangle)
        {
            Rectangle ground = movingRectangle;
            ground.Offset(0, 1);
            return Game1.GenerateMap.Collision(ground);

        }


        protected string NextToWall(Rectangle movingRectangle)
        {
            wall = movingRectangle;
            wallLeft = movingRectangle;
            wall.Offset(1, 0);
            wallLeft.Offset(-1, 0);
            if (Game1.GenerateMap.Collision(wall)) return "right";
            return Game1.GenerateMap.Collision(wallLeft) ? "left" : "no";
        }

        void Gravity()
        {
            if (!OnGround(hitbox)) Mvmnt += Vector2.UnitY * 2.5f;

            Mvmnt.X *= 0.8f;
            Mvmnt.Y *= 0.9f;

            Mvmnt += velocity;
        }

        void StopMoving()
        {
            if (lastMovement.X == 0) Mvmnt *= Vector2.UnitY;
            if (lastMovement.Y == 0) Mvmnt *= Vector2.UnitX;

        }

        void Damaged()
        {
            Color = Color.Red;
            Color.A = 0;
            elapsedD++;

            if (elapsedD % 10 == 0)
            {
                if (elapsedD % 30 == 0)
                {
                    if (!x) x = true;
                    if (x) x = false;
                }

                if (x) Color.A += 80;
                else Color.A -= 80;
            }

            if (elapsedD == 60)
            {
                Game1.Karakter.Invulnerable = false;
                Game1.Karakter.Color = Color.White;
                elapsedD = 0;
            }
        }
    }
}
