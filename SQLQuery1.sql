select UserName, Name from AspNetUsers, AspNetUserRoles, AspNetRoles
where AspNetUsers.Id = AspNetUserRoles.UserId
and AspNetUserRoles.RoleId = AspNetRoles.Id