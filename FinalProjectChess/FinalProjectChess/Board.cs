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
    class Board
    {
    int x, y;
    public Rectangle[] boardsquares = new Rectangle[24];
    public int squaredimen = 98;

    public Rectangle[] drawBoard()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i < 3)
            {
                x = i * 100+2;
                boardsquares[i] = new Rectangle(x, 2, squaredimen, squaredimen);
            }
            else if (i < 6)
            {
                x = (i - 3) * 100+2;
                boardsquares[i] = new Rectangle(x, 102, squaredimen, squaredimen);
            }
            else if (i < 9)
            {
                x = (i - 6) * 100+2;
                boardsquares[i] = new Rectangle(x, 202, squaredimen, squaredimen);
            }
            else if (i < 12)
            {
                x = (i - 9) * 100 + 2;
                boardsquares[i] = new Rectangle(x, 302, squaredimen, squaredimen);
            }
            else if (i < 15)
            {
                y = (i - 11) * 100 + 2;
                boardsquares[i] = new Rectangle(502, y, squaredimen, squaredimen);
            }
            else if (i < 18)
            {
                y = (i - 14) * 100 + 2;
                boardsquares[i] = new Rectangle(602, y, squaredimen, squaredimen);
            }
            else if (i < 21)
            {
                y = (i - 17) * 100 + 2;
                boardsquares[i] = new Rectangle(702, y, squaredimen, squaredimen);
            }
            else if (i < 24)
            {
                y = (i - 20) * 100 + 2;
                boardsquares[i] = new Rectangle(802, y, squaredimen, squaredimen);
            }
        }
        return boardsquares;
    }


    }
}
