using System;
using System.Drawing;
using System.IO;

namespace OpenCvSharp.DebuggerVisualizers
{
    /// <summary>
    /// Proxy used to exchange non-serializable classes
    /// When sending this Proxy, Packs the serializable data necessary for
    /// display and sends it to the receiver, decompressing it on the receiving side.
    /// </summary>
    [Serializable]
    public class MatProxy
    {
        public byte[] ImageData { get; private set; }

        public MatProxy(Mat image)
        {
            ImageData = image.ToBytes();
        }

        public Bitmap CreateBitmap()
        {
            if (ImageData is null)
                throw new Exception("ImageData is null");

            using (var stream = new MemoryStream(ImageData))
            {
                return new Bitmap(stream);
            }
        }
    }
}
