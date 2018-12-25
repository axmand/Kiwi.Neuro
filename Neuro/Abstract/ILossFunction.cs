using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro.Loss
{
    public interface ILossFunction
    {
        /// <summary>
        /// 适合RNN的loss计算方式
        /// </summary>
        /// <param name="probs"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        double[][] CalculateError(double[][] probs, double[][] targets);
    }
}
