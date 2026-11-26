using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using necro.Classes;
using System.Diagnostics;




namespace necro.Classes;

public class Necro : Entity
{
    // Declare the variables
    Controller pad;
    private float speed = .7f;
    Texture2D cursor_sprite;
    int followers = 0;            // How many skellybobs are following you

    //int dir=1;

    // Cursor variables
    float cursor_frame=0f;
    Vector2 cursor_target = Vector2.Zero;
    Vector2 cursor = Vector2.Zero;
    float cursor_radius=32f;
    float call_distance=32f;    // Radius for calling skellybobs to follow ya
    LevelController controller;

    // Constructor
    public Necro(SpriteBatch sprite_batch, ref List<Entity> entities, Vector2 position, Controller pad, LevelController controller)
        : base(sprite_batch, ref entities)
    {
        //this.sprite_index = sprite_index;
        this.position = position;
        this.pad = pad;
        this.controller=controller;

        velocity = Vector2.Zero;
        state = 1f;
        cursor_sprite=Global.spr_cursor;
        dir=1;

        // Set the health
        hp=10;
        hpmax=10;

        sprite_index=Global.spr_necro;


    }


    // Step Event
    public override void step_event()
    {
        // Update the collision box
        collision_box = new Rectangle((int)position.X,(int)position.Y,sprite_index.Width,sprite_index.Height);
    }
  
    // Step Event
    public void step_event(Rectangle room)
    {
        // State 1, moving around
        if (state==1)
        {
            
            // Let's get the movement from input, from the new helper!
            velocity=necro_move();

            // Press button to get skellybobs to follow
            if (pad.X_but_pressed())
            {
                command_skellybob();
            }

            // Press the trigger to sendeth thou minions forth to battleth
            else if (pad.LT_but_pressed())
            {
                // Change State
                state=2;

                // Reset the velocity because state 2 is gonna calculate that, itself.
                velocity=Vector2.Zero;

                // Reset the cursor stats
                cursor=Vector2.Zero;
                if (dir==1)
                    cursor_target=new Vector2(32,0);
                else
                    cursor_target=new Vector2(-32,0);
            }

            // Apply the movement
            position+=velocity * speed;


            // Let's clamp the player in the room
            position.X=Math.Clamp(position.X,0,room.Width-8);
            position.Y=Math.Clamp(position.Y,0,room.Height-8);

            // Update the direction code
            if (velocity.X>0)
                dir=1;
            if (velocity.X<0)
                dir=-1;

            
        }

        // Moving targeting cursor
        if (state==2)
        {
            // Animation for cursor
            cursor_frame=(cursor_frame+.05f) % 4;

            // Set the cursor direction from the player

            // Let's get the movement from input, from the new helper!
            velocity=necro_move();

            // Apply the movement
            position+=velocity * speed;

            // Let's update the cursor and its target
            cursor_target+=new Vector2(pad.stick_right_x()*1.2f,-pad.stick_right_y()*1.2f);
            if (cursor_target.LengthSquared() > cursor_radius * cursor_radius)  
            {
                cursor_target.Normalize();
                cursor_target *= cursor_radius;
            }

            // Now lerp it to keep it looking preem.
            cursor=Vector2.Lerp(cursor, cursor_target, .2f);


            // If no longer holding trigger then go back to state 1
            if (!pad.LT_but_down())
                state=1;
            
            // Has the right trigger been pressed though? 
            else if (pad.RT_but_pressed())
            {
                // Send all the skellybobs there
                foreach (Entity p in entities)
                {
                    if (p is Skellybob)
                    {
                        if (p.get_state==2)
                        {
                            p.set_state(3);   
                            p.set_target_move(position+cursor_target);
                        }
                    }        
                }        
                // Create the sky marker
                //controller.create_sky_marker(position+cursor_target);

                // Change back to state 1
                state=1;
            }
            


            // Update the facing direction
            if (cursor.X > 0)
                dir=1;
            if (cursor.X < 0)
                dir=-1;

        }
    
        // Update the collision box
        collision_box = new Rectangle((int)position.X,(int)position.Y,sprite_index.Width,sprite_index.Height);
        
    }

    // Draw Event
    public override void draw_event()
    {
        if (dir>0)
            sprite_batch.Draw(sprite_index,position,null,Color.White,0,new Vector2(sprite_index.Width*.5f,sprite_index.Height),1f,SpriteEffects.None,0f);   
        if (dir<0)
            sprite_batch.Draw(sprite_index,position,null,Color.White,0,new Vector2(sprite_index.Width*.5f,sprite_index.Height),1f,SpriteEffects.FlipHorizontally,0f);       
    }

    public void draw_cursor()
    {
    if (state==2)
        sprite_batch.Draw(cursor_sprite,position+cursor,new Rectangle((int)cursor_frame * 8,0,8,8),Color.White,0,new Vector2(4f,4f),1f,SpriteEffects.None,0f);       
    }




    // Helpers

    private Vector2 necro_move()
    {
        Vector2 direction = Vector2.Zero;

        // Check whether the analogue stick is being used
        // Having to use ternary because Monogame won't allow me to read a boolean as a 1 or 0 :((
        if (pad.stick_left_x() == 0 && pad.stick_left_y() == 0)
        {
            // D-pad movement
            direction.X = (pad.Right_but_down() ? 1 : 0)
                        - (pad.Left_but_down() ? 1 : 0);

            direction.Y = (pad.Down_but_down() ? 1 : 0)
                        - (pad.Up_but_down() ? 1 : 0);
        }
        else
        {
            // Analogue stick movement
            direction.X = pad.pad1.ThumbSticks.Left.X;
            direction.Y = -pad.pad1.ThumbSticks.Left.Y;
        }

        // Prevent diagonal movement from being faster
        if (direction.LengthSquared() > 1f)
            direction.Normalize();

        return direction;
    }

    // Get skellybobs to follow
    public void command_skellybob()
    {
        foreach(Entity p in entities)
        {
            if (p is Skellybob)
            {
                // Find the position of the other entity...
                Vector2 other_position = new Vector2(p.x, p.y);

                // And see get the distance from it.
                Vector2 difference = position - other_position;

                // Now we need the distance from it...
                float distance = difference.Length();

                Debug.WriteLine($"Distance is {distance}.");

                if (distance<call_distance)
                {
                    p.set_state(2);
                }


            }
        }
    }



    // Getters
    public float get_state
    {
        get { return state; }
    }
    public Vector2 get_cursor
    {
        get { return cursor; }
    }
    public Vector2 get_position
    {
        get { return position; }
    }


}