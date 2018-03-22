using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
  public  class Karakter
    {
        Texture2D[] idle;
        protected  Texture2D[] walk;
        readonly Texture2D[] jump;
        protected Texture2D[] attack;
        readonly Texture2D[] jumpA;
        readonly Texture2D crouch;
        protected Texture2D[] death;
        Rectangle rectanglei;
        protected Rectangle Rectanglew, hitbox, hitboxA;
        Rectangle rectanglejump;
        protected Rectangle rectangleA, RectanglejumpA, RectangleD;
        public int health;
        public Rectangle Hitbox => hitbox;
        public Rectangle HitboxA => hitboxA;
        public Rectangle RectangleW => Rectanglew;
        protected int elapsed;
        int idleI;
        protected int WalkI;
        int jumpI;
        protected int attackI, deadI;
        int jumpint;
        int elapsedD;
        public Color color;
        Rectangle wall, wallLeft;

        protected enum Direction { Left,Right,Forward,Back};

        protected Direction Facing=Direction.Right;
        protected Vector2 mvmnt, prevPosition;
           public Vector2 position;
        Vector2 velocity;
        Vector2 lastMovement;
        public Vector2 Position => position;

        public bool IsCrouching => isCrouching;
        bool x;

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        protected bool isJumping;
        protected bool isCrouching;
        public   bool isAttack;
        protected  bool   isDead;
        public bool invulnerable, damaged;

        public Karakter()
        {
            idle = new Texture2D[10];
            walk = new Texture2D[10];
            jump = new Texture2D[6];
            attack = new Texture2D[10];
            jumpA=new Texture2D[10];

            const int o = 4;
            rectanglei = new Rectangle(0, 0, 232/o, 439/o);
            Rectanglew = new Rectangle(0, 0,363/o , 458/o);
            hitbox = new Rectangle(0, 0, 60, 108);
            hitboxA = new Rectangle(0, 0, 60, 108);
            rectanglejump = new Rectangle(0, 0, 362/o, 483/o);
            rectangleA = new Rectangle(0, 0, 536 / o, 495 / o);
            RectanglejumpA = new Rectangle(0, 0, 504 / o, 522 / o);
            position =new Vector2(0,300);

            elapsed = 0; idleI = 0; WalkI = 0;jumpI = 0; jumpint = 0;  attackI = 0;  deadI = 0;  elapsedD = 0;

            isJumping = false;
            isCrouching = false;
            invulnerable = false;
            damaged = false;
            x = false;

            health = 6;

            color=Color.White;

            for (int i = 0; i < 10; i++)
            {
                idle[i] = Game1.ContentMgr.Load<Texture2D>("bob/idle/Idle__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                walk[i] = Game1.ContentMgr.Load<Texture2D>("bob/walk/Run__00" + i);
            }

            for (int i = 0; i < 6; i++)
            {
                jump[i] = Game1.ContentMgr.Load<Texture2D>("bob/jump/jump__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                attack[i] = Game1.ContentMgr.Load<Texture2D>("bob/attack/Attack__00" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                jumpA[i] = Game1.ContentMgr.Load<Texture2D>("bob/jumpAttack/Jump_Attack__00" + i);
            }

            crouch = Game1.ContentMgr.Load<Texture2D>("bob/crouch");

        }
        
        
        public void Update(GameTime gameTime)
        {
            
            elapsed += 1;

            if (invulnerable) Damaged();

            prevPosition = position;

            UpdateMovement();

            Gravity();

            UpdatePosition(gameTime);

            lastMovement = position - prevPosition;
            StopMoving();

        }


        protected virtual void UpdateMovement()
        {


            if (Game1.Newkey.IsKeyDown(Keys.Right) && Game1.Newkey.IsKeyUp(Keys.Left) &&
                NextToWall(Hitbox) != "right" && !isAttack)
            {

                    mvmnt += new Vector2(2, 0);
                    if (elapsed > 3)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }
                

                Facing = Direction.Right;

            }

           else if (Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right) && NextToWall(Hitbox) != "left" &&
                !isAttack && !isCrouching)
            {

                    mvmnt += new Vector2(-2, 0);
                    if (elapsed > 3)
                    {
                        elapsed = 0;
                        WalkI++;
                        if (WalkI > 9) WalkI = 0;
                    }


                Facing = Direction.Left;
            }


            if (Game1.Newkey.IsKeyDown(Keys.Up) && Game1.Newkey.IsKeyUp(Keys.Down) && OnGround() &&
                Game1.Prevkey.IsKeyUp((Keys.Up)))
            {
                isJumping = true;
                jumpint = 0;
                velocity.Y = -5;

            }


            if (Game1.Newkey.IsKeyDown(Keys.Space) && Game1.Prevkey.IsKeyUp(Keys.Space) &&
                 !invulnerable)
            {
                isAttack = true;
                
            }


         else  if (Game1.Newkey.IsKeyDown(Keys.Down) && Game1.Newkey.IsKeyUp(Keys.Up) &&
                Game1.Prevkey.IsKeyUp(Keys.Down) && OnGround() && !isCrouching)
            {
                isCrouching = true;

            }

          else  if (Game1.Prevkey.IsKeyDown(Keys.Down) && Game1.Newkey.IsKeyUp(Keys.Down) && OnGround() && isCrouching )
            {            
                isCrouching = false;
            }

            if (health == 0) IsDead = true;

            else
            {
                if (!(elapsed > 6)) return;
                elapsed = 0;
                idleI++;
                if (idleI > 9) idleI = 0;

            }

    
        }

        public virtual void Draw(SpriteBatch sbatch)
        {

            if (isJumping)
            {
                if (lastMovement.Y == 0)
                {
                    velocity.Y = 0;
                    jumpI = 0;
                    isJumping = false;
                }

                jumpint++;
                if (jumpint > 11)
                    velocity.Y = 0;
            }

            if (isAttack)
            {
                elapsed++;
                if (elapsed > 4 && attackI < 9) attackI++;

                rectangleA.Y -= 2;
                if (Facing == Direction.Left)
                {
                    rectangleA.X -= 70;
                    sbatch.Draw(attack[attackI], rectangleA, null, color, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    sbatch.Draw(attack[attackI], rectangleA, color);
                }

                if (attackI == 9)
                {
                    attackI = 0;
                    isAttack = false;
                }

            }

            else if (isJumping)
            {
                if (jumpI < 5) jumpI++;


                if (Facing == Direction.Left)
                {
                    rectanglejump.X -= 15;
                    sbatch.Draw(jump[jumpI], rectanglejump, null, color, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);

                }
                else
                {
                    rectanglejump.X -= 15;
                    sbatch.Draw(jump[jumpI], rectanglejump, color);
                }


            }


            else if (Game1.Newkey.IsKeyDown(Keys.Right) && Game1.Newkey.IsKeyUp(Keys.Left) && OnGround() && NextToWall(Hitbox) != "right")
            {
              
                sbatch.Draw(walk[WalkI], Rectanglew, color);

            }

            else if (Game1.Newkey.IsKeyDown(Keys.Left) && Game1.Newkey.IsKeyUp(Keys.Right) && OnGround() &&
                     NextToWall(Hitbox) != "left" && !isCrouching)
            {
                Rectanglew.X -= 35;
                sbatch.Draw(walk[WalkI], Rectanglew, null, color, 0, new Vector2(0, 0),
                    SpriteEffects.FlipHorizontally, 0);

            }

            else if (IsCrouching)
            {
                if (Facing == Direction.Left)
                {

                    sbatch.Draw(crouch, rectanglei, null, color, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    sbatch.Draw(crouch, rectanglei, color);
                }

            }

            else if (lastMovement.Y > 0)
            {
                if (Facing == Direction.Left)
                {
                    rectanglejump.X -= 30;
                    sbatch.Draw(jump[5], rectanglejump, null, color, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    sbatch.Draw(jump[5], rectanglejump, color);
                }

            }

            else
            {
                if (Facing == Direction.Left)
                {

                    sbatch.Draw(idle[idleI], rectanglei, null, color, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    sbatch.Draw(idle[idleI], rectanglei, color);
                }

            }
        }

        protected virtual void UpdatePosition(GameTime gametime)
        {
            position += mvmnt * (float) gametime.ElapsedGameTime.TotalMilliseconds / 17;
    

            hitbox.Location=new Point ( (int) position.X,(int)position.Y);
                   
            position = Game1.GenerateMap.CollisionV2(prevPosition, position, hitbox);


            if (position.X < 0) position.X = 0; 

            if (position.Y > 1200) IsDead = true;

            if (isAttack)
            {
                hitboxA.Location = Facing==Direction.Right ? new Point((int) position.X + 60, (int) position.Y) : new Point((int)position.X -60, (int)position.Y);
            }
            else hitboxA.Location = new Point((int)position.X, (int)position.Y);

            hitbox.Location = isCrouching ? new Point((int)position.X, (int)position.Y + 30) : new Point((int) position.X, (int) position.Y);

            

             rectanglei.Location = new Point((int)position.X, (int)position.Y);
            Rectanglew.Location = new Point((int) position.X, (int) position.Y);
            rectanglejump.Location = new Point((int) position.X, (int) position.Y);
            rectangleA.Location = new Point((int) position.X, (int) position.Y);
            RectanglejumpA.Location = new Point((int) position.X, (int) position.Y);



        }

        bool OnGround()
        {
            Rectangle ground = hitbox;
            ground.Offset(0,1);
            return Game1.GenerateMap.Collision(ground);

        }

      
        protected string NextToWall(Rectangle movingRectangle)
        {
            wall = movingRectangle;
            wallLeft = movingRectangle;
            wall.Offset(1,0);
            wallLeft.Offset(-1,0);
            if (Game1.GenerateMap.Collision(wall)) return "right";
            return Game1.GenerateMap.Collision(wallLeft) ? "left" : "no";
        }

        void Gravity()
        {
            if (!OnGround()) mvmnt += Vector2.UnitY * 2.5f;

            mvmnt.X *= 0.8f;
            mvmnt.Y *= 0.9f;

            mvmnt += velocity;
        }

        void StopMoving()
        {          
            if (lastMovement.X == 0) mvmnt *= Vector2.UnitY;
            if (lastMovement.Y == 0) mvmnt *= Vector2.UnitX;

        }

        void Damaged()
        {
            color=Color.Red;
            color.A = 0;
            elapsedD++;
            
            if (elapsedD % 10 == 0)
            {
                if (elapsedD % 30 == 0)
                {
                    if (!x) x = true;
                    if (x) x = false;
                }

              if (x) color.A += 80;
                else color.A -= 80;
            }

            if (elapsedD == 100)
            {
                Game1.Karakter.invulnerable = false;
                Game1.Karakter.color = Color.White;
                elapsedD = 0;
            }
        }
    }
}
