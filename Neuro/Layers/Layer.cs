using Neuro.Neurons;
using System;

namespace Neuro.Layers
{
    public abstract class Layer
    {
        /// <summary>
        /// Layer's inputs count.
        /// </summary>
        protected int inputsCount = 0;

        /// <summary>
        /// Layer's neurons count.
        /// </summary>
        protected int neuronsCount = 0;

        /// <summary>
        /// Layer's neurons.
        /// </summary>
        protected Neuron[] neurons;

        /// <summary>
        /// Layer's output vector.
        /// </summary>
        protected double[] output;

        /// <summary>
        /// Layer's inputs count.
        /// </summary>
        public int InputsCount
        {
            get { return inputsCount; }
        }

        /// <summary>
        /// Layer's neurons.
        /// </summary>
        public Neuron[] Neurons
        {
            get { return neurons; }
        }

        /// <summary>
        /// Layer's output vector.
        /// The calculation way of layer's output vector is determined by neurons, which comprise the layer
        /// </summary>
        public double[] Output
        {
            get { return output; }
        }

        /// <summary>
        ///  Initializes a new instance of the Layer
        /// </summary>
        protected Layer(int neuronsCount, int inputsCount)
        {
            // the input count
            this.inputsCount = Math.Max(1, inputsCount);
            // create layer output neurons count
            this.neuronsCount = Math.Max(1, neuronsCount);
            // create collection of neurons
            neurons = new Neuron[this.neuronsCount];
        }

        /// <summary>
        /// Compute output vector of the layer.
        /// </summary>
        public virtual double[] Compute(double[] input)
        {
            // local variable to avoid mutlithread conflicts
            double[] output = new double[neuronsCount];
            // compute each neuron
            for (int i = 0; i < neurons.Length; i++)
                output[i] = neurons[i].Compute(input);
            // assign output property as well (works correctly for single threaded usage)
            this.output = output;
            return output;
        }

        /// <summary>
        /// Randomize neurons of the layer.
        /// <summary>
        public virtual void Randomize()
        {
            foreach (Neuron neuron in neurons)
                neuron.Randomize();
        }
    }
}
