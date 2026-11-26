using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using necro.Classes;
using System.Diagnostics;

namespace necro.Classes;

public class Grasstile : Entity
{

    protected Texture2D spite_index;
    protected int sprite_frame;
    

    public Grasstile(SpriteBatch sprite_batch, ref List<Entity> entities, Vector2 position, int sprite_frame)
        : base (sprite_batch, ref entities)
    {
        sprite_index = Global.spr_grass;
        //this.sprite_index = sprite_index;
        this.position=position;
        this.sprite_frame=sprite_frame;
        
    
    }

    public override void step_event()
    {
        
    }


    public override void draw_event()
    {
        sprite_batch.Draw(sprite_index,position,new Rectangle(0+(8*sprite_frame),0,8,8),Color.White,0,new Vector2(8f,8f),1f,SpriteEffects.None,0);
    }
}
