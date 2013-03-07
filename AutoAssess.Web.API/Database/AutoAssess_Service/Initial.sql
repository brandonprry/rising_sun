CREATE EXTENSION "uuid-ossp";

drop table client;
CREATE TABLE Client (
	ClientID		uuid PRIMARY KEY,
	Name			text,
	LogoPath		text,
	HasAPIAccess	boolean,
	CreatedOn		timestamp,
	CreatedBy		uuid,
	LastModifiedOn	timestamp,
	LastModifiedBy	uuid,
	IsActive		boolean
);

drop table "user";
CREATE TABLE "user" (
	UserID			uuid PRIMARY KEY,
	ClientID		uuid,
	Fullname		text,
	HasAPIAccess	boolean,
	Username		text,
	UserLevel		int,
	CreatedOn		timestamp,
	CreatedBy		uuid,
	LastModifiedOn	timestamp,
	LastModifiedBy	uuid,
	IsActive		boolean
);

drop table profile;
create table Profile (
	ProfileID			uuid PRIMARY KEY,
	UserID				uuid,
	WebUserID			uuid,
	ClientID			uuid,
	CurrentResultsID	uuid,
	BadgeState			text,
	Name				text,
	Description			text,
	Domain				text,
	Range				text,
	HasRun				boolean,
	Duration			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table profilehost;
create table ProfileHost (
	ProfileHostID		uuid primary key,
	ProfileID			uuid,
	IPv4Address			text,
	Name				text,
	VerifiedOn			timestamp,
	IsVerified			boolean,
	VerifiedByFile		boolean,
	VerifiedByWhois		boolean,
	WasManuallyVerified boolean,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table profilehostverification;
create table ProfilehostVerification (
	ProfileHostVerificationID	uuid PRIMARY KEY,
	ProfileHostID				uuid,
	WhoisRegex				text,
	VerificationFileName	text,
	VerificationData		text,
    VerificationError   	text,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
);

drop table scan;
create table Scan (
	ScanID					uuid PRIMARY KEY,
	ProfileID				uuid,
	ScanOptionsID			uuid,
	UserID					uuid,
	NessusScanID			uuid,
	NexposeSiteID			text,
	OpenVASReportID			text,
	MetasploitReportTaskID	text,
	Name					text,
	HasRun					boolean,
    Duration        text,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
);

drop table scanoptions;
create table ScanOptions (
	ScanOptionsID			uuid PRIMARY KEY,
	ParentScanID			uuid,
	SQLMapOptionsID			uuid,
	UserID					uuid,
    IsBruteforce    boolean,
	IsSQLMap				boolean,
    IsDSXS          boolean,
    IsOpenVASAssessment boolean,
    IsNessusAssessment boolean,
    IsNexposeAssessment boolean,
    IsMetasploitAssessment boolean,
    MetasploitDiscovers boolean,
    MetasploitBruteforces boolean,
	RemoteNessusPolicyID	int,
	RemoteOpenVASConfigID	text,
	RemoteNexposeSiteID		int,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
	
);
		
drop table sqlmapoptions;
create table SQLMapOptions (
	SQLMapOptionsID		uuid PRIMARY KEY,
	ScanOptionsID		uuid,
	Risk				int,
	Level				int,
	CrawlLevel			int,
	TestForms			boolean,
	Threads				int,
	URL					varchar(256),
	Port				int,
	DBMS				varchar(128),
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table tracerouteresult;
create table TracerouteResult (
	TracerouteResultID 	uuid PRIMARY KEY,
	ProfileID			uuid,
	NMapHostID			uuid,
	UserID					uuid,
	FullOutput			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table tracerouteroute;
create table TracerouteRoute (
	TracerouteRouteID 		uuid PRIMARY KEY,
	TracerouteResultID		uuid,
	ProfileID				uuid,
	UserID					uuid,
	Hop						int,
	FirstHostname			text,
	SecondHostname			varchar(128),
	ThirdHostname			varchar(128),
	FirstIPAddress			varchar(128),
	SecondIPAddress			varchar(128),
	ThirdIPAddress			varchar(128),
	FirstResult				varchar(128),
	SecondResult			varchar(128),
	ThirdResult				varchar(128),
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
);

drop table whoisresult;
create table WhoisResult (
	WhoisResultID 	uuid PRIMARY KEY,
	ProfileID		uuid,
	NMapHostID		uuid,
	UserID					uuid,
	FullOutput		text,
	CreatedOn		timestamp,
	CreatedBy		uuid,
	LastModifiedOn	timestamp,
	LastModifiedBy	uuid,
	IsActive		boolean
);

drop table sslscanresult;
create table SSLScanResult (
	SSLScanResultID 	uuid PRIMARY KEY,
	ProfileID			uuid,
	HostPortID			uuid,
	UserID				uuid,
	FullOutput			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table niktoresult;
create table NiktoResult (
	NiktoResultID 	uuid PRIMARY KEY,
	ProfileID			uuid,
	FullOutput			text,
	HostPortID			uuid,
	UserID				uuid,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table niktoitem;
create table NiktoItem (
	NiktoItemID 	uuid PRIMARY KEY,
	NiktoResultID	uuid,
	Data			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table nmapresult;
create table NmapResult (
	NmapResultID 	uuid PRIMARY KEY,
	ProfileID		uuid,
	UserID			uuid,
	FullOutput		text,
	CreatedOn		timestamp,
	CreatedBy		uuid,
	LastModifiedOn	timestamp,
	LastModifiedBy	uuid,
	IsActive		boolean
);

drop table nmaphost;
create table NmapHost (
	NmapHostID		uuid PRIMARY KEY,
	ProfileHostID	uuid,
	NmapResultID 	uuid,
	ProfileID		uuid,
	UserID			uuid,
	Hostname		text,
	IPAddressv4		text,
	IPAddressv6		text,
	MAC				text,
	DeviceType		text,
	OS				text,
	OS_Details		text,
	NetworkDistance text,
	CreatedOn		timestamp,
	CreatedBy		uuid,
	LastModifiedOn	timestamp,
	LastModifiedBy	uuid,
	IsActive		boolean
);

drop table hostport;
create table HostPort (
	HostPortID			uuid PRIMARY KEY,
	NmapHostID			uuid,
	UserID				uuid,
	parentprofileid		uuid,
	parentscanid		uuid,
	PortNumber			int,
	IsUDP				boolean,
	DeepScan			text,
	Service				text,
	HydraServiceName	text,
	State				text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table smbresult;
create table SMBResult (
	SMBResultID			uuid PRIMARY KEY,
	HostPortID			uuid,
	ProfileID			uuid,
	ScanID				uuid,
	UserID				uuid,
	FullOutput			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table smbresultchild;
create table SMBResultChild (
	SMBResultChildID	uuid PRIMARY KEY,
	SMBResultID			uuid,
	FileList			text,
	Sharename			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table sqlmapresult;
create table SQLmapResult (
	SQLMapResultID		uuid PRIMARY KEY,
	HostPortID			uuid,
	ScanID				uuid,
	UserID				uuid,
	Fulloutput			text,
	Log				text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
	
);

drop table sqlmapresultchildren;
create table SQLMapResultChildren (
	SQLMapResultChildID		uuid PRIMARY KEY,
	SQLMapResultID			uuid,
	URL						text,
	HTTPRequestType			text,
	Parameter				text,
	PayloadType				text,
	Payload					text,
	Title					text,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
);

drop table nessusscan;
create table NessusScan (
	NessusScanID			uuid PRIMARY KEY,
	RemoteScanID			text,
	ParentScanID			uuid,
	NessusReportID			uuid,
	Name					text,
	Owner					text,
	Range					text,
	UniqueReportNo			int,
	StartTime				timestamp,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
);

drop table nessusreporthost;
create table nessusreporthost (
	nessusreporthostid		uuid PRIMARY KEY,
	nessushostpropertiesid  uuid,
	nessusscanid		uuid,
	name				text,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
	
);

drop table nessushostproperties;
create table nessushostproperties (
	nessushostpropertiesid		uuid PRIMARY KEY,
	nessusreporthostid		uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby			uuid,
	lastmodifiedon			timestamp,
	isactive			boolean,
	hostbegin			text,
	hostend				text,
	hostfqdn			text,
	hostip				text,
	operatingsystem			text,
	systemtype			text
);

drop table nessusreportitem;
create table nessusreportitem (
	nessusreportitemid 		uuid PRIMARY KEY,
	nessusreporthostid		uuid,
	bid				text,
	cert				text,
	cpe				text,
	cve				text,
	cvssbasescore			text,
	cvsstemporalvector		text,
	cvsstemporalscore		text,
	cvssvector			text,
	description			text,
	edbid				text,
	exploitease			text,
	exploitincore			boolean,
	exploitinmetasploit		boolean,
	exploitisavailable		boolean,
	filename			text,
	iava				text,
	metasploitname			text,
	osvdb				text,
	patchpublicationdate		text,
	pluginfamily			text,
	pluginid			text,
	pluginmodificationdate		text,
	pluginname			text,
	pluginoutput			text,
	pluginpublicationdate		text,
	plugintype			text,
	port				text,
	protocol			text,
	riskfactor			text,
	seealso				text,
	servicename			text,
	severity			text,
	solution			text,
	synopsis			text,
	vulnerabilitypublicationdate    text,
	xref				text,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
);

drop table nessusreport;
create table NessusReport (
	NessusReportID			uuid PRIMARY KEY,
	RemoteReportID			text,
	ParentNessusScanID		uuid,
	ReadableName			text,
	Status					text,
	FullReport				text,
	ReportType				text,
	TS						timestamp,
	Report					text,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
);

drop table metasploitscan;
create table metasploitscan (
	metasploitscanid		uuid PRIMARY KEY,
	scanid					uuid,
        createdon                               timestamp,
        createdby                               uuid,
        lastmodifiedon                  timestamp,
        lastmodifiedby                  uuid,
        isactive                                boolean

);

drop table metasploitmodulereference;
create table metasploitmodulereference (
	metasploitmodulereferenceid		uuid PRIMARY KEY,
	metasploitmoduledetailsid		uuid,
	createdby						uuid,
	createdon						timestamp,
	lastmodifiedby					uuid,
	lastmodifiedon					timestamp,
	isactive						boolean,
	name 							text,
	remoteid						integer,
	remotemoduledetailid			integer
);

drop table metasploitwebsite;
create table metasploitwebsite (
	metasploitwebsiteid			uuid PRIMARY KEY,
	metasploitscanid			uuid,
	createdby					uuid,
	createdon					timestamp,
	lastmodifiedby				uuid,
	lastmodifiedon				timestamp,
	isactive					boolean,
	host						text,
	options						text,
	port						integer,
	remotecreatedat				text,
	remoteid					integer,
	remoteserviceid				integer,
	remoteupdatedat				text,
	virtualhost					text
);

drop table metasploitservice;
create table metasploitservice (
	metasploitserviceid		uuid PRIMARY KEY,
	metasploithostid		uuid,
	createdby				uuid,
	createdon				timestamp,
	lastmodifiedby			uuid,
	lastmodifiedon			timestamp,
	isactive				boolean,
	info					text,
	name					text,
	protocol				text,
	remotecreatedat			text,
	remotehostid			integer,
	remoteid				integer,
	remoteupdatedat			text,
	state					text
);

drop table metasploitmoduleauthor;
create table metasploitmoduleauthor (
	metasploitmoduleauthorid		uuid PRIMARY KEY,
	metasploitmoduledetailsid		uuid,
	createdby						uuid,
	createdon						timestamp,
	lastmodifiedby					uuid,
	lastmodifiedon					timestamp,
	isactive 						boolean,
	name							text,
	email							text
);

drop table metasploitmoduledetails;
create table metasploitmoduledetails (
	metasploitmoduledetailsid		uuid PRIMARY KEY,
	metasploitscanid				uuid,
	createdby						uuid,
	createdon						timestamp,
	lastmodifiedby					uuid,
	lastmodifiedon					timestamp,
	isactive						boolean,
	defaulttarget					integer,
	description						text,
	disclosuredate					text,
	file							text,
	fullname						text,
	license							text,
	moduletype						text,
	name							text,
	priviledged						boolean,
	rank							text,
	ready							text,
	referencename					text,
	remoteid						integer,
	stance							text
);

drop table metasploitexploitsession;
create table metasploitexploitsession (
	metasploitexploitsessionid		uuid PRIMARY KEY,
	metasploithostid				uuid,
	createdby						uuid,
	createdon						timestamp,
	lastmodifiedby					uuid,
	lastmodifiedon					timestamp,
	isactive						boolean,
	closedat						text,
	closedreason					text,
	description						text,
	encodeddatastore				text,
	hostid							text,
	lastseen						text,
	localid							text,
	openedat						text,
	platform						text,
	port							text,
	sessiontype						text,
	viaexploit						text,
	viapayload						text
);

drop table metasploitvulnerabilityreference;
create table metasploitvulnerabilityreference (
	metasploitvulnerabilityreferenceid		uuid PRIMARY KEY,
	metasploitvulnerabilityid				uuid,
	reference								text,
	createdby								uuid,
	createdon								timestamp,
	lastmodifiedby							uuid,
	lastmodifiedon							timestamp,
	isactive								boolean
);

drop table metasploitvulnerability;
create table metasploitvulnerability (
	metasploitvulnerabilityid		uuid PRIMARY KEY,
	metasploithostid				uuid,
	createdby						uuid,
	createdon						timestamp,
	lastmodifiedby					uuid,
	lastmodifiedon					timestamp,
	isactive						boolean,
	name							text,
	remotecreatedat					text,
	info							text,
	details							text,
	exploitedat						text,
	remotehostid					integer,
	remoteid						integer,
	remoteserviceid					integer,
	remoteupdatedat					text
);

drop table metasploitnote;
create table metasploitnote (
	metasploitnoteid	uuid PRIMARY KEY,
	metasploithostid	uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby		uuid,
	lastmodifiedon		timestamp,
	isactive			boolean,
	data				text,
	notetype			text,
	remotecreatedat		text,
	remotehostid		integer,
	remoteid			integer,
	remoteserviceid		integer,
	remoteupdatedat		text,
	remoteworkspaceid	integer
);

drop table metasploitexploitattempt;
create table metasploitexploitattempt (
	metasploitexploitattemptid		uuid PRIMARY KEY,
	metasploithostid				uuid,
	createdby						uuid,
	createdon						timestamp,
	lastmodifiedby					uuid,
	lastmodifiedon					timestamp,
	isactive						boolean,
	attemptedat						text,
	faildetail						text,
	failreason						text,
	module							text,
	port							integer,
	protocol						text,
	remotehostid					integer,
	remoteid						integer,
	remotelootid					integer,
	remoteserviceid					integer,
	remotesessionid					integer,
	remotevulnerabilityid			integer,
	username						text
);

drop table metasploitvulnerabilitydetail;
create table metasploitvulnerabilitydetail (
	metasploitvulnerabilitydetailid 		uuid PRIMARY KEY,
	metasploitvulnerabilityid				uuid,
	createdby								uuid,
	createdon								timestamp,
	lastmodifiedby							uuid,
	lastmodifiedon							timestamp,
	isactive								boolean,
	cvssscore								decimal,
	cvssvector								text,
	description								text,
	nexposeaddeddate						text,
	nexposeconsoleid						integer,
	nexposedeviceid							integer,
	nexposemodifieddate						text,
	nexposepcicompliancestatus				text,
	nexposepciseverity						decimal,
	nexposeproofkey							text,
	nexposepublisheddate					text,
	nexposescanid							integer,
	nexposeseverity							decimal,
	nexposetags								text,
	nexposevulnerabilityid					text,
	nexposevulnerablesince					text,
	nexposevulnerabilitystatus				text,
	proof									text,
	remoteid								integer,
	remotevulnerabilityid					integer,
	solution								text,
	source									text,
	title									text
);

drop table metasploithostdetail;
create table metasploithostdetail (
	metasploithostdetailid			uuid PRIMARY KEY,
	metasploithostid				uuid,
	createdby						uuid,
	createdon						timestamp,
	lastmodifiedby					uuid,
	lastmodifiedon					timestamp,
	isactive						boolean,
	nexposeconsoleid				integer,
	nexposedeviceid					integer,
	nexposeriskscore				decimal,
	nexposescantemplate				text,
	nexposesiteimportance			text,
	nexposesitename					text,
	remotehostid					integer,
	remoteid						integer,
	source							text
);

drop table metasploithost;
create table metasploithost (
	metasploithostid	uuid PRIMARY KEY,
	metasploitscanid	uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby		uuid,
	lastmodifiedon		timestamp,
	isactive			boolean,
	address				text,
	comm				text,
	exploitattemptcount integer,
	hostdetailscount	integer,
	info 				text,
	mac					text,
	name				text,
	notecount			integer,
	osarchitecture		text,
	osflavor			text,
	oslanguage			text,
	osname				text,
	osservicepack		text,
	purpose				text,
	remotecreatedat		text,
	remoteid			integer,
	remoteupdatedat		text,
	remoteworkspaceid	integer,
	servicecount		integer,
	state				text,
	vulncount			integer
);

drop table metasploitcredential;
create table metasploitcredential (
		metasploitcredentialid 			uuid PRIMARY KEY,
		metasploithostid	 			uuid,
		metasploitscanid	 			uuid,
		port		 					integer,
		servicename		 				text,
		remotecreatedat	 				text,
		remoteupdatedat	 				text,
		username	 					text,
		password	 					text,
		active		 					boolean,
		proof	 						text,
		passwordtype	 				text,
		sourceid		 				integer,
		sourcetype	 					text,
		createdby	 					uuid,
		createdon	 					timestamp,
		lastmodifiedby	 				uuid,
		lastmodifiedon	 				timestamp,
		isactive		 				boolean
	);

drop table metasploitevent;
create table metasploitevent (
	metasploiteventid		uuid PRIMARY KEY,
	metasploitscanid		uuid,
	createdon			timestamp,
	createdby			uuid,
	lastmodifiedon			timestamp,
	lastmodifiedby			uuid,
	isactive			boolean,
	info				text,
	modulename			text,
	modulerhost			text,
	name				text,
	remotecreatedat			text,
	remotehostid			integer,
	remoteid			integer,
	remoteupdatedat			text,
	remoteworkspaceid		integer,
	username			text
);


drop table openvasscan;
create table openvasscan (
	openvasscanid			uuid PRIMARY KEY,
	scanid				uuid,
	scanstart			text,
	remotereportid			uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby			uuid,
	lastmodifiedon			timestamp,
	isactive			boolean
);

drop table openvasfilter;
create table openvasfilter (
	openvasfilterid			uuid PRIMARY KEY,
	openvasscanid			uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby			uuid,
	lastmodifiedon			timestamp,
	isactive			boolean
);

drop table openvasreportresult;
create table openvasreportresult (
	openvasreportresultid			uuid PRIMARY KEY,
	openvasscanid			uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby			uuid,
	lastmodifiedon			timestamp,
	isactive			boolean,
	description			text,
	host				text,
	port				text,
	subnet				text,
	threat				text,
	openvasnvtid			uuid
);

drop table openvasreportnvt;
create table openvasreportnvt (
	openvasreportnvtid		uuid PRIMARY KEY,
	openvasresultid 		uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby			uuid,
	lastmodifiedon			timestamp,
	isactive			boolean,
	oid					text,
	name				text,
	riskfactor			text,
	cvssbasescore		text
);

drop table openvasreportport;
create table openvasreportport (
	openvasreportportid			uuid PRIMARY KEY,
	openvasscanid			uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby			uuid,
	lastmodifiedon			timestamp,
	isactive			boolean
);

drop table nexposescan;
create table NexposeScan (
	NexposeScanID			uuid PRIMARY KEY,
	RemoteScanID 			int,
	RemoteSiteID			int,
	FullReport				text,
	ReportType				text,
	ScanID					uuid,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
);

drop table nexposeasset;
create table nexposeasset (
	nexposeassetid			uuid PRIMARY KEY,
	nexposescanid			uuid,
	createdby			uuid,
	createdon			timestamp,
	lastmodifiedby			uuid,
	lastmodifiedon			timestamp,
	isactive			boolean,
	remotedeviceid			integer,
	riskscore			decimal,
	scantemplate			text,
	siteimportance			text,
	sitename			text,
	status				text,
	ipaddressv4			text
);

drop table nexposehosttest;
create table nexposehosttest (
	nexposehosttestid			uuid PRIMARY KEY,
	nexposeassetid				uuid,
	createdby				uuid,
	createdon				timestamp,
	lastmodifiedby				uuid,
	lastmodifiedon				timestamp,
	isactive				boolean,
	ispcicompliant				boolean,
	nexposeparagraph			text,
	remotedeviceid				integer,
	remotescanid				integer,	
	status					text,
	vulnerablesince				text
);

drop table nexposehostfingerprint;
create table nexposehostfingerprint (
	nexposehostfingerprintid		uuid PRIMARY KEY,
	nexposeassetid				uuid,
	createdby				uuid,
	createdon				timestamp,
	lastmodifiedby				uuid,
	lastmodifiedon				timestamp,
	isactive				boolean,
	certainty				decimal,
	deviceclass				text,
	family					text,
	product					text,
	vendor					text
);

drop table nexposehostname;
create table nexposehostname (
	nexposehostnameid			uuid PRIMARY KEY,
	nexposeassetid				uuid,
	name					text,
	createdby				uuid,
	createdon				timestamp,
	lastmodifiedby				uuid,
	lastmodifiedon				timestamp,
	isactive				boolean
);

drop table nexposehostservice;
create table nexposehostservice (
	nexposehostserviceid			uuid PRIMARY KEY,
	nexposeassetid				uuid,
	port					integer,
	name					text,
	protocol				text,
	status					text,
	createdby				uuid,
	createdon				timestamp,
	lastmodifiedby				uuid,
	lastmodifiedon				timestamp,
	isactive				boolean
);

drop table nexposeservicetest;
create table nexposeservicetest (
	nexposeservicetestid 			uuid PRIMARY KEY,
	nexposehostserviceid			uuid,
	nexposeparagraph			text,
	remotedeviceid				integer,
	remotescanid				integer,
	status					text,
	vulnerablesince				text,
	createdby				uuid,
	createdon				timestamp,
	lastmodifiedby				uuid,
	lastmodifiedon				timestamp,
	isactive				boolean
);

drop table nexposeserviceconfiguration;
create table nexposeserviceconfiguration(
	nexposeserviceconfigurationid 		uuid PRIMARY KEY,
	nexposehostserviceid			uuid,
	createdby				uuid,
	createdon				timestamp,
	lastmodifiedby				uuid,
	lastmodifiedon				timestamp,
	isactive				boolean,
	name					text,
	value					text
);

drop table nexposeservicefingerprint;
create table nexposeservicefingerprint (
	nexposeservicefingerprintid 		uuid PRIMARY KEY,
	nexposehostserviceid			uuid,
	createdby				uuid,
	createdon				timestamp,
	lastmodifiedby				uuid,
	lastmodifiedon				timestamp,
	isactive				boolean,
	certainty				decimal,
	family					text,
	product					text,
	vendor					text,
	version					text
);

drop table nexposereport;
create table nexposereport (
	NexposeReportID			uuid PRIMARY KEY,
	NexposeScanID			uuid,
	FullReport				text,
	ReportType				text,
	CreatedOn				timestamp,
	CreatedBy				uuid,
	LastModifiedOn			timestamp,
	LastModifiedBy			uuid,
	IsActive				boolean
	
);

drop table onesixtyoneresult;
create table onesixtyoneresult (
	OneSixtyOneResultID		uuid PRIMARY KEY,
	ProfileID			uuid,
	FullOutput			text,
	HostPortID			uuid,
	UserID				uuid,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table cve;
create table CVE (
	CVEID				uuid PRIMARY KEY,
	Name				text,
	Description			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table cvereference;
create table CVEReference (
	CVEReferenceID		uuid PRIMARY KEY,
	CVEID				uuid,
	Source				text,
	URL					text,
	Description			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table cvecomment;
create table CVEComment (
	CVECommentID		uuid PRIMARY KEY,
	CVEID				uuid,
	Voter				text,
	Comment				text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table nvd;
create table NVD (
	NVDID				uuid PRIMARY KEY,
	RemoteNVDID			text,
	NVDCVSSID			uuid,
	CVEID				uuid,
	RemoteCVEID			text,
	DatePublished		timestamp,
	LastModified		timestamp,
	CWE					text,
	Summary				text,
	Name				text,
	Description			text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table nvdreference;
create table NVDReference (
	NVDReferenceID 		uuid PRIMARY KEY,
	NVDID				uuid,
	Source				text,
	URL					text,
	Description			text,
	Type				text,
	CreatedOn			timestamp,
	CreatedBy			uuid,
	LastModifiedOn		timestamp,
	LastModifiedBy		uuid,
	IsActive			boolean
);

drop table nvdvulnerablesoftware;
create table NVDVulnerableSoftware (
	NVDVulnerableSoftwareID		uuid PRIMARY KEY,
	NVDID						uuid,
	Software					text,
	CreatedOn					timestamp,
	CreatedBy					uuid,
	LastModifiedOn				timestamp,
	LastModifiedBy				uuid,
	IsActive					boolean
);

drop table nvdcvss;
create table NVDCVSS (
	NVDCVSSID					uuid PRIMARY KEY,
	NVDID						uuid,
	Score						decimal,
	Vector						text,
	Complexity					text,
	Authentication				text,
	ConfidentialityImpact		text,
	IntegrityImpact				text,
	AvailabilityImpact			text,
	CreatedOn					timestamp,
	CreatedBy					uuid,
	LastModifiedOn				timestamp,
	LastModifiedBy				uuid,
	IsActive					boolean
);

drop table wapiti;
create table Wapiti (
  wapitiresultid    uuid PRIMARY KEY,
  portid	    uuid,
  parentprofileid   uuid,
  fulloutput        text,
  hostipaddressv4   text,
  hostport	    integer,
  istcp		    boolean,
  createdby         uuid,
  createdon         timestamp,
  lastmodifiedby    uuid,
  lastmodifiedon    timestamp,
  isactive          boolean
);

drop table wapitibug;
create table WapitiBug (
  wapitibugid     uuid PRIMARY KEY,
  wapitiresultid  uuid,
  host            text,
  info            text,
  parameter	  text,
  level           integer,
  port            integer,
  type            text,
  url             text,
  wapititimestamp text,
  createdby       uuid,
  createdon       timestamp,
  lastmodifiedby  uuid,
  lastmodifiedon  timestamp,
  isactive        boolean
);


drop table wapitireference;
create table WapitiReference (
  wapitireferenceid     uuid PRIMARY KEY,
  wapitibugid           uuid,
  title                 text,
  url                   text,
  createdby             uuid,
  createdon             timestamp,
  lastmodifiedby        uuid,
  lastmodifiedon        timestamp,
  isactive              boolean
);

drop table event;
create table Event (
	EventID				uuid PRIMARY KEY,
	ProfileID			uuid,
	WebUserID			uuid,
	EventSeverity		integer,
	EventDescription 	text,
	EventAt				timestamp,
    createdby           uuid,
    createdon           timestamp,
    lastmodifiedby      uuid,
    lastmodifiedon      timestamp,
    isactive            boolean
);

drop table virtualmachines;
create table VirtualMachines (
	VirtualMachineID		uuid PRIMARY KEY,
	ScanID					uuid,
	Name					text,
	Guid					uuid,
	Username				text,
	Password				text,
    createdby           uuid,
    createdon           timestamp,
    lastmodifiedby      uuid,
    lastmodifiedon      timestamp,
    isactive            boolean
);