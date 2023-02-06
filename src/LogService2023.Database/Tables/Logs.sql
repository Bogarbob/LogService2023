﻿CREATE TABLE [dbo].[Logs]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [LogType] INT NOT NULL, 
    [TimeStamp] DATETIMEOFFSET NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL,
)
