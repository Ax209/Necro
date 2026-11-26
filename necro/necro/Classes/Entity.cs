using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using necro.Classes;

namespace necro.Classes;

public abstract class Entity
{
    // Declare the variables
    protected SpriteBatch sprite_batch;
    protected Vector2 position;
    protected Texture2D sprite_index;
    protected Vector2 velocity;
    protected Rectangle collision_box;
    protected float state;
    protected List<Entity> entities;
    protected int dir=1;
    protected Vector2 target_move=Vector2.Zero;        // Coordinates where to move
    protected bool delete_me=false;                     // Set to "true" when it is to be delete

    protected int hp;
    protected int hpmax;
    




// Constructor
    public Entity(SpriteBatch sprite_batch, ref List<Entity> entities)
    {
        
        this.sprite_batch=sprite_batch;
        position = new Vector2(64, 64);

        this.entities=entities;

    }

    // Let's make an abstract step event so we can refer to the children
    public abstract void step_event();

    // Let's make an abstract draw event so we can refer to the children
    public abstract void draw_event();


#region     // "Getters and setters" and methods
    // What the flip is a "property"? Guess we're gonna find out!
    // Return the position of the class, when Game1 asks for the position.Y variable.
    public float y
    {
        get { return position.Y; }
    }
    // Let's do it for X too, so we can feed it into the camera
    public float x
    {
        get { return position.X; }
    }

    // Get the state
    public float get_state
    {
        get { return state; }
    }

    // Set the state
    public void set_state(float state)
    {
        this.state=state;
    }

    // Set the target direction
    public void set_target_move(Vector2 target_move)
    {
        this.target_move=target_move;
    }

    // If we need to get the collision box
    public Rectangle collisionBox
    {
        get { return collision_box; }
    }


    // Method/Helper for facing the right direction
    protected void facing_direction()
    {    
    if (velocity.X>0)
        dir=1;
    if (velocity.X<0)
        dir=-1;
    }

    public bool todelete
    {
        get { return delete_me; }
    }
 
#endregion



}
