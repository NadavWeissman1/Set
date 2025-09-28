using Set.Models;
namespace Set.ModelsLogic
{
    internal class LoginWithInfo:LoginWithInfoModel
    {
        public override string IsLoginInfo(bool IsLoginInfo)
        {
            if (IsLoginInfo)
            {
                return Strings.LoginWithInfoPrompt;
            }
            else
            { 
                 return Strings.LoginWithoutInfoPrompt;
            }
        }
    }
}
