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
        Black,
        Yellow,
        White,
        Green,
        Blue,
        Red,
        Orange
    }

    class Cube
    {
        class Piece
        {
            public readonly Color[] Colors = new Color[6];

            public void Reset()
            {
                for (int i = 0; i < 6; i++)
                    Colors[i] = Color.Black;
            }

            public void Apply(Move move)
            {
                switch (move)
                {
                    case Move.U:
                    case Move.DPrime:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.F];
                            Colors[(int)FrostBlade.FaceOrSlice.F] = Colors[(int)FrostBlade.FaceOrSlice.R];
                            Colors[(int)FrostBlade.FaceOrSlice.R] = Colors[(int)FrostBlade.FaceOrSlice.B];
                            Colors[(int)FrostBlade.FaceOrSlice.B] = Colors[(int)FrostBlade.FaceOrSlice.L];
                            Colors[(int)FrostBlade.FaceOrSlice.L] = tmp;
                        }
                        break;

                    case Move.UPrime:
                    case Move.D:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.F];
                            Colors[(int)FrostBlade.FaceOrSlice.F] = Colors[(int)FrostBlade.FaceOrSlice.L];
                            Colors[(int)FrostBlade.FaceOrSlice.L] = Colors[(int)FrostBlade.FaceOrSlice.B];
                            Colors[(int)FrostBlade.FaceOrSlice.B] = Colors[(int)FrostBlade.FaceOrSlice.R];
                            Colors[(int)FrostBlade.FaceOrSlice.R] = tmp;
                        }
                        break;

                    case Move.U2:
                    case Move.D2:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.F];
                            Colors[(int)FrostBlade.FaceOrSlice.F] = Colors[(int)FrostBlade.FaceOrSlice.B];
                            Colors[(int)FrostBlade.FaceOrSlice.B] = tmp;
                            tmp = Colors[(int)FrostBlade.FaceOrSlice.L];
                            Colors[(int)FrostBlade.FaceOrSlice.L] = Colors[(int)FrostBlade.FaceOrSlice.R];
                            Colors[(int)FrostBlade.FaceOrSlice.R] = tmp;
                        }
                        break;

                    case Move.L:
                    case Move.RPrime:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.F];
                            Colors[(int)FrostBlade.FaceOrSlice.F] = Colors[(int)FrostBlade.FaceOrSlice.U];
                            Colors[(int)FrostBlade.FaceOrSlice.U] = Colors[(int)FrostBlade.FaceOrSlice.B];
                            Colors[(int)FrostBlade.FaceOrSlice.B] = Colors[(int)FrostBlade.FaceOrSlice.D];
                            Colors[(int)FrostBlade.FaceOrSlice.D] = tmp;
                        }
                        break;

                    case Move.LPrime:
                    case Move.R:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.F];
                            Colors[(int)FrostBlade.FaceOrSlice.F] = Colors[(int)FrostBlade.FaceOrSlice.D];
                            Colors[(int)FrostBlade.FaceOrSlice.D] = Colors[(int)FrostBlade.FaceOrSlice.B];
                            Colors[(int)FrostBlade.FaceOrSlice.B] = Colors[(int)FrostBlade.FaceOrSlice.U];
                            Colors[(int)FrostBlade.FaceOrSlice.U] = tmp;
                        }
                        break;

                    case Move.L2:
                    case Move.R2:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.U];
                            Colors[(int)FrostBlade.FaceOrSlice.U] = Colors[(int)FrostBlade.FaceOrSlice.D];
                            Colors[(int)FrostBlade.FaceOrSlice.D] = tmp;
                            tmp = Colors[(int)FrostBlade.FaceOrSlice.F];
                            Colors[(int)FrostBlade.FaceOrSlice.F] = Colors[(int)FrostBlade.FaceOrSlice.B];
                            Colors[(int)FrostBlade.FaceOrSlice.B] = tmp;
                        }
                        break;

                    case Move.F:
                    case Move.BPrime:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.U];
                            Colors[(int)FrostBlade.FaceOrSlice.U] = Colors[(int)FrostBlade.FaceOrSlice.L];
                            Colors[(int)FrostBlade.FaceOrSlice.L] = Colors[(int)FrostBlade.FaceOrSlice.D];
                            Colors[(int)FrostBlade.FaceOrSlice.D] = Colors[(int)FrostBlade.FaceOrSlice.R];
                            Colors[(int)FrostBlade.FaceOrSlice.R] = tmp;
                        }
                        break;

                    case Move.FPrime:
                    case Move.B:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.U];
                            Colors[(int)FrostBlade.FaceOrSlice.U] = Colors[(int)FrostBlade.FaceOrSlice.R];
                            Colors[(int)FrostBlade.FaceOrSlice.R] = Colors[(int)FrostBlade.FaceOrSlice.D];
                            Colors[(int)FrostBlade.FaceOrSlice.D] = Colors[(int)FrostBlade.FaceOrSlice.L];
                            Colors[(int)FrostBlade.FaceOrSlice.L] = tmp;
                        }
                        break;

                    case Move.F2:
                    case Move.B2:
                        {
                            var tmp = Colors[(int)FrostBlade.FaceOrSlice.U];
                            Colors[(int)FrostBlade.FaceOrSlice.U] = Colors[(int)FrostBlade.FaceOrSlice.D];
                            Colors[(int)FrostBlade.FaceOrSlice.D] = tmp;
                            tmp = Colors[(int)FrostBlade.FaceOrSlice.L];
                            Colors[(int)FrostBlade.FaceOrSlice.L] = Colors[(int)FrostBlade.FaceOrSlice.R];
                            Colors[(int)FrostBlade.FaceOrSlice.R] = tmp;
                        }
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        enum PieceIndex
        {
            // Centers
            U, D, F, B, L, R,

            // Edges
            UF, UL, UB, UR,
            FL, BL, BR, FR,
            DF, DL, DB, DR,

            // Corners
            UFL, UFR, UBL, UBR,
            DFL, DFR, DBL, DBR
        }

        class Face
        {
            public readonly FaceOrSlice FaceOrSlice;
            public readonly Color Color;
            public readonly PieceIndex[] PieceIndices;

            readonly Cube _cube;

            public Face(Cube cube, FaceOrSlice faceOrSlice, Color color, PieceIndex[] pieceIndices)
            {
                _cube = cube;
                FaceOrSlice = faceOrSlice;
                Color = color;
                PieceIndices = pieceIndices;
            }

            public void Reset()
            {
                foreach (var index in PieceIndices)
                    _cube._pieces[(int)index].Colors[(int)FaceOrSlice] = Color;
            }

            public void Apply(Move move)
            {
                foreach (var index in PieceIndices)
                    _cube._pieces[(int)index].Apply(move);

                switch (MoveHelpers.GetMoveType(move))
                {
                    case MoveType.Clockwise:
                        {
                            // Edges
                            var tmp = _cube._pieces[(int)PieceIndices[1]];
                            _cube._pieces[(int)PieceIndices[1]] = _cube._pieces[(int)PieceIndices[3]];
                            _cube._pieces[(int)PieceIndices[3]] = _cube._pieces[(int)PieceIndices[7]];
                            _cube._pieces[(int)PieceIndices[7]] = _cube._pieces[(int)PieceIndices[5]];
                            _cube._pieces[(int)PieceIndices[5]] = tmp;

                            // Corners
                            tmp = _cube._pieces[(int)PieceIndices[0]];
                            _cube._pieces[(int)PieceIndices[0]] = _cube._pieces[(int)PieceIndices[6]];
                            _cube._pieces[(int)PieceIndices[6]] = _cube._pieces[(int)PieceIndices[8]];
                            _cube._pieces[(int)PieceIndices[8]] = _cube._pieces[(int)PieceIndices[2]];
                            _cube._pieces[(int)PieceIndices[2]] = tmp;
                        }
                        break;

                    case MoveType.CounterClockwise:
                        {
                            // Edges
                            var tmp = _cube._pieces[(int)PieceIndices[1]];
                            _cube._pieces[(int)PieceIndices[1]] = _cube._pieces[(int)PieceIndices[5]];
                            _cube._pieces[(int)PieceIndices[5]] = _cube._pieces[(int)PieceIndices[7]];
                            _cube._pieces[(int)PieceIndices[7]] = _cube._pieces[(int)PieceIndices[3]];
                            _cube._pieces[(int)PieceIndices[3]] = tmp;

                            // Corners
                            tmp = _cube._pieces[(int)PieceIndices[0]];
                            _cube._pieces[(int)PieceIndices[0]] = _cube._pieces[(int)PieceIndices[2]];
                            _cube._pieces[(int)PieceIndices[2]] = _cube._pieces[(int)PieceIndices[8]];
                            _cube._pieces[(int)PieceIndices[8]] = _cube._pieces[(int)PieceIndices[6]];
                            _cube._pieces[(int)PieceIndices[6]] = tmp;
                        }
                        break;

                    case MoveType.Double:
                        {
                            // Edges
                            var tmp = _cube._pieces[(int)PieceIndices[1]];
                            _cube._pieces[(int)PieceIndices[1]] = _cube._pieces[(int)PieceIndices[7]];
                            _cube._pieces[(int)PieceIndices[7]] = tmp;
                            tmp = _cube._pieces[(int)PieceIndices[3]];
                            _cube._pieces[(int)PieceIndices[3]] = _cube._pieces[(int)PieceIndices[5]];
                            _cube._pieces[(int)PieceIndices[5]] = tmp;

                            // Corners
                            tmp = _cube._pieces[(int)PieceIndices[0]];
                            _cube._pieces[(int)PieceIndices[0]] = _cube._pieces[(int)PieceIndices[8]];
                            _cube._pieces[(int)PieceIndices[8]] = tmp;
                            tmp = _cube._pieces[(int)PieceIndices[2]];
                            _cube._pieces[(int)PieceIndices[2]] = _cube._pieces[(int)PieceIndices[6]];
                            _cube._pieces[(int)PieceIndices[6]] = tmp;
                        }
                        break;
                }
            }
        }

        readonly Piece[] _pieces = new Piece[26];
        readonly Face[] _faces = new Face[6];

        public Cube()
        {
            for (int i = 0; i < _pieces.Length; i++)
                _pieces[i] = new Piece();

            _faces[(int)FaceOrSlice.U] = new Face(this, FaceOrSlice.U, Color.Yellow, new PieceIndex[]
                {
                    PieceIndex.UBL,
                    PieceIndex.UB,
                    PieceIndex.UBR,
                    PieceIndex.UL,
                    PieceIndex.U,
                    PieceIndex.UR,
                    PieceIndex.UFL,
                    PieceIndex.UF,
                    PieceIndex.UFR
                });
            _faces[(int)FaceOrSlice.D] = new Face(this, FaceOrSlice.D, Color.White, new PieceIndex[]
                {
                    PieceIndex.DFL,
                    PieceIndex.DF,
                    PieceIndex.DFR,
                    PieceIndex.DL,
                    PieceIndex.D,
                    PieceIndex.DR,
                    PieceIndex.DBL,
                    PieceIndex.DB,
                    PieceIndex.DBR
                });
            _faces[(int)FaceOrSlice.F] = new Face(this, FaceOrSlice.F, Color.Green, new PieceIndex[]
                {
                    PieceIndex.UFL,
                    PieceIndex.UF,
                    PieceIndex.UFR,
                    PieceIndex.FL,
                    PieceIndex.F,
                    PieceIndex.FR,
                    PieceIndex.DFL,
                    PieceIndex.DF,
                    PieceIndex.DFR
                });
            _faces[(int)FaceOrSlice.B] = new Face(this, FaceOrSlice.B, Color.Blue, new PieceIndex[]
                {
                    PieceIndex.UBR,
                    PieceIndex.UB,
                    PieceIndex.UBL,
                    PieceIndex.BR,
                    PieceIndex.B,
                    PieceIndex.BL,
                    PieceIndex.DBR,
                    PieceIndex.DB,
                    PieceIndex.DBL
                });
            _faces[(int)FaceOrSlice.L] = new Face(this, FaceOrSlice.L, Color.Red, new PieceIndex[]
                {
                    PieceIndex.UBL,
                    PieceIndex.UL,
                    PieceIndex.UFL,
                    PieceIndex.BL,
                    PieceIndex.L,
                    PieceIndex.FL,
                    PieceIndex.DBL,
                    PieceIndex.DL,
                    PieceIndex.DFL
                });
            _faces[(int)FaceOrSlice.R] = new Face(this, FaceOrSlice.R, Color.Orange, new PieceIndex[]
                {
                    PieceIndex.UFR,
                    PieceIndex.UR,
                    PieceIndex.UBR,
                    PieceIndex.FR,
                    PieceIndex.R,
                    PieceIndex.BR,
                    PieceIndex.DFR,
                    PieceIndex.DR,
                    PieceIndex.DBR
                });

            Reset();
        }

        public void Reset()
        {
            foreach (var piece in _pieces)
                piece.Reset();
            foreach (var face in _faces)
                face.Reset();
        }

        public void Apply(Move move)
        {
            _faces[(int)MoveHelpers.GetMoveFaceOrSlice(move)].Apply(move);
        }

        public void Apply(Algorithm alg)
        {
            foreach (var move in alg.Moves)
                Apply(move);
        }

        public void Print()
        {
            var originalColor = Console.ForegroundColor;

            var face = _faces[(int)FaceOrSlice.U];
            for (int y = 0; y < 3; y++)
            {
                Console.Write("   ");
                for (int x = 0; x < 3; x++)
                {
                    var piece = _pieces[(int)face.PieceIndices[y * 3 + x]];
                    setConsoleColor(piece.Colors[(int)face.FaceOrSlice]);
                    Console.Write("X");
                }
                Console.WriteLine();
            }

            var faces = new FaceOrSlice[] { FaceOrSlice.L, FaceOrSlice.F, FaceOrSlice.R, FaceOrSlice.B };
            for (int y = 0; y < 3; y++)
            {
                for (int f = 0; f < faces.Length; f++)
                {
                    face = _faces[(int)faces[f]];
                    for (int x = 0; x < 3; x++)
                    {
                        var piece = _pieces[(int)face.PieceIndices[y * 3 + x]];
                        setConsoleColor(piece.Colors[(int)face.FaceOrSlice]);
                        Console.Write("X");
                    }
                }
                Console.WriteLine();
            }

            face = _faces[(int)FaceOrSlice.D];
            for (int y = 0; y < 3; y++)
            {
                Console.Write("   ");
                for (int x = 0; x < 3; x++)
                {
                    var piece = _pieces[(int)face.PieceIndices[y * 3 + x]];
                    setConsoleColor(piece.Colors[(int)face.FaceOrSlice]);
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
                case Color.Black:
                default:
                    consoleColor = ConsoleColor.Black;
                    break;

                case Color.Yellow: consoleColor = ConsoleColor.Yellow; break;
                case Color.White: consoleColor = ConsoleColor.White; break;
                case Color.Green: consoleColor = ConsoleColor.Green; break;
                case Color.Blue: consoleColor = ConsoleColor.Blue; break;
                case Color.Red: consoleColor = ConsoleColor.Red; break;
                case Color.Orange: consoleColor = ConsoleColor.DarkYellow; break; // TODO: Make this actually orange :)
            }
            Console.ForegroundColor = consoleColor;
        }
    }
}
