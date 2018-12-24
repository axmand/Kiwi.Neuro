using Neuro.Abstract;
using Neuro.Utils;
using System;

namespace Neuro.Neurons
{
    public class RecurrentNeuron : IRecurrentNeuron, ICloneable
    {
        /// <summary>
        /// rnn是神经元需要在每个神经元里，记录其他神经元信息
        /// </summary>
        public RecurrentNeuron[] BrotherNeurons { get; set; }

        /// <summary>
        /// Neuron's inputs count.
        /// </summary>
        public int InputsCount { get; private set; }

        /// <summary>
        /// w权重初始化长度
        /// 因为输入的长度是全内容，所以w权重初始化计算方式不一样
        /// </summary>
        public int TotalCount { get; private set; }

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
        /// warning: input = size_total
        /// </summary>
        public RecurrentNeuron(int inputsCount, int totalCount, IActivationFunction function)
        {
            // allocate weights
            InputsCount = Math.Max(1, inputsCount);
            // total count
            TotalCount = Math.Max(1, totalCount);
            // new weights
            Weights = new double[TotalCount];
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
            for (int i = 0; i < TotalCount; i++)
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
        public double[] Compute(double[] input)
        {
            Output = new double[InputsCount];
            for (int t = 0; t < InputsCount; t++)
            {
                RecurrentNeuron neuron = BrotherNeurons[t];
                double sum = neuron.Threshold;
                for (int i = 0; i < TotalCount; i++)
                    sum += neuron.Weights[i] * input[i];
                Output[t] = ActivationFunction.Function(sum);
            }
            return Output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            RecurrentNeuron neuron = new RecurrentNeuron(InputsCount, TotalCount, ActivationFunction)
            {
                Weights = Weights,
                Threshold = Threshold,
                Output = Output
            };
            return neuron;
        }

    }
}
