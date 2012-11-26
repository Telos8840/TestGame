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

//Github update test5

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        SceneManager scenemanager;
        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 240 * 3;
            graphics.ApplyChanges();
            scenemanager = new SceneManager(Content, graphics.GraphicsDevice);
            //scenemanager.addScene(new PlaygroundScene(0,0,320,480,Content, graphics.GraphicsDevice));
            string[] menuText = new String[1];
            menuText[0] = @"test";
            Scene[] menuDest = new Scene[1];
            menuDest[0] = new PlaygroundScene(0,0,320,480,Content, graphics.GraphicsDevice,scenemanager);
            scenemanager.addScene(new MainMenu(0, 0, 320, 480, Content, graphics.GraphicsDevice,scenemanager,menuText,menuDest));
            //add Menu screen here
        }

        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            scenemanager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            scenemanager.Draw(gameTime);
            base.Draw(gameTime);
        }

        public SceneManager SceneManager
        {
            get { return sceneManager; }
            internal set { sceneManager = value; }
        }

        SceneManager sceneManager;
    }
}
