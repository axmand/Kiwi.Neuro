using Neuro.Abstract;
using Neuro.Utils;
using System;

namespace Neuro.Neurons
{
    public class ActivationNeuron
    {

        /// <summary>
        /// Neuron's inputs count.
        /// </summary>
        protected int inputsCount = 0;

        /// <summary>
        /// Nouron's wieghts.
        /// </summary>
        protected double[] weights = null;

        /// <summary>
        /// Neuron's output value.
        /// </summary>
        protected double output = 0;

        /// <summary>
        /// Neuron's inputs count.
        /// </summary>
        public int InputsCount
        {
            get { return inputsCount; }
        }

        /// <summary>
        /// Neuron's output value.
        /// </summary>
        public double Output
        {
            get { return output; }
        }

        /// <summary>
        /// Neuron's weights.
        /// </summary>
        public double[] Weights
        {
            get { return weights; }
        }

        /// <summary>
        /// Initializes a new instance of the Neuron class.
        /// </summary>
        public ActivationNeuron(int inputs,IActivationFunction function)
        {
            // allocate weights
            inputsCount = Math.Max(1, inputs);
            weights = new double[inputsCount];
            //set actviation function
            ActivationFunction = function;
            // randomize the neuron
            Randomize();
        }

        /// <summary>
        /// Randomize neuron.
        /// </summary>
        public void Randomize()
        {
            // randomize weights
            for (int i = 0; i < inputsCount; i++)
                weights[i] = NP.RandomByNormalDistribute();
            //
            threshold = NP.RandomByNormalDistribute();
        }

        /// <summary>
        /// Threshold value.
        /// </summary>
        protected double threshold = 0.0;

        /// <summary>
        /// Threshold value.
        /// </summary>
        public double Threshold
        {
            get { return threshold; }
            set { threshold = value; }
        }

        /// <summary>
        /// Neuron's activation function.
        /// </summary>
        public IActivationFunction ActivationFunction { get; set; }

        /// <summary>
        /// Computes output value of neuron.
        /// </summary>
        public double Compute(double[] input)
        {
            // check for corrent input vector
            if (input.Length != inputsCount)
                throw new ArgumentException("Wrong length of the input vector.");
            // initial sum value
            double sum = 0.0;
            // compute weighted sum of inputs
            for (int i = 0; i < weights.Length; i++)
                sum += weights[i] * input[i];
            // compute output
            sum += threshold;
            // local variable to avoid mutlithreaded conflicts
            double output = ActivationFunction.Function(sum);
            // assign output property as well (works correctly for single threaded usage)
            this.output = output;
            return output;
        }

    }
}
