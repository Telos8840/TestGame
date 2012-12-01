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
    public class PlaygroundScene : Scene
    {
        Pathfinder pathfinder;

        //private Texture2D background;
        SpriteManager spriteManager;
        //private Sprite monsterSprite;
        private SpriteBatch spritebatch;
        private SceneManager sceneManager;
        private Texture2D line;
        private Texture2D[,] background = new Texture2D[10,15];
        private int[,] mapdata = {{101,26,26,26,26,26,26,26,26,26,26,8,9,10,11},
                                  {24,0,0,0,0,0,0,0,0,0,0,12,13,14,15},
                                  {24,0,0,6,0,0,6,0,0,0,0,16,17,18,19},
                                  {24,0,0,0,0,0,0,0,0,0,0,20,21,22,23},
                                  {24,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                  {24,0,0,0,0,0,0,0,0,0,0,0,0,0,25},
                                  {24,0,0,0,0,0,0,0,0,0,0,0,0,0,25},
                                  {24,0,0,0,0,0,0,0,0,0,0,0,0,0,25},
                                  {24,0,0,0,0,0,0,0,0,0,0,0,0,0,25},
                                  {103,26,26,26,26,26,26,26,26,26,26,26,26,26,104}
                                 };
        public int[,] movedata = {{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                  {1,0,0,0,1,0,0,0,0,0,0,1,1,1,1},
                                  {1,0,0,0,1,0,0,0,0,0,0,1,1,1,1},
                                  {1,0,0,0,1,0,0,0,0,0,0,1,1,0,1},
                                  {1,0,0,1,1,0,0,0,1,0,0,0,0,0,1},
                                  {1,0,0,1,1,1,1,0,1,0,0,0,0,0,1},
                                  {1,0,0,0,0,0,0,0,1,0,0,0,0,0,1},
                                  {1,0,0,0,0,1,0,1,0,0,0,0,0,0,1},
                                  {1,0,0,0,0,1,0,1,0,0,0,0,0,0,1},
                                  {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                                 };

        public int Width 
        { 
            get { return movedata.GetLength(1); } 
        }

        public int Height
        {
            get { return movedata.GetLength(0); }
        }

        public PlaygroundScene() { }

        public PlaygroundScene(int x, int y, int h, int w,ContentManager cm, GraphicsDevice gd, SceneManager sm)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h; 
            this.sceneManager = sm;
            _contentManager = cm;
            _graphicsDevice = gd;
            Initilize();
            LoadContent();
            
        }

        private void Initilize()
        {
            spritebatch = new SpriteBatch(_graphicsDevice);
            spriteManager = new SpriteManager(spritebatch, graphicsDevice, contentManager);

            pathfinder = new Pathfinder(this);
            List<Vector2> path = pathfinder.FindPath(new Point(3, 2), new Point(9, 8));

            spriteManager.AddSprite(new Human(
                contentManager.Load<Texture2D>(@"Sprites/player"),
                new Point(42, 42),
                new Vector2(32 * 3, 32 * 3),
                Vector2.Zero,
                10, 0, movedata, sceneManager,
                new Poop(contentManager.Load<Texture2D>(@"Sprites/poop"))));

            spriteManager.AddSprite(new ComputerAI(
                contentManager.Load<Texture2D>(@"Sprites/player"),
                new Point(42, 42),
                new Vector2(32 * 3, 32 * 3),
                Vector2.Zero,
                10, 0, movedata, sceneManager, path));

            //spriteManager.AddSprite(new Poop(contentManager.Load<Texture2D>(@"Sprites/poop"), new Vector2(50 * 3, 50 * 3)));

            foreach (Vector2 point in path)
            {
                Console.WriteLine(point);
            }
            
            //sceneManager.Game.Components.Add(spriteManager);
            //monsterSprite = new Sprite(32*3,32*3,0,contentManager,graphicsDevice,movedata);
            //line = new Texture2D(_graphicsDevice, 1, 1);
            //line.SetData(new[] { Color.Black });
        }
        /*
        private void Initilize(){
            spritebatch = new SpriteBatch(_graphicsDevice);
            monsterSprite = new Sprite(32*3,32*3,0,contentManager,graphicsDevice,movedata);
            line = new Texture2D(_graphicsDevice, 1, 1);
            line.SetData(new[] { Color.Black });
        }
        */
        private void LoadContent()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    String tile = "Tiles/" + mapdata[i,j];
                    background[i, j] = _contentManager.Load<Texture2D>(tile);
                    //Console.Write(mapdata[i, j]);
                }
                //Console.WriteLine("");
            }
            //this.background = _contentManager.Load<Texture2D>(@"Background");
        }

        /// <summary>
        /// Returns the tile index for the given cell.
        /// </summary>
        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return movedata[cellY, cellX];
        }

        public override void Draw(GameTime gametime)
        {
            spritebatch.Begin();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    spritebatch.Draw(background[i, j], new Rectangle(j * 16 * 3, i * 16 * 3, 16 * 3, 16 * 3), Color.White);
                }
            }
            for (int i = 0; i < 50; i++)
            {
                //spritebatch.Draw(line, new Rectangle(0, 16 * i, _graphicsDevice.DisplayMode.Width, 1), Color.Black);
                //spritebatch.Draw(line, new Rectangle(16 * i, 0, 1, _graphicsDevice.DisplayMode.Height), Color.Black);

            }
            //spritebatch.Draw(background, new Rectangle(0, 0, background.Width * 3, background.Height * 3), Color.White);
            spritebatch.End();
            //monsterSprite.Draw(gametime);
            spriteManager.Draw(gametime);
        }

        public override void Update(GameTime gametime)
        {
            spriteManager.Update(gametime);
        }
    }
}
