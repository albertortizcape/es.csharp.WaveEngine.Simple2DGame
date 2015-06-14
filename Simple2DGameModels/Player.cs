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

        private float _x;
        private float _y;
        private PlayerOrientation _CurrentOrientation, _LastOrientation;
        private int _XDirection;
        private int _YDirection;
        private PlayerState _CurrentState, _LastState;

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
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float YPlayerPosition
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
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
            
            TouchInput();
            KeyboardInput();

            // Set current animation if that one is diferent
            if (CurrentState != LastState || CurrentOrientation != LastOrientation)
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
            else if(CurrentState == PlayerState.Idle)
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

            LastOrientation = CurrentOrientation;
            LastState = CurrentState;

            // Move sprite
            trans2D.X += XDirection * MOVEMENT_SPEED * (gameTime.Milliseconds / 10);
            trans2D.Y += YDirection * MOVEMENT_SPEED * (gameTime.Milliseconds / 10);

            // Check borders
            if (trans2D.X < XBORDER_OFFSET)
            {
                trans2D.X = XBORDER_OFFSET;
            }
            else if (trans2D.X > WaveServices.Platform.ScreenWidth - XBORDER_OFFSET)
            {
                trans2D.X = WaveServices.Platform.ScreenWidth - XBORDER_OFFSET;
            }

            if (trans2D.Y < YBORDER_OFFSET)
            {
                trans2D.Y = YBORDER_OFFSET;
            }
            else if (trans2D.Y > WaveServices.Platform.ScreenHeight - YBORDER_OFFSET)
            {
                trans2D.Y = WaveServices.Platform.ScreenHeight - YBORDER_OFFSET;
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

        private void TouchInput()
        {
            var touches = WaveServices.Input.TouchPanelState;
            if (touches.Count > 0)
            {
                //CurrentState = PlayerState.Walking;

                var firstTouch = touches[0];
                float XTouch_Screen = firstTouch.Position.X; // --
                float YTouch_Screen = firstTouch.Position.Y; // |

                float XRealTouch = XPlayerPosition - XTouch_Screen;
                float YRealTouch = YPlayerPosition - YTouch_Screen;

                //if (XTouch_Screen < XPlayerPosition)
                //{
                //    XRealTouch = XRealTouch * -1;
                //}

                //if (YTouch_Screen < YPlayerPosition)
                //{
                //    YRealTouch = YRealTouch * -1;
                //}

                if (YRealTouch > 0)
                {
                    // Norht, East, West
                    if (XRealTouch > 0)
                    {
                        // North, East
                        if (YRealTouch < XRealTouch)
                        {
                            // East
                            CurrentOrientation = PlayerOrientation.East;
                        }
                        else if (YRealTouch > XRealTouch)
                        {
                            // North
                            CurrentOrientation = PlayerOrientation.North;
                        }
                    }
                    else
                    {
                        // North, West
                        if (YRealTouch > XRealTouch)
                        {
                            // North
                            CurrentOrientation = PlayerOrientation.North;
                        }
                        else if (YRealTouch < XRealTouch)
                        {
                            // West
                            CurrentOrientation = PlayerOrientation.West;
                        }
                    }
                }
                else
                {
                    // South, East, West
                    if (XRealTouch > XPlayerPosition)
                    {
                        // South, East
                        if (YPlayerPosition < YRealTouch)
                        {
                            // East
                            CurrentOrientation = PlayerOrientation.East;
                        }
                        else if (YPlayerPosition > YRealTouch)
                        {
                            // South
                            CurrentOrientation = PlayerOrientation.South;
                        }
                    }
                    else
                    {
                        // South, West
                        if (YPlayerPosition > YRealTouch) 
                        {
                            // West
                            CurrentOrientation = PlayerOrientation.West;
                        }
                        else if (YPlayerPosition < YRealTouch)
                        {
                            // South
                            CurrentOrientation = PlayerOrientation.South;
                        }
                    }
                }
            }
        }
    }
}
