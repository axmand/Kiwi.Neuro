using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro.Networks
{
    public class RecurrentNetwork:Network
    {
        //
        protected readonly ParallelOptions options = new ParallelOptions();
        //
        public int BufferSize { get; set; } = 24;

        // Dimensions.
        private int size_output;
        private int size_input;
        private int size_total;

        // State.
        private double[][] node_output;
        private double[][] vcx;

        // Parameters.
        private double[] b_node_output;
        private double[][] w_node_output;

        // Gradients.
        private double[] db_node_output;
        private double[][] dw_node_output;

        // Caches.
        private double[] cb_node_output;
        private double[][] cw_node_output;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size_input"></param>
        /// <param name="size_output"></param>
        public RecurrentNetwork(int inputsCount, int outputsCount) : base(inputsCount, 1)
        {
            //size
            size_input = inputsCount;
            size_output = outputsCount;
            size_total = size_input + size_output;
            //state
            node_output = new double[BufferSize][];
            node_output[0] = new double[size_output];
            vcx = new double[BufferSize][];
            //parameters
            b_node_output = new double[size_output];
            w_node_output = new double[size_output][];
            //gradients
            db_node_output = new double[size_output];
            dw_node_output = new double[size_output][];
            //caches
            cb_node_output = new double[size_output];
            cw_node_output = new double[size_output][];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[][] Compute(double[][] buffer)
        {
            node_output[0] = node_output[BufferSize - 1].ToArray();
            for(int t = 1; t < BufferSize; t++)
            {
                buffer[t].CopyTo(vcx[t], 0);
                node_output[t - 1].CopyTo(vcx[t], size_input);
                double[] row_vcx_state = vcx[t];
                node_output[t] = new double[size_output];
                Parallel.For(0, size_output, options, j =>
                {
                    double sum = b_node_output[j];
                    double[] row = w_node_output[j];
                    for (var i = 0; i < size_total; i++)
                        sum += row_vcx_state[i] * row[i];
                    //使用activation
                    //node_output[t][j] = Tanh(sum);
                });
            }
            return node_output;
        }

        /// <summary>
        /// 结构， [position][window-1] = 1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double[][] FillTextBuffer(string rawText)
        {
            return null;
        }

        /// <summary>
        /// 设置rnn需要的辞典文件
        /// </summary>
        /// <param name="lexiconName"></param>
        public void SetLexicon(string lexiconName)
        {

        }



    }
}
