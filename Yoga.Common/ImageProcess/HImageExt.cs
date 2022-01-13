using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Yoga.Common.Basic;

namespace Yoga.Common.ImageProcess
{
    [Serializable]
    public class HImageExt : HImage, ISerializeCheck, ISerializable
    {
        /// <summary>
        /// 采集当前图像时候的位置X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 采集当前图像时候的位置X
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// 采集当前图像时候的位置X
        /// </summary>
        public double Z { get; set; }
        /// <summary>
        /// X轴和直角坐标系X轴夹角
        /// </summary>
        public double PhiX { get; set; }
        /// <summary>
        /// X轴和直角坐标系旋转重叠后 Y轴和直角坐标系Y轴夹角
        /// </summary>
        public double PhiY { get; set; }
        /// <summary>
        /// X方向像素比率
        /// </summary>
        public double ScaleX { get; set; }
        /// <summary>
        /// Y方向像素比率
        /// </summary>
        public double ScaleY { get; set; }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public HImageExt() : base()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName"></param>
        public HImageExt(string fileName) : base(fileName)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="copy"></param>
        public HImageExt(IntPtr key, bool copy) : base(key, copy)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="obj"></param>
        public HImageExt(HObject obj) : base(obj)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pixelPointer"></param>
        public HImageExt(string type, int width, int height, IntPtr pixelPointer) : base(type, width, height, pixelPointer)
        {
        }

        #endregion
        #region 静态函数，通过已有HImage 获取HImageExt
        /// <summary>
        /// 静态函数，通过已有HImage 获取HImageExt
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static HImageExt Instance(HObject image)
        {
            //string type;
            //int width;
            //int height;

            //IntPtr ptr = image.GetImagePointer1(out type, out width, out height);
            //return new HImageExt(type, width, height, ptr);
            return new HImageExt(image);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public HImageExt(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }

            this.X = info.GetDouble("X");
            this.Y = info.GetDouble("Y");
            this.Z = info.GetDouble("Z");
            this.PhiX = info.GetDouble("PhiX");
            this.PhiY = info.GetDouble("PhiY");
            this.ScaleX = info.GetDouble("ScaleX");
            this.ScaleY = info.GetDouble("ScaleY");
        }

        //<summary>
        //序列化
        //</summary>
        //<param name="info"></param>
        //<param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }

            info.AddValue("X", X);
            info.AddValue("Y", Y);
            info.AddValue("Z", Z);
            info.AddValue("PhiX", PhiX);
            info.AddValue("PhiY", PhiY);
            info.AddValue("ScaleX", ScaleX);
            info.AddValue("ScaleY", ScaleY);

            //Himage 内部函数 反编译得到的
            HSerializedItem item = this.SerializeImage();
            byte[] buffer = (byte[])item;
            item.Dispose();
            info.AddValue("data", buffer, typeof(byte[]));
        }

        #endregion

        /// <summary>
        /// 从he文件中获取ImageExt对象
        /// </summary>
        /// <param name="fileName">he文件所在路径</param>
        /// <returns></returns>
        public static HImageExt ReadImageExt(string fileName)
        {
            HImageExt ImgExt = null;
            try
            {
                string ext = System.IO.Path.GetExtension(fileName).ToLower();
                if (ext == ".he")
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))//作为语句：用于定义一个范围，在此范围的末尾将释放对象。 请参阅 using 语句。 by:longteng
                    {
                        fs.Seek(0, SeekOrigin.Begin);
                        BinaryFormatter binaryFmt = new BinaryFormatter();
                        ImgExt = (HImageExt)binaryFmt.Deserialize(fs);
                        //fs.Close();  //by:longteng
                    }
                }
                else
                {
                    HImage temp = new HImage(fileName);
                    ImgExt = HImageExt.Instance(temp);
                }
                return ImgExt;
            }
            catch (System.Exception ex)
            {
                return ImgExt;
            }
        }

        /// <summary>
        /// 保存HimageExt 到本地
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public void WriteImageExt(string fileName)
        {
            string ext = Path.GetExtension(fileName);

            if (ext == ".he")
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    BinaryFormatter binaryFmt = new BinaryFormatter();
                    fs.Seek(0, SeekOrigin.Begin);
                    binaryFmt.Serialize(fs, this);
                    //fs.Close(); //by:longteng
                }
            }
            else if (ext == "") //当没有传入有后缀的fileName,默认保存png magical 20170822
            {
                string type = this.GetImageType().ToString();
                if (type.Contains("real"))
                {
                    this.WriteImage("tiff", 0, fileName);
                }
                else
                {
                    this.WriteImage("png best", 0, fileName);
                }
            }
            else
            {
                this.WriteImage(ext.Substring(1), 0, fileName);
            }

        }

        /// <summary>
        /// 获取校正图片矩阵
        /// </summary>
        /// <returns></returns>
        public HHomMat2D getHomImg()
        {
            HHomMat2D hom = new HHomMat2D();
            //hom = hom.HomMat2dRotateLocal(PhiX);
            hom = hom.HomMat2dScaleLocal(ScaleX, ScaleY);
            //hom=hom.HomMat2dTranslate(Y * Math.Sin(PhiX), X * Math.Sin(PhiX));//有待验证
            return hom;
        }
        /// <summary>
        /// 获取校正轴矩阵
        /// </summary>
        /// <returns></returns>
        public HHomMat2D getHomAxis()
        {
            HHomMat2D homA = new HHomMat2D();
            homA = homA.HomMat2dRotateLocal(PhiX);
            //homA = homA.HomMat2dSlantLocal(-PhiY, "y");
            return homA;
        }
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        public new HImageExt Clone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as HImageExt;
            }
        }

        public void SerializeCheck()
        {

        }
    }
}
