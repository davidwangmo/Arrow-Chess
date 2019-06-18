using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProjectChess
{
    public enum team
    {
        Team1 = 1,
        Team2 = 2
    }
    public abstract class ChessPiece
    {
        private string rank;

        team teamNumber;
        private int position;
        protected bool isActive;
        private string pictureName;
        private Color color;
        protected int positionAfterCapture;

        public int Position
        {
            get { return position; }
            set { position = value; }
        }
        public string Rank
        {
            get { return rank; }
            set { rank = value; }
        }
        public team TeamNumber
        {
            get { return teamNumber; }
            set 
            {
                teamNumber = value;
                setColorBasedOnTeam();
                setPositionAfterCaptureBasedOnTeam();
            }
        }
        public Color Color
        {
            get { return color; }
        }
        public int PositionAfterCapture
        {
            get { return positionAfterCapture; }
        }
        public string PictureName
        {
            get { return pictureName; }
            set { pictureName = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public ChessPiece(string rank, team team, int position, string pictureName, bool isActive)
        {
            this.rank = rank;
            this.teamNumber = team;
            this.position = position;
            this.isActive = true;
            this.pictureName = pictureName;
            this.isActive = isActive;
            setColorBasedOnTeam();
            setPositionAfterCaptureBasedOnTeam();
        }

        private void setColorBasedOnTeam()
        {
            switch(teamNumber)
            {
                case team.Team1:
                    color = Color.LightBlue;
                    break;
                case team.Team2:
                    color = Color.LightGreen;
                    break;
                default:
                    break;
            }
        }
        public abstract void setPositionAfterCaptureBasedOnTeam();
        
        public abstract void nextMove(int nextPosition, bool isVacant);

        private int columnNumberOf(int position)
        {
            return position % 3;
        }
        private int rowNumberOf(int position)
        {
            return position / 3;
        }
        protected bool isUpMove(int nextPosition)
        {
            bool isUpMove = false;
            if (nextPosition < 12)
            {
                int columnOfCurrentPosition = columnNumberOf(Position);
                int rowOfCurrentPosition = rowNumberOf(Position);
                int columnOfNextPosition = columnNumberOf(nextPosition);
                int rowOfNextPosition = rowNumberOf(nextPosition);
                if (columnOfCurrentPosition - columnOfNextPosition != 0) return false;
                switch (TeamNumber)
                {
                    case team.Team1:
                        isUpMove = (rowOfCurrentPosition - rowOfNextPosition == 1);
                        break;
                    case team.Team2:
                        isUpMove = (rowOfCurrentPosition - rowOfNextPosition == -1);
                        break;
                    default:
                        break;
                }
            }
            return isUpMove;
        }
        protected bool isDownMove(int nextPosition)
        {
            bool isDownMove = false;
            if (nextPosition < 12)
            {
                int columnOfCurrentPosition = columnNumberOf(Position);
                int rowOfCurrentPosition = rowNumberOf(Position);
                int columnOfNextPosition = columnNumberOf(nextPosition);
                int rowOfNextPosition = rowNumberOf(nextPosition);
                if (columnOfCurrentPosition - columnOfNextPosition != 0) return false;
                switch (TeamNumber)
                {
                    case team.Team1:
                        isDownMove = (rowOfCurrentPosition - rowOfNextPosition == -1);
                        break;
                    case team.Team2:
                        isDownMove = (rowOfCurrentPosition - rowOfNextPosition == 1);
                        break;
                    default:
                        break;
                }
            }
            return isDownMove;
        }
        protected bool isLeftMove(int nextPosition)
        {
            bool isLeftMove = false;
            if (nextPosition < 12)
            {
                int columnOfCurrentPosition = columnNumberOf(Position);
                int rowOfCurrentPosition = rowNumberOf(Position);
                int columnOfNextPosition = columnNumberOf(nextPosition);
                int rowOfNextPosition = rowNumberOf(nextPosition);
                if (rowOfCurrentPosition - rowOfNextPosition != 0) return false;
                switch (TeamNumber)
                {
                    case team.Team1:
                        isLeftMove = (columnOfCurrentPosition - columnOfNextPosition == 1);
                        break;
                    case team.Team2:
                        isLeftMove = (columnOfCurrentPosition - columnOfNextPosition == -1);
                        break;
                    default:
                        break;
                }
            }
            return isLeftMove;
        }
        protected bool isRightMove(int nextPosition)
        {
            bool isRightMove = false;
            if (nextPosition < 12)
            {
                int columnOfCurrentPosition = columnNumberOf(Position);
                int rowOfCurrentPosition = rowNumberOf(Position);
                int columnOfNextPosition = columnNumberOf(nextPosition);
                int rowOfNextPosition = rowNumberOf(nextPosition);
                if (rowOfCurrentPosition - rowOfNextPosition != 0) return false;
                switch (TeamNumber)
                {
                    case team.Team1:
                        isRightMove = (columnOfCurrentPosition - columnOfNextPosition == -1);
                        break;
                    case team.Team2:
                        isRightMove = (columnOfCurrentPosition - columnOfNextPosition == 1);
                        break;
                    default:
                        break;
                }
            }
            return isRightMove;
        }
        protected bool isUpLeftMove(int nextPosition)
        {
            bool isUpLeftMove = false;
            if (nextPosition < 12)
            {
                int columnOfCurrentPosition = columnNumberOf(Position);
                int rowOfCurrentPosition = rowNumberOf(Position);
                int columnOfNextPosition = columnNumberOf(nextPosition);
                int rowOfNextPosition = rowNumberOf(nextPosition);
                switch (TeamNumber)
                {
                    case team.Team1:
                        isUpLeftMove = (rowOfCurrentPosition - rowOfNextPosition == 1) &&
                                   (columnOfCurrentPosition - columnOfNextPosition == 1);
                        break;
                    case team.Team2:
                        isUpLeftMove = (rowOfCurrentPosition - rowOfNextPosition == -1) &&
                                       (columnOfCurrentPosition - columnOfNextPosition == -1);
                        break;
                    default:
                        break;
                }
            }
            return isUpLeftMove;
        }
        protected bool isUpRightMove(int nextPosition)
        {
            bool isUpRightMove = false;
            if (nextPosition < 12)
            {
                int columnOfCurrentPosition = columnNumberOf(Position);
                int rowOfCurrentPosition = rowNumberOf(Position);
                int columnOfNextPosition = columnNumberOf(nextPosition);
                int rowOfNextPosition = rowNumberOf(nextPosition);
                switch (TeamNumber)
                {
                    case team.Team1:
                        isUpRightMove = (rowOfCurrentPosition - rowOfNextPosition == 1) &&
                                   (columnOfCurrentPosition - columnOfNextPosition == -1);
                        break;
                    case team.Team2:
                        isUpRightMove = (rowOfCurrentPosition - rowOfNextPosition == -1) &&
                                       (columnOfCurrentPosition - columnOfNextPosition == 1);
                        break;
                    default:
                        break;
                }
            }
            return isUpRightMove;
        }
        protected bool isDownLeftMove(int nextPosition)
        {
            bool isDownLeftMove = false;
            if (nextPosition < 12)
            {
                int columnOfCurrentPosition = columnNumberOf(Position);
                int rowOfCurrentPosition = rowNumberOf(Position);
                int columnOfNextPosition = columnNumberOf(nextPosition);
                int rowOfNextPosition = rowNumberOf(nextPosition);
                switch (TeamNumber)
                {
                    case team.Team1:
                        isDownLeftMove = (rowOfCurrentPosition - rowOfNextPosition == -1) &&
                                   (columnOfCurrentPosition - columnOfNextPosition == 1);
                        break;
                    case team.Team2:
                        isDownLeftMove = (rowOfCurrentPosition - rowOfNextPosition == 1) &&
                                       (columnOfCurrentPosition - columnOfNextPosition == -1);
                        break;
                    default:
                        break;
                }
            }
            return isDownLeftMove;
        }
        protected bool isDownRightMove(int nextPosition)
        {
            bool isDownRightMove = false;
            if (nextPosition < 12)
            {
                int columnOfCurrentPosition = columnNumberOf(Position);
                int rowOfCurrentPosition = rowNumberOf(Position);
                int columnOfNextPosition = columnNumberOf(nextPosition);
                int rowOfNextPosition = rowNumberOf(nextPosition);
                switch (TeamNumber)
                {
                    case team.Team1:
                        isDownRightMove = (rowOfCurrentPosition - rowOfNextPosition == -1) &&
                                   (columnOfCurrentPosition - columnOfNextPosition == -1);
                        break;
                    case team.Team2:
                        isDownRightMove = (rowOfCurrentPosition - rowOfNextPosition == 1) &&
                                       (columnOfCurrentPosition - columnOfNextPosition == 1);
                        break;
                    default:
                        break;
                }
            }
            return isDownRightMove;
        }
        public void flipTeam()
        {
            switch (TeamNumber)
            {
                case team.Team1:
                    TeamNumber = team.Team2;
                    break;
                case team.Team2:
                    TeamNumber = team.Team1;
                    break;
                default:
                    break;
            }
        }
        public bool isMyTurn(int turn)
        {
            team teamNumberTurn = (team)(turn % 2 + 1);
            return TeamNumber == teamNumberTurn;
        }

        public bool isNextMoveValid(ChessPiece pieceFound, int nextPosition, bool isVacant)
        {
            if (IsActive)
            {
                return !isAttackingFriendly(pieceFound) && isNextMoveConsistentWithRank(nextPosition);
            }
            else
            {
                return isReturnToBoardValid(isVacant, nextPosition);
            }
        }

        public bool isAttackingFriendly(ChessPiece pieceFound)
        {
            if (pieceFound != null && TeamNumber == pieceFound.TeamNumber) return true;
            else return false;
        }
        public bool isReturnToBoardValid(bool isVacant, int nextPosition)
        {
            if (isVacant)
            {
                return (TeamNumber == team.Team1 && nextPosition < 12 && nextPosition > 2 || TeamNumber == team.Team2 && nextPosition < 9);
            }
            else return false;
        }
        public abstract bool isNextMoveConsistentWithRank(int nextPosition);
        protected void revivePiece(int nextPosition, bool isVacant)
        {
            if (isVacant)
            {
                if (TeamNumber == team.Team1 && nextPosition < 12 && nextPosition > 2)
                {
                    Position = nextPosition;
                    IsActive = true;
                }
                else if (TeamNumber == team.Team2 && nextPosition < 9)
                {
                    Position = nextPosition;
                    IsActive = true;
                }
            }
        }
        public virtual bool isGameOver(ChessPiece enemyKing, int turn)
        {
            return isEnemyKingCaptured(enemyKing) || isKingAtFurthestRowFor2ConsecutiveTurns(turn);
        }
        protected virtual bool isEnemyKingCaptured(ChessPiece enemyKing)
        {
            return enemyKing != null && enemyKing.Rank == "king" && !enemyKing.IsActive;
        }
        private bool isKingAtFurthestRowFor2ConsecutiveTurns(int turn)
        {
            return Rank == "king" && isAtEnemyHomeRow(Position) && isMyTurn(turn);
        }
        public virtual void goToPrison(bool isVacant)
        {
            IsActive = false;

            if (!isVacant)
            {
                Position = PositionAfterCapture + 3;
            }
            else
            {
                Position = PositionAfterCapture;
            }
            flipTeam();
        }
        protected bool isAtEnemyHomeRow(int nextPosition)
        {
            return ((TeamNumber == team.Team1 && nextPosition < 3) || (TeamNumber == team.Team2 && nextPosition < 12 && nextPosition > 8));
        }

    }
}
