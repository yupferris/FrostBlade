using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrostBlade;
using FrostBlade.Algorithms;

namespace Hypercube
{
    enum Color
    {
        White,
        Yellow,
        Green,
        Blue,
        Red,
        Orange
    }

    class Cube
    {
        enum FaceIndex
        {
            Top,
            Left,
            Front,
            Right,
            Back,
            Bottom
        }

        enum MoveType
        {
            Clockwise,
            CounterClockwise,
            Double
        }

        readonly Color[,] _faces = new Color[6, 9];

        public Cube()
        {
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < 9; i++)
            {
                _faces[(int)FaceIndex.Top, i] = Color.Yellow;
                _faces[(int)FaceIndex.Left, i] = Color.Red;
                _faces[(int)FaceIndex.Front, i] = Color.Green;
                _faces[(int)FaceIndex.Right, i] = Color.Orange;
                _faces[(int)FaceIndex.Back, i] = Color.Blue;
                _faces[(int)FaceIndex.Bottom, i] = Color.White;
            }
        }

        public void Apply(Move move)
        {
            switch (move)
            {
                case Move.U:
                    {
                        // Rotate U colors
                        rotateFaceColors((int)FaceIndex.Top, MoveType.Clockwise);

                        // Rotate adjacent edge colors
                        for (int i = 0; i < 3; i++)
                        {
                            var tmp = _faces[(int)FaceIndex.Left, i];
                            _faces[(int)FaceIndex.Left, i] = _faces[(int)FaceIndex.Front, i];
                            _faces[(int)FaceIndex.Front, i] = _faces[(int)FaceIndex.Right, i];
                            _faces[(int)FaceIndex.Right, i] = _faces[(int)FaceIndex.Back, i];
                            _faces[(int)FaceIndex.Back, i] = tmp;
                        }
                    }
                    break;

                case Move.UPrime:
                    {
                        // Rotate U colors
                        rotateFaceColors((int)FaceIndex.Top, MoveType.CounterClockwise);

                        // Rotate adjacent edge colors
                        for (int i = 0; i < 3; i++)
                        {
                            var tmp = _faces[(int)FaceIndex.Left, i];
                            _faces[(int)FaceIndex.Left, i] = _faces[(int)FaceIndex.Back, i];
                            _faces[(int)FaceIndex.Back, i] = _faces[(int)FaceIndex.Right, i];
                            _faces[(int)FaceIndex.Right, i] = _faces[(int)FaceIndex.Front, i];
                            _faces[(int)FaceIndex.Front, i] = tmp;
                        }
                    }
                    break;

                case Move.U2:
                    {
                        // Rotate U colors
                        rotateFaceColors((int)FaceIndex.Top, MoveType.Double);

                        // Rotate adjacent edge colors
                        for (int i = 0; i < 3; i++)
                        {
                            var tmp = _faces[(int)FaceIndex.Left, i];
                            _faces[(int)FaceIndex.Left, i] = _faces[(int)FaceIndex.Right, i];
                            _faces[(int)FaceIndex.Right, i] = tmp;
                            tmp = _faces[(int)FaceIndex.Front, i];
                            _faces[(int)FaceIndex.Front, i] = _faces[(int)FaceIndex.Back, i];
                            _faces[(int)FaceIndex.Back, i] = tmp;
                        }
                    }
                    break;

                case Move.R: // TODO: BROKEN AS FUCK
                    {
                        // Rotate R colors
                        rotateFaceColors((int)FaceIndex.Right, MoveType.Clockwise);

                        // Rotate adjacent edge colors
                        for (int i = 0; i < 3; i++)
                        {
                            int pos = i * 3 + 2;
                            int backPos = pos - 2;
                            var tmp = _faces[(int)FaceIndex.Front, pos];
                            _faces[(int)FaceIndex.Front, pos] = _faces[(int)FaceIndex.Bottom, pos];
                            _faces[(int)FaceIndex.Bottom, pos] = _faces[(int)FaceIndex.Back, backPos];
                            _faces[(int)FaceIndex.Back, backPos] = _faces[(int)FaceIndex.Top, pos];
                            _faces[(int)FaceIndex.Top, pos] = tmp;
                        }
                    }
                    break;

                case Move.R2:
                    {
                        // Rotate R colors
                        rotateFaceColors((int)FaceIndex.Right, MoveType.Double);

                        // Rotate adjacent edge colors
                        for (int i = 0; i < 3; i++)
                        {
                            int pos = i * 3 + 2;
                            int backPos = pos - 2;
                            var tmp = _faces[(int)FaceIndex.Front, pos];
                            _faces[(int)FaceIndex.Front, pos] = _faces[(int)FaceIndex.Back, backPos];
                            _faces[(int)FaceIndex.Back, backPos] = tmp;
                            tmp = _faces[(int)FaceIndex.Top, pos];
                            _faces[(int)FaceIndex.Top, pos] = _faces[(int)FaceIndex.Bottom, pos];
                            _faces[(int)FaceIndex.Bottom, pos] = tmp;
                        }
                    }
                    break;

                case Move.D:
                    {
                        // Rotate D colors
                        rotateFaceColors((int)FaceIndex.Bottom, MoveType.Clockwise);

                        // Rotate adjacent edge colors
                        for (int i = 6; i < 9; i++)
                        {
                            var tmp = _faces[(int)FaceIndex.Left, i];
                            _faces[(int)FaceIndex.Left, i] = _faces[(int)FaceIndex.Back, i];
                            _faces[(int)FaceIndex.Back, i] = _faces[(int)FaceIndex.Right, i];
                            _faces[(int)FaceIndex.Right, i] = _faces[(int)FaceIndex.Front, i];
                            _faces[(int)FaceIndex.Front, i] = tmp;
                        }
                    }
                    break;

                case Move.DPrime:
                    {
                        // Rotate D colors
                        rotateFaceColors((int)FaceIndex.Bottom, MoveType.CounterClockwise);

                        // Rotate adjacent edge colors
                        for (int i = 6; i < 9; i++)
                        {
                            var tmp = _faces[(int)FaceIndex.Left, i];
                            _faces[(int)FaceIndex.Left, i] = _faces[(int)FaceIndex.Front, i];
                            _faces[(int)FaceIndex.Front, i] = _faces[(int)FaceIndex.Right, i];
                            _faces[(int)FaceIndex.Right, i] = _faces[(int)FaceIndex.Back, i];
                            _faces[(int)FaceIndex.Back, i] = tmp;
                        }
                    }
                    break;

                case Move.D2:
                    {
                        // Rotate D colors
                        rotateFaceColors((int)FaceIndex.Bottom, MoveType.Double);

                        // Rotate adjacent edge colors
                        for (int i = 6; i < 9; i++)
                        {
                            var tmp1 = _faces[(int)FaceIndex.Left, i];
                            _faces[(int)FaceIndex.Left, i] = _faces[(int)FaceIndex.Right, i];
                            _faces[(int)FaceIndex.Right, i] = tmp1;
                            var tmp2 = _faces[(int)FaceIndex.Front, i];
                            _faces[(int)FaceIndex.Front, i] = _faces[(int)FaceIndex.Back, i];
                            _faces[(int)FaceIndex.Back, i] = tmp2;
                        }
                    }
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        void rotateFaceColors(int faceIndex, MoveType type)
        {
            switch (type)
            {
                case MoveType.Clockwise:
                    {
                        // Corners
                        var tmp = _faces[faceIndex, 0];
                        _faces[faceIndex, 0] = _faces[faceIndex, 6];
                        _faces[faceIndex, 6] = _faces[faceIndex, 8];
                        _faces[faceIndex, 8] = _faces[faceIndex, 2];
                        _faces[faceIndex, 2] = tmp;

                        // Edges
                        tmp = _faces[faceIndex, 1];
                        _faces[faceIndex, 1] = _faces[faceIndex, 3];
                        _faces[faceIndex, 3] = _faces[faceIndex, 7];
                        _faces[faceIndex, 7] = _faces[faceIndex, 5];
                        _faces[faceIndex, 5] = tmp;
                    }
                    break;

                case MoveType.CounterClockwise:
                    {
                        // Corners
                        var tmp = _faces[faceIndex, 0];
                        _faces[faceIndex, 0] = _faces[faceIndex, 2];
                        _faces[faceIndex, 2] = _faces[faceIndex, 8];
                        _faces[faceIndex, 8] = _faces[faceIndex, 6];
                        _faces[faceIndex, 6] = tmp;

                        // Edges
                        tmp = _faces[faceIndex, 1];
                        _faces[faceIndex, 1] = _faces[faceIndex, 5];
                        _faces[faceIndex, 5] = _faces[faceIndex, 7];
                        _faces[faceIndex, 7] = _faces[faceIndex, 3];
                        _faces[faceIndex, 3] = tmp;
                    }
                    break;

                case MoveType.Double:
                    {
                        // Corners
                        var tmp = _faces[faceIndex, 0];
                        _faces[faceIndex, 0] = _faces[faceIndex, 8];
                        _faces[faceIndex, 8] = tmp;
                        tmp = _faces[faceIndex, 2];
                        _faces[faceIndex, 2] = _faces[faceIndex, 6];
                        _faces[faceIndex, 6] = tmp;

                        // Edges
                        tmp = _faces[faceIndex, 1];
                        _faces[faceIndex, 1] = _faces[faceIndex, 7];
                        _faces[faceIndex, 7] = tmp;
                        tmp = _faces[faceIndex, 3];
                        _faces[faceIndex, 3] = _faces[faceIndex, 5];
                        _faces[faceIndex, 5] = tmp;
                    }
                    break;
            }
        }

        public void Apply(Algorithm alg)
        {
            // TODO
            /*foreach (var move in alg.Moves)
                Apply(move);*/
        }

        public void Print()
        {
            var originalColor = Console.ForegroundColor;

            for (int y = 0; y < 3; y++)
            {
                Console.Write("   ");
                for (int x = 0; x < 3; x++)
                {
                    int pos = y * 3 + x;
                    var color = _faces[(int)FaceIndex.Top, pos];
                    setConsoleColor(color);
                    Console.Write("X");
                }
                Console.WriteLine();
            }

            for (int y = 0; y < 3; y++)
            {
                for (int f = (int)FaceIndex.Left; f <= (int)FaceIndex.Back; f++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        int pos = y * 3 + x;
                        var color = _faces[f, pos];
                        setConsoleColor(color);
                        Console.Write("X");
                    }
                }
                Console.WriteLine();
            }

            for (int y = 0; y < 3; y++)
            {
                Console.Write("   ");
                for (int x = 0; x < 3; x++)
                {
                    int pos = y * 3 + x;
                    var color = _faces[(int)FaceIndex.Bottom, pos];
                    setConsoleColor(color);
                    Console.Write("X");
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.ForegroundColor = originalColor;
        }

        void setConsoleColor(Color color)
        {
            ConsoleColor consoleColor;
            switch (color)
            {
                case Color.White:
                default:
                    consoleColor = ConsoleColor.White;
                    break;

                case Color.Yellow: consoleColor = ConsoleColor.Yellow; break;
                case Color.Green: consoleColor = ConsoleColor.Green; break;
                case Color.Blue: consoleColor = ConsoleColor.Blue; break;
                case Color.Red: consoleColor = ConsoleColor.Red; break;
                case Color.Orange: consoleColor = ConsoleColor.DarkYellow; break; // TODO: Make this actually orange :)
            }
            Console.ForegroundColor = consoleColor;
        }
    }
}
