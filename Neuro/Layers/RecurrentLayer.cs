using Neuro.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro.Layers
{
    public class RecurrentLayer: ActivationLayer
    {
        //dimensions
        private readonly int size_output, size_input, size_total;

        //state
        private double[][] node_output, vcx;

        //Parameters
        private double[] b_node_output;
        private double[][] w_node_output;

        //Gradients
        private double[] db_node_output;
        private double[][] dw_node_output;

        //caches
        private double[] cb_node_output;
        private double[][] cw_node_output;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuronsCount"></param>
        /// <param name="inputsCount"></param>
        /// <param name="function"></param>
        public RecurrentLayer(int neuronsCount, int inputsCount, IActivationFunction function) : base(neuronsCount, inputsCount, function)
        {
            size_input = inputsCount;
            size_output = neuronsCount;
            size_total = size_input + size_output;
        }



    }
}
