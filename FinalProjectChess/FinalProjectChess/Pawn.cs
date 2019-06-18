using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectChess
{
    class Pawn : ChessPiece
    {
        private bool promoted = false;

        public Pawn(string rank, team team, int position, string pictureName, bool isActive)
            : base(rank, team, position, pictureName, isActive)
        {

        }

        public bool Promoted
        {
            get { return promoted; }
            set { promoted = value; }
        }

        public override void nextMove(int nextPosition, bool isVacant)
        {
            if (IsActive)
            {
                if (promoted) //promoted pawn moves
                {
                    if (isNextPromotedPawnMoveValid(nextPosition))
                    {
                        Position = nextPosition;
                    }
                }
                else //normal pawn moves
                {
                    if (isNextNormalPawnMoveValid(nextPosition))
                    {
                        if (isNextMoveAValidPromotion(nextPosition))
                        {
                            Promoted = true;
                            switch (TeamNumber)
                            {
                                case team.Team1:
                                    PictureName = "flippawn1";
                                    break;
                                case team.Team2:
                                    PictureName = "flippawn2";
                                    break;
                                default:
                                    break;
                            }
                        }
                        Position = nextPosition;
                    }
                }
            }
            else
            {
                revivePiece(nextPosition, isVacant);
            }
        }

        public override bool isNextMoveConsistentWithRank(int nextPosition)
        {
            if (promoted) return isNextPromotedPawnMoveValid(nextPosition);
            else return isNextNormalPawnMoveValid(nextPosition);
        }

        public bool isNextMoveAValidPromotion(int nextPosition)
        {
            if (!Promoted && isNextNormalPawnMoveValid(nextPosition) && isAtEnemyHomeRow(nextPosition))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isNextPromotedPawnMoveValid(int nextPosition)
        {
            return isUpMove(nextPosition) || isDownMove(nextPosition) ||
                   isLeftMove(nextPosition) || isRightMove(nextPosition) || 
                   isDownLeftMove(nextPosition) || isDownRightMove(nextPosition);
        }
        private bool isNextNormalPawnMoveValid(int nextPosition)
        {
            return isUpMove(nextPosition);
        }
        public override void setPositionAfterCaptureBasedOnTeam()
        {
            switch (TeamNumber)
            {
                case team.Team1:
                    positionAfterCapture = 20;
                    break;
                case team.Team2:
                    positionAfterCapture = 14;
                    break;
                default:
                    break;
            }
        }
        public override void goToPrison(bool isVacant)
        {
            base.goToPrison(isVacant);
            switch (TeamNumber)
            {
                case team.Team1:
                    PictureName = "pawn1";
                    break;
                case team.Team2:
                    PictureName = "pawn2";
                    break;
                default:
                    break;
            }
        }
    }
}
