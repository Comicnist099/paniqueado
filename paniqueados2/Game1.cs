using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Text;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;







namespace paniqueados2
{

    public class Tile {
    int tileX;
    int tileY;
    char estado;
    int ancho;
    int alto;

        public Tile(int x, int y, char s) {
            this.tileX = x;
            this.tileY = y;
            this.estado = s;
        }

        public int getX() {
            return (this.tileX);
        }

        public int getY() {
            return (this.tileY);
        }

        public int getAncho() {
            return (this.ancho);
        }

        public int getAlto() {
            return (this.alto);
        }

        public char getEstado() {
            return (this.estado);
        }

        public void cambiarEstado(char estadoNuevo) {
            this.estado = estadoNuevo;
        }
    }

    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textura;
        private Rectangle _rectangule;
        private Vector2 posicionPlayer;
        private Vector2 posicionPlayerAnte;

        float time;
        int contador = 0;
        int a = 0;

        historyLine[] _historyLine = new historyLine[7000];


        public List<Vector2> pixelScreen = new List<Vector2>();
        List<Tile> tilesPantalla = new List<Tile>();

        //Rastroo
        private Texture2D _texturaRastro;
        private Rectangle _rectanguleRastro;
        SpriteFont font;

        int LimitX = 1000;
        int LimitY = 700;

        public bool tocandoArea(Vector2 posPlayer, List<Tile> listatiles) {
            bool res = false;
            for(int i = 0; i < listatiles.Count; i++) {
                if (listatiles[i].getEstado() == 'C') {
                    if (posPlayer.X == listatiles[i].getX() && posPlayer.Y == listatiles[i].getY()) {
                        res = true; break;
                    }
                }
            }
            return res;
            
        }

        public void cerrarTiles(List<Tile> listatiles) {
            List<Tile> nuevaLista = listatiles;
        }

        public bool verificarRepetido(List<Tile> listatiles, Tile tileAgregar) {
            bool res = false;
            for(int i = 0; i < listatiles.Count; i++) {
                if(tileAgregar.getX() <= (listatiles[i].getX() + 9) && tileAgregar.getX() >= (listatiles[i].getX() - 9)) {
                    if(tileAgregar.getY() <= (listatiles[i].getY() + 9) && tileAgregar.getY() >= (listatiles[i].getY() - 9) && listatiles[i].getEstado() == 'C') { res = true;}
                }

                if(res == false) {
                    if(tileAgregar.getX() == listatiles[i].getX() && tileAgregar.getY() == listatiles[i].getY()) res = true;
                }

            }

            
            return res;
        }

        public bool generarAreaInicial(List<Tile> listaTiles) {
            List<Tile> nuevaLista = listaTiles;
            bool res = true;
            Random r = new Random();
            int areaX, areaY;
            int areaDescubierta = 100;
            int randomX = r.Next(0,LimitX-20);
            randomX = randomX - (randomX%5);

            int randomY = r.Next(0,LimitY-20);
            randomY = randomY - (randomY%5);



            
            int numRand = r.Next(2,10);
            numRand = numRand - (numRand%2);
            areaX = numRand;

            areaY = areaDescubierta / areaX;
            for(int j = 0; j < areaY; j++) {
                for(int i = 0; i < areaX; i++) {

                    if ((randomX + i*10) > LimitX || (randomY + j*10) > LimitY) {
                        res = false;
                    }
                    else {
                        Tile tileNuevo = new Tile((randomX + i*10), (randomY + j*10), 'C');
                        nuevaLista.Add(tileNuevo);
                    }
                }
            }
            
            if (res == true) { listaTiles = nuevaLista; }
            return res;

        }

        public void LimitMap()
        {
            if (posicionPlayer.X <= 0)
            {
                posicionPlayer.X = 0;
            }
            if (posicionPlayer.X >= LimitX)
            {
                posicionPlayer.X = LimitX - 10;
            }
            if (posicionPlayer.Y <= 0)
            {
                posicionPlayer.Y = 0;
            }
            if (posicionPlayer.Y >= LimitY)
            {
                posicionPlayer.Y = LimitY - 10;
            }
        }



        Texture2D pixel;

        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            posicionPlayerAnte = posicionPlayer;
            

            historyLine _historyLineDraw = new historyLine(pixel, posicionPlayerAnte);

            _historyLine[0] = _historyLineDraw;
            _historyLine[1] = _historyLineDraw;

            pixelScreen.Add(posicionPlayer);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[1] { Color.White });        // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = LimitX;
            _graphics.PreferredBackBufferHeight = LimitY;
            _graphics.IsFullScreen = false;
            
            _graphics.ApplyChanges();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            int total = LimitX * LimitY;




            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bool areaGenerada = false;
            while (!areaGenerada) {
                areaGenerada = generarAreaInicial(tilesPantalla);
            }
            

            if(tilesPantalla.Count > 0) {
                posicionPlayer = new Vector2(tilesPantalla[0].getX(), tilesPantalla[0].getY());
                posicionPlayerAnte = new Vector2(tilesPantalla[0].getX(), tilesPantalla[0].getY());
            }
            else {
                posicionPlayer = new Vector2(0, 0);
                posicionPlayerAnte = new Vector2(0, 0);
            }

            font = Content.Load<SpriteFont>("File");

            _textura = Content.Load<Texture2D>("puntito");
            _texturaRastro = Content.Load<Texture2D>("rastro");
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            contador++;
            time = contador / 1000;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            LimitMap();


            _rectangule = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);
            _rectanguleRastro = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);


            historyLine _historyLineDraw = new historyLine(pixel, posicionPlayerAnte);

            _historyLine[a] = _historyLineDraw;
       



            if (true)
            {
                Trazar(tilesPantalla);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) )
            {

                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.X += 5;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.Y -= 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S) )
            {
                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.Y += 5;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string playerX = "";
            string playerY = "";
            Vector2 position2 = new Vector2(10, 10);
            Vector2 textMiddlePoint = font.MeasureString("text") / 2;


            _spriteBatch.Begin();
            for(int i = 0; i < tilesPantalla.Count; i++) {
                if (tilesPantalla[i].getEstado() == 'C') {
                    _spriteBatch.Draw(pixel, new Rectangle(tilesPantalla[i].getX(), tilesPantalla[i].getY(), 10, 10), Color.Blue);
                }
                else {
                    _spriteBatch.Draw(pixel, new Rectangle(tilesPantalla[i].getX(), tilesPantalla[i].getY(), 10, 10), Color.Red);
                }
                
            }
            
            ///Texto
            playerX = new StringBuilder().Append(posicionPlayer.X).ToString();
            playerY = new StringBuilder().Append(posicionPlayer.Y).ToString();
            _spriteBatch.Draw(_textura, _rectangule, Color.White);
            _spriteBatch.DrawString(font, "X:" + playerX + " Y:" + playerY + "Array:" + _historyLine[a].getPosition(), position2, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            ///PUNTO

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        private void Trazar(List<Tile> tilesPantalla)
        {
            Tile tileNuevo = new Tile((int) posicionPlayer.X, (int) posicionPlayer.Y, 'A');
            if (!verificarRepetido(tilesPantalla, tileNuevo)) {
                tilesPantalla.Add(tileNuevo);
            }
            

            if(tocandoArea(posicionPlayer, tilesPantalla)) {
                cerrarTiles(tilesPantalla);
            }

        }
    }

}
