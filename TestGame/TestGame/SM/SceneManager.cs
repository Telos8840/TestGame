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

namespace TestGame
{
    public class SceneManager
    {
       //Refferes to the current Scene 
       public bool exit = false;
       private Scene _currentScene;
       public Scene currentScene
       {
           get
           {
               return _currentScene;
           }
           set
           {
               _currentScene = value;
           }
       }
       private ContentManager _contentManager;
       public ContentManager contentManager
       {
           get
           {
               return _contentManager;
           }
       }
       private GraphicsDevice _graphicsDevice;
       public GraphicsDevice graphicsDevice
       {
           get
           {
               return _graphicsDevice;
           }
       }
       //Stack of All of our Scenes
        private ArrayList sceneStack = new ArrayList();

        public SceneManager(ContentManager cm, GraphicsDevice gd)
        {
            _contentManager = cm;
            _graphicsDevice = gd;
        }

        public void addScene(Scene newScene){
            sceneStack.Add(newScene);
            _currentScene = (Scene)sceneStack[sceneStack.Count-1];
        }

        public void removeScene(Scene oldScene)
        {
            sceneStack.Remove(oldScene);
            if (sceneStack.Count == 0)
            {
                _currentScene = null;
            }
            else
            {
                _currentScene = (Scene)sceneStack[sceneStack.Count - 1];
            }
        }

        public Scene getScene()
        {
            return currentScene;
        }

        public void UnloadContent()
        {
            currentScene.UnloadContent();
        }

        public void Update(GameTime gametime)
        {
            if(_currentScene != null){
                _currentScene.Update(gametime);
            }
        }

        public void Draw(GameTime gametime)
        {
            //if (_currentScene != null)
            //{
                _currentScene.Draw(gametime);
            //}
        }
    }
}
