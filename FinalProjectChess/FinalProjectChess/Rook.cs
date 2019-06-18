using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectChess
{
    class Rook : ChessPiece
    {
        public Rook(string rank, team team, int position, string pictureName, bool isActive)
            : base(rank, team, position, pictureName, isActive)
        {

        }
        public override void nextMove(int nextPosition, bool isVacant)
        {
            if (IsActive)
            {
                Position = nextPosition;
            }
            else
            {
                revivePiece(nextPosition, isVacant);
            }
        }

        public override bool isNextMoveConsistentWithRank(int nextPosition)
        {
            return isNextRookMoveValid(nextPosition);
        }

        private bool isNextRookMoveValid(int nextPosition)
        {
            return isUpMove(nextPosition) || isDownMove(nextPosition) ||
                   isLeftMove(nextPosition) || isRightMove(nextPosition);
        }
        public override void setPositionAfterCaptureBasedOnTeam()
        {
            switch (TeamNumber)
            {
                case team.Team1:
                    positionAfterCapture = 18;
                    break;
                case team.Team2:
                    positionAfterCapture = 12;
                    break;
                default:
                    break;
            }
        }
    }
}
