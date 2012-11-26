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
using System.Collections;

namespace TestGame
{
   abstract class Scene 
    {
        public int height;
        public int width;
        public int x;
        public int y;
        protected ContentManager _contentManager;
        public ContentManager contentManager
        {
            get
            {
                return _contentManager;
            }
        }
        protected GraphicsDevice _graphicsDevice;
        public GraphicsDevice graphicsDevice
        {
            get
            {
                return _graphicsDevice;
            }
        }

        public Scene()
        {

        }
 
       public Scene(int x, int y, int h, int w,ContentManager cm, GraphicsDevice gd)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            _contentManager = cm;
            _graphicsDevice = gd;
        }

        public virtual void Draw(GameTime gametime)
        {
        }

        public virtual void Update(GameTime gametime)
        {
        }

        

    }
}
