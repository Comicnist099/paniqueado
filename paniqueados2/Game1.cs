using System.Text;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace paniqueados2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textura;
        private Rectangle _rectangule;
        private Vector2 posicionPlayer;

//Rastroo
         private Texture2D _texturaRastro;
        private Rectangle _rectanguleRastro;
        SpriteFont font;

        int LimitX=1000;
        int LimitY=700;


public void LimitMap(){
    if(posicionPlayer.X<=0){
           posicionPlayer.X=0;
    }
      if(posicionPlayer.X>=LimitX){
           posicionPlayer.X=LimitX-10;
    }
     if(posicionPlayer.Y<=0){
           posicionPlayer.Y=0;
    }
     if(posicionPlayer.Y>=LimitY){
           posicionPlayer.Y=LimitY-10;
    }
}
            public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";
            
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
         
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth=LimitX;
            _graphics.PreferredBackBufferHeight=LimitY;
            _graphics.IsFullScreen=false;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            posicionPlayer=new Vector2(0,0);
            font =Content.Load<SpriteFont>("File");

            _textura =Content.Load<Texture2D>("puntito");
            _texturaRastro =Content.Load<Texture2D>("rastro");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(); 

                LimitMap();

               _rectangule=new Rectangle((int)posicionPlayer.X,(int)posicionPlayer.Y,10,10);
               _rectanguleRastro=new Rectangle((int)posicionPlayer.X,(int)posicionPlayer.Y,10,10);

                if(Keyboard.GetState().IsKeyDown(Keys.Right)){
                posicionPlayer.X+=10;}
                if(Keyboard.GetState().IsKeyDown(Keys.Left)){
                posicionPlayer.X-=10;}
                if(Keyboard.GetState().IsKeyDown(Keys.Up)){
                posicionPlayer.Y-=10;}
                if(Keyboard.GetState().IsKeyDown(Keys.Down)){
                posicionPlayer.Y+=10;}
               



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);




                _spriteBatch.Begin();

                ///PUNTO
                _spriteBatch.Draw(_textura,_rectangule,Color.White);
                ///Texto
                Vector2 textMiddlePoint = font.MeasureString("text") / 2;
                Vector2 position2 = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);

                string playerX = new StringBuilder().Append(posicionPlayer.X).ToString();
                string playerY = new StringBuilder().Append(posicionPlayer.Y).ToString();

                _spriteBatch.DrawString(font, "X:"+playerX+" Y:"+playerY, position2, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

                _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
    
}
