USE master;
ALTER DATABASE [SettingsDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [SettingsDb] ;

CREATE DATABASE [SettingsDb]
GO

USE [SettingsDb]
GO

CREATE TABLE dbo.settings_application (
	[id] INT NOT NULL identity(1, 1)
	,[name] VARCHAR(50) NOT NULL
	,[description] NVARCHAR(150) NULL
	,[created] DATETIME NOT NULL CONSTRAINT def_settings_application_created DEFAULT(getdate())
	,CONSTRAINT pk_settings_application PRIMARY KEY (id)
	,CONSTRAINT ux_settings_application_name UNIQUE ([name])
	)
GO

CREATE TABLE dbo.settings_directory (
	id INT identity(1, 1)
	,application_id INT
	,[name] VARCHAR(20)
	,description VARCHAR(150)
	,created datetime not null  CONSTRAINT def_ssettings_directory_created DEFAULT(getdate()) 
	,CONSTRAINT pk_settings_directory PRIMARY KEY (id)
	,CONSTRAINT ux_settings_directory_name UNIQUE ([name],[application_id])
	,CONSTRAINT fk_settings_directory_application_id FOREIGN KEY ([application_id]) REFERENCES settings_application([id])
	)
GO

CREATE TABLE dbo.settings_version (
	 version_id INT identity(1, 1)
	,application_id INT NOT NULL
	,[version] INT NOT NULL CONSTRAINT def_setting_version_version DEFAULT(1)
	,created DATETIME NOT NULL CONSTRAINT def_setting_version_created DEFAULT(getdate())
	,CONSTRAINT ux_settings_version_application_version UNIQUE (
		application_id
		,[version]
		)
	,CONSTRAINT pk_settings_version PRIMARY KEY (version_id)
	,CONSTRAINT fk_settings_version_application_id FOREIGN KEY (application_id) REFERENCES settings_application(id)
	)
GO

CREATE TABLE dbo.settings_api_key (
	[id] INT identity(1, 1)
	,[application_id] INT NOT NULL
	,[apikey] NVARCHAR(50)
	,last_used DATETIME NULL
	,admin_key BIT NOT NULL CONSTRAINT def_settings_api_key_edit_directories DEFAULT(1)
	,[active] BIT NOT NULL CONSTRAINT def_settings_api_key_active DEFAULT(1)
	,created DATETIME NOT NULL CONSTRAINT def_settings_api_key_created DEFAULT(getdate())
	,CONSTRAINT pk_settings_api_key PRIMARY KEY ([id])
	,CONSTRAINT pk_settings_api_key_application_id FOREIGN KEY ([application_id]) REFERENCES settings_application(id)
	,CONSTRAINT ck_settings_api_key_len CHECK (LEN([apikey]) >= 32)
	,CONSTRAINT ux_settings_api_key UNIQUE ([apikey])
	)
GO

CREATE TABLE dbo.settings_api_directory_access (
	[apikey_id] INT
	,directory_id INT
	,allow_write BIT NOT NULL
	,allow_delete BIT NOT NULL
	,allow_create BIT NOT NULL
	,CONSTRAINT pk_settings_api_access PRIMARY KEY (
		[apikey_id]
		,directory_id
		)
	,CONSTRAINT fk_settings_api_access_key FOREIGN KEY ([apikey_id]) REFERENCES settings_api_key(id)
	,CONSTRAINT fk_settings_api_access_directory_id FOREIGN KEY ([directory_id]) REFERENCES settings_directory([id])
	)
GO

CREATE TRIGGER   [dbo].[trigger_directory_access_master_key_admin_keys]
   ON   [dbo].[settings_directory]
   AFTER INSERT 
AS 
BEGIN
	 -- inserts the master api key and application admin keys to allow full access.

	 insert into  [dbo].[settings_api_directory_access](apikey_id,directory_id,allow_create,allow_delete,allow_write)
	 select
	 -1
	 ,i.id
	 ,1
	 ,1
	 ,1
	 from inserted i
	 union
	 select 
	  a.id
	 ,b.id
	 ,1
	 ,1
	 ,1
	 from settings_api_key a
	  join inserted b
		on b.application_id = a.application_id
	 where a.admin_key = 1 

END
GO
 

CREATE TABLE dbo.settings (
	[object_id] INT
	,[version_id] INT NOT NULL
	,[directory_id] INT NOT NULL
	,[setting_key] VARCHAR(50) NOT NULL
	,[setting_value] NVARCHAR(max) NULL
	,[created] DATETIME CONSTRAINT def_settings_created DEFAULT(getdate())
	,[modified] DATETIME NULL
	,CONSTRAINT pk_settings PRIMARY KEY (
		[object_id]
		,[version_id]
		,[directory_id]
		,[setting_key]
		)
	,CONSTRAINT fk_settings_version_id FOREIGN KEY (version_id) REFERENCES settings_version(version_id)
	,CONSTRAINT fk_settings_directory_id FOREIGN KEY (directory_id) REFERENCES settings_directory(id)
	)
GO


-- SEED
DECLARE @masterKey VARCHAR(50) = '=a33a5f531f49480eac31d64d02163bcf'
DECLARE @sampleKey VARCHAR(50) = '70eb9a6ad2e64647b220e3245024c325'

SET IDENTITY_INSERT settings_application ON

INSERT INTO settings_application (
	id
	,NAME
	,description
	)
VALUES (
	- 1
	,'system'
	,'System reserved Application for the Settings API and procedures.'
	)
	,(
	1
	,'SampleApplication'
	,'Sample application'
	)

SET IDENTITY_INSERT settings_application OFF

SET IDENTITY_INSERT settings_api_key ON
INSERT INTO settings_api_key (
	 [id]
	,[apikey]
	,application_id
	)
VALUES (
	-1
	,@masterKey
	,- 1
	)
	,(
	 1
	,@sampleKey
	,1
	)
SET IDENTITY_INSERT settings_api_key OFF

SET IDENTITY_INSERT settings_directory ON

INSERT INTO settings_directory (
	id
	,application_id
	,NAME
	,description
	)
VALUES (
	- 1
	,- 1
	,'system'
	,'System reserved directory.'
	)
	,(
	- 2
	,- 1
	,'root'
	,'System reserved directory.'
	)
	,(
	1
	,1
	,'root'
	,'Standard directory for Application specific settings.'
	)
	,(
	2
	,1
	,'Company'
	,'Standard directory for Company specific settings. When used, the object_id of the setting is expected to be the company_id of an existing company.'
	)
	,(
	3
	,1
	,'User'
	,'Standard directory for User specific settings. When used, the object_id of the setting is expected to be the user_id a existing user.'
	)

SET IDENTITY_INSERT settings_directory OFF

INSERT INTO settings_version (
	application_id
	,[version]
	)
VALUES (
	- 1
	,1
	)
	,(
	1
	,1
	); 

 
