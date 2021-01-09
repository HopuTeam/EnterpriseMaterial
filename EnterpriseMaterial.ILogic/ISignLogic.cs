namespace EnterpriseMaterial.ILogic
{
    public interface ISignLogic
    {
        Model.User GetSign(Model.Sign sign);
        Model.Sign GetAccount(string Account, int ID = 0);
        bool GetRegister(Dto.UserDto.UserOut info);
        bool EditPassword(Model.Sign sign);
    }
}