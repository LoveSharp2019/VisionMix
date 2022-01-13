using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
namespace Yoga.Tools
{
    [Serializable]
    public class SacleImagePretreatment
    {
        int offset = 0;
        double scale = 1;
        public int Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }
        public double Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }

        }

        public HImage Run(HImage img)
        {
            double offsetTmp = Scale * Offset;
            return img.ScaleImage(Scale, offsetTmp);
        }
    }
}
