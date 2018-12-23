using Neuro.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro.Abstract
{
    public interface ILayer
    {
    }

    public interface IActivationLayer : ILayer
    {
        /// <summary>
        /// neurons collection
        /// </summary>
        ActivationNeuron[] Neurons { get; }

        /// <summary>
        /// input vector length
        /// </summary>
        int InputsCount {get;}
    }

}
