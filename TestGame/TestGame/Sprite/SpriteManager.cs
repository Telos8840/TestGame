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

using System.Diagnostics;

namespace TestGame
{
    public class SpriteManager
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        ContentManager contentManager;
        SceneManager sceneManager;
        Human player;
        ComputerAI enemy;
        public int[,] movedata;

        public SpriteManager(ContentManager cm, GraphicsDevice gd, SpriteBatch sb, int [,] md)
        {
            contentManager = cm;
            graphicsDevice = gd;
            spriteBatch = sb;
            movedata = md;
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            spriteBatch = new SpriteBatch(graphicsDevice);
            player = new Human(
                contentManager.Load<Texture2D>(@"Sprites/player"),
                new Point(42, 42),
                new Vector2(32 * 3, 32 * 3),
                Vector2.Zero,
                10, 0, movedata);

            enemy = new ComputerAI(
                contentManager.Load<Texture2D>(@"Sprites/player"),
                new Point(42, 42),
                new Vector2(60 * 3, 60 * 3),
                Vector2.Zero,
                10, 0, movedata);
            //enemy.SetWaypoints(scene.Waypoints);

            //base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            //base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            enemy.Update(gameTime);

            if (enemy.collisionRect.Intersects(player.collisionRect))
                Debug.WriteLine("COLLISION!!!!!!!!!!!!!!!!!");

            //base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            player.Draw(gameTime, spriteBatch);
            enemy.Draw(gameTime, spriteBatch);

            spriteBatch.End();

           // base.Draw(gameTime);
        }
    }
}
