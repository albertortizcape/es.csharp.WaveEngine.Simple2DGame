#region Using Statements
using Simple2DGameModels;
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Animation;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Resources;
using WaveEngine.Framework.Services;
#endregion

namespace Simple2DGameProject
{
    public class MyScene : Scene
    {
        protected override void CreateScene()
        {
            FixedCamera2D camera2D = new FixedCamera2D("Camera2D")
            {
                BackgroundColor = Color.Black
            };
            this.EntityManager.Add(camera2D);

            // Poner fondo
            CreateBackGround();

            // Poner monigote
            CreatePlayer();

            // Recuadarar monigote para saber en que celda está con un color verduzco

            // Darle capacidad de movimiento al monigote
            // Poner obstaculos y respetarlos
        }

        private void CreatePlayer()
        {
            // Create Players
            float XPosition = WaveServices.Platform.ScreenWidth / 2;
            float YPosition = WaveServices.Platform.ScreenHeight / 2;
            var player = new Entity("Player")
                .AddComponent(new Transform2D()
                {
                    Origin = new Vector2(0.5f, 1),
                    X = XPosition,
                    Y = YPosition
                })
                .AddComponent(new Sprite("Content/Sprite.png"))
                .AddComponent(Animation2D.Create<TexturePackerGenericXml>("Content/Sprite.xml")
                .Add("VerticalIdle", new SpriteSheetAnimationSequence()
                {
                    First = 10,
                    Length = 1,
                    FramesPerSecond = 1
                })
                .Add("VerticalWalking", new SpriteSheetAnimationSequence()
                {
                    First = 11,
                    Length = 2,
                    FramesPerSecond = 10
                })
                .Add("HorizontalIdle", new SpriteSheetAnimationSequence()
                {
                    First = 7,
                    Length = 1,
                    FramesPerSecond = 1
                })
                .Add("HorizontalWalking", new SpriteSheetAnimationSequence()
                {
                    First = 8,
                    Length = 2,
                    FramesPerSecond = 10
                })
                .Add("DiagonalIdle", new SpriteSheetAnimationSequence()
                {
                    First = 1,
                    Length = 1,
                    FramesPerSecond = 1
                })
                .Add("DiagonalWalking", new SpriteSheetAnimationSequence()
                {
                    First = 5,
                    Length = 2,
                    FramesPerSecond = 10
                })
                .Add("DiagonalInvIdle", new SpriteSheetAnimationSequence()
                {
                    First = 2,
                    Length = 1,
                    FramesPerSecond = 1
                })
                .Add("DiagonalInvWalking", new SpriteSheetAnimationSequence()
                {
                    First = 3,
                    Length = 2,
                    FramesPerSecond = 10
                })
                )
                .AddComponent(new AnimatedSpriteRenderer())
                //.AddComponent(new RectangleCollider())
                .AddComponent(new Player(XPosition, YPosition, PlayerOrientation.South, 5, 100, 100));
            this.EntityManager.Add(player);

            var anim2D = player.FindComponent<Animation2D>();
            anim2D.Play(true);
        }

        private void CreateBackGround()
        {
            var background = new Entity("Background")
                .AddComponent(new Sprite("Content/Background.png"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    DrawOrder = 1,
                    Origin = new Vector2(0.5f, 1),
                    X = WaveServices.Platform.ScreenWidth / 2,
                    Y = WaveServices.Platform.ScreenHeight
                });
            this.EntityManager.Add(background);
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }
    }
}
