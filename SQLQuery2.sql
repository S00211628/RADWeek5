Select Username, name from AspNetUsers, AspNetUserRoles, AspNetRoles
where aspnetusers.Id = AspNetUserRoles.UserId	
and AspNetUserRoles.RoleId = AspNetRoles.Id