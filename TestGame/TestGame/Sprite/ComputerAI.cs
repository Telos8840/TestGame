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
    class ComputerAI : Sprite
    {
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        private Vector2 oldPosition;
        private float speed = 2.5f;
        SceneManager sceneManager;

        public ComputerAI(Texture2D image, Point playerFrameSize, Vector2 position, Vector2 velocity,
            int collisionOffset, int type, int[,] movedata, SceneManager sm, List<Vector2> wp)
        {
            this.image = image;
            this.playerFrameSize = playerFrameSize;
            this.position = position;
            oldPosition = position;
            this.velocity = velocity;
            this.collisionOffset = collisionOffset;
            this.type = type;
            this.movedata = movedata;
            sceneManager = sm;
            PassWaypoint(wp);
            status = State.Waiting;
        }

        public void PassWaypoint(List<Vector2> waypoint) 
        {
            foreach (Vector2 point in waypoint)
            {
                waypoints.Enqueue(new Vector2(point.X, point.Y));
            }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);

            this.position = this.waypoints.Dequeue();
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    switch (status)
                    {

                        case State.MoveRight:
                            {
                                if (walking)
                                {
                                    if (movedata[(int)(((oldPosition.Y) / 48) % 16), (int)(((oldPosition.X + 48) / 48) % 16)] == 0 && keepmoving)
                                    {
                                        oldPosition.X += 4;
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
                                        if ((int)oldPosition.X % 48 == 0)
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
                                        //status = State.Waiting;
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
                                    if (movedata[(int)(((oldPosition.Y + 48) / 48) % 16), (int)((oldPosition.X / 48) % 16)] == 0 && keepmoving)
                                    {
                                        oldPosition.Y += 4;
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
                                        if ((int)oldPosition.Y % 48 == 0)
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
                                        //status = State.Waiting;
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
                                    if (movedata[(int)(((oldPosition.Y - 1) / 48) % 16), (int)((oldPosition.X / 48) % 16)] == 0 && keepmoving)
                                    {
                                        oldPosition.Y -= 4;
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
                                        if ((int)oldPosition.Y % 48 == 0)
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
                                    if (movedata[(int)(((oldPosition.Y) / 48) % 16), (int)(((oldPosition.X - 1) / 48) % 16)] == 0 && keepmoving)
                                    {
                                        oldPosition.X -= 4;
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
                                        if ((int)oldPosition.X % 48 == 0)
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
                                        //status = State.Waiting;
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

                        case State.Waiting:
                            {
                                if (waypoints.Count > 0)
                                {
                                    //if (DistanceToDestination < 1f)
                                    //{
                                        //position = waypoints.Peek();
                                        //Console.WriteLine(waypoints.Count);
                                        position = waypoints.Dequeue();
                                    //}

                                    //else
                                    //{
                                    //    Vector2 newDirection = waypoints.Peek() - position;
                                    //    newDirection.Normalize();

                                    //    //velocity = Vector2.Multiply(newDirection, speed);

                                    //    //position += velocity;

                                    //}
                                        walking = true;
                                        curFrame = 0;
                                        //keepmoving = true;
                                    if (oldPosition.X == position.X && oldPosition.Y != position.Y)
                                    {
                                        if (position.Y > oldPosition.Y)
                                        {
                                            direction = 0; //down
                                            status = State.MoveDown;
                                            oldPosition = position;
                                        }
                                        else
                                        {
                                            direction = 3; //up
                                            status = State.MoveUp;
                                            oldPosition = position;
                                        }
                                    }
                                    else if (oldPosition.Y == position.Y && oldPosition.X != position.X)
                                    {
                                        if (position.X > oldPosition.X)
                                        {
                                            direction = 2; //right
                                            status = State.MoveRight;
                                            oldPosition = position;
                                        }
                                        else
                                        {
                                            direction = 1; //left
                                            status = State.MoveLeft;
                                            oldPosition = position;
                                        }
                                    }
                                    else if (oldPosition.X == position.X && oldPosition.Y == position.Y)
                                        status = State.Waiting;
                             }
                                break;
                }                    

            }
                    Console.WriteLine(status);    
            


            if (position.X == 624 && position.Y == 144)
            {
                sceneManager.removeScene(sceneManager.getScene());
                position = new Vector2(32 * 3, 32 * 3);
            }

            //base.Update(gameTime);
        }
    }
}
