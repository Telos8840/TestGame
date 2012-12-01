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
        private GraphicsDevice gd;
        private SoundEffectInstance themeSong;
        private string[] title;
        private Scene[] destination;
        private int selectedindex = 0;
        private float timer = 0f;

        public MainMenu(int x, int y, int h, int w, ContentManager cm, GraphicsDevice gd, SceneManager sm, string[] title, Scene[] destination)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            _contentManager = cm;
            _graphicsDevice = gd;
            this.gd = gd;
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
            this.spriteFont = _contentManager.Load<SpriteFont>(@"Fonts/Times New Roman");
            if (themeSong == null)
            {
                themeSong = _contentManager.Load<SoundEffect>(@"Music/themesong").CreateInstance();
            }
            themeSong.IsLooped = true;
            themeSong.Play();
        }

        public override void UnloadContent()
        {
            themeSong.Stop();
            themeSong.Dispose();
        }

        public override void Draw(GameTime gametime)
        {
            spritebatch.Begin();
            spritebatch.GraphicsDevice.Clear(Color.Black);
            Vector2 textloaction = new Vector2(200, 50);
            for (int i = 0; i < title.Length; i ++)
            {
                textloaction.X = (_graphicsDevice.Viewport.Width - spriteFont.MeasureString(title[i]).X)/2;
                textloaction.Y += 50;
                if (selectedindex == i)
                {
                    spritebatch.DrawString(spriteFont, title[i], textloaction, Color.Yellow);
                }
                else
                {
                    spritebatch.DrawString(spriteFont, title[i], textloaction, Color.White);

                }
            }
            spritebatch.End();
        }

        public override void Update(GameTime gametime)
        {
            if (themeSong.State == SoundState.Stopped)
            {
                themeSong.Play();
            }
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            KeyboardState pressed = Keyboard.GetState();
            if( timer > 150f){
                timer = 0f;
                if (pressed.IsKeyDown(Keys.Up))
                {
                    selectedindex--;
                    if (selectedindex < 0)
                    {
                        selectedindex = title.Length - 1;
                    }
                }
                else if (pressed.IsKeyDown(Keys.Down))
                {
                    selectedindex++;
                    if (selectedindex > title.Length - 1)
                    {
                        selectedindex = 0;
                    }
                }
            }
            if (pressed.IsKeyDown(Keys.Enter))
            {
                if (title[selectedindex] == "End Game")
                {
                    themeSong.Stop();
                    themeSong.Dispose();
                    sceneManager.exit = true;
                }
                else
                {
                    themeSong.Stop();
                    sceneManager.addScene(destination[selectedindex]);
                }
            }
        }
    }
}
