using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostBlade
{
    public enum FaceOrSlice
    {
        U, D, F, B, L, R
    }

    public enum Move
    {
        U, UPrime, U2,
        D, DPrime, D2,
        L, LPrime, L2,
        R, RPrime, R2,
        F, FPrime, F2,
        B, BPrime, B2
    }

    public enum MoveType
    {
        Clockwise,
        CounterClockwise,
        Double
    }

    public static class MoveHelpers
    {
        public static string GetNotation(Move move)
        {
            switch (move)
            {
                case Move.U:
                case Move.U2:
                case Move.D:
                case Move.D2:
                case Move.L:
                case Move.L2:
                case Move.R:
                case Move.R2:
                case Move.F:
                case Move.F2:
                case Move.B:
                case Move.B2:
                    return move.ToString();

                case Move.UPrime:
                case Move.DPrime:
                case Move.LPrime:
                case Move.RPrime:
                case Move.FPrime:
                case Move.BPrime:
                    return GetMoveFaceOrSlice(move).ToString() + "'";
            }

            return null;
        }

        public static FaceOrSlice GetMoveFaceOrSlice(Move move)
        {
            switch (move)
            {
                case Move.U:
                case Move.UPrime:
                case Move.U2:
                    return FaceOrSlice.U;

                case Move.D:
                case Move.DPrime:
                case Move.D2:
                    return FaceOrSlice.D;

                case Move.F:
                case Move.FPrime:
                case Move.F2:
                    return FaceOrSlice.F;

                case Move.B:
                case Move.BPrime:
                case Move.B2:
                    return FaceOrSlice.B;

                case Move.L:
                case Move.LPrime:
                case Move.L2:
                    return FaceOrSlice.L;

                case Move.R:
                case Move.RPrime:
                case Move.R2:
                    return FaceOrSlice.R;

                default:
                    throw new NotImplementedException();
            }
        }

        public static MoveType GetMoveType(Move move)
        {
            switch (move)
            {
                case Move.U:
                case Move.D:
                case Move.F:
                case Move.B:
                case Move.L:
                case Move.R:
                    return MoveType.Clockwise;

                case Move.UPrime:
                case Move.DPrime:
                case Move.FPrime:
                case Move.BPrime:
                case Move.LPrime:
                case Move.RPrime:
                    return MoveType.CounterClockwise;

                case Move.U2:
                case Move.D2:
                case Move.F2:
                case Move.B2:
                case Move.L2:
                case Move.R2:
                    return MoveType.Double;

                default:
                    throw new NotImplementedException();
            }
        }

        public static Move GetRandom(Random random)
        {
            var values = Enum.GetValues(typeof(Move));
            return (Move)values.GetValue(random.Next(values.Length));
        }

        public static bool IsOnUFace(Move m)
        {
            return GetMoveFaceOrSlice(m) == FaceOrSlice.U;
        }

        public static bool IsOnDFace(Move m)
        {
            return GetMoveFaceOrSlice(m) == FaceOrSlice.D;
        }

        public static bool IsOnLFace(Move m)
        {
            return GetMoveFaceOrSlice(m) == FaceOrSlice.L;
        }

        public static bool IsOnRFace(Move m)
        {
            return GetMoveFaceOrSlice(m) == FaceOrSlice.R;
        }

        public static bool IsOnFFace(Move m)
        {
            return GetMoveFaceOrSlice(m) == FaceOrSlice.F;
        }

        public static bool IsOnBFace(Move m)
        {
            return GetMoveFaceOrSlice(m) == FaceOrSlice.B;
        }

        public static bool AreOnSameFace(Move a, Move b)
        {
            return GetMoveFaceOrSlice(a) == GetMoveFaceOrSlice(b);
        }

        public static bool AreOnOppositeFace(Move a, Move b)
        {
            if ((IsOnUFace(a) && IsOnDFace(b)) ||
                (IsOnDFace(a) && IsOnUFace(b)) ||
                (IsOnLFace(a) && IsOnRFace(b)) ||
                (IsOnRFace(a) && IsOnLFace(b)) ||
                (IsOnFFace(a) && IsOnBFace(b)) ||
                (IsOnBFace(a) && IsOnFFace(b))) return true;
            return false;
        }

        public static bool AreAdjacent(Move a, Move b)
        {
            return !(AreOnSameFace(a, b) || AreOnOppositeFace(a, b));
        }

        public static bool TryParse(string s, out Move m)
        {
            var s2 = s.EndsWith("2'") ? s.TrimEnd('\'') : s;
            return Enum.TryParse(s2.Replace("'", "Prime"), out m);
        }
    }
}
