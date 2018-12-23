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
        public int NeuronsCount { get; } = 0;

        /// <summary>
        /// Layer's output vector.
        /// </summary>
        protected double[] output;

        /// <summary>
        /// Layer's inputs count.
        /// </summary>
        public int InputsCount { get; } = 0;

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
        public double[] Compute(string rawText)
        {
            //convert to target-context input data
            double[][] buffer = FillTextBuffer(rawText);
            //neurons
            Neurons[0] = Neurons[BufferSize - 1].Clone() as RecurrentNeuron;
            //
            for(int t = 1; t < BufferSize; t++)
            {
                buffer[t].CopyTo(vcx[t], 0);
                Neurons[t - 1].Output.CopyTo(vcx[t], InputsCount);
                double[] raw_vcx_state = vcx[t];
                //calcute outputs
            }
        
            
            
            // local variable to avoid mutlithread conflicts
            double[] output = new double[NeuronsCount];
            // compute each neuron
            for (int i = 0; i < Neurons.Length; i++)
                output[i] = Neurons[i].Compute(null);
            // assign output property as well (works correctly for single threaded usage)
            this.output = output;
            return output;
        }

        /// <summary>
        /// returen the alaysised
        /// </summary>
        /// <param name="rawText"></param>
        /// <returns></returns>
        private double[][] FillTextBuffer(string rawText)
        {
            return null;
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
        public RecurrentLayer(int neuronsCount, int inputsCount, IActivationFunction function)
        {
            // the input count
            InputsCount = Math.Max(1, inputsCount);
            // create layer output neurons count
            NeuronsCount = Math.Max(1, neuronsCount);
            // create collection of neurons
            Neurons = new RecurrentNeuron[NeuronsCount];
            //
            vcx = new double[BufferSize][];
            // create each neuron
            for (int i = 0; i < Neurons.Length; i++)
                Neurons[i] = new RecurrentNeuron(inputsCount, function);
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
