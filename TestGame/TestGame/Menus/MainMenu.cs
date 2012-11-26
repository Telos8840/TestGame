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
    class MainMenu : Scene
    {
        private Texture2D background;
        private SpriteBatch spritebatch;
        private SpriteFont spriteFont;
        private SceneManager sceneManager;
        private string[] title;
        private Scene[] destination;
        public MainMenu(int x, int y, int h, int w,ContentManager cm, GraphicsDevice gd, SceneManager sm ,string[] title,Scene[] destination)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            _contentManager = cm;
            _graphicsDevice = gd;
            this.sceneManager = sm;
            this.title = title.ToArray();
            this.destination = destination.ToArray();
            Initilize();
            LoadContent();
            
        }
        
        private void Initilize(){
            spritebatch = new SpriteBatch(_graphicsDevice);
        }

        private void LoadContent()
        {
            //this.background = _contentManager.Load<Texture2D>(@"Background");
            this.spriteFont = _contentManager.Load<SpriteFont>(@"Fonts/Times New Roman");
        }

        public override void Draw(GameTime gametime)
        {
            spritebatch.Begin();
            spritebatch.GraphicsDevice.Clear(Color.Black);
            Vector2 textloaction = new Vector2(200, 50);
            foreach (String x in title)
            {
                textloaction.Y += 20;
                spritebatch.DrawString(spriteFont, x, textloaction, Color.White);
            }
            //spritebatch.Draw(background, new Rectangle(0, 0, background.Width * 3, background.Height * 3), Color.White);
            spritebatch.End();
        }

        public override void Update(GameTime gametime)
        {
            KeyboardState pressed = Keyboard.GetState();
            if (pressed.IsKeyDown(Keys.A))
            {
                Console.Write(@"pressed");
                sceneManager.addScene(destination[0]);
            }
        }
    }
}
