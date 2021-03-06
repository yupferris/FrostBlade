﻿using System;
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
                        cycleFaces(FaceOrSlice.F, FaceOrSlice.R, FaceOrSlice.B, FaceOrSlice.L);
                        break;

                    case Move.UPrime:
                    case Move.D:
                        cycleFaces(FaceOrSlice.F, FaceOrSlice.L, FaceOrSlice.B, FaceOrSlice.R);
                        break;

                    case Move.U2:
                    case Move.D2:
                        cycleFaces(FaceOrSlice.F, FaceOrSlice.B);
                        cycleFaces(FaceOrSlice.L, FaceOrSlice.R);
                        break;

                    case Move.L:
                    case Move.RPrime:
                        cycleFaces(FaceOrSlice.F, FaceOrSlice.U, FaceOrSlice.B, FaceOrSlice.D);
                        break;

                    case Move.LPrime:
                    case Move.R:
                        cycleFaces(FaceOrSlice.F, FaceOrSlice.D, FaceOrSlice.B, FaceOrSlice.U);
                        break;

                    case Move.L2:
                    case Move.R2:
                        cycleFaces(FaceOrSlice.U, FaceOrSlice.D);
                        cycleFaces(FaceOrSlice.F, FaceOrSlice.B);
                        break;

                    case Move.F:
                    case Move.BPrime:
                        cycleFaces(FaceOrSlice.U, FaceOrSlice.L, FaceOrSlice.D, FaceOrSlice.R);
                        break;

                    case Move.FPrime:
                    case Move.B:
                        cycleFaces(FaceOrSlice.U, FaceOrSlice.R, FaceOrSlice.D, FaceOrSlice.L);
                        break;

                    case Move.F2:
                    case Move.B2:
                        cycleFaces(FaceOrSlice.U, FaceOrSlice.D);
                        cycleFaces(FaceOrSlice.L, FaceOrSlice.R);
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            void cycleFaces(params FaceOrSlice[] faces)
            {
                if (faces.Length < 2)
                    throw new ArgumentException("Must specify at least two faces.", "faces");
                var tmp = Colors[(int)faces[0]];
                for (int i = 0; i < faces.Length - 1; i++)
                    Colors[(int)faces[i]] = Colors[(int)faces[i + 1]];
                Colors[(int)faces[faces.Length - 1]] = tmp;
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

        class Group
        {
            public readonly FaceOrSlice FaceOrSlice;
            public readonly Color Color;
            public readonly PieceIndex[] PieceIndices;

            readonly Cube _cube;

            public Group(Cube cube, FaceOrSlice faceOrSlice, Color color, PieceIndex[] pieceIndices)
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
                        cyclePieces(1, 3, 7, 5);
                        cyclePieces(0, 6, 8, 2);
                        break;

                    case MoveType.CounterClockwise:
                        cyclePieces(1, 5, 7, 3);
                        cyclePieces(0, 2, 8, 6);
                        break;

                    case MoveType.Double:
                        cyclePieces(1, 7);
                        cyclePieces(3, 5);
                        cyclePieces(0, 8);
                        cyclePieces(2, 6);
                        break;
                }
            }

            void cyclePieces(params int[] pieces)
            {
                if (pieces.Length < 2)
                    throw new ArgumentException("Must specify at least two pieces.", "pieces");
                var tmp = _cube._pieces[(int)PieceIndices[pieces[0]]];
                for (int i = 0; i < pieces.Length - 1; i++)
                    _cube._pieces[(int)PieceIndices[pieces[i]]] = _cube._pieces[(int)PieceIndices[pieces[i + 1]]];
                _cube._pieces[(int)PieceIndices[pieces[pieces.Length - 1]]] = tmp;
            }
        }

        readonly Piece[] _pieces = new Piece[26];
        readonly Group[] _groups = new Group[6];

        public Cube()
        {
            for (int i = 0; i < _pieces.Length; i++)
                _pieces[i] = new Piece();

            _groups[(int)FaceOrSlice.U] = new Group(this, FaceOrSlice.U, Color.Yellow, new PieceIndex[]
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
            _groups[(int)FaceOrSlice.D] = new Group(this, FaceOrSlice.D, Color.White, new PieceIndex[]
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
            _groups[(int)FaceOrSlice.F] = new Group(this, FaceOrSlice.F, Color.Green, new PieceIndex[]
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
            _groups[(int)FaceOrSlice.B] = new Group(this, FaceOrSlice.B, Color.Blue, new PieceIndex[]
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
            _groups[(int)FaceOrSlice.L] = new Group(this, FaceOrSlice.L, Color.Red, new PieceIndex[]
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
            _groups[(int)FaceOrSlice.R] = new Group(this, FaceOrSlice.R, Color.Orange, new PieceIndex[]
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
            foreach (var group in _groups)
                group.Reset();
        }

        public void Apply(Move move)
        {
            _groups[(int)MoveHelpers.GetMoveFaceOrSlice(move)].Apply(move);
        }

        public void Apply(Algorithm alg)
        {
            foreach (var move in alg.Moves)
                Apply(move);
        }

        public void Print()
        {
            var originalColor = Console.ForegroundColor;

            var group = _groups[(int)FaceOrSlice.U];
            for (int y = 0; y < 3; y++)
            {
                Console.Write("   ");
                for (int x = 0; x < 3; x++)
                {
                    var piece = _pieces[(int)group.PieceIndices[y * 3 + x]];
                    setConsoleColor(piece.Colors[(int)group.FaceOrSlice]);
                    Console.Write("X");
                }
                Console.WriteLine();
            }

            var faces = new FaceOrSlice[] { FaceOrSlice.L, FaceOrSlice.F, FaceOrSlice.R, FaceOrSlice.B };
            for (int y = 0; y < 3; y++)
            {
                for (int f = 0; f < faces.Length; f++)
                {
                    group = _groups[(int)faces[f]];
                    for (int x = 0; x < 3; x++)
                    {
                        var piece = _pieces[(int)group.PieceIndices[y * 3 + x]];
                        setConsoleColor(piece.Colors[(int)group.FaceOrSlice]);
                        Console.Write("X");
                    }
                }
                Console.WriteLine();
            }

            group = _groups[(int)FaceOrSlice.D];
            for (int y = 0; y < 3; y++)
            {
                Console.Write("   ");
                for (int x = 0; x < 3; x++)
                {
                    var piece = _pieces[(int)group.PieceIndices[y * 3 + x]];
                    setConsoleColor(piece.Colors[(int)group.FaceOrSlice]);
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
