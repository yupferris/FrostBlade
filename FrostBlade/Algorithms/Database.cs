using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace FrostBlade.Algorithms
{
    public class Database
    {
        [XmlRoot("Database")]
        public class StorageDatabase
        {
            public class StorageAlgorithm
            {
                [XmlAttribute]
                public AlgorithmType Type { get; set; }

                [XmlAttribute]
                public string Name { get; set; }

                [XmlAttribute]
                public string ImageFileName { get; set; }

                [XmlAttribute]
                public string Moves { get; set; }

                [XmlAttribute]
                public string Comments { get; set; }
            }

            [XmlArray, XmlArrayItem("Algorithm")]
            public List<StorageAlgorithm> Algorithms;
        }

        readonly ObservableCollection<Algorithm> _algorithms = new ObservableCollection<Algorithm>();
        public ObservableCollection<Algorithm> Algorithms { get { return _algorithms; } }

        public Database(string dirName)
        {
            using (var inputStream = File.OpenRead(dirName + "/Database.xml"))
            {
                var storageDatabase = (StorageDatabase)(new XmlSerializer(typeof(StorageDatabase)).Deserialize(inputStream));
                foreach (var storageAlgorithm in storageDatabase.Algorithms)
                    _algorithms.Add(new Algorithm(
                        storageAlgorithm.Type,
                        storageAlgorithm.Name,
                        new BitmapImage(new Uri(Path.GetFullPath(dirName + "/" + storageAlgorithm.ImageFileName))),
                        storageAlgorithm.Moves,
                        storageAlgorithm.Comments));
            }
        }
    }
}
