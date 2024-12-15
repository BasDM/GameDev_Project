using GameDev_Project.AreaGameComponents;
using GameDev_Project.Camera_s;
using GameDev_Project.Characters;
using GameDev_Project.Characters.Enemies;
using GameDev_Project.Events;
using GameDev_Project.Factories;
using GameDev_Project.GameComponents;
using GameDev_Project.Input;
using GameDev_Project.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
namespace GameDev_Project.Scenes
{
    public class GameScene : Scene
    {
        Texture2D heroTexture;
        Texture2D enemyTexture;
        Texture2D runawayEnemyTexture;
        Texture2D flyingEnemyTexture;
        public static Hero Hero;
        public static Enemy Enemy;
        public static RunawayEnemy runawayEnemy;
        public static FlyingEnemy flyingEnemy;
        UserInterface ui;
        EnemyHealthBar enemyHealthBar;
        EnemyHealthBar runawayEnemyHealthBar;
        EnemyHealthBar flyingEnemyHealthBar;

        //Tiles
        List<Block> blocks = new List<Block>();
        public static Texture2D BlockTexture;

        //Background
        public static Rectangle Screen;
        public static Texture2D BackgroundTexture;
        public Background Background;

        //Sounds
        private SoundEffect slashEffect;
        private float soundEffectVolume = 0.40f;
        private SoundEffectInstance slashEffectInstance;

        //Camera
        private FollowingCamera camera;

        //Music
        private Song themeSong;


        int[,] gameBoard = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
        };
        public GameScene(Game1 game) : base(game)
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            heroTexture = game.Content.Load<Texture2D>("NightBorne");
            BlockTexture = game.Content.Load<Texture2D>("[64x64] Dungeon Bricks Plain");
            BackgroundTexture = game.Content.Load<Texture2D>("crystal_cave_background");
            enemyTexture = game.Content.Load<Texture2D>("Skeleton enemy");
            runawayEnemyTexture = game.Content.Load<Texture2D>("Rotbo-Sheet");
            flyingEnemyTexture = game.Content.Load<Texture2D>("DroneR-Sheet");

            //Sounds
            slashEffect = game.Content.Load<SoundEffect>(@"sounds\sword-slash-and-swing-185432");
            slashEffectInstance = slashEffect.CreateInstance();
            themeSong = game.Content.Load<Song>(@"music\dark8bitThemesong");

            Hero = new Hero(heroTexture, new KeyboardReader(), game.GraphicsDevice);
            Enemy = new Enemy(new Vector2(400, 20), enemyTexture, game.GraphicsDevice, Hero);
            runawayEnemy = new RunawayEnemy(new Vector2(1000,20), runawayEnemyTexture, game.GraphicsDevice, Hero);
            flyingEnemy = new FlyingEnemy(new Vector2(1200, 20), flyingEnemyTexture, game.GraphicsDevice, Hero);
            blocks = MapFactory.CreateBlocks(this.gameBoard);
            CollisionHandler.AddCharacter(Hero);

            ui = new UserInterface(Hero, game.Content, 20, 20, new Vector2(10, 10));
            enemyHealthBar = new EnemyHealthBar(Enemy, game.Content, 20, 20);
            runawayEnemyHealthBar = new EnemyHealthBar(runawayEnemy, game.Content, 20, 20);
            flyingEnemyHealthBar = new EnemyHealthBar(flyingEnemy, game.Content, 20, 20);
            Background = new Background();

            //Camera and screen
            Screen = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            camera = new FollowingCamera(Vector2.Zero);

            //Music
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = soundEffectVolume;
            MediaPlayer.Play(themeSong);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Hero.ChangeInput(new KeyboardReader());
            }

            //-Optional-
            //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    Hero.ChangeInput(new MouseReader());
            //}

            //Attack sound effect
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (slashEffectInstance.State != SoundState.Playing)
                {
                    slashEffectInstance.Volume = soundEffectVolume;
                    slashEffectInstance.Play();

                    if (Hero.BoundingBox.Intersects(Enemy.BoundingBox))
                        Hero.Attack(Enemy);
                    if (Hero.BoundingBox.Intersects(runawayEnemy.BoundingBox))
                        Hero.Attack(runawayEnemy);
                    if (Hero.BoundingBox.Intersects(flyingEnemy.BoundingBox))
                        Hero.Attack(flyingEnemy);
                }
            }

            //Play or pause music
            if (Keyboard.GetState().IsKeyDown(Keys.M) && MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.M) && MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = soundEffectVolume;
                MediaPlayer.Play(themeSong);
            }

            Hero.Update(gameTime);
            Enemy.Update(gameTime);
            enemyHealthBar.Update(gameTime);
            runawayEnemy.Update(gameTime);
            runawayEnemyHealthBar.Update(gameTime);
            flyingEnemy.Update(gameTime);
            flyingEnemyHealthBar.Update(gameTime);
            camera.Follow(Hero.Position, new Rectangle(0, 0, Screen.Width, Screen.Height));
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);

            Matrix cameraTransform = Matrix.CreateTranslation(new Vector3(-camera.Position, 0));

            _spriteBatch.Begin(transformMatrix: cameraTransform, samplerState: SamplerState.PointClamp);
            Background.Draw(_spriteBatch, BackgroundTexture, camera.Position, Screen);
            foreach (var item in blocks)
            {
                item.Draw(_spriteBatch);
            }

            Enemy.Draw(_spriteBatch);
            enemyHealthBar.Draw(_spriteBatch);
            runawayEnemy.Draw(_spriteBatch);
            runawayEnemyHealthBar.Draw(_spriteBatch);
            flyingEnemy.Draw(_spriteBatch);
            flyingEnemyHealthBar.Draw(_spriteBatch);
            Hero.Draw(_spriteBatch);
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            ui.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(_spriteBatch);
        }
    }
}
