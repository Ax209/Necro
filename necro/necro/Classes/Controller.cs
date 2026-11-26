using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace necro.Classes;

public class Controller
{
    public GamePadState pad1;
    public GamePadState pad1_previous;
    public int abut=0;
    public int bbut=0;
    public int xbut=0;
    public int ybut=0;
    public int rightbut=0;
    public int upbut=0;
    public int leftbut=0;
    public int downbut=0;
    public int startbut=0;
    public int selectbut=0;
    public int rt=0;
    public int lt=0;
    public int lb=0;
    public int rb=0;
    public float lstickx=0;
    public float lsticky=0;
    public float rstickx=0;
    public float rsticky=0;

    public int deadzone_l;
    public int deadzone_r;
    
    

    // Constructor
    public Controller()
    {
        
    }


    public void step_event()
    {
        // The snapshot data should probs be moved as the previous data
        pad1_previous=pad1;

        // Update the input snapshot
        pad1=GamePad.GetState(PlayerIndex.One);  

        // Update the variables using ternaries - if the button is held, the number climbs. If released, it becomes 0.
        abut = (abut + 1) * (pad1.Buttons.A == ButtonState.Pressed ? 1 : 0);
        bbut = (bbut + 1) * (pad1.Buttons.B == ButtonState.Pressed ? 1 : 0);
        xbut = (xbut + 1) * (pad1.Buttons.X == ButtonState.Pressed ? 1 : 0);
        ybut = (ybut + 1) * (pad1.Buttons.Y == ButtonState.Pressed ? 1 : 0);


        rightbut = (rightbut + 1) * (pad1.DPad.Right == ButtonState.Pressed ? 1 : 0);
        upbut = (upbut + 1) * (pad1.DPad.Up == ButtonState.Pressed ? 1 : 0);
        leftbut = (leftbut + 1) * (pad1.DPad.Left == ButtonState.Pressed ? 1 : 0);
        downbut = (downbut + 1) * (pad1.DPad.Down == ButtonState.Pressed ? 1 : 0);
        
        startbut = (startbut + 1) * (pad1.Buttons.Start == ButtonState.Pressed ? 1 : 0); 
        selectbut = (selectbut + 1) * (pad1.Buttons.Back == ButtonState.Pressed ? 1 : 0); 

        rt = (rt + 1) * (pad1.Triggers.Right > 0.1f ? 1 : 0); 
        lt = (lt + 1) * (pad1.Triggers.Left > 0.1f ? 1 : 0); 
        rb = (rb + 1) * (pad1.Buttons.RightShoulder == ButtonState.Pressed ? 1 : 0); 
        lb = (lb + 1) * (pad1.Buttons.LeftShoulder == ButtonState.Pressed ? 1 : 0); 
         
        

        
    }
        
    // A Button
    public bool A_but_down()
    {
        return abut > 0;
    }
    public bool A_but_pressed()
    {
        return abut==1;
    }
    public bool A_but_released()
    {
        return pad1.Buttons.A == ButtonState.Released && pad1_previous.Buttons.A == ButtonState.Pressed;
    }
    public void A_but_cancel()
    {
        abut=-1;
    }

    // B Button
    public bool B_but_down()
    {
        return bbut > 0;
    }

    public bool B_but_pressed()
    {
        return bbut == 1;
    }

    public bool B_but_released()
    {
        return pad1.Buttons.B == ButtonState.Released && pad1_previous.Buttons.B == ButtonState.Pressed;
    }

    public void B_but_cancel()
    {
        bbut = -1;
    }


    // X Button
    public bool X_but_down()
    {
        return xbut > 0;
    }

    public bool X_but_pressed()
    {
        return xbut == 1;
    }

    public bool X_but_released()
    {
        return pad1.Buttons.X == ButtonState.Released && pad1_previous.Buttons.X == ButtonState.Pressed;
    }

    public void X_but_cancel()
    {
        xbut = -1;
    }


    // Y Button
    public bool Y_but_down()
    {
        return ybut > 0;
    }

    public bool Y_but_pressed()
    {
        return ybut == 1;
    }

    public bool Y_but_released()
    {
        return pad1.Buttons.Y == ButtonState.Released && pad1_previous.Buttons.Y == ButtonState.Pressed;
    }

    public void Y_but_cancel()
    {
        ybut = -1;
    }

    // Right
    public bool Right_but_down()
    {
        return rightbut > 0;
    }

