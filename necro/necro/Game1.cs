using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using necro.Classes;
using System.Diagnostics;
using System.IO;




namespace necro;

public class Game1 : Game
{
    // For changing the internal resolution
    private RenderTarget2D _renderTarget;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public Camera obj_camera;
    public Random obj_random;
    
    
    // Declare the sprites/textures
    Texture2D spr_skellybob;
    Texture2D spr_necro;
    Texture2D spr_gamebar;
    Texture2D spr_cursor;
    Texture2D spr_grass;
    Texture2D spr_sky_marker;
    Texture2D spr_grass_tiles;

    // Object references
    Necro obj_necro;
    //Skellybob obj_skellybob;
    Controller pad;
    LevelController obj_level_controller;

    // Add any fonts
    SpriteFont fnt_goldbox;
    SpriteFont fnt_cairo;
    SpriteFont fnt_porto;
    private Texture2D spr_dbreak2;
    private DBreak2Font fnt_dbreak2;


    // List for the Y positions
    List<Entity> entities = new List<Entity>();


    // Misc
    float state = 5;

    


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 512;
        _graphics.PreferredBackBufferHeight = 512;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // Let's create the camera
        obj_camera = new Camera();

        // and the controller class
        //pad = new Controller();

        // Let's make a random class
        obj_random=new Random();

        // Load the assets in the new global class
        Global.load_content(Content,  GraphicsDevice, _spriteBatch);
        Global.camera = obj_camera;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Create the spritebatch and then send it to the Global class.
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Global.sprite_batch = _spriteBatch;

        // Change the internal resolution
        _renderTarget = new RenderTarget2D(GraphicsDevice,128,128);

        


        // Let's load the sprites into the game
        //spr_necro=Content.Load<Texture2D>("necro");
        //spr_skellybob=Content.Load<Texture2D>("skelly");
        spr_gamebar=Content.Load<Texture2D>("gamebar");
        //spr_cursor=Content.Load<Texture2D>("cursor");
        //spr_grass=Content.Load<Texture2D>("grass");
        //spr_sky_marker=Content.Load<Texture2D>("skymarker");
        //spr_grass_tiles=Content.Load<Texture2D>("grasssprites");
        
        // Load any fonts
        //fnt_goldbox=Content.Load<SpriteFont>("fonts/goldbox");
        //fnt_cairo=Content.Load<SpriteFont>("fonts/cairopixel");
        //fnt_porto=Content.Load<SpriteFont>("fonts/portobuena");
      

        // Create the classes
        //entities.Add(obj_necro = new Necro(_spriteBatch, ref entities, new Vector2(64, 64), pad, this));
        entities.Add(new Skellybob(_spriteBatch, ref entities, new Vector2(8, 8)));
        entities.Add(new Skellybob(_spriteBatch, ref entities, new Vector2(76, 76)));
        entities.Add(new Skellybob(_spriteBatch, ref entities, new Vector2(8*1, 64)));
        entities.Add(new Skellybob(_spriteBatch, ref entities, new Vector2(8*3, 64)));
        entities.Add(new Skellybob(_spriteBatch, ref entities, new Vector2(8*5, 64)));
        entities.Add(new Skellybob(_spriteBatch, ref entities, new Vector2(8*7, 64)));
        
        

        // Create the grass
        create_grass_tiles_2();
        
        // Let's create the level controller here
        obj_level_controller = new LevelController();
        

        
    }

    // Step Event
    protected override void Update(GameTime gameTime)
    {
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        // Update the controller class
        Global.gamepad.step_event();

        // Run the level controller's step event
        obj_level_controller?.step_event();
        
        // Step event for the player
        //obj_necro.step_event(obj_camera.room);

        // Step event for the skellybobs
        foreach(Entity p in entities)
        {
            if (p is Skellybob)
            {
	            p.step_event();
            }
        }

        // Step event for the sky marker
        foreach(Entity p in entities)
        {
            if (p is Sky_marker)
            {
	            p.step_event();
            }
        }

        // Delete unused entities
        entities.RemoveAll(p => p.todelete);


        //// Camera 
        //// First we check as to whether the player's state is 2 or not. 
        //if (obj_necro.get_state==2)
        //{   
        //    Vector2 mid = Vector2.Lerp(obj_necro.get_position,obj_necro.get_position+obj_necro.get_cursor,.5f);
        //    obj_camera.update_camera(mid.X,mid.Y);
        //    //Debug.WriteLine($"cam x = {mid.X}, cam y = {mid.Y}");
        //}
      //// Instead we just focus on the player
        //else 
        //    obj_camera.update_camera(obj_necro.x,obj_necro.y);
        
       //Right, let's see if this code works to sort the list by class:
        entities.Sort((a, b) => a.y.CompareTo(b.y));


        base.Update(gameTime);
    }

    // Draw Event
    protected override void Draw(GameTime gameTime)
    {
        // Draw the actual game at 128x128
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.Black);



        // Let's see if we can get the camera working.
        // (let's try to stop the pixels flickering back and forth)
        float camera_x = MathF.Round(obj_camera.position.X);
        float camera_y = MathF.Round(obj_camera.position.Y);
        //Matrix cameraMatrix = Matrix.CreateTranslation(-obj_camera.position.X,-obj_camera.position.Y,0);
        Matrix cameraMatrix = Matrix.CreateTranslation(-camera_x,-camera_y,0);

        // try the new Begin
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp,transformMatrix: cameraMatrix);
               

        // Let's draw the cursor first as it needs to be below everything
        //obj_necro.draw_cursor();

        //obj_skellybob.draw_event();

        foreach (Entity entity in entities)
        {
            entity.draw_event();
        }

        obj_level_controller?.draw_event();

        _spriteBatch.End();


        // DRAW THE GUI    
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        obj_level_controller?.draw_event_gui();
        _spriteBatch.End();



        // Draw all of this to the original 512*512 window
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(_renderTarget,new Rectangle(0, 0, 512, 512),Color.White);
        _spriteBatch.End();
        

        base.Draw(gameTime);
    }

    #region   // Methods, getters and setters!
    
    ////////////////////////////////////
    // Methods, getters, setters
    ////////////////////////////////////
    public void create_sky_marker(Vector2 position)
    {
        entities.Add(new Sky_marker(_spriteBatch, ref entities, position));
    }


    public void create_grass_tiles_2()
    {
        int gap=14;
        int gap2;
        int wid=obj_camera.room.Width;
        int hei=obj_camera.room.Height;
        int xpos=0;
        int ypos=4;

        //for (int ypos=4; ypos<hei; ypos++)
        do
        {
            // Get a random amount to jump by
            //gap2=2+obj_random.Next(8);
            xpos+=gap;

            // Create the grass class
            entities.Add(new Grasstile(_spriteBatch, ref entities, new Vector2(xpos+(obj_random.Next(10)-4), ypos+(obj_random.Next(10)-4)),obj_random.Next(11)));
            //entities.Add(new Grasstile(_spriteBatch, ref entities, spr_grass_tiles, new Vector2(xpos, ypos),obj_random.Next(11)));

            // Has the variable gone past the edge of the screen? If so, move downwards, and move back.
            if (xpos>wid)
            {
                xpos=0;
                ypos+=10;
            }    
        }
        while
            (ypos<hei);
        
    }
     #endregion

}
   


