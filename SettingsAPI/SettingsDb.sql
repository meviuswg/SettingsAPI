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
	,[created] DATETIME CONSTRAINT def_settings_application_created DEFAULT(getdate())
	,CONSTRAINT pk_settings_application PRIMARY KEY (id)
	,CONSTRAINT ux_settings_application_name UNIQUE ([name])
	)
GO

CREATE TABLE dbo.settings_directory (
	id INT identity(1, 1)
	,application_id INT
	,[name] VARCHAR(20)
	,description VARCHAR(150)
	,CONSTRAINT pk_settings_directory PRIMARY KEY (id)
	,CONSTRAINT ux_settings_directory_name UNIQUE ([name])
	,CONSTRAINT fk_settings_directory_application_id FOREIGN KEY ([application_id]) REFERENCES settings_application([id])
	)
GO

CREATE TABLE dbo.settings_repository (
	repository_id INT identity(1, 1)
	,application_id INT NOT NULL
	,[version] INT NOT NULL CONSTRAINT def_setting_repository_repository DEFAULT(1)
	,created DATETIME NOT NULL CONSTRAINT def_setting_repository_created DEFAULT(getdate())
	,CONSTRAINT ux_settings_repository_application_repository UNIQUE (
		application_id
		,[version]
		)
	,CONSTRAINT pk_settings_repository PRIMARY KEY (repository_id)
	,CONSTRAINT fk_settings_repository_application_id FOREIGN KEY (application_id) REFERENCES settings_application(id)
	)
GO

CREATE TABLE dbo.settings_api_key (
	[id] INT identity(1, 1)
	,[application_id] INT NOT NULL
	,[apikey] NVARCHAR(50)
	,last_used DATETIME NULL
	,edit_directories BIT NOT NULL CONSTRAINT def_settings_api_key_edit_directories DEFAULT(1)
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

CREATE TABLE dbo.settings (
	[object_id] INT
	,[repository_id] INT NOT NULL
	,[directory_id] INT NOT NULL
	,[setting_key] VARCHAR(50) NOT NULL
	,[setting_value] NVARCHAR(max) NULL
	,[created] DATETIME CONSTRAINT def_settings_created DEFAULT(getdate())
	,[modified] DATETIME NULL
	,CONSTRAINT pk_settings PRIMARY KEY (
		[object_id]
		,[repository_id]
		,[directory_id]
		,[setting_key]
		)
	,CONSTRAINT fk_settings_repository_id FOREIGN KEY (repository_id) REFERENCES settings_repository(repository_id)
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
	,'_system'
	,'System reserved Application for the Settings API and procedures.'
	)
	,(
	1
	,'SampleApplication'
	,'Sample application'
	)

SET IDENTITY_INSERT settings_application OFF
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
	,'_system'
	,'System reserved directory.'
	)
	,(
	- 2
	,- 1
	,'_directory'
	,'System reserved directory.'
	)
	,(
	1
	,1
	,'Application'
	,'Standard directory for Application specific settings. When used, the object_id of the settings should be 0.'
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
	,'Standard directory for User specific settings. When used, the object_id of the setting is expected to be the user_id a valid user.'
	)

SET IDENTITY_INSERT settings_directory OFF

INSERT INTO settings_repository (
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

INSERT INTO settings_api_key (
	[apikey]
	,application_id
	)
VALUES (
	@masterKey
	,- 1
	)
	,(
	@sampleKey
	,1
	)

INSERT INTO settings_api_directory_access (
	[apikey_id]
	,directory_id
	,allow_write
	,allow_delete
	,allow_create
	)
VALUES (
	1
	,- 1
	,1
	,1
	,1
	)
	,(
	2
	,1
	,0
	,0
	,0
	)
	,(
	2
	,2
	,1
	,1
	,1
	);
