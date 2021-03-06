USE [master]
GO
CREATE DATABASE [TimeRecordsDB]
GO
USE [TimeRecordsDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeRecords](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ActivityNumber] [int] NOT NULL,
	[UsedTime] [numeric](18, 2) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[DateRecord] [datetime] NOT NULL,
	[State] [bit] NULL,
 CONSTRAINT [PK__TimeReco__3214EC077205B211] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTimeRecord]
	@IdTimeRecord int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM TimeRecords
	WHERE Id = @IdTimeRecord

	SELECT @IdTimeRecord
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTimeRecordById]
	@IdTimeRecord int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id,
			ActivityNumber,
			UsedTime,
			Comments,
			DateRecord,
			[State]
	FROM TimeRecords
	WHERE Id = @IdTimeRecord
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTimeRecords]
	@InitialDate datetime,
	@FinalDate datetime
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id
			,ActivityNumber
			,UsedTIme
			,Comments
			,DateRecord
			,[State]
	FROM TimeRecords
	WHERE DateRecord >= @InitialDate AND DateRecord <= @FinalDate
	ORDER BY DateRecord DESC
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[SaveTimeRecord]
	@ActivityNumber int,
	@UsedTime numeric(18,2),
	@Comments varchar(max),
	@DateRecord date
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO TimeRecords
	(
		ActivityNumber, 
		UsedTIme, 
		Comments,
		DateRecord,
		[State]
	)
	VALUES
	(
		@ActivityNumber,
		@UsedTime,
		@Comments,
		@DateRecord,
		0
	)

	SELECT SCOPE_IDENTITY()
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateStateTimeRecord]
	@IdTimeRecord int
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE TimeRecords
	SET [State] = 1
	WHERE Id = @IdTimeRecord

	SELECT @IdTimeRecord
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateTimeRecord]
	@IdTimeRecord int,
	@ActivityNumber int,
	@UsedTime numeric(18,2),
	@Comments varchar(max)
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE TimeRecords
	SET ActivityNumber = @ActivityNumber,
		UsedTime = @UsedTime,
		Comments = @Comments
	WHERE Id = @IdTimeRecord

	SELECT @IdTimeRecord
END
GO
USE [master]
GO
ALTER DATABASE [TimeRecordsDB] SET  READ_WRITE 
GO
