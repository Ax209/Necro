using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using necro.Classes;

namespace necro.Classes;

public static class Global
{

    #region // Declare the variables

    // sprites
    public static Texture2D spr_necro;
    public static Texture2D spr_skellybob;
    public static Texture2D spr_gamebar;
    public static Texture2D spr_cursor;
    public static Texture2D spr_grass;
    public static Texture2D spr_sky_marker;
    public static Texture2D spr_grass_tiles;
    public static Texture2D spr_dbreak2;


    // fonts
    public static SpriteFont fnt_goldbox;
    public static SpriteFont fnt_cairo;
    public static SpriteFont fnt_porto;
    public static DBreak2Font fnt_dbreak2;

    // instances
    public static Random random;
    public static SpriteBatch sprite_batch;
    public static Controller gamepad;
    public static Camera camera;



    #endregion



    // Constructor

    static Global()
    {
        

    }

    public static void load_content(ContentManager Content, GraphicsDevice GraphicsDevice, SpriteBatch sprite_batch)
    {
        //////////////////////////////////////
        //      SPRITES
        //////////////////////////////////////
        spr_necro = Content.Load<Texture2D>("necro");
        spr_skellybob=Content.Load<Texture2D>("skelly");
        spr_gamebar=Content.Load<Texture2D>("gamebar");
        spr_cursor=Content.Load<Texture2D>("cursor");
        spr_grass=Content.Load<Texture2D>("grass");
        spr_sky_marker=Content.Load<Texture2D>("skymarker");
        spr_grass_tiles=Content.Load<Texture2D>("grasssprites");

        //////////////////////////////////////
        //      FONTS
        //////////////////////////////////////
        fnt_goldbox=Content.Load<SpriteFont>("fonts/goldbox");
        fnt_cairo=Content.Load<SpriteFont>("fonts/cairopixel");
        fnt_porto=Content.Load<SpriteFont>("fonts/portobuena");
        using (var stream = File.OpenRead("Content/Fonts/dbreak2.png"))
            spr_dbreak2 = Texture2D.FromStream(GraphicsDevice, stream);
        fnt_dbreak2 = new DBreak2Font(spr_dbreak2, 5, 5);

        //////////////////////////////////////
        //      CREATE INSTANCES
        //////////////////////////////////////
        random = new Random();
        gamepad=gamepad = new Controller();


        
    }

   





}