CREATE EXTENSION "uuid-ossp";

drop table "webuser";
create table "webuser" (
	WebUserID			uuid	PRIMARY KEY,
	ClientID		uuid,
	EmailAddress 	text,
  	UserLevel		int DEFAULT 3,
	Username		text,
	PasswordHash	text,
	IsActive		boolean
);

drop table "webuserinfo";
create table "webuserinfo" (
	WebUserInfoID	uuid PRIMARY KEY,
	WebUserID		uuid,
	LastLogin		timestamp,
	FirstName		text,
	LastName		text,
	PrimaryPhone	text,
	SecondaryPhone	text,
    CompanyName     text,
    CompanyAddress  text,
    Hosts			integer,
    Provider		text,
    MainSecurityConcern text,
    PrimaryWebsite	text,
	IsActive		boolean
);

drop table "client";
create table "client" (
	ClientID		uuid PRIMARY KEY,
	Name			text,
	HasAPIAccess	boolean,
	Logopath		text,
	CreatedBy		uuid,
	CreatedOn		timestamp,
	LastModifiedBy	uuid,
	LastmodifiedOn	timestamp,
	IsActive		boolean
);

drop table "userverification";
create table "userverification" (
	VerificationID		uuid PRIMARY KEY,
	WebUserID			uuid,
	Key					uuid,
	IsSent				boolean,
	IsVerified			boolean,
	CreatedBy			uuid,
	CreatedOn			timestamp,
	LastModifiedBy		uuid,
	LastmodifiedOn		timestamp,
	IsActive			boolean	
);

drop table "package";
create table "package" (
  PackageID          uuid PRIMARY KEY,
  Name            text,
  Cost            integer,
  AllowsRecurring boolean,
  Description     text,
  CreatedBy       uuid,
  CreatedOn       timestamp,
  LastModifiedBy  uuid,
  LastModifiedOn  timestamp,
  IsActive        boolean
);

drop table "packageconfig";
create table "packageconfig" (
	PackageConfigID		uuid PRIMARY KEY,
	PackageID		uuid,
	IsCustomizable		boolean,
	WebTools		integer,
	VulnAssessments		integer,	
	CreatedBy		uuid,
	CreatedOn		timestamp,
	LastModifiedBy		uuid,
	LastModifiedOn		timestamp,
	IsActive		boolean
);
