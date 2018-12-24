using System.Collections.Generic;

namespace Lexicon
{
    public class OnehotEncode
    {
        Lexicon _lexicon;

        public int VocaSize { get; }

        public Vocabulary[] VocaArray { get; }

        int MaxCodeLength;

        public OnehotEncode(Lexicon lexicon)
        {
            _lexicon = lexicon;
            VocaSize = lexicon.VocaSize;
            VocaArray = lexicon.VocaArray;
        }

        /// <summary>
        /// 更新onehot编码
        /// </summary>
        public void BuildOrUpdate()
        {
            for(int i=0;i<VocaSize;i++)
            {
                VocaArray[i].Point = new int[VocaSize];
                VocaArray[i].Point[i] = 1;
                _lexicon.DictIndex[VocaArray[i].Word] = i;
            }
        }
    }
}
