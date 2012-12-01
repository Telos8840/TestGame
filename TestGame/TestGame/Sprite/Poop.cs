using System;
using System.Collections;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

using System.Diagnostics;

namespace TestGame
{
    class Poop
    {
        ArrayList poopList = new ArrayList();
        Texture2D poopImage;
        //Vector2 poopPos;

        public Poop(Texture2D i) 
        {
            poopImage = i;

        }

        public void addPoop(Vector2 p) 
        {
           // poopPos = p;
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch, Vector2 poopPos)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(poopImage, poopPos, Color.White);
            spriteBatch.End();
        }
    }
}
