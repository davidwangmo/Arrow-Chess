using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProjectChess
{
    class ChessPiece
    {
        private string name;
        private bool isOpponent;

        public ChessPiece(string pieceName, bool isOpponent)
        {
            this.name = pieceName;
            this.isOpponent = isOpponent;
        }

        /*public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsOpponent
        {
            get { return isOpponent; }
            set { isOpponent = value; }
        }


           
        /*
        int position;
        bool inGame;
        bool up, down, left, right, upleft, upright, downleft, downright;
        bool colorteam;
        Game1 game1 = new Game1();

        public int Position
        {
            get { return position; }
            set { position = value; }
        }
        public bool InGame
        {
            get { return inGame; }
            set { inGame = value; }
        }
        public bool Up
        {
            get { return up; }
            set { up = value; }
        }
        public bool Down
        {
            get { return down; }
            set { down = value; }
        }
        public bool Left
        {
            get { return left; }
            set { left = value; }
        }
        public bool Right
        {
            get { return right; }
            set { right = value; }
        }
        public bool Upleft
        {
            get { return upleft; }
            set { upleft = value; }
        }
        public bool Upright
        {
            get { return upright; }
            set { upright = value; }
        }
        public bool Downleft
        {
            get { return downleft; }
            set { downleft = value; }
        }
        public bool Downright
        {
            get { return downright; }
            set { downright = value; }
        }
        public bool Colorteam
        {
            get { return colorteam; }
            set { colorteam = value; }
        }

        public int selectedpiece()
        {
            for(int i =0; i<12;i++)
            {
                if (i == game1.pawn2)
                {
                    return 4;
                }
            }
        }
        public int team()
        {
            for (int i = 0; i < 12; i++)
            {

            }
        }*/
    }
}
