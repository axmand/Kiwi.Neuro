using Neuro.Activation;
using Neuro.Neurons;

namespace Neuro.Layers
{
    public class ActivationLayer:Layer
    {
        /// <summary>
        /// Initializes a new instance of the ActivationLayer class.
        /// The new layer is randomized after it is created.
        /// </summary>
        public ActivationLayer(int neuronsCount, int inputsCount, IActivationFunction function) : base(neuronsCount, inputsCount)
        {
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
