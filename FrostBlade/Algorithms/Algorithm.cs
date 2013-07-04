using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Media.Imaging;

namespace FrostBlade.Algorithms
{
    public enum AlgorithmType
    {
        Oll,
        Pll,
    }

    public class Algorithm
    {
        readonly AlgorithmType _type;
        public AlgorithmType Type { get { return _type; } }

        // TODO: Groups?

        readonly string _name;
        public string Name { get { return _name; } }

        readonly BitmapImage _image;
        public BitmapImage Image { get { return _image; } }

        readonly string _moves;
        public string Moves { get { return _moves; } }

        readonly string _comments;
        public string Comments { get { return _comments; } }

        public Algorithm(AlgorithmType type, string name, BitmapImage image, string moves, string comments)
        {
            _type = type;
            _name = name;
            _image = image;
            _moves = moves;
            _comments = comments;
        }
    }
}
