using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProjectChess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /* Description of the game:
         * 
         * Objective: Capture the opponent's king
         *            or have your king on the furthest side for two consecutive turns.
         * Pieces:    Rook, Bishop, King, Pawn, Promoted Pawn
         *
         * Rules:     Rooks move horizontally and vertically.
         *            Bishops move diagonally.
         *            Kings move in all directions.
         *            Pawns move forward.
         *            Promoted pawns move horizontally, vertically, and diagonally backwards.
         *            Each piece can only move 1 square per turn.
         *            When a piece is captured, it becomes your piece.
         *            You may place it back onto the main board in your closest 3 by 3 square as a turn.
         *            When a pawn reaches the furthest side, it is promoted.     
         * */
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D transparent, bishop, king, rook, pawnone, pawntwo, flippedpawnone, flippedpawntwo;

        Board gameboard;
        ChessPiece[] allPieces = new ChessPiece[8];
        ChessPiece selectedPiece;
        ChessPiece opponentPiece;
        int turn;
        bool selectionClick;
        int clickposition, newclickposition;
        int bw = 2; //borderwidth
        //int king1 = 10, king2 = 1;
        //int k1turns, k2turns; // number of turns king is on opposite side to win
        //first number is normal piece, second number is captured piece
        public int[] rook1 = new int[] { 11, 12 }, bishop1 = new int[] { 9, 13 }, pawn1 = new int[] { 7, 14 };
        public int[] rook2 = new int[] { 0, 18 }, bishop2 = new int[] { 2, 19 }, pawn2 = new int[] { 4, 20 };
        //bool rook1moves, rook2moves, king1moves, king2moves, bishop1moves, bishop2moves, pawn1moves, pawn2moves;
        //bool crook1moves, crook2moves, cbishop1moves, cbishop2moves, cpawn1moves, cpawn2moves; //captured piece
        //bool flippedpawn1moves, flippedpawn2moves, flippedcpawn1moves, flippedcpawn2moves, flippedpawn1, flippedpawn2, flippedcpawn1, flippedcpawn2;
        //bool gameover, capturedpawn1, capturedpawn2, capturedrook1, capturedrook2, capturedbishop1, capturedbishop2;
        //bool bringtoboard;
        //string selectedpiece;
        SpriteFont winningMsg;
        SpriteFont teamdisplay;
        MouseState mouse, oldMouse;
        Point cursorPoint;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 450;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            ChessPiece Pawn1 = new Pawn("pawn", team.Team1, 7, "pawn1", true);
            ChessPiece Pawn2 = new Pawn("pawn", team.Team2, 4, "pawn2", true);
            ChessPiece Rook1 = new Rook("rook", team.Team1, 11, "rook", true);
            ChessPiece Rook2 = new Rook("rook", team.Team2, 0, "rook", true);
            ChessPiece Bishop1 = new Bishop("bishop", team.Team1, 9, "bishop", true);
            ChessPiece Bishop2 = new Bishop("bishop", team.Team2, 2, "bishop", true);
            ChessPiece King1 = new King("king", team.Team1, 10, "king", true);
            ChessPiece King2 = new King("king",team.Team2, 1, "king", true);
            allPieces = new ChessPiece[] {Pawn1, Pawn2, Rook1, Rook2, Bishop1, Bishop2, King1, King2 };
            gameboard = new Board();
            turn = 0;
            selectionClick = false;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            winningMsg = Content.Load<SpriteFont>("WinningFont");
            teamdisplay = Content.Load<SpriteFont>("TeamFont");
            transparent = Content.Load<Texture2D>("transparent");
            king = Content.Load<Texture2D>("king");
            rook = Content.Load<Texture2D>("rook");
            bishop = Content.Load<Texture2D>("bishop");
            pawnone = Content.Load<Texture2D>("pawn1");
            pawntwo = Content.Load<Texture2D>("pawn2");
            flippedpawnone = Content.Load<Texture2D>("flippawn1");
            flippedpawntwo = Content.Load<Texture2D>("flippawn2");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            mouse = Mouse.GetState();
            cursorPoint = new Point(mouse.X, mouse.Y);
            //first click to select a piece
            if (selectionClick == false)
            {
                for (int i = 0; i < 24; i++)
                {
                    if (gameboard.drawBoard()[i].Contains(cursorPoint))
                    {
                        if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                        {
                            //captured piece is allowed back to main board
                            selectionClick = true;
                            clickposition = i;
                            //if (i > 12) bringtoboard = true;
                            if (findPieceAtPositionOf(i) == null || findPieceAtPositionOf(i).isMyTurn(turn) == false)
                            {
                                selectedPiece = new Pawn("none", 0, -1, "none", true);
                                selectionClick = false;
                            }
                            else
                                selectedPiece = findPieceAtPositionOf(i);

                            /*
                            if (team == 1)
                            {
                                if (i == rook1[0] && capturedrook1 == false)
                                {
                                    rook1moves = true;
                                    selectedpiece = "Rook";
                                }
                                else if (i == king1)
                                {
                                    king1moves = true;
                                    selectedpiece = "King";
                                }
                                else if (i == bishop1[0] && capturedbishop1 == false)
                                {
                                    bishop1moves = true;
                                    selectedpiece = "Bishop";
                                }
                                else if (i == pawn1[0] && capturedpawn1 == false && flippedpawn1 == true)
                                {
                                    flippedpawn1moves = true;
                                    selectedpiece = "Promoted Pawn";
                                }
                                else if (i == pawn1[0] && capturedpawn1 == false)
                                {
                                    pawn1moves = true;
                                    selectedpiece = "Pawn";
                                }
                                else if (i == pawn1[1] && capturedpawn2 == true && flippedcpawn2 == true)
                                {
                                    flippedcpawn2moves = true;
                                    selectedpiece = "Promoted Pawn";
                                }
                                else if (i == pawn1[1] && capturedpawn2 == true)
                                {
                                    cpawn2moves = true;
                                    selectedpiece = "Pawn";
                                }
                                else if (i == bishop1[1] && capturedbishop2 == true)
                                {
                                    cbishop2moves = true;
                                    selectedpiece = "Bishop";
                                }
                                else if (i == rook1[1] && capturedrook2 == true)
                                {
                                    crook2moves = true;
                                    selectedpiece = "Rook";
                                }
                                else
                                {
                                    bringtoboard = false;
                                    selectionClick = false;
                                }
                            }
                            else if (team == 2)
                            {
                                if (i == rook2[0] && capturedrook2 == false)
                                {
                                    rook2moves = true;
                                    selectedpiece = "Rook";
                                }

                                else if (i == king2)
                                {
                                    king2moves = true;
                                    selectedpiece = "King";
                                }

                                else if (i == bishop2[0] && capturedbishop2 == false)
                                {
                                    bishop2moves = true;
                                    selectedpiece = "Bishop";
                                }
                                else if (i == pawn2[0] && capturedpawn2 == false && flippedpawn2 == true)
                                {
                                    flippedpawn2moves = true;
                                    selectedpiece = "Promoted Pawn";
                                }
                                else if (i == pawn2[0] && capturedpawn2 == false)
                                {
                                    pawn2moves = true;
                                    selectedpiece = "Pawn";
                                }
                                else if (i == pawn2[1] && capturedpawn1 == true && flippedcpawn1 == true)
                                {
                                    flippedcpawn1moves = true;
                                    selectedpiece = "Promoted Pawn";
                                }
                                else if (i == pawn2[1] && capturedpawn1 == true)
                                {
                                    cpawn1moves = true;
                                    selectedpiece = "Pawn";
                                }
                                else if (i == bishop2[1] && capturedbishop1 == true)
                                {
                                    cbishop1moves = true;
                                    selectedpiece = "Bishop";
                                }
                                else if (i == rook2[1] && capturedrook1 == true)
                                {
                                    crook1moves = true;
                                    selectedpiece = "Rook";
                                }
                                else
                                {
                                    bringtoboard = false;
                                    selectionClick = false;
                                }
                            }*/
                        }
                        //what piece is selected and which team
                    }
                }
            }
            //second click to choose the new position of the selected piece.
            else
            {
                for (int i = 0; i < 24; i++)
                {
                    bool isMouseHoveredWithinGameArea = gameboard.drawBoard()[i].Contains(cursorPoint);
                    bool isMousedClicked = mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released;
                    if (isMouseHoveredWithinGameArea && isMousedClicked && selectedPiece != null)
                    {
                        newclickposition = i;
                        selectionClick = false;

                        if (selectedPiece.isNextMoveValid(findPieceAtPositionOf(newclickposition), newclickposition, isVacant(newclickposition)))
                        {
                            opponentPiece = getOpponentIfResidesAt(selectedPiece, newclickposition);
                            if (isOpponent(selectedPiece, newclickposition))
                            {
                                opponentPiece.goToPrison(isVacant(opponentPiece.PositionAfterCapture));
                            }
                            selectedPiece.nextMove(newclickposition, isVacant(newclickposition));
                            turn++;
                        }
                        else if (isVacant(newclickposition) || isOpponent(selectedPiece, newclickposition))
                        {
                            selectionClick = true;
                        }
                        else if (selectedPiece.isAttackingFriendly(findPieceAtPositionOf(newclickposition)))
                        {
                            selectedPiece = findPieceAtPositionOf(newclickposition);
                            selectionClick = true;

                        }
                        /*if(flippedpawn1moves == true)
                            if(!isTeam1(i) && isFlippedPawn1Move(clickposition, newclickposition))
                            {
                                pawn1[0] = i;
                                flippedpawn1moves = false;
                                team = 2;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        else if(flippedpawn2moves == true)
                            if(!isTeam2(i) && isFlippedPawn2Move(clickposition, newclickposition))
                            {
                                pawn2[0] = i;
                                flippedpawn2moves = false;
                                team = 1;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        else if (rook1moves == true)
                        {
                            if (bringtoboard == true && i > 2 && i < 12 && isVacant(i))
                            {
                                rook1[0] = i;
                                rook1moves = false;
                                team = 2;
                                bringtoboard = false;
                            }
                            else if (!isTeam1(i) && isOneStepStraight(clickposition, newclickposition))
                            {
                                rook1[0] = i;
                                rook1moves = false;
                                team = 2;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (rook2moves == true)
                        {
                            if (bringtoboard == true && i < 9 && isVacant(i))
                            {
                                rook2[0] = i;
                                rook2moves = false;
                                team = 1;
                                bringtoboard = false;
                            }
                            else if (!isTeam2(i) && isOneStepStraight(clickposition, newclickposition))
                            {
                                rook2[0] = i;
                                rook2moves = false;
                                team = 1;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (king1moves == true && !isTeam1(i) && isOneStepAnyDirection(clickposition, newclickposition))
                            {
                                king1 = i;
                                king1moves = false;
                                team = 2;
                            }
                        else if (king2moves == true && !isTeam2(i) && isOneStepAnyDirection(clickposition, newclickposition))
                            {
                                king2 = i;
                                king2moves = false;
                                team = 1;
                            }
                        else if (bishop1moves == true)
                        {
                            if (bringtoboard == true && i > 2 && i < 12 && isVacant(i))
                            {
                                bishop1[0] = i;
                                bishop1moves = false;
                                team = 2;
                                bringtoboard = false;
                            }
                            else if (!isTeam1(i) && isOneStepDiagonal(clickposition, newclickposition))
                            {
                                bishop1[0] = i;
                                bishop1moves = false;
                                team = 2;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (bishop2moves == true)
                        {
                            if (bringtoboard == true && i < 9 && isVacant(i))
                            {
                                bishop2[0] = i;
                                bishop2moves = false;
                                team = 1;
                                bringtoboard = false;
                            }
                            else if (!isTeam2(i) && isOneStepDiagonal(clickposition, newclickposition))
                            {
                                bishop2[0] = i;
                                bishop2moves = false;
                                team = 1;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (pawn1moves == true)
                        {
                            if (bringtoboard == true && i > 2 && i < 12 && isVacant(i))
                            {
                                pawn1[0] = i;
                                pawn1moves = false;
                                team = 2;
                                bringtoboard = false;
                            }
                            else if (!isTeam1(i) && (i == clickposition - 3))
                            {
                                pawn1[0] = i;
                                pawn1moves = false;
                                team = 2;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (pawn2moves == true)
                        {
                            if (bringtoboard == true && i < 9 && isVacant(i))
                            {
                                pawn2[0] = i;
                                pawn2moves = false;
                                team = 1;
                                bringtoboard = false;
                            }
                            else if (!isTeam2(i) && (i == clickposition + 3))
                            {
                                pawn2[0] = i;
                                pawn2moves = false;
                                team = 1;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        //These are for moving captured pieces
                        else if (flippedcpawn2moves == true)
                            if (!isTeam1(i) && isFlippedPawn1Move(clickposition, newclickposition))
                            {
                                pawn1[1] = i;
                                flippedcpawn2moves = false;
                                team = 2;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        else if (flippedcpawn1moves == true)
                            if (!isTeam2(i) && isFlippedPawn2Move(clickposition, newclickposition))
                            {
                                pawn2[1] = i;
                                flippedcpawn1moves = false;
                                team = 1;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        else if (cpawn2moves == true)
                        {
                            if (bringtoboard == true && i > 2 && i < 12 && isVacant(i))
                            {
                                pawn1[1] = i;
                                cpawn2moves = false;
                                team = 2;
                                bringtoboard = false;
                            }
                            else if (!isTeam1(i) && (i == clickposition - 3))
                            {
                                pawn1[1] = i;
                                cpawn2moves = false;
                                team = 2;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (cpawn1moves == true)
                        {
                            if (bringtoboard == true && i < 9 && isVacant(i))
                            {
                                pawn2[1] = i;
                                cpawn1moves = false;
                                team = 1;
                                bringtoboard = false;
                            }
                            else if (!isTeam2(i) && (i == clickposition + 3))
                            {
                                pawn2[1] = i;
                                cpawn1moves = false;
                                selectionClick = false;
                                newclickposition = i;
                                team = 1;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (crook2moves == true)
                        {
                            if (bringtoboard == true && i > 2 && i < 12 && isVacant(i))
                            {
                                rook1[1] = i;
                                crook2moves = false;
                                team = 2;
                                bringtoboard = false;
                            }
                            else if (!isTeam1(i) && isOneStepStraight(clickposition, newclickposition))
                            {
                                rook1[1] = i;
                                crook2moves = false;
                                team = 2;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (crook1moves == true)
                        {
                            if (bringtoboard == true && i < 9 && isVacant(i))
                            {
                                rook2[1] = i;
                                crook1moves = false;
                                team = 1;
                                bringtoboard = false;
                            }
                            else if (!isTeam2(i) && isOneStepStraight(clickposition, newclickposition))
                            {
                                rook2[1] = i;
                                crook1moves = false;
                                team = 1;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (cbishop2moves == true)
                        {
                            if (bringtoboard == true && i > 2 && i < 12 && isVacant(i))
                            {
                                bishop1[1] = i;
                                cbishop2moves = false;
                                team = 2;
                                bringtoboard = false;
                            }
                            else if (!isTeam1(i) && isOneStepDiagonal(clickposition, newclickposition))
                            {
                                bishop1[1] = i;
                                cbishop2moves = false;
                                team = 2;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else if (cbishop1moves == true)
                        {
                            if (bringtoboard == true && i < 9 && isVacant(i))
                            {
                                bishop2[1] = i;
                                bishop1moves = false;
                                team = 1;
                                bringtoboard = false;
                            }
                            else if (!isTeam2(i) && isOneStepDiagonal(clickposition, newclickposition))
                            {
                                bishop2[1] = i;
                                cbishop1moves = false;
                                team = 1;
                            }
                            else
                            {
                                resetForInvalidMove();
                            }
                        }
                        else
                        {
                            resetForInvalidMove();
                        }*/
                        //selectedpiece = "None";
                        ////king turns on furthest side
                        //if (king1 < 3) k1turns++;
                        //else k1turns = 0;
                        //if (king2 > 8) k2turns++;
                        //else k2turns = 0;
                        ////is the pawn promoted
                        //if (pawn1[0] < 3)
                        //    flippedpawn1 = true;
                        //if (pawn1[1] < 3)
                        //    flippedcpawn2 = true;
                        //if (pawn2[0] > 8 && pawn2[0] < 12)
                        //    flippedpawn2 = true;
                        //if (pawn2[1] > 8 && pawn2[1] < 12)
                        //    flippedcpawn1 = true;
                    }
                }
            }
            //check if a piece is captured
            //if (newclickposition == rook2[0] && team == 2)
            //{
            //    rook2[0] = 21;
            //    capturedrook2 = true;
            //}
            //else if (newclickposition == bishop2[0] && team == 2)
            //{
            //    bishop2[0] = 22;
            //    capturedbishop2 = true;
            //}
            //else if (newclickposition == pawn2[0] && team == 2)
            //{
            //    pawn2[0] = 23;
            //    capturedpawn2 = true;
            //    flippedpawn2 = false;
            //}
            //else if (newclickposition == king2 && team == 2)
            //{
            //    player1win = true;
            //    gameover = true;
            //}
            //else if (newclickposition == rook1[0] && team == 1)
            //{
            //    rook1[0] = 15;
            //    capturedrook1 = true;
            //}
            //else if (newclickposition == bishop1[0] && team == 1)
            //{
            //    bishop1[0] = 16;
            //    capturedbishop1 = true;
            //}
            //else if (newclickposition == pawn1[0] && team == 1)
            //{
            //    pawn1[0] = 17;
            //    capturedpawn1 = true;
            //    flippedpawn1 = false;
            //}
            //else if (newclickposition == king1 && team == 1)
            //{
            //    player2win = true;
            //    gameover = true;
            //}
            //if (newclickposition == rook2[1] && team == 2)
            //{
            //    rook2[1] = 18;
            //    capturedrook1 = false;
            //}
            //else if (newclickposition == bishop2[1] && team == 2)
            //{
            //    bishop2[1] = 19;
            //    capturedbishop1 = false;
            //}
            //else if (newclickposition == pawn2[1] && team == 2)
            //{
            //    pawn2[1] = 20;
            //    capturedpawn1 = false;
            //    flippedcpawn1 = false;
            //}
            //else if (newclickposition == rook1[1] && team == 1)
            //{
            //    rook1[1] = 12;
            //    capturedrook2 = false;
            //}
            //else if (newclickposition == bishop1[1] && team == 1)
            //{
            //    bishop1[1] = 13;
            //    capturedbishop2 = false;
            //}
            //else if (newclickposition == pawn1[1] && team == 1)
            //{
            //    pawn1[1] = 14;
            //    capturedpawn2 = false;
            //    flippedcpawn2 = false;
            //}

            //king wins on opponent side
        //    if (k1turns == 2)
        //    {
        //        player1win = true;
        //        gameover = true;
        //    }
        //    if (k2turns == 2)
        //    {
        //        player2win = true;
        //        gameover = true;
        //    }
            //reset the game
            if(selectedPiece  != null && selectedPiece.isGameOver(opponentPiece, turn) && mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released)
            {
                Initialize();
                selectedPiece = null;
                opponentPiece = null;
            }

            // TODO: Add your update logic here

            oldMouse = mouse;
            base.Update(gameTime);
        }
        ////invalid move resets
        //private void resetForInvalidMove()
        //{
        //    selectionClick = false;
        //    rook1moves = false;
        //    rook2moves = false;
        //    king1moves = false;
        //    king2moves = false;
        //    bishop1moves = false;
        //    bishop2moves = false;
        //    pawn1moves = false;
        //    pawn2moves = false;
        //    crook1moves = false;
        //    crook2moves = false;
        //    cbishop1moves = false;
        //    cbishop2moves = false;
        //    cpawn1moves = false;
        //    cpawn2moves = false;
        //    bringtoboard = false;
        //    flippedpawn1moves = false;
        //    flippedpawn2moves = false;
        //    flippedcpawn1moves = false;
        //    flippedcpawn2moves = false;
        //    newclickposition = -1;
        //    selectedpiece = "None";
        /*private bool isVacant (int newPosition)
        {
            return newPosition != king1 && newPosition != king2 && newPosition != bishop1[0] && 
                   newPosition != bishop1[1] && newPosition != pawn1[0] && newPosition != rook1[0] &&
                   newPosition != pawn2[0] && newPosition != rook2[0] && newPosition != rook1[1] &&
                   newPosition != pawn1[1] && newPosition != rook2[1] && newPosition != pawn2[1] &&
                   newPosition != bishop2[1] && newPosition != bishop2[0];
        }

        private bool isTeam1(int newPosition)
        {
            return newPosition == king1 || newPosition == bishop1[0] || newPosition == bishop1[1] ||
                   newPosition == pawn1[0] || newPosition == rook1[0] || newPosition == rook1[1] ||
                   newPosition == pawn1[1];
        }
        private bool isTeam2(int newPosition)
        {
            return newPosition == king2 || newPosition == bishop2[0] || newPosition == bishop2[1] ||
                   newPosition == pawn2[0] || newPosition == rook2[0] || newPosition == rook2[1] ||
                   newPosition == pawn2[1];
        }*/

        private ChessPiece findPieceAtPositionOf(int clickPosition)
        {
            ChessPiece pieceFound = null;
            for (int j = 0; j < 8; j++)
            {
                if (allPieces[j].Position == clickPosition) {
                    pieceFound = allPieces[j];
                    break;
                }
            }
            return pieceFound;
        }
        private bool isVacant(int clickPosition)
        {
            return findPieceAtPositionOf(clickPosition) == null;
        }

        private ChessPiece getOpponentIfResidesAt(ChessPiece myChessPiece, int nextPosition) //can return null if nextPosition is vacant
        {
            ChessPiece aPiece = findPieceAtPositionOf(nextPosition);
            if (findPieceAtPositionOf(nextPosition) != null)
            {
                if (aPiece.TeamNumber == myChessPiece.TeamNumber)
                {
                    aPiece = null;
                }
            }
            return aPiece;
        }
        private bool isOpponent(ChessPiece myChessPiece, int nextPosition)
        {
            return getOpponentIfResidesAt(myChessPiece, nextPosition) != null;
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.White);

            if (selectedPiece != null && selectedPiece.isGameOver(opponentPiece, turn))
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(winningMsg, "Player " + (int)selectedPiece.TeamNumber + " Wins", new Vector2(150, 150), selectedPiece.Color);
                spriteBatch.DrawString(teamdisplay, "Right Click to Play Again", new Vector2(150, 250), selectedPiece.Color);
            }
            else
            {
                //gameboard
                spriteBatch.Draw(transparent, new Rectangle(0, 0, bw, 400), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(100, 0, bw, 400), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(200, 0, bw, 400), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(300, 0, bw, 400), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(0, 0, 300, bw), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(0, 100, 300, bw), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(0, 200, 300, bw), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(0, 300, 300, bw), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(0, 400, 300, bw), Color.Black);
                //capture box
                spriteBatch.Draw(transparent, new Rectangle(500, 100, bw, 300), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(700, 100, bw, 300), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(500, 100, 200, bw), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(500, 400, 200, bw), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(700, 100, 200, bw), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(700, 400, 200, bw), Color.Black);
                spriteBatch.Draw(transparent, new Rectangle(900, 100, bw, 300), Color.Black);

                spriteBatch.DrawString(teamdisplay, "Player " + (turn % 2 + 1) + "'s turn", new Vector2(305, 150), Color.Black);
                spriteBatch.DrawString(teamdisplay, "Turn's Passed: " + turn, new Vector2(305, 170), Color.Black);
                spriteBatch.DrawString(teamdisplay, "Selected Piece: " + "\n" + ((selectedPiece == null) ? "none" : selectedPiece.Rank), new Vector2(305, 250), Color.Black);
                spriteBatch.DrawString(teamdisplay, "Player 1's Pieces", new Vector2(505, 50), Color.Black);
                spriteBatch.DrawString(teamdisplay, "Player 2's Pieces", new Vector2(705, 50), Color.Black);

                //Which piece is drawn

                foreach (ChessPiece piece in allPieces)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>(piece.PictureName), gameboard.drawBoard()[piece.Position], piece.Color);
                }


                /*
                if (capturedpawn1 == false && flippedpawn1 == true)
                {
                    spriteBatch.Draw(flippedpawnone, gameboard.drawBoard()[pawn1[0]], Color.LightBlue);
                }
                else if (capturedpawn1 == false)
                {
                    spriteBatch.Draw(pawnone, gameboard.drawBoard()[pawn1[0]], Color.LightBlue);
                }
                else if (capturedpawn1 == true && flippedcpawn1 == true)
                {
                    spriteBatch.Draw(flippedpawntwo, gameboard.drawBoard()[pawn2[1]], Color.LightGreen);
                }
                else
                {
                    spriteBatch.Draw(pawntwo, gameboard.drawBoard()[pawn2[1]], Color.LightGreen);
                }
                if (capturedpawn2 == false && flippedpawn2 == true)
                {
                    spriteBatch.Draw(flippedpawntwo, gameboard.drawBoard()[pawn2[0]], Color.LightGreen);
                }
                else if (capturedpawn2 == false)
                {
                    spriteBatch.Draw(pawntwo, gameboard.drawBoard()[pawn2[0]], Color.LightGreen);
                }
                else if (capturedpawn2 == true && flippedcpawn2 == true)
                {
                    spriteBatch.Draw(flippedpawnone, gameboard.drawBoard()[pawn1[1]], Color.LightBlue);
                }
                else
                {
                    spriteBatch.Draw(pawnone, gameboard.drawBoard()[pawn1[1]], Color.LightBlue);
                }
                if (capturedrook2 == false)
                {
                    spriteBatch.Draw(rook, gameboard.drawBoard()[rook2[0]], Color.LightGreen);
                }
                else
                {
                    spriteBatch.Draw(rook, gameboard.drawBoard()[rook1[1]], Color.LightBlue);
                }
                if (capturedrook1 == false)
                {
                    spriteBatch.Draw(rook, gameboard.drawBoard()[rook1[0]], Color.LightBlue);
                }
                else
                {
                    spriteBatch.Draw(rook, gameboard.drawBoard()[rook2[1]], Color.LightGreen);
                }
                if (capturedbishop2 == false)
                {
                    spriteBatch.Draw(bishop, gameboard.drawBoard()[bishop2[0]], Color.LightGreen);
                }
                else
                {
                    spriteBatch.Draw(bishop, gameboard.drawBoard()[bishop1[1]], Color.LightBlue);
                }
                if (capturedbishop1 == false)
                {
                    spriteBatch.Draw(bishop, gameboard.drawBoard()[bishop1[0]], Color.LightBlue);
                }
                else
                {
                    spriteBatch.Draw(bishop, gameboard.drawBoard()[bishop2[1]], Color.LightGreen);
                }*/
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
