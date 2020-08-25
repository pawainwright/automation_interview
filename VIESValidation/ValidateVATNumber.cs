
using System.Activities;
using System.ComponentModel;
using VIESValidation.eu.europa.ec;
using System.Text.RegularExpressions;

namespace VIESValidation

{
    public class ValidateVATNumber : CodeActivity
    {
        //Create inputs and outputs for activity
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> VATNumber { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> CompanyName { get; set; }

        [Category("Output")]
        public OutArgument<bool> ValidationResult { get; set; }

        [Category("Output")]
        public OutArgument<string> CompanyNameResponse { get; set; }

        private string FormatVATNumber(string referenceNumber)
        {
            return Regex.Replace(referenceNumber, "[^.0-9]", "");
        }

        protected override void Execute(CodeActivityContext context)
        {
            //Format VAT number
            string VATNumberToCheck = VATNumber.Get(context);
            VATNumberToCheck = FormatVATNumber(VATNumberToCheck);

            //Initialise variables for web service
            string countryCode = "GB";
            bool isValid;
            string respCompanyName, respAddress;

            //Post to VIES
            checkVatService check = new checkVatService();
            check.checkVat(ref countryCode, ref VATNumberToCheck, out isValid, out respCompanyName, out respAddress);

            //Return result
            ValidationResult.Set(context, isValid);
            CompanyNameResponse.Set(context, respCompanyName);
            return;
        }

    }
}






    
