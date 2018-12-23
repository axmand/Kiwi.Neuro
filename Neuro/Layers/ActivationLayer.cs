using Neuro.Abstract;
using Neuro.Neurons;
using System;

namespace Neuro.Layers
{
    public class ActivationLayer : IActivationLayer
    {
        /// <summary>
        /// Layer's neurons count.
        /// </summary>
        public int NeuronsCount { get; } = 0;

        /// <summary>
        /// Layer's neurons.
        /// </summary>
        protected ActivationNeuron[] neurons;

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
        public ActivationNeuron[] Neurons
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
        /// Compute output vector of the layer.
        /// </summary>
        public virtual double[] Compute(double[] input)
        {
            // local variable to avoid mutlithread conflicts
            double[] output = new double[NeuronsCount];
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
            foreach (ActivationNeuron neuron in neurons)
                neuron.Randomize();
        }

        /// <summary>
        /// Initializes a new instance of the ActivationLayer class.
        /// The new layer is randomized after it is created.
        /// </summary>
        public ActivationLayer(int neuronsCount, int inputsCount, IActivationFunction function)
        {
            // the input count
            InputsCount = Math.Max(1, inputsCount);
            // create layer output neurons count
            NeuronsCount = Math.Max(1, neuronsCount);
            // create collection of neurons
            neurons = new ActivationNeuron[NeuronsCount];
            // create each neuron
            for (int i = 0; i < neurons.Length; i++)
                neurons[i] = new ActivationNeuron(inputsCount, function);
        }

        /// <summary>
        /// Set new activation function for all neurons of the layer.
        /// </summary>
        public void SetActivationFunction(IActivationFunction function)
        {
            for (int i = 0; i < neurons.Length; i++)
                ((ActivationNeuron)neurons[i]).ActivationFunction = function;
        }

    }
}
