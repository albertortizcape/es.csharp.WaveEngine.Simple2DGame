using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Input;
using WaveEngine.Components.Animation;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace Simple2DGameModels
{
    public enum PlayerOrientation
    {
        North = 0,
        NorthEast = 1,
        East = 2,
        SouthEast = 3,
        South = 4,
        SouthWest = 5,
        West = 6,
        NorthWest = 7
    }

    public enum PlayerState
    {
        Idle = 0,
        Walking = 1
    }

    public class Player : Behavior
    {
        private int MOVEMENT_SPEED;
        private int YBORDER_OFFSET;
        private int XBORDER_OFFSET;

        [RequiredComponent]
        public Animation2D anim2D;
        [RequiredComponent]
        public Transform2D trans2D;

        private float _XPlayerPosition;
        private float _YPlayerPosition;
        private PlayerOrientation _CurrentOrientation, _LastOrientation;
        private PlayerState _CurrentState, _LastState;
        private int _XDirection;
        private int _YDirection;

        public Player()
        {
            this.XPlayerPosition = 0;
            this.YPlayerPosition = 0;

            this.MOVEMENT_SPEED = 5;
            this.YBORDER_OFFSET = 100;
            this.XBORDER_OFFSET = 100;

            CurrentOrientation = PlayerOrientation.South;
            LastOrientation = PlayerOrientation.South;
            CurrentState = PlayerState.Idle;
            LastState = PlayerState.Idle;

            this.anim2D = null;
            this.trans2D = null;
        }

        public Player(float pXpos, float pYpos, PlayerOrientation pOrientation, int pPlayerSpeed, int pYBorderOffset, int pXBorderOffset)
        {
            this.XPlayerPosition = pXpos;
            this.YPlayerPosition = pYpos;

            this.MOVEMENT_SPEED = pPlayerSpeed;
            this.YBORDER_OFFSET = pYBorderOffset;
            this.XBORDER_OFFSET = pXBorderOffset;
            
            CurrentOrientation = pOrientation;
            LastOrientation = pOrientation;
            CurrentState = PlayerState.Idle;
            LastState = PlayerState.Idle;


            this.anim2D = null;
            this.trans2D = null;
        }

        #region Properties

        public float XPlayerPosition
        {
            get
            {
                return _XPlayerPosition;
            }
            set
            {
                _XPlayerPosition = value;
            }
        }

        public float YPlayerPosition
        {
            get
            {
                return _YPlayerPosition;
            }
            set
            {
                _YPlayerPosition = value;
            }
        }

        public PlayerOrientation CurrentOrientation
        {
            get
            {
                return _CurrentOrientation;
            }
            set
            {
                _CurrentOrientation = value;
            }
        }
        
        public PlayerOrientation LastOrientation
        {
            get
            {
                return _LastOrientation;
            }
            set
            {
                _LastOrientation = value;
            }
        }

        public PlayerState CurrentState
        {
            get
            {
                return _CurrentState;
            }
            set
            {
                _CurrentState = value;
            }
        }

        public PlayerState LastState
        {
            get
            {
                return _LastState;
            }
            set
            {
                _LastState = value;
            }
        }

        public int XDirection
        {
            get
            {
                return _XDirection;
            }
            set
            {
                _XDirection = value;
            }
        }

        public int YDirection
        {
            get
            {
                return _YDirection;
            }
            set
            {
                _YDirection = value;
            }
        }

        #endregion Properties

        protected override void Update(TimeSpan gameTime)
        {
            CurrentState = PlayerState.Idle;

            KeyboardInput();

            // Set current animation if that one is diferent
            if (CurrentState != LastState || CurrentOrientation != LastOrientation)
            {
                ChangePlayerSprite_Walking();
            }
            else if(CurrentState == PlayerState.Idle)
            {
                ChangePlayerSprite_Idle();
            }

            LastOrientation = CurrentOrientation;
            LastState = CurrentState;

            // Move sprite
            trans2D.X += XDirection * MOVEMENT_SPEED * (gameTime.Milliseconds / 10);
            trans2D.Y += YDirection * MOVEMENT_SPEED * (gameTime.Milliseconds / 10);

            XPlayerPosition = trans2D.X;
            YPlayerPosition = trans2D.Y;

            CheckScreenBorders();
        }

        private void CheckScreenBorders()
        {
            // Check X borders
            if (trans2D.X < XBORDER_OFFSET)
            {
                trans2D.X = XBORDER_OFFSET;
            }
            else if (trans2D.X > WaveServices.Platform.ScreenWidth - XBORDER_OFFSET)
            {
                trans2D.X = WaveServices.Platform.ScreenWidth - XBORDER_OFFSET;
            }

            // Check Y borders
            if (trans2D.Y < YBORDER_OFFSET)
            {
                trans2D.Y = YBORDER_OFFSET;
            }
            else if (trans2D.Y > WaveServices.Platform.ScreenHeight - YBORDER_OFFSET)
            {
                trans2D.Y = WaveServices.Platform.ScreenHeight - YBORDER_OFFSET;
            }
        }

        private void ChangePlayerSprite_Idle()
        {
            XDirection = 0;
            YDirection = 0;

            switch (CurrentOrientation)
            {
                case PlayerOrientation.North:
                    anim2D.CurrentAnimation = "VerticalIdle";
                    trans2D.Effect = SpriteEffects.FlipVertically;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.NorthEast:
                    anim2D.CurrentAnimation = "DiagonalIdle";
                    trans2D.Effect = SpriteEffects.FlipVertically;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.East:
                    anim2D.CurrentAnimation = "HorizontalIdle";
                    trans2D.Effect = SpriteEffects.None;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.SouthEast:
                    anim2D.CurrentAnimation = "DiagonalIdle";
                    trans2D.Effect = SpriteEffects.None;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.South:
                    anim2D.CurrentAnimation = "VerticalIdle";
                    trans2D.Effect = SpriteEffects.None;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.SouthWest:
                    anim2D.CurrentAnimation = "DiagonalInvIdle";
                    trans2D.Effect = SpriteEffects.None;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.West:
                    anim2D.CurrentAnimation = "HorizontalIdle";
                    trans2D.Effect = SpriteEffects.FlipHorizontally;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.NorthWest:
                    anim2D.CurrentAnimation = "DiagonalInvIdle";
                    trans2D.Effect = SpriteEffects.FlipVertically;
                    anim2D.Play(true);
                    break;
            }
        }

        private void ChangePlayerSprite_Walking()
        {
            switch (CurrentOrientation)
            {
                case PlayerOrientation.North:
                    anim2D.CurrentAnimation = "VerticalWalking";
                    trans2D.Effect = SpriteEffects.FlipVertically;
                    XDirection = 0;
                    YDirection = -1;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.NorthEast:
                    anim2D.CurrentAnimation = "DiagonalWalking";
                    trans2D.Effect = SpriteEffects.FlipVertically;
                    XDirection = 1;
                    YDirection = -1;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.East:
                    anim2D.CurrentAnimation = "HorizontalWalking";
                    trans2D.Effect = SpriteEffects.None;
                    XDirection = 1;
                    YDirection = 0;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.SouthEast:
                    anim2D.CurrentAnimation = "DiagonalWalking";
                    trans2D.Effect = SpriteEffects.None;
                    XDirection = 1;
                    YDirection = 1;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.South:
                    anim2D.CurrentAnimation = "VerticalWalking";
                    trans2D.Effect = SpriteEffects.None;
                    XDirection = 0;
                    YDirection = 1;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.SouthWest:
                    anim2D.CurrentAnimation = "DiagonalInvWalking";
                    trans2D.Effect = SpriteEffects.None;
                    XDirection = -1;
                    YDirection = 1;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.West:
                    anim2D.CurrentAnimation = "HorizontalWalking";
                    XDirection = -1;
                    YDirection = 0;
                    trans2D.Effect = SpriteEffects.FlipHorizontally;
                    anim2D.Play(true);
                    break;
                case PlayerOrientation.NorthWest:
                    anim2D.CurrentAnimation = "DiagonalInvWalking";
                    trans2D.Effect = SpriteEffects.FlipVertically;
                    XDirection = -1;
                    YDirection = -1;
                    anim2D.Play(true);
                    break;
            }
        }

        private void KeyboardInput()
        {
            var keyboard = WaveServices.Input.KeyboardState;
            if (keyboard.Up == ButtonState.Pressed)
            {
                if (keyboard.Right == ButtonState.Pressed)
                {
                    CurrentOrientation = PlayerOrientation.NorthEast;
                    CurrentState = PlayerState.Walking;
                }
                else if (keyboard.Left == ButtonState.Pressed)
                {
                    CurrentOrientation = PlayerOrientation.NorthWest;
                    CurrentState = PlayerState.Walking;
                }
                else
                {
                    CurrentOrientation = PlayerOrientation.North;
                    CurrentState = PlayerState.Walking;
                }
            }
            else if (keyboard.Right == ButtonState.Pressed)
            {
                if (keyboard.Up == ButtonState.Pressed)
                {
                    CurrentOrientation = PlayerOrientation.NorthEast;
                    CurrentState = PlayerState.Walking;
                }
                else if (keyboard.Down == ButtonState.Pressed)
                {
                    CurrentOrientation = PlayerOrientation.SouthEast;
                    CurrentState = PlayerState.Walking;
                }
                else
                {
                    CurrentOrientation = PlayerOrientation.East;
                    CurrentState = PlayerState.Walking;
                }
            }
            else if (keyboard.Down == ButtonState.Pressed)
            {
                if (keyboard.Right == ButtonState.Pressed)
                {
                    CurrentOrientation = PlayerOrientation.SouthEast;
                    CurrentState = PlayerState.Walking;
                }
                else if (keyboard.Left == ButtonState.Pressed)
                {
                    CurrentOrientation = PlayerOrientation.SouthWest;
                    CurrentState = PlayerState.Walking;
                }
                else
                {
                    CurrentOrientation = PlayerOrientation.South;
                    CurrentState = PlayerState.Walking;
                }
            }
            else if (keyboard.Left == ButtonState.Pressed)
            {
                if (keyboard.Up == ButtonState.Pressed)
                {
                    CurrentOrientation = PlayerOrientation.NorthWest;
                    CurrentState = PlayerState.Walking;
                }
                else if (keyboard.Down == ButtonState.Pressed)
                {
                    CurrentOrientation = PlayerOrientation.SouthWest;
                    CurrentState = PlayerState.Walking;
                }
                else
                {
                    CurrentOrientation = PlayerOrientation.West;
                    CurrentState = PlayerState.Walking;
                }
            }
        }
    }
}
