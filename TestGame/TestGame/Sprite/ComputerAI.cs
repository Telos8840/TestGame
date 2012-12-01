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
        private Vector2 position;
        private Vector2 destination;
        private bool automove = false;
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
            autmove = true;
            destination = waypoints.Dequeue();
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
                        case State.Waiting:
                            {
                                if (waypoints.Count > 0 && automove == true)
                                {
                                    //if (DistanceToDestination < 1f)
                                    //{
                                        //position = waypoints.Peek();
                                        //Console.WriteLine(waypoints.Count);
                                        //position = waypoints.Dequeue();
                            
                            if (position.X < destination.X)
                            {
                                status = State.MoveRight;
                            }
                            else if (position.X == destination.X){
                            }
                            else
                            {
                                status = State.MoveLeft;
                            }

                            if (position.Y < destination.Y)
                            {
                                status = State.MoveDown;
                            }
                            else if (position.Y == destination.Y)
                            {
                            }
                            else
                            {
                                status = State.MoveUp;
                            }

                            if (position.X == destination.X && position.Y == destination.Y)
                            {
                                destination = waypoints.Dequeue();
                            }

                                }
                                else{
                                    automove = false;
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
