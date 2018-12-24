using Lexicon.Extend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neuro.Abstract;
using Neuro.Activation;
using Neuro.Layers;
using System.IO;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class Network_RecurrentNeural
    {
        /// <summary>
        /// 原始待统计数据源
        /// </summary>
        string vocabularyFullFilename = Directory.GetCurrentDirectory() + @"\TestDatasets\RawText.txt";

        [TestMethod]
        public void ReadVocabularyLibrary()
        {
            //form raw vocabulary file
            Lexicon.Lexicon lexicon = Lexicon.Lexicon.FromVocabularyFile(vocabularyFullFilename);
            Assert.IsTrue(lexicon.VocaSize == 29);
        }

        [TestMethod]
        public void RecurrentNeuralNetwork()
        {
            //form raw vocabulary file
            Lexicon.Lexicon lexicon = Lexicon.Lexicon.FromVocabularyFile(vocabularyFullFilename,Lexicon.EncodeScheme.Onehot);
            //每次学习文字的长度
            
            //
            using(StreamReader sr = new StreamReader(vocabularyFullFilename))
            {
                string rawText = "";
                while (!sr.EndOfStream)
                    rawText += sr.ReadLine().Trim().ClearPunctuation();
                //
                int pos = 0;
                int bufferSize = 24;
                //
                IActivationFunction func = new SigmoidFunction();
                RecurrentLayer layer = new RecurrentLayer(lexicon.VocaSize, 300, func);
                //
                string[] text = lexicon.Sgement(rawText);
                //
                while (pos + bufferSize < text.Length)
                {
                    var buffer = FillBuffer(pos, bufferSize, text, lexicon);
                    var pred = layer.Compute(buffer);
                }
                //
            }
        }

        private double[][] FillBuffer(int offset, int bufferSize, string[] text, Lexicon.Lexicon lexicon)
        {
            double[][] buffer = new double[bufferSize][];
            for(int pos = 1; pos < bufferSize; pos++)
            {
                buffer[pos] = new double[lexicon.VocaSize];
                buffer[pos][lexicon.DictIndex[text[pos + offset - 1]]] = 1;
            }
            return buffer;
        }

        



    }
}
