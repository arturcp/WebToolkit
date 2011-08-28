using Lucene.Net.Analysis;

namespace WebToolkit.Index
{
    class AccentedAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, System.IO.TextReader reader)
        {
            TokenStream tokenizer = new LowerCaseTokenizer(reader);
            tokenizer = new ASCIIFoldingFilter(tokenizer);
            return tokenizer;
        }

        public override TokenStream ReusableTokenStream(string fieldName, System.IO.TextReader reader)
        {
            return TokenStream(fieldName, reader);
        }
    }
}
