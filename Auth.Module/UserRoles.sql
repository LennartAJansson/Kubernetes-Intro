SELECT ur.UserId, u.UserName, u.Email, u.PhoneNumber, ur.RoleId, r.Name 
FROM authdb.aspnetusers AS u
JOIN authdb.aspnetuserroles AS ur ON u.Id = ur.UserId
JOIN authdb.aspnetroles AS r ON ur.RoleId = r.Id;