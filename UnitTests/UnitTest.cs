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

        /// <summary>
        /// 样本的label为0 -1 适合用sigmoid 函数拟合
        /// </summary>
        public void InitialSamples()
        {
            Random rand = new Random();
            //
            inputs[0] = new double[12] { 0.8, 0.9, 0.7, 0.8, 0.9, 0.7, 0.8, 0.9, 0.7, 0.8, 0.9, 0.7 };
            outputs[0] = new double[1] { 1 };
            //
            inputs[1] = new double[12] { 0.8, 0.7, 0.6, 0.8, 0.7, 0.6, 0.8, 0.7, 0.6, 0.8, 0.7, 0.6 };
            outputs[1] = new double[1] { 1 };
            //
            inputs[2] = new double[12] { 0.6, 0.9, 0.9, 0.6, 0.9, 0.9, 0.6, 0.9, 0.9, 0.6, 0.9, 0.9 };
            outputs[2] = new double[1] { 1 };
            //
            inputs[3] = new double[12] { 0.2, 0.3, 0.1, 0.2, 0.3, 0.1, 0.2, 0.3, 0.1, 0.2, 0.3, 0.1 };
            outputs[3] = new double[1] { 0 };
            //
            inputs[4] = new double[12] { 0.1, 0.4, 0.2, 0.1, 0.4, 0.2, 0.1, 0.4, 0.2, 0.1, 0.4, 0.2 };
            outputs[4] = new double[1] { 0 };
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
            ActivationNetwork network = new ActivationNetwork(activefunc, 12, 24, 10, 10, 1);
            //network.Randomize();
            BackPropagationLearning teacher = new BackPropagationLearning(network) { Momentum = 0.9, LearningRate = 0.1 };
            //train
            double loss = 1;
            for (int step = 0; step < 3000; step++)
                loss = teacher.RunEpoch(inputs, outputs);
            Assert.IsTrue(loss < 1.0);
        }
    }
}
