namespace GLOB.Domain.Model.Auth.Enumz;

public enum UserTypeEnum
{
  Admin = 0,
  Manager = 1,
  Employee = 2,
  Support = 3,
  Creator = 4,
  UserStandard = 5,
  UserBusiness = 6,
  UserCreator = 7

}

[Flags]
public enum Permission
{
  None = 0,
  Add = 1 << 0,  // 1
  View = 1 << 1,  // 2
  Update = 1 << 2,  // 4
  Delete = 1 << 3,  // 8
  ChangeStatus = 1 << 4, // 16
  Export = 1 << 5, // 32
  Import = 1 << 6, // 64
  FullAccess = Add | View | Update | Delete | ChangeStatus | Export | Import // 127
}
