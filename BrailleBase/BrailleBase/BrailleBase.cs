using BrailleBase;

namespace BrailleBase.BrailleBase
{
    internal class BrailleBase
    {
        private Dictionary<string, List<string>> _letter_brailles = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> _letter_special_braille_rules_simbol = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> _letter_special_braille_rules_uppercase = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> _letter_special_braille_rules_CJK = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> _letter_special_braille_rules_RTL = new Dictionary<string, List<string>>();


        public enum BrailleRuleType
        {
            Default = 0,
            Uppercase = 1,
            CJK = 2,
            RTL = 3,
            Symbol = 4
        }

        public BrailleBase() { }


        // 0001-AA
        public void AppendBrailleLetter(string letter, List<string> brailleList, BrailleRuleType type = BrailleRuleType.Default)
        {
            /*
                0001-AA

                Registers a letter and its associated braille list.
                If the letter already exists, its mapping is overwritten.

                default: 0
                rules_uppercase: 1
                CJK: 2
                RTL: 3
                simbol: 4
            */

            if (string.IsNullOrEmpty(letter))
                throw new ArgumentException("letter cannot be null or empty", nameof(letter));

            ValidateBrailleList(brailleList);

            _letter_brailles[letter] = brailleList;

            switch (type)
            {
                case BrailleRuleType.Uppercase:
                    _letter_special_braille_rules_uppercase[letter] = brailleList;
                    break;

                case BrailleRuleType.CJK:
                    _letter_special_braille_rules_CJK[letter] = brailleList;
                    break;

                case BrailleRuleType.RTL:
                    _letter_special_braille_rules_RTL[letter] = brailleList;
                    break;

                case BrailleRuleType.Symbol:
                    _letter_special_braille_rules_simbol[letter] = brailleList;
                    break;
            }
        }


        private void ValidateBrailleList(List<string> brailleList)
        {
            if (brailleList == null)
                throw new ArgumentNullException(nameof(brailleList));

            foreach (string b in brailleList)
            {
                if (b == null)
                    throw new ArgumentException("each braille item must be a string");

                if (b.Length != 1)
                    throw new ArgumentException(
                        "each braille item must contain exactly one character");

                if (b[0] < '\u2800' || b[0] > '\u283F')
                    throw new ArgumentException($"invalid braille character: {b}");
            }
        }


    }
}
