using System;
using System.Activities;
using System.ComponentModel;


namespace ColorCodeRow
{
    public class ColorCodeRow : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<bool> VATisValid { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<bool> NameMatched { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<double> DistanceScore { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<int> SimilarityThreshold { get; set; }

        [Category("Output")]
        public OutArgument<string> RowColour { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            //If VAT number is invalid, row is coloured red
            if (!VATisValid.Get(context))
            {
                RowColour.Set(context, "IndianRed");
                return;
            }
            //If VAT is valid and name matched, code green
            else if (NameMatched.Get(context))
            {
                RowColour.Set(context, "YellowGreen");
                return;
            }
            //If VAT is valid and name was not matched, but similarity is over the threshold, code yellow
            else if (DistanceScore.Get(context) >= SimilarityThreshold.Get(context))
            {
                RowColour.Set(context, "Gold");
                return;
            }
            else
            {
                //Otherwise, highlight in red, since above checks all failed
                RowColour.Set(context, "IndianRed");
                return;
            }
        }

    }
}
