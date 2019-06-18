using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectChess
{
    class King : ChessPiece
    {
        public King(string rank, team team, int position, string pictureName, bool isActive)
            : base(rank, team, position, pictureName, isActive)
        {

        }

        public override void nextMove(int nextPosition, bool isVacant)
        {
            if (isActive)
            {
                if (isNextKingMoveValid(nextPosition))
                {
                    Position = nextPosition;
                }
            }
            else
            {
                
            }
        }

        public override bool isNextMoveConsistentWithRank(int nextPosition)
        {
            return isNextKingMoveValid(nextPosition);
        }

        private bool isNextKingMoveValid(int nextPosition)
        {
            return isUpLeftMove(nextPosition) || isUpRightMove(nextPosition) ||
                   isDownLeftMove(nextPosition) || isDownRightMove(nextPosition) ||
                   isUpMove(nextPosition) || isDownMove(nextPosition) ||
                   isLeftMove(nextPosition) || isRightMove(nextPosition);
        }
        public override void setPositionAfterCaptureBasedOnTeam()
        {
        }
        public override void goToPrison(bool isVacant)
        {
            IsActive = false;
        }
    }
}
