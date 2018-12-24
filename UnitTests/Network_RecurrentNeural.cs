using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Network_RecurrentNeural
    {
        double[][] inputs = new double[5][];

        double[][] outputs = new double[5][];

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

    }
}
