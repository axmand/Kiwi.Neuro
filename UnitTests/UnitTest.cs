using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neuro.Activation;
using Neuro.Learning;
using Neuro.Networks;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {

        double[][] inputs = new double[5][];

        double[][] outputs = new double[5][];

        public void InitialSamples()
        {
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                inputs[i] = new double[3] { rand.NextDouble(), rand.NextDouble(), rand.NextDouble() };
                outputs[i] = new double[1] { i % 2 };
            }
        }

        /// <summary>
        /// initialization samples
        /// </summary>
        public UnitTest()
        {
            InitialSamples();
        }

        [TestMethod]
        public void DeepNeuralNetwork()
        {
            //build neural network
            IActivationFunction activefunc = new SigmoidFunction();
            ActivationNetwork network = new ActivationNetwork(activefunc, 3, 2, 1);
            network.Randomize();
            BackPropagationLearning teacher = new BackPropagationLearning(network) { Momentum = 0.9, LearningRate = 0.1};
            //train
            double loss = 1;
            for (int step = 0; step < 100; step++)
                loss = teacher.RunEpoch(inputs, outputs);
            //
            Assert.IsTrue(loss < 1.0);
        }
    }
}
