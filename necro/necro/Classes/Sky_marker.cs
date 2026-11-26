using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using necro.Classes;
using System.Diagnostics;

namespace necro.Classes;

public class Sky_marker : Entity
{

    
    
    int sprite_frame;
  

    public Sky_marker(SpriteBatch sprite_batch, ref List<Entity> entities, Vector2 position)
        : base (sprite_batch, ref entities)
    {
        sprite_index=Global.spr_sky_marker;
        this.position=position;
        sprite_frame=-1;
        


    }


    public override void step_event()
    {
        //sprite_frame=(sprite_frame+1) % 26;
        if (sprite_frame==26)
            delete_me=true;
        else    
            sprite_frame++;
    }


    public override void draw_event()
    {
        //sprite_batch.Draw(sprite_index,position,new Rectangle(0+(1*sprite_frame),0,1,16),Color.White,0,new Vector2(0f,16f),1f,SpriteEffects.None,0);
        sprite_batch.Draw(sprite_index,position,new Rectangle(0+(sprite_frame),0,1,16),Color.White,0,new Vector2(0f,16f),1f,SpriteEffects.None,0);
    }

    // Getters, Setter, and Methods
    

}
