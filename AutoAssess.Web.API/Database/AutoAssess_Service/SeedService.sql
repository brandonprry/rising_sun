insert into client (ClientID, Name, Logopath, HasAPIAccess, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, IsActive) 
    VALUES (uuid_generate_v4(), 'BP Security', '', true, '00000000-0000-0000-0000-000000000000', now(), '00000000-0000-0000-0000-000000000000', now(), true);

insert into "user" (UserID, ClientID, Fullname, HasAPIAccess, Username, Userlevel, createdon, createdby, lastmodifiedon, lastmodifiedby, IsActive) 
    VALUES (uuid_generate_v4(),COALESCE((Select clientid from client where name = 'BP Security'),'00000000-0000-0000-0000-000000000000'), 'Administrator', true, 'Administrator', 0, now(), '00000000-0000-0000-0000-000000000000', now(), '00000000-0000-0000-0000-000000000000', true);
