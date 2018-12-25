using Neuro.Abstract;
using Neuro.Neurons;
using System;

namespace Neuro.Layers
{
    public class RecurrentLayer
    {
        //State
        private double[][] vcx;

        public int BufferSize { get; set; } = 24;

        /// <summary>
        /// Layer's neurons count.
        /// </summary>
        public int Size_Output { get; } = 0;

        /// <summary>
        /// Layer's output vector.
        /// </summary>
        protected double[] output;

        /// <summary>
        /// Layer's inputs count.
        /// </summary>
        public int Size_Input { get; } = 0;

        /// <summary>
        /// Layer's Size_Total count.
        /// </summary>
        public int Size_Total { get; } = 0;

        /// <summary>
        /// Layer's neurons.
        /// </summary>
        public RecurrentNeuron[] Neurons { get; private set; }

        /// <summary>
        /// Layer's output vector.
        /// The calculation way of layer's output vector is determined by neurons, which comprise the layer
        /// </summary>
        public double[] Output
        {
            get { return output; }
        }

        /// <summary>
        /// Compute output vector of the layer.
        /// forward
        /// </summary>
        public double[][] Compute(double[][] buffer)
        {
            //outputs
            double[][] output = new double[BufferSize][];
            //neurons
            Neurons[0] = Neurons[BufferSize - 1].Clone() as RecurrentNeuron;
            //
            for(int t = 1; t < BufferSize; t++)
            {
                vcx[t] = new double[Size_Total];
                buffer[t].CopyTo(vcx[t], 0);
                Neurons[t - 1].Output.CopyTo(vcx[t], Size_Input);
                double[] raw_vcx_state = vcx[t];
                //calcute outputs
                output[t] = Neurons[t].Compute(raw_vcx_state);
            }
            return output;
        }

        /// <summary>
        /// Randomize neurons of the layer.
        /// <summary>
        public void Randomize()
        {
            foreach (RecurrentNeuron neuron in Neurons)
                neuron.Randomize();
        }

        /// <summary>
        /// Initializes a new instance of the ActivationLayer class.
        /// The new layer is randomized after it is created.
        /// </summary>
        public RecurrentLayer(int size_input, int size_output, IActivationFunction function)
        {
            // the input count
            Size_Input = Math.Max(1, size_input);
            // create layer output neurons count
            Size_Output = Math.Max(1, size_output);
            //
            Size_Total = Size_Input + Size_Output;
            // 神经元个数，与size_output一致
            Neurons = new RecurrentNeuron[Size_Output];
            //
            vcx = new double[BufferSize][];
            // create each neuron
            for (int i = 0; i < Neurons.Length; i++)
                Neurons[i] = new RecurrentNeuron(Size_Output, Size_Total, function);
            // 神经元互通信息
            for (int i = 0; i < Neurons.Length; i++)
                Neurons[i].BrotherNeurons = Neurons;
        }

        /// <summary>
        /// Set new activation function for all neurons of the layer.
        /// </summary>
        public void SetActivationFunction(IActivationFunction function)
        {
            for (int i = 0; i < Neurons.Length; i++)
                Neurons[i].ActivationFunction = function;
        }

    }
}
