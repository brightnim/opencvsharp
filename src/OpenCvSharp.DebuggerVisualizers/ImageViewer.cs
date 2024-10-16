﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenCvSharp.DebuggerVisualizers
{
    public partial class ImageViewer : Form
    {
        private readonly Bitmap bitmap;

        public ImageViewer()
        {
            InitializeComponent();
        }

        public ImageViewer(MatProxy proxy)
            : this()
        {
            bitmap = proxy.CreateBitmap();
        }

        /// <summary>
        /// For development purposes only.
        /// </summary>
        /// <param name="imgFile"></param>
        public ImageViewer(string imgFile)
            : this()
        {
            bitmap = new Bitmap(imgFile);
        }

        private void DisposeBitmap() => bitmap?.Dispose();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var ratio = SetClientSize(new System.Drawing.Size(bitmap.Width, bitmap.Height));
            DisplayRatio(ratio);

            pictureBox.Image = bitmap;
        }

        /// <summary>
        /// Adjust and set the size so that it does not extend beyond the screen.
        /// </summary>
        /// <param name="size"></param>
        private double SetClientSize(System.Drawing.Size size)
        {
            var screenSize = Screen.PrimaryScreen.Bounds.Size;
            var ratioX = (double)screenSize.Width / size.Width;
            var ratioY = (double)screenSize.Height / size.Height;
            var ratio = Math.Max(ratioX, ratioY);
            ratio = ReformRatio(ratio);
            size.Width = Convert.ToInt32(size.Width * ratio);
            size.Height = Convert.ToInt32(size.Height * ratio);
            ClientSize = size;
            pictureBox.Size = size;
            return ratio;
        }

        private static double ReformRatio(double ratio)
        {
            var lg2 = Math.Log(ratio, 2);
            var lgz = Math.Floor(lg2);
            var pw = lgz == lg2 ? lgz - 1 : lgz;
            var r = Math.Pow(2, pw);
            return r;
        }

        private void DisplayRatio(double ratio) => Text = $@"ImageViewer Zoom: {ratio:P1}";
    }
}
