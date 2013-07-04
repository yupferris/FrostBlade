using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostBlade
{
    public enum Moves
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
        public static string GetNotation(Moves move)
        {
            switch (move)
            {
                case Moves.U:
                case Moves.U2:
                case Moves.D:
                case Moves.D2:
                case Moves.L:
                case Moves.L2:
                case Moves.R:
                case Moves.R2:
                case Moves.F:
                case Moves.F2:
                case Moves.B:
                case Moves.B2:
                    return move.ToString();

                case Moves.UPrime:
                case Moves.DPrime:
                case Moves.LPrime:
                case Moves.RPrime:
                case Moves.FPrime:
                case Moves.BPrime:
                    return move.ToString().First() + "'";
            }

            return null;
        }

        public static Moves GetRandom(Random random)
        {
            var values = Enum.GetValues(typeof(Moves));
            return (Moves)values.GetValue(random.Next(values.Length));
        }

        public static bool IsOnUFace(Moves m)
        {
            return m.ToString().First() == 'U';
        }

        public static bool IsOnDFace(Moves m)
        {
            return m.ToString().First() == 'D';
        }

        public static bool IsOnLFace(Moves m)
        {
            return m.ToString().First() == 'L';
        }

        public static bool IsOnRFace(Moves m)
        {
            return m.ToString().First() == 'R';
        }

        public static bool IsOnFFace(Moves m)
        {
            return m.ToString().First() == 'F';
        }

        public static bool IsOnBFace(Moves m)
        {
            return m.ToString().First() == 'B';
        }

        public static bool AreOnSameFace(Moves a, Moves b)
        {
            return GetNotation(a).First() == GetNotation(b).First();
        }

        public static bool AreOnOppositeFace(Moves a, Moves b)
        {
            if ((IsOnUFace(a) && IsOnDFace(b)) ||
                (IsOnDFace(a) && IsOnUFace(b)) ||
                (IsOnLFace(a) && IsOnRFace(b)) ||
                (IsOnRFace(a) && IsOnLFace(b)) ||
                (IsOnFFace(a) && IsOnBFace(b)) ||
                (IsOnBFace(a) && IsOnFFace(b))) return true;
            return false;
        }

        public static bool AreAdjacent(Moves a, Moves b)
        {
            if (AreOnSameFace(a, b) || AreOnOppositeFace(a, b))
                return false;

            return true;
        }
    }
}
