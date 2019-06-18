using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectChess
{
    class Bishop : ChessPiece
    {
        public Bishop(string rank, team team, int position, string pictureName, bool isActive)
            : base(rank, team, position, pictureName, isActive)
        {

        }
        public override void nextMove(int nextPosition, bool isVacant)
        {
            if (IsActive)
            {
                if (isNextBishopMoveValid(nextPosition))
                {
                    Position = nextPosition;
                }
            }
            else
            {
                revivePiece(nextPosition, isVacant);
            }
        }

        public override bool isNextMoveConsistentWithRank(int nextPosition)
        {
            return isNextBishopMoveValid(nextPosition);
        }

        private  bool isNextBishopMoveValid(int nextPosition)
        {
            return isUpLeftMove(nextPosition) || isUpRightMove(nextPosition) ||
                   isDownLeftMove(nextPosition) || isDownRightMove(nextPosition);
        }

        public override void setPositionAfterCaptureBasedOnTeam()
        {
            switch (TeamNumber)
            {
                case team.Team1:
                    positionAfterCapture = 19;
                    break;
                case team.Team2:
                    positionAfterCapture = 13;
                    break;
                default:
                    break;
            }
        }
    }
}
