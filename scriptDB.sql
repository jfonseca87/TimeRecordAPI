USE [TimeRecordsDB]
GO
/****** Object:  Table [dbo].[TimeRecords]    Script Date: 10/12/2019 21:41:58 ******/
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
 CONSTRAINT [PK__TimeReco__3214EC077205B211] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetTimeRecordById]    Script Date: 10/12/2019 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTimeRecordById]
	@IdTimeRecord int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id,
			ActivityNumber,
			UsedTime,
			Comments,
			DateRecord
	FROM TimeRecords
	WHERE Id = @IdTimeRecord
END
GO
/****** Object:  StoredProcedure [dbo].[GetTimeRecords]    Script Date: 10/12/2019 21:41:59 ******/
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
	FROM TimeRecords
	WHERE DateRecord >= @InitialDate AND DateRecord <= @FinalDate
	ORDER BY DateRecord DESC
END

GO
/****** Object:  StoredProcedure [dbo].[SaveTimeRecord]    Script Date: 10/12/2019 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SaveTimeRecord]
	@ActivityNumber int,
	@UsedTime numeric(18,2),
	@Comments varchar(max),
	@DateRecord date
as
begin
	set nocount on;

	insert into TimeRecords
	(
		ActivityNumber, 
		UsedTIme, 
		Comments,
		DateRecord
	)
	values
	(
		@ActivityNumber,
		@UsedTime,
		@Comments,
		@DateRecord
	)

	select SCOPE_IDENTITY()
end

GO
/****** Object:  StoredProcedure [dbo].[UpdateTimeRecord]    Script Date: 10/12/2019 21:41:59 ******/
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
