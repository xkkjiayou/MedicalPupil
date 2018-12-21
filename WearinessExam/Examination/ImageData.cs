using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WearinessExam.Examination
{
    class ImageData
    {
        public Point _point;
        public byte[] _data;

        public ImageData(Point point, byte[] data)
        {
            _point = point;
            _data = data;
        }
    }
}
