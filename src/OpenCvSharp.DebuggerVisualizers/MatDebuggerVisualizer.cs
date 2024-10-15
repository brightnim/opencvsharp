using System;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace OpenCvSharp.DebuggerVisualizers
{
    /// <summary>
    /// Visualization process in Visualizer
    /// </summary>
    public class MatDebuggerVisualizer : DialogDebuggerVisualizer
    {
        public MatDebuggerVisualizer() : base(FormatterPolicy.Legacy)
        {
        }

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            using (var stream = objectProvider.GetData())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                var proxy = (MatProxy)formatter.Deserialize(stream);

                if (proxy == null)
                {
                    throw new ArgumentException();
                }

                // Form display
                using (var form = new ImageViewer(proxy))
                {
                    windowService.ShowDialog(form);
                }
            }
        }
    }
}
