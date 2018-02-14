using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace THE_GAME
{
    class Save
    {
        string player;
        DateTime time;
        // currentMap
        Vector2 currentPosition;


        public Save(string player, Vector2 currentPosition)
        {
            this.player = player;
            this.time=DateTime.Now;
            this.currentPosition = currentPosition;
        }
    }
}
