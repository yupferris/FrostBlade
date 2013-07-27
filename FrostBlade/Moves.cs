using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostBlade
{
    public enum Move
    {
        U, UPrime, U2,
        D, DPrime, D2,
        L, LPrime, L2,
        R, RPrime, R2,
        F, FPrime, F2,
        B, BPrime, B2,
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
                    return move.ToString().First() + "'";
            }

            return null;
        }

        public static Move GetRandom(Random random)
        {
            var values = Enum.GetValues(typeof(Move));
            return (Move)values.GetValue(random.Next(values.Length));
        }

        public static bool IsOnUFace(Move m)
        {
            return m.ToString().First() == 'U';
        }

        public static bool IsOnDFace(Move m)
        {
            return m.ToString().First() == 'D';
        }

        public static bool IsOnLFace(Move m)
        {
            return m.ToString().First() == 'L';
        }

        public static bool IsOnRFace(Move m)
        {
            return m.ToString().First() == 'R';
        }

        public static bool IsOnFFace(Move m)
        {
            return m.ToString().First() == 'F';
        }

        public static bool IsOnBFace(Move m)
        {
            return m.ToString().First() == 'B';
        }

        public static bool AreOnSameFace(Move a, Move b)
        {
            return GetNotation(a).First() == GetNotation(b).First();
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
            if (AreOnSameFace(a, b) || AreOnOppositeFace(a, b))
                return false;

            return true;
        }
    }
}
