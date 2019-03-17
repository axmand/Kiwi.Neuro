using Neuro.Abstract;
using Neuro.Layers;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Neuro.Networks
{
    public class ActivationNetwork
    {

        /// <summary>
        /// Network's inputs count.
        /// </summary>
        protected int inputsCount;

        /// <summary>
        /// Network's layers count.
        /// </summary>
        protected int layersCount;

        /// <summary>
        /// Network's output vector.
        /// </summary>
        protected double[] output;

        /// <summary>
        /// Network's inputs count.
        /// </summary>
        public int InputsCount { get; private set; }

        /// <summary>
        /// Network's layers.
        /// </summary>
        public ActivationLayer[] Layers { get; private set; }

        /// <summary>
        /// Network's output vector.
        /// </summary>
        public double[] Output
        {
            get { return output; }
        }

        /// <summary>
        /// Compute output vector of the network.
        /// </summary>
        public virtual double[] Compute(double[] input)
        {
            // local variable to avoid mutlithread conflicts
            double[] output = input;
            // compute each layer
            for (int i = 0; i < Layers.Length; i++)
                output = Layers[i].Compute(output);
            // assign output property as well (works correctly for single threaded usage)
            this.output = output;
            return output;
        }
        /// <summary>
        /// Randomize layers of the network.
        /// </summary>
        public virtual void Randomize()
        {
            foreach (ActivationLayer layer in Layers)
                layer.Randomize();
        }
        /// <summary>
        /// Save network to specified file.
        /// </summary>
        public void Save(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            Save(stream);
            stream.Close();
        }
        /// <summary>
        /// Save network to specified file.
        /// </summary>
        public void Save(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }
        /// <summary>
        /// Load network from specified file.
        /// </summary>
        public static ActivationNetwork Load(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            ActivationNetwork network = Load(stream);
            stream.Close();
            return network;
        }
        /// <summary>
        /// Load network from specified file.
        /// </summary>
        public static ActivationNetwork Load(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            ActivationNetwork network = (ActivationNetwork)formatter.Deserialize(stream);
            return network;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationNetwork"/> class.
        /// </summary>
        public ActivationNetwork(IActivation function, int inputsCount, params int[] neuronsCount)
        {
            this.inputsCount = Math.Max(1, inputsCount);
            layersCount = Math.Max(1, layersCount);
            //
            Layers = new ActivationLayer[neuronsCount.Length];
            // create each layer
            for (int i = 0; i < neuronsCount.Length; i++)
            {
                Layers[i] = new ActivationLayer(
                    // neurons count in the layer
                    neuronsCount[i],
                    // inputs count of the layer
                    (i == 0) ? inputsCount : neuronsCount[i - 1],
                    // activation function of the layer
                    function);
            }
        }
        /// <summary>
        /// Set new activation function for all neurons of the network.
        /// </summary>
        public void SetActivationFunction(IActivation function)
        {
            for (int i = 0; i < Layers.Length; i++)
                Layers[i].SetActivationFunction(function);
        }
    }
}
