using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using necro.Classes;
using System.Diagnostics;


namespace necro.Classes;

public class Skellybob : Entity
{

    // Declare the variables

    // For movement/pathfinding/hording
    protected float separation_distance = 10f;
    protected float separation_strength = .15f;
    protected float speed = .6f;
    protected int still_count=0;                // How many frames the entity has been still
   

    //protected int dir=1;
    

    public Skellybob(SpriteBatch sprite_batch, ref List<Entity> entities, Vector2 position)
        :base (sprite_batch, ref entities)
    {
        this.position=position;

        dir=1;
        sprite_index = Global.spr_skellybob;
        //state=2;
    }

      // Step Event
    public override void step_event()
    {
        //Debug.WriteLine($"State is : {state}");


        // Move towards the necromancer
        if (state==2)
        {
            // Find the Necro
            foreach (Entity p in entities)
            {   
                // Found the Necromancer, let's move towards it
                if (p is Necro)
                {
                    horde_move(new Vector2(p.x, p.y));
                    break;
                }
            }    
        }

        if (state==3)
        {
            horde_move(target_move);

            if (velocity.LengthSquared() < .01)
                still_count++;
            else 
                still_count=0;
            

            if (still_count>30)
            {
                state=1;
                Debug.WriteLine("moved to state 1 - still for too long");
                still_count=0;
            }
            if (Vector2.Distance(position, target_move) < 2)
            {
                state=1;
                Debug.WriteLine("moved to state 1 - distance to target");
                still_count=0;
            }
                         
        }

        // Update the collision box
        collision_box = new Rectangle((int)position.X,(int)position.Y,sprite_index.Width,sprite_index.Height);

        // Update the facing direction 
        facing_direction();

        // Reset Volocity
        velocity=Vector2.Zero;
    }


    

     // Draw Event
    public override void draw_event()
    { 
        //sprite_batch.Draw(sprite_index,position,null,Color.White,0,new Vector2(sprite_index.Width*.5f,sprite_index.Height),1f,SpriteEffects.None,0f);
        if (dir<0)
            sprite_batch.Draw(sprite_index,position,null,Color.White,0,new Vector2(sprite_index.Width*.5f,sprite_index.Height),1f,SpriteEffects.None,0f);   
        if (dir>0)
            sprite_batch.Draw(sprite_index,position,null,Color.White,0,new Vector2(sprite_index.Width*.5f,sprite_index.Height),1f,SpriteEffects.FlipHorizontally,0f);
    }




    #region     // Methods, Setters and Getters

    // We need a method for movement calculations
    protected void horde_move(Vector2 target_position)
    {
        // Where is the ultimate coordinate to move towards?
        Vector2 seek = target_position - position;

        // What's the direction towards the target?
        if (seek != Vector2.Zero) 
            seek.Normalize();
        
        // Separation from other Skellybobs
        Vector2 separation = Vector2.Zero;

        // Loop around each other physical thing, and avoid them.
        foreach (Entity p in entities)
        {
            // If this is a skellybob or the necromancer, don't use the bounding boxes, but distance
            if ((p is Skellybob || p is Necro) && p != this)   // Exclude this (Can't avoid itself!!!)
            {
                // Find the position of the other entity...
                Vector2 other_position = new Vector2(p.x, p.y);

                // And see get the distance from it...
                Vector2 difference = position - other_position;

                // Now we need the distance from it...
                float distance = difference.Length();

                // If the entity is too close, 
                if (distance < separation_distance && distance > 0)
                {
                    // Direction to move, away from other target
                    difference.Normalize();

                    // We need to figure out magnitude. How far away we gonna move?
                    float overlap = separation_distance - distance;

                    // Let's get the final calculation
                    separation += difference * overlap;
                }
            }
            else if (p != this)
            {
                // Get the other entity's collision box
                Rectangle other_box = p.collisionBox;
    
                // If the two boxes overlap at all
                if (collision_box.Intersects(other_box))
                {
                    // Work out how much they overlap on each axis
                    float overlap_x = Math.Min(collision_box.Right, other_box.Right)-Math.Max(collision_box.Left, other_box.Left);
                    float overlap_y = Math.Min(collision_box.Bottom, other_box.Bottom)-Math.Max(collision_box.Top, other_box.Top);

                    // Find out which side we're on
                    Vector2 difference = new Vector2(collision_box.Center.X - other_box.Center.X,collision_box.Center.Y - other_box.Center.Y);

                    // Push out along the smallest overlap
                    if (overlap_x < overlap_y)
                    {
                        if (difference.X < 0)
                            separation.X -= overlap_x;
                        else
                            separation.X += overlap_x;
                    }
                    else
                    {
                        if (difference.Y < 0)
                            separation.Y -= overlap_y;
                        else
                            separation.Y += overlap_y;
                    }
                }  
            }
        }


        // Combine where we're going along with moveing from all other entities
        Vector2 movement = seek + separation * separation_strength;


        // Let's turn this back into a direction
        if (movement.Length() > 1f) 
            movement.Normalize();
    
        // Ignore extremely tiny movements (as they wiggle)
        if (movement.Length() < 0.05f)
            movement = Vector2.Zero;
        
        // Wait, we need to consider the collision boxes!
        velocity += movement * speed;

        position+=velocity;
    }

    
   



    #endregion


}
