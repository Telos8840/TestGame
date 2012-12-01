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
    class Human : Sprite
    {
        SceneManager sceneManager;
        Poop poop;
        
        public Human(Texture2D image, Point playerFrameSize, Vector2 position, Vector2 velocity, 
            int collisionOffset, int type, int[,] movedata, SceneManager sm, Poop p)
        {
            this.image = image;
            this.playerFrameSize = playerFrameSize;
            this.position = position;
            this.velocity = velocity;
            this.collisionOffset = collisionOffset;
            this.type = type;
            this.movedata = movedata;
            sceneManager = sm;
            poop = p;
            status = State.Waiting;
        }

        public override void Update(GameTime gametime)
        {
            var newState = Keyboard.GetState();
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            //Console.WriteLine(status);
            switch (status)
            {
                case State.Waiting:
                    {
                        if (newState.IsKeyDown(Keys.Right))
                        {
                            status = State.MoveRight;
                        }
                        else if (newState.IsKeyDown(Keys.Left))
                        {
                            status = State.MoveLeft;
                        }
                        else if (newState.IsKeyDown(Keys.Up))
                        {
                            status = State.MoveUp;
                        }
                        else if (newState.IsKeyDown(Keys.Down))
                        {
                            status = State.MoveDown;
                        }

                        break;
                    }

                case State.MoveRight:
                    {
                        if (walking)
                        {
                            if (movedata[(int)(((position.Y) / 48) % 16), (int)(((position.X + 48) / 48) % 16)] == 0 && keepmoving)
                            {
                                position.X += 4;
                                keepmoving = true;
                                if (timer > 100f)
                                {
                                    curFrame++;
                                    timer = 0f;
                                }
                                if (curFrame == 4)
                                {
                                    curFrame = 0;
                                }
                                if ((int)position.X % 48 == 0)
                                {
                                    keepmoving = false;
                                    walking = false;
                                    status = State.Waiting;
                                }
                            }
                            else
                            {
                                keepmoving = false;
                                walking = false;
                                status = State.Waiting;
                            }
                        }
                        else
                        {
                            curFrame = 0;
                            direction = 2;
                            walking = true;
                            keepmoving = true;
                        }
                        break;
                    }
                case State.MoveDown:
                    {
                        if (walking)
                        {
                            if (movedata[(int)(((position.Y + 48) / 48) % 16), (int)((position.X / 48) % 16)] == 0 && keepmoving)
                            {
                                position.Y += 4;
                                keepmoving = true;
                                if (timer > 100f)
                                {
                                    curFrame++;
                                    timer = 0f;
                                }
                                if (curFrame == 4)
                                {
                                    curFrame = 0;
                                }
                                if ((int)position.Y % 48 == 0)
                                {
                                    keepmoving = false;
                                    walking = false;
                                    status = State.Waiting;
                                }
                            }
                            else
                            {
                                keepmoving = false;
                                walking = false;
                                status = State.Waiting;
                            }
                        }
                        else
                        {
                            curFrame = 0;
                            direction = 0;
                            walking = true;
                            keepmoving = true;
                        }
                        break;
                    }
                case State.MoveUp:
                    {
                        if (walking)
                        {
                            if (movedata[(int)(((position.Y - 1) / 48) % 16), (int)((position.X / 48) % 16)] == 0 && keepmoving)
                            {
                                position.Y -= 4;
                                keepmoving = true;
                                if (timer > 100f)
                                {
                                    curFrame++;
                                    timer = 0f;
                                }
                                if (curFrame == 4)
                                {
                                    curFrame = 0;
                                }
                                if ((int)position.Y % 48 == 0)
                                {
                                    keepmoving = false;
                                    walking = false;
                                    status = State.Waiting;
                                }
                            }
                            else
                            {
                                curFrame = 0;
                                keepmoving = false;
                                walking = false;
                                status = State.Waiting;
                            }
                        }
                        else
                        {
                            curFrame = 0;
                            direction = 3;
                            walking = true;
                            keepmoving = true;
                        }
                        break;
                    }
                case State.MoveLeft:
                    {
                        if (walking)
                        {
                            if (movedata[(int)(((position.Y) / 48) % 16), (int)(((position.X - 1) / 48) % 16)] == 0 && keepmoving)
                            {
                                position.X -= 4;
                                keepmoving = true;
                                if (timer > 100f)
                                {
                                    curFrame++;
                                    timer = 0f;
                                }
                                if (curFrame == 4)
                                {
                                    curFrame = 0;
                                }
                                if ((int)position.X % 48 == 0)
                                {
                                    keepmoving = false;
                                    walking = false;
                                    status = State.Waiting;
                                }
                            }
                            else
                            {
                                keepmoving = false;
                                walking = false;
                                status = State.Waiting;
                            }
                        }
                        else
                        {
                            curFrame = 0;
                            direction = 1;
                            walking = true;
                            keepmoving = true;
                        }
                        break;
                    }
            }
            oldState = newState;
          
            
            if (position.X == 624 && position.Y == 144)
            {
                sceneManager.removeScene(sceneManager.getScene());
                position = new Vector2(32 * 3, 32 * 3);
            }

            //if (newState.IsKeyDown(Keys.A))
              //  poop.Draw(position);
             
        }
    }
}
