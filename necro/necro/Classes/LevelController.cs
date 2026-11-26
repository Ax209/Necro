using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using necro.Classes;
using System.Diagnostics;
using System.IO;




namespace necro;

public class LevelController 
{

    // Let's declare the variables
    //SpriteBatch sprite_batch;
    Random obj_random;


    float level_number;

    // Get the textures
    private Texture2D spr_gamebar = Global.spr_gamebar;

    // Get the spritebatch
    private SpriteBatch sprite_batch = Global.sprite_batch;

    // Create the entity list
    List<Entity> lst_entities = new List<Entity>();

    // Variables for instances
    private Necro obj_necro;
    
    
    

    // Constructor
    public LevelController()
    {
        
        obj_random = Global.random;


        lst_entities.Add(obj_necro = new Necro(Global.sprite_batch, ref lst_entities, new Vector2(64, 64), Global.gamepad, this));

    }

    // Step Event
    public void step_event()
    {

        // Step event for the player
        obj_necro.step_event(Global.camera.room);

        // Step event for the skellybobs
        foreach(Entity p in lst_entities)
        {
            if (p is Skellybob)
            {
	            p.step_event();
            }
        }  

        // Update the camera position
        update_camera();
     
   
        
    //Right, let's see if this code works to sort the list by class:
    lst_entities.Sort((a, b) => a.y.CompareTo(b.y));


    }

    // Draw Event - standard
    public void draw_event()
    {
     foreach (Entity entity in lst_entities)
        {
            entity.draw_event();
        }   
    }

    // Draw Event - gui
    public void draw_event_gui()
    {
        // Draw the game panel window
       sprite_batch.Draw(spr_gamebar,new Vector2(0,112),Color.White);   

       Global.fnt_dbreak2.DrawString(sprite_batch,"Test",new Vector2(105,114),Color.White);
    }

    private void update_camera()
    {
        // First we check as to whether the player's state is 2 or not.
        if (obj_necro.get_state==2)
        {   
            // If so, update it between the player and the target area
            Vector2 mid = Vector2.Lerp(obj_necro.get_position,obj_necro.get_position+obj_necro.get_cursor,.5f);
            Global.camera.update_camera(mid.X,mid.Y);
        }
      // Instead we just focus on the player
        else 
            Global.camera.update_camera(obj_necro.x,obj_necro.y);
    }



}