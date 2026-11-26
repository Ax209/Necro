using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using necro.Classes;


namespace necro.Classes;

public class Camera
{
    public Rectangle gamecam = new Rectangle(50,50,128,128);
    public Rectangle room = new Rectangle(0,0,512,512);
    public Vector2 position = Vector2.Zero;

    public Vector2 position_target=Vector2.Zero;


    public void update_camera(float x, float y)
    {


        position_target.X = x - 64+4;
        position_target.Y = y - 64+12;

        position=Vector2.Lerp(position,position_target,.05f);       // WOOOO the lerp looks amazing!

        // Now let's keep it within the boundaries of the room.
        position.X=Math.Clamp(position.X,0,room.Width-128);
        position.Y=Math.Clamp(position.Y,0,room.Height-128);
    }

}
