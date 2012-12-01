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


namespace TestGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SceneView : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public SceneView(Game game) : base(game)
        {
            // TODO: Construct any child components here
        }

        private Texture2D _backgroundImage;
        private SpriteBatch _sceneSpriteBatch;
        private String _title;
        private Texture2D _forwardImage;
        private Texture2D _backImage;
        public SpriteBatch sceneSpriteBatch
        {
            get
            {
                return _sceneSpriteBatch;
            }
            set
            {
                _sceneSpriteBatch = value;
            }
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            base.Update(gameTime);
        }
        
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime);
        }
    }
}
