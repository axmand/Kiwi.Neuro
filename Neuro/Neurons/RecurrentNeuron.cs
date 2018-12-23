using Neuro.Abstract;
using Neuro.Utils;
using System;

namespace Neuro.Neurons
{
    public  class RecurrentNeuron: IRecurrentNeuron,ICloneable
    {
        /// <summary>
        /// Neuron's inputs count.
        /// </summary>
        public int InputsCount { get; private set; }

        /// <summary>
        /// Neuron's output value.
        /// </summary>
        public double[] Output { get; internal set; }

        /// <summary>
        /// Neuron's weights.
        /// </summary>
        public double[] Weights { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Neuron class.
        /// </summary>
        public RecurrentNeuron(int inputs, IActivationFunction function)
        {
            // allocate weights
            InputsCount = Math.Max(1, inputs);
            // new weights
            Weights = new double[InputsCount];
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
            //init w
            for (int i = 0; i < InputsCount; i++)
                Weights[i] = NP.RandomByNormalDistribute();
            //init b
            Threshold = NP.RandomByNormalDistribute();
        }

        /// <summary>
        /// Threshold value.
        /// </summary>
        public double Threshold { get; set; }

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
            if (input.Length != InputsCount)
                throw new ArgumentException("Wrong length of the input vector.");
            // initial sum value
            double sum = 0.0;
            // compute weighted sum of inputs
            for (int i = 0; i < Weights.Length; i++)
                sum += Weights[i] * input[i];
            // compute output
            sum += Threshold;
            // local variable to avoid mutlithreaded conflicts
            double output = ActivationFunction.Function(sum);
            // assign output property as well (works correctly for single threaded usage)
            //Output = output;
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            RecurrentNeuron neuron = new RecurrentNeuron(InputsCount,ActivationFunction);
            neuron.Weights = Weights;
            neuron.Threshold = Threshold;
            neuron.Output = Output;
            return neuron;
        }

    }
}
