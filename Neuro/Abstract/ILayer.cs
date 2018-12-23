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
        ActivationNeuron[] Neurons { get; }
        int InputsCount {get;}
    }

}
