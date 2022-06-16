using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Text;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace paniqueados2
{
    public class historyLine
    {
        private Texture2D sprite;
        private Vector2 Position;

        public  historyLine(Texture2D _sprite, Vector2 _position)
        {
            this.sprite=_sprite;
            this.Position=_position;
        }

        

    }
}