    public bool Right_but_pressed()
    {
        return rightbut == 1;
    }

    public bool Right_but_released()
    {
        return pad1.DPad.Right == ButtonState.Released && pad1_previous.DPad.Right == ButtonState.Pressed;
    }

    public void Right_but_cancel()
    {
        rightbut = -1;
    }


    // Up
    public bool Up_but_down()
    {
        return upbut > 0;
    }

    public bool Up_but_pressed()
    {
        return upbut == 1;
    }

    public bool Up_but_released()
    {
        return pad1.DPad.Up == ButtonState.Released && pad1_previous.DPad.Up == ButtonState.Pressed;
    }

    public void Up_but_cancel()
    {
        upbut = -1;
    }


    // Left
    public bool Left_but_down()
    {
        return leftbut > 0;
    }

    public bool Left_but_pressed()
    {
        return leftbut == 1;
    }

    public bool Left_but_released()
    {
        return pad1.DPad.Left == ButtonState.Released && pad1_previous.DPad.Left == ButtonState.Pressed;
    }

    public void Left_but_cancel()
    {
        leftbut = -1;
    }


    // Down
    public bool Down_but_down()
    {
        return downbut > 0;
    }

    public bool Down_but_pressed()
    {
        return downbut == 1;
    }

    public bool Down_but_released()
    {
        return pad1.DPad.Down == ButtonState.Released && pad1_previous.DPad.Down == ButtonState.Pressed;
    }

    public void Down_but_cancel()
    {
        downbut = -1;
    }
   

    // Start
    public bool Start_but_down()
    {
        return startbut > 0;
    }

    public bool Start_but_pressed()
    {
        return startbut == 1;
    }

    public bool Start_but_released()
    {
        return pad1.Buttons.Start == ButtonState.Released && pad1_previous.Buttons.Start == ButtonState.Pressed;
    }

    public void Start_but_cancel()
    {
        startbut = -1;
    }


    // Select / Back
    public bool Select_but_down()
    {
        return selectbut > 0;
    }

    public bool Select_but_pressed()
    {
        return selectbut == 1;
    }

    public bool Select_but_released()
    {
        return pad1.Buttons.Back == ButtonState.Released && pad1_previous.Buttons.Back == ButtonState.Pressed;
    }

    public void Select_but_cancel()
    {
        selectbut = -1;
    }


    // Right Shoulder
    public bool RB_but_down()
    {
        return rb > 0;
    }

    public bool RB_but_pressed()
    {
        return rb == 1;
    }

    public bool RB_but_released()
    {
        return pad1.Buttons.RightShoulder == ButtonState.Released && pad1_previous.Buttons.RightShoulder == ButtonState.Pressed;
    }

    public void RB_but_cancel()
    {
        rb = -1;
    }


    // Left Shoulder
    public bool LB_but_down()
    {
        return lb > 0;
    }

    public bool LB_but_pressed()
    {
        return lb == 1;
    }

    public bool LB_but_released()
    {
        return pad1.Buttons.LeftShoulder == ButtonState.Released && pad1_previous.Buttons.LeftShoulder == ButtonState.Pressed;
    }

    public void LB_but_cancel()
    {
        lb = -1;
    }

    // Now the bloody triggers :(
    // Right Trigger
    public bool RT_but_down()
    {
        return rt > 0;
    }

    public bool RT_but_pressed()
    {
        return rt == 1;
    }

    public bool RT_but_released()
    {
        return rt==0 && pad1_previous.Triggers.Right > 0.09f;
    }

    public void RT_but_cancel()
    {
        rt = -1;
    }


    // Left Trigger
    public bool LT_but_down()
    {
        return lt > 0;
    }

    public bool LT_but_pressed()
    {
        return lt == 1;
    }

    public bool LT_but_released()
    {
        return lt==0 && pad1_previous.Triggers.Left > 0.09f;
    }

    public void LT_but_cancel()
    {
        lt = -1;
    }


    // Let's get the values of the analogue sticks
    public float stick_right_x()
    {
        return pad1.ThumbSticks.Right.X;
    }
    public float stick_right_y()
    {
        return pad1.ThumbSticks.Right.Y;
    }

   public float stick_left_x()
    {
        return pad1.ThumbSticks.Left.X;
    }
    public float stick_left_y()
    {
        return pad1.ThumbSticks.Left.Y;
    }

}
