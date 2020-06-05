namespace Proplex.Core.Evaluator
{
    public struct TextSpan
    {
        public int Start
        {
            get;
        }

        public int Length
        {
            get;
        }

        public int End => this.Start + this.Length;

        public TextSpan(int start, int length)
        {
            this.Start = start;
            this.Length = length;
        }
    }
}