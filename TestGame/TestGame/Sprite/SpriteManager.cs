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
    public class SpriteManager
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        ContentManager contentManager;
        //sprite list is your list of sprites
        ArrayList spriteList = new ArrayList();

        public SpriteManager(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //these are passed to u from playground
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
        }

        public void AddSprite(Sprite newSprite){
            spriteList.Add(newSprite);
        }

        public void RemoveSprite(Sprite oldSprite)
        {
            spriteList.Remove(oldSprite);
        }

        protected void LoadContent()
        {
            /*
            player = new Human(
                contentManager.Load<Texture2D>(@"Sprites/player"),
                new Point(42, 42),
                new Vector2(32 * 3, 32 * 3),
                Vector2.Zero,
                10, 0, scene.MoveData);

            enemy = new ComputerAI(
                contentManager.Load<Texture2D>(@"Sprites/player"),
                new Point(42, 42),
                new Vector2(60 * 3, 60 * 3),
                Vector2.Zero,
                10, 0, scene.MoveData);
            //enemy.SetWaypoints(scene.Waypoints);

            //base.LoadContent();
            */
        }

        public void Update(GameTime gameTime)
        {
            //you need to loop through all your sprite objects and update them.
            //Boundary Detection Arraylist
            ArrayList boundsList = new ArrayList();
            foreach (Sprite s in spriteList)
            {
                s.Update(gameTime);
                boundsList.Add(s.collisionRect);

            }
            //this is how you check collision against all other possible sprites
            foreach (Rectangle r in boundsList)
            {
                foreach (Rectangle t in boundsList)
                {
                    if (r.Intersects(t) && r != t)
                    {
                        Debug.WriteLine("COLLISION!!!!!!!!!!!!!!!!!");
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Sprite s in spriteList)
            {
                s.Draw(gameTime,spriteBatch);
            }
        }
    }
}
