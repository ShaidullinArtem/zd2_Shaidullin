using System.Text.RegularExpressions;

namespace zd2_Shaidullin.utils
{
    public class Validator
    {
        public ErrorStruct RegexValidator(Regex regex, string value, string errorMessage)
        {
            ErrorStruct response = new ErrorStruct();
            if (!regex.IsMatch(value))
            {
                response.IsValid = false;
                response.Message = errorMessage;
                return response;

            }

            response.IsValid = true;
            response.Message = "Success";
            return response;
        }
    }
}