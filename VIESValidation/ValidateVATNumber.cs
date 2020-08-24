using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Activities;
using System.ComponentModel;
using System.Net;
using System.Xml;
using System.IO;
using VIESValidation.eu.europa.ec;
using System.Web.Services.Protocols;
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
        public OutArgument<string> ValidationResponse { get; set; }

        private string FormatVATNumber(string referenceNumber)
        {
            return Regex.Replace(referenceNumber, "[^.0-9]", "");
        }

        private bool compareNames(string nameProvided, string nameFound)
        {
            return nameFound.Contains(nameProvided);
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

            //Check response and return results:

            //If VAT number is invalid
            if (!isValid) 
            { 
                ValidationResult.Set(context, false);
                ValidationResponse.Set(context, "The VAT Number is invalid");
                return;
            }

            //If the number is valid but the names cannot be matched
            bool namesMatch = compareNames(CompanyName.Get(context), respCompanyName);
            if (!namesMatch)
            {
                ValidationResult.Set(context, true);
                ValidationResponse.Set(context, "The VAT Number is valid, but the names could not be matched");
                return;
            }

            //If the number is valid and the names match
            ValidationResult.Set(context, true);
            ValidationResponse.Set(context, "The VAT Number is valid, and the company name has been matched");
            return;
        }
        

    }
}






    
