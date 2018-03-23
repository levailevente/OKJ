using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace THE_GAME
{
    class Items
    {
        Sprite texture;
        public Items(string type,Vector2 position)
        {
            switch (type)
            {
                case "heart": texture = new Sprite(Game1.ContentMgr.Load<Texture2D>("heart/fullheart"),new Rectangle());
                    break;
            }
        }
    }
}
