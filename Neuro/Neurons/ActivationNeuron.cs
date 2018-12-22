using Neuro.Activation;
using Neuro.Utils;
using System;

namespace Neuro.Neurons
{
    public class ActivationNeuron : Neuron
    {
        /// <summary>
        /// Threshold value.
        /// </summary>
        protected double threshold = 0.0;

        /// <summary>
        /// Activation function.
        /// </summary>
        protected IActivationFunction function = null;

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
        public IActivationFunction ActivationFunction
        {
            get { return function; }
            set { function = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationNeuron"/> class.
        /// </summary>
        public ActivationNeuron(int inputs, IActivationFunction function): base(inputs)
        {
            this.function = function;
        }

        /// <summary>
        /// Randomize neuron.
        /// </summary>
        public override void Randomize()
        {
            // randomize weights
            base.Randomize();
            // randomize threshold
            threshold = NP.RandomByNormalDistribute();
        }

        /// <summary>
        /// Computes output value of neuron.
        /// </summary>
        public override double Compute(double[] input)
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
            double output = function.Function(sum);
            // assign output property as well (works correctly for single threaded usage)
            this.output = output;
            return output;
        }
    }
}
