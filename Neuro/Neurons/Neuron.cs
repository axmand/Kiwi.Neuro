using Neuro.Utils;
using System;

namespace Neuro.Neurons
{
    /// <summary>
    /// reference:
    /// https://github.com/andrewkirillov/AForge.NET/blob/master/Sources/Neuro/Neurons/Neuron.cs
    /// 神经元对象，包含三个内容：
    /// 1. input
    /// 2.weight
    /// 3.output
    /// </summary>
    public abstract class Neuron
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
        protected Neuron(int inputs)
        {
            // allocate weights
            inputsCount = Math.Max(1, inputs);
            weights = new double[inputsCount];
            // randomize the neuron
            Randomize();
        }

        /// <summary>
        /// Randomize neuron.
        /// </summary>
        public virtual void Randomize()
        {
            // randomize weights
            for (int i = 0; i < inputsCount; i++)
                weights[i] = NP.RandomByNormalDistribute();
        }

        /// <summary>
        /// Computes output value of neuron.
        /// </summary>
        public abstract double Compute(double[] input);
    }
}
