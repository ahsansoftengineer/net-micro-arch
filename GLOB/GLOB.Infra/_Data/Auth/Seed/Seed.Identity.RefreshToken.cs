// using GLOB.Domain.Model.Auth;
// // using GLOB.Infra.Data.Auth;
// 

// namespace GLOB.Infra.Data.Auth;
// public static partial class InfraSeedIdentity
// {
//   public static void SeedRefreshToken(this ModelBuilder mb)
//   {
//     mb.Entity<RefreshToken>().HasData(SeedDataRefreshToken<RefreshToken>());
//   }
//   public static void SeedRefreshToken(this DBCtxInfraIdentity context)
//   {
//     if (!context.RefreshTokens.Any(x => x.Id > 0))
//     {
//       context.RefreshTokens.AddRange(SeedDataRefreshToken<RefreshToken>());
//       context.SaveChanges();
//     }
//   }
//   public static List<T> SeedDataRefreshToken<T>() where T : RefreshToken, new()
//   {
//     string className = typeof(T).Name;
//     List<T> list = new List<T>();
//     var guid = new []
//     {
//       "22c74fbc-9b0d-4848-85db-f09d58750006",
//       "46eb923d-8529-4b77-b311-96e98ea6ea06",
//       "8118fea8-a644-4d67-9eca-1d689465a1bf"
//     };

//     for (int i = 1; i <= 3; i++)
//     {
//       string name = $"{className}_{i}";
//       var data = new T()
//       {

//         Id = i,
//         Name = name,
//         CreatedByIp = " 111.88.109.5",
//         ExpiresAt = DateTime.Now.AddDays(7),
//         IsRevoked = false,
//         Token = "",
//         InfraUserId = guid[i-1],
//         Status = Domain.Enums.Status.None
//         // Status = if(ExpiresAt < DateTime.Now)
//       };
//       list.Add(data);
//     }
//     return list;
//   }
// }