/*
***** Object Creation Script: EventDB   			*****
***** Database Schema and Stored Procedures			*****
***** Script Date: 09/01/2014						*****
***** Created By: Martin Harwar						*****
***** Point8020 Limited | www.point8020.com			*****
***** Created For: Contoso Events - An MSDN App 	*****
***** Purpose: Create Database Objects in SQL Azure	*****
*/
/****** Object:  StoredProcedure [dbo].[AddAgendaItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAgendaItem]
	@EventID uniqueidentifier,
	@Title nvarchar(50),
	@Description nvarchar(max) = null
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Agenda WHERE EventID = @EventID AND Title = @Title)
	BEGIN
		SELECT 'Point8020.DuplicateAgendaItemForEvent' AS [Status]
		RETURN
	END

	DECLARE @ItemOrder tinyint
	SET @ItemOrder = (SELECT MAX(ItemOrder) FROM Agenda WHERE EventID = @EventID)
	IF @ItemOrder IS NULL
	BEGIN
		SET @ItemOrder = 1
	END
	ELSE
	BEGIN
		SET @ItemOrder = @ItemOrder+1
	END


	INSERT Agenda(EventID, Title, Description, ItemOrder)
	VALUES(@EventID, @Title, @Description, @ItemOrder)

	SELECT 'Point8020.Success' AS [Status], AgendaItemID FROM Agenda
	WHERE EventID = @EventID AND Title = @Title
END
GO
/****** Object:  StoredProcedure [dbo].[AddAttachmentItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAttachmentItem]
	@EventID uniqueidentifier,
	@Url nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Attachment WHERE EventID = @EventID AND AttachmentURL = @Url)
	BEGIN
		SELECT 'Point8020.DuplicateAttachmentForEvent' AS [Status]
		RETURN
	END

	INSERT Attachment(EventID, AttachmentURL)
	VALUES(@EventID, @Url)

	SELECT 'Point8020.Success' AS [Status], AttachmentID FROM Attachment
	WHERE EventID = @EventID AND AttachmentURL = @Url
END
GO
/****** Object:  StoredProcedure [dbo].[AddAttendeeItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAttendeeItem]
	@EventID uniqueidentifier,
	@Name nvarchar(50),
	@Email nvarchar(50) = null
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Attendee WHERE EventID = @EventID AND AttendeeEmail = @Email)
	BEGIN
		SELECT 'Point8020.DuplicateAttendeeForEvent' AS [Status]
		RETURN
	END

	INSERT Attendee(EventID, AttendeeName, AttendeeEmail)
	VALUES(@EventID, @Name, @Email)

	SELECT 'Point8020.Success' AS [Status], AttendeeID FROM Attendee
	WHERE EventID = @EventID AND AttendeeEmail = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[AddCateringItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddCateringItem]
	@EventID uniqueidentifier,
	@Title nvarchar(50),
	@Description nvarchar(max) = null
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Catering WHERE EventID = @EventID AND Title = @Title)
	BEGIN
		SELECT 'Point8020.DuplicateCateringItemForEvent' AS [Status]
		RETURN
	END

	DECLARE @ItemOrder tinyint
	SET @ItemOrder = (SELECT MAX(MealOrder) FROM Catering WHERE EventID = @EventID)
	IF @ItemOrder IS NULL
	BEGIN
		SET @ItemOrder = 1
	END
	ELSE
	BEGIN
		SET @ItemOrder = @ItemOrder+1
	END


	INSERT Catering(EventID, Title, Description, MealOrder)
	VALUES(@EventID, @Title, @Description, @ItemOrder)

	SELECT 'Point8020.Success' AS [Status], CateringID FROM Catering
	WHERE EventID = @EventID AND Title = @Title
END
GO
/****** Object:  StoredProcedure [dbo].[AddCoordinator]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddCoordinator]
	@EventID uniqueidentifier,
	@MemberName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Role WHERE EventID = @EventID AND MemberName = @MemberName)
	BEGIN
		SELECT 'Point8020.DuplicateCoordinatorForEvent' AS [Status]
		RETURN
	END

	INSERT Role(EventID, MemberName)
	VALUES(@EventID, @MemberName)

	SELECT 'Point8020.Success' AS [Status], * FROM Role
	WHERE EventID = @EventID AND MemberName = @MemberName
END



GO
/****** Object:  StoredProcedure [dbo].[AddEvent]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddEvent]
	@Title nvarchar(50),
	@Venue nvarchar(50) = null,
	@Address1 nvarchar(50) = null,
	@Address2 nvarchar(50) = null,
	@City nvarchar(50) = null,
	@State nvarchar(50) = null,
	@PostalCode nvarchar(10) = null,
	@StartDateTime smalldatetime,
	@EndDateTime smalldatetime,
	@LogoURL nvarchar(50) = null,
	@Description nvarchar(max) = null,
	@BudgetURL nvarchar(50) = null
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT Event(Title, Venue, Address1, Address2, City, State, PostalCode, StartDateTime, EndDateTime, LogoURL, Description, BudgetURL)
	VALUES(@Title, @Venue, @Address1, @Address2, @City, @State, @PostalCode, @StartDateTime, @EndDateTime, @LogoURL, @Description, @BudgetURL)

	SELECT 'Point8020.Success' AS [Status], EventID FROM Event
	WHERE Title = @Title
END

GO
/****** Object:  StoredProcedure [dbo].[AddPresenterItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPresenterItem]
	@EventID uniqueidentifier,
	@Title nvarchar(50),
	@Description nvarchar(max) = null
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Presenter WHERE EventID = @EventID AND PresenterName = @Title)
	BEGIN
		SELECT 'Point8020.DuplicatePresenterForEvent' AS [Status]
		RETURN
	END

	DECLARE @ItemOrder tinyint
	SET @ItemOrder = (SELECT MAX(ItemOrder) FROM Presenter WHERE EventID = @EventID)
	IF @ItemOrder IS NULL
	BEGIN
		SET @ItemOrder = 1
	END
	ELSE
	BEGIN
		SET @ItemOrder = @ItemOrder+1
	END


	INSERT Presenter(EventID, PresenterName, PresenterBio, ItemOrder)
	VALUES(@EventID, @Title, @Description, @ItemOrder)

	SELECT 'Point8020.Success' AS [Status], PresenterID FROM Presenter
	WHERE EventID = @EventID AND PresenterName = @Title
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAgendaItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAgendaItem]
	@AgendaItemID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Agenda WHERE AgendaItemID = @AgendaItemID)
	BEGIN
		SELECT 'Point8020.AgendaItemNotExists' AS [Status]
		RETURN
	END
	DELETE Agenda
	WHERE AgendaItemID = @AgendaItemID
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllCoordinators]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllCoordinators]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	DELETE Role WHERE EventID = @EventID	
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAttachmentItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAttachmentItem]
	@AttachmentItemID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Attachment WHERE AttachmentID = @AttachmentItemID)
	BEGIN
		SELECT 'Point8020.AttachmentNotExists' AS [Status]
		RETURN
	END
	DELETE Attachment
	WHERE AttachmentID = @AttachmentItemID
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAttendeeItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAttendeeItem]
	@AttendeeItemID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Attendee WHERE AttendeeID = @AttendeeItemID)
	BEGIN
		SELECT 'Point8020.AttendeeNotExists' AS [Status]
		RETURN
	END
	DELETE Attendee
	WHERE AttendeeID = @AttendeeItemID
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCateringItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCateringItem]
	@CateringItemID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Catering WHERE CateringID = @CateringItemID)
	BEGIN
		SELECT 'Point8020.CateringItemNotExists' AS [Status]
		RETURN
	END
	DELETE Catering
	WHERE CateringID = @CateringItemID
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEvent]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteEvent]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	DELETE Agenda WHERE EventID = @EventID
	DELETE Role WHERE EventID = @EventID
	DELETE Catering WHERE EventID = @EventID
	DELETE Presenter WHERE EventID = @EventID
	DELETE Attendee WHERE EventID = @EventID
	DELETE Attachment WHERE EventID = @EventID
	DELETE Event WHERE EventID = @EventID
END
GO
/****** Object:  StoredProcedure [dbo].[DeletePresenterItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeletePresenterItem]
	@PresenterItemID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Presenter WHERE PresenterID = @PresenterItemID)
	BEGIN
		SELECT 'Point8020.PresenterNotExists' AS [Status]
		RETURN
	END
	DELETE Presenter
	WHERE PresenterID = @PresenterItemID
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[GetAgendaItems]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAgendaItems]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	SELECT 'Point8020.Success' AS [Status], * FROM Agenda
	WHERE EventID = @EventID
	ORDER BY ItemOrder ASC
END
GO
/****** Object:  StoredProcedure [dbo].[GetAttachmentItems]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAttachmentItems]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	SELECT 'Point8020.Success' AS [Status], * FROM Attachment
	WHERE EventID = @EventID
	ORDER BY AttachmentURL ASC
END


GO
/****** Object:  StoredProcedure [dbo].[GetAttendeeItems]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAttendeeItems]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	SELECT 'Point8020.Success' AS [Status], * FROM Attendee
	WHERE EventID = @EventID
	ORDER BY AttendeeName ASC
END


GO
/****** Object:  StoredProcedure [dbo].[GetCateringItems]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCateringItems]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	SELECT 'Point8020.Success' AS [Status], * FROM Catering
	WHERE EventID = @EventID
	ORDER BY MealOrder ASC
END
GO
/****** Object:  StoredProcedure [dbo].[GetCoordinators]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCoordinators]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	SELECT 'Point8020.Success' AS [Status], * FROM Role
	WHERE EventID = @EventID
	ORDER BY MemberName ASC
END
GO
/****** Object:  StoredProcedure [dbo].[GetEventDetails]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEventDetails]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	SELECT 'Point8020.Success' AS [Status], * FROM Event
	WHERE EventID = @EventID

END
GO
/****** Object:  StoredProcedure [dbo].[GetPresenterItems]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPresenterItems]
	@EventID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	SELECT 'Point8020.Success' AS [Status], * FROM Presenter
	WHERE EventID = @EventID
	ORDER BY ItemOrder ASC
END
GO
/****** Object:  StoredProcedure [dbo].[IsUserCoordinator]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsUserCoordinator]
	@EventID uniqueidentifier,
	@UserName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status], 'FALSE' AS [IsInRole]
		RETURN
	END

	IF EXISTS(SELECT * FROM Role WHERE EventID = @EventID AND MemberName = @UserName)
	BEGIN
		SELECT 'Point8020.Success' AS [Status], 'TRUE' AS [IsInRole]
		RETURN
	END
	ELSE
	BEGIN
		SELECT 'Point8020.Success' AS [Status], 'FALSE' AS [IsInRole]
		RETURN
	END
END


GO
/****** Object:  StoredProcedure [dbo].[ListEvents]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListEvents]
		@UserName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	SELECT 'Point8020.Success' AS [Status],
		(SELECT COUNT(*) FROM Role WHERE MemberName = @UserName AND EventID = evt.EventID) AS [CurrentUserCanEdit], 
		evt.EventID, 
		evt.Title, 
		evt.Venue, 
		evt.Address1,
		evt.Address2,
		evt.City,
		evt.State,
		evt.PostalCode,
		CAST(evt.StartDateTime AS nvarchar(25)) AS [StartDateTime],
		CAST(evt.EndDateTime AS nvarchar(25)) AS [EndDateTime],
		--StartDateTime,
		--EndDateTime,
		evt.LogoURL,
		evt.Description,
		evt.BudgetURL
	FROM Event evt
	ORDER BY StartDateTime ASC, Title ASC
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateAgenda]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAgenda]
	@AgendaItemID uniqueidentifier,
	@ItemOrder tinyint
AS
BEGIN
	SET NOCOUNT ON
	UPDATE Agenda
	SET ItemOrder = @ItemOrder 
	WHERE AgendaItemID = @AgendaItemID
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAgendaItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAgendaItem]
	@EventID uniqueidentifier,
	@AgendaItemID uniqueidentifier,
	@Title nvarchar(50),
	@Description nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Agenda WHERE AgendaItemID = @AgendaItemID)
	BEGIN
		SELECT 'Point8020.ItemNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Agenda WHERE AgendaItemID != @AgendaItemID AND Title = @Title AND EventID = @EventID)
	BEGIN
		SELECT 'Point8020.DuplicateItemForEvent' AS [Status], @AgendaItemID AS AgendaItemID
		RETURN
	END
	UPDATE Agenda
	SET Title= @Title, Description = @Description 
	WHERE AgendaItemID = @AgendaItemID
	SELECT 'Point8020.Success' AS [Status], @AgendaItemID AS [AgendaItemID]
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAttendeeItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAttendeeItem]
	@EventID uniqueidentifier,
	@AttendeeItemID uniqueidentifier,
	@Name nvarchar(50),
	@Email nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Attendee WHERE AttendeeID = @AttendeeItemID)
	BEGIN
		SELECT 'Point8020.ItemNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Attendee WHERE AttendeeID != @AttendeeItemID AND AttendeeEmail = @Email AND EventID = @EventID)
	BEGIN
		SELECT 'Point8020.DuplicateItemForAttendee' AS [Status], @AttendeeItemID AS AttendeeItemID
		RETURN
	END
	UPDATE Attendee
	SET AttendeeName = @Name, AttendeeEmail = @Email 
	WHERE AttendeeID = @AttendeeItemID
	SELECT 'Point8020.Success' AS [Status], @AttendeeItemID AS [AttendeeItemID]
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCatering]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCatering]
	@CateringItemID uniqueidentifier,
	@ItemOrder tinyint
AS
BEGIN
	SET NOCOUNT ON
	UPDATE Catering
	SET MealOrder = @ItemOrder 
	WHERE CateringID = @CateringItemID
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCateringItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCateringItem]
	@EventID uniqueidentifier,
	@CateringItemID uniqueidentifier,
	@Title nvarchar(50),
	@Description nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Catering WHERE CateringID = @CateringItemID)
	BEGIN
		SELECT 'Point8020.ItemNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Catering WHERE CateringID != @CateringItemID AND Title = @Title AND EventID = @EventID)
	BEGIN
		SELECT 'Point8020.DuplicateItemForEvent' AS [Status], @CateringItemID AS CateringItemID
		RETURN
	END
	UPDATE Catering
	SET Title= @Title, Description = @Description 
	WHERE CateringID = @CateringItemID
	SELECT 'Point8020.Success' AS [Status], @CateringItemID AS [CateringItemID]
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEvent]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEvent]
	@EventID uniqueidentifier,
	@Title nvarchar(50),
	@Venue nvarchar(50) = null,
	@Address1 nvarchar(50) = null,
	@Address2 nvarchar(50) = null,
	@City nvarchar(50) = null,
	@State nvarchar(50) = null,
	@PostalCode nvarchar(10) = null,
	@StartDateTime smalldatetime,
	@EndDateTime smalldatetime,
	@LogoURL nvarchar(50) = null,
	@Description nvarchar(max) = null,
	@BudgetURL nvarchar(50) = null
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Event WHERE EventID = @EventID)
	BEGIN
		SELECT 'Point8020.EventNotExists' AS [Status]
		RETURN
	END
	UPDATE Event
		SET Title = @Title, 
		Venue= @Venue, 
		Address1 = @Address1,
		Address2 = @Address2, 
		City = @City,
		State = @State, 
		PostalCode = @PostalCode, 
		StartDateTime = @StartDateTime, 
		EndDateTime = @EndDateTime, 
		LogoURL = @LogoURL, 
		Description = @Description, 
		BudgetURL = @BudgetURL
	WHERE EventID = @EventID

	SELECT 'Point8020.Success' AS [Status], @EventID AS EventID

END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePresenter]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePresenter]
	@PresenterItemID uniqueidentifier,
	@ItemOrder tinyint
AS
BEGIN
	SET NOCOUNT ON
	UPDATE Presenter
	SET ItemOrder = @ItemOrder 
	WHERE PresenterID = @PresenterItemID
	SELECT 'Point8020.Success' AS [Status]
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePresenterItem]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePresenterItem]
	@EventID uniqueidentifier,
	@PresenterItemID uniqueidentifier,
	@Title nvarchar(50),
	@Description nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT * FROM Presenter WHERE PresenterID = @PresenterItemID)
	BEGIN
		SELECT 'Point8020.ItemNotExists' AS [Status]
		RETURN
	END
	IF EXISTS(SELECT * FROM Presenter WHERE PresenterID != @PresenterItemID AND PresenterName = @Title AND EventID = @EventID)
	BEGIN
		SELECT 'Point8020.DuplicateItemForPresenter' AS [Status], @PresenterItemID AS PresenterItemID
		RETURN
	END
	UPDATE Presenter
	SET PresenterName= @Title, PresenterBio = @Description 
	WHERE PresenterID = @PresenterItemID
	SELECT 'Point8020.Success' AS [Status], @PresenterItemID AS [PresenterItemID]
END
GO
/****** Object:  Table [dbo].[Agenda]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agenda](
	[EventID] [uniqueidentifier] NOT NULL,
	[AgendaItemID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ItemOrder] [tinyint] NOT NULL,
 CONSTRAINT [PK_Agenda] PRIMARY KEY CLUSTERED 
(
	[AgendaItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 

GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachment](
	[EventID] [uniqueidentifier] NOT NULL,
	[AttachmentID] [uniqueidentifier] NOT NULL,
	[AttachmentURL] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[AttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 

GO
/****** Object:  Table [dbo].[Attendee]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendee](
	[EventID] [uniqueidentifier] NOT NULL,
	[AttendeeID] [uniqueidentifier] NOT NULL,
	[AttendeeEmail] [nvarchar](50) NOT NULL,
	[AttendeeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Attendee] PRIMARY KEY CLUSTERED 
(
	[AttendeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 

GO
/****** Object:  Table [dbo].[Catering]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catering](
	[EventID] [uniqueidentifier] NOT NULL,
	[CateringID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[MealOrder] [tinyint] NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Catering] PRIMARY KEY CLUSTERED 
(
	[CateringID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 

GO
/****** Object:  Table [dbo].[Event]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Venue] [nvarchar](50) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[StartDateTime] [smalldatetime] NOT NULL,
	[EndDateTime] [smalldatetime] NOT NULL,
	[LogoURL] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[BudgetURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)  

GO
/****** Object:  Table [dbo].[Presenter]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Presenter](
	[EventID] [uniqueidentifier] NOT NULL,
	[PresenterID] [uniqueidentifier] NOT NULL,
	[PresenterName] [nvarchar](50) NOT NULL,
	[PresenterBio] [nvarchar](max) NULL,
	[ItemOrder] [smallint] NOT NULL,
 CONSTRAINT [PK_Presenter] PRIMARY KEY CLUSTERED 
(
	[PresenterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 

GO
/****** Object:  Table [dbo].[Role]    Script Date: 20/01/2014 10:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[EventID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[MemberName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 

GO
ALTER TABLE [dbo].[Agenda] ADD  CONSTRAINT [DF_Agenda_AgendaItemID]  DEFAULT (newid()) FOR [AgendaItemID]
GO
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_AttachmentID]  DEFAULT (newid()) FOR [AttachmentID]
GO
ALTER TABLE [dbo].[Attendee] ADD  CONSTRAINT [DF_Attendee_AttendeeID]  DEFAULT (newid()) FOR [AttendeeID]
GO
ALTER TABLE [dbo].[Catering] ADD  CONSTRAINT [DF_Catering_CateringID]  DEFAULT (newid()) FOR [CateringID]
GO
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_EventID]  DEFAULT (newid()) FOR [EventID]
GO
ALTER TABLE [dbo].[Presenter] ADD  CONSTRAINT [DF_Presenter_PresenterID]  DEFAULT (newid()) FOR [PresenterID]
GO
ALTER TABLE [dbo].[Presenter] ADD  CONSTRAINT [DF_Presenter_InvitationStatus]  DEFAULT ((0)) FOR [ItemOrder]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_RoleID]  DEFAULT (newid()) FOR [RoleID]
GO
ALTER TABLE [dbo].[Agenda]  WITH CHECK ADD  CONSTRAINT [FK_Agenda_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Agenda] CHECK CONSTRAINT [FK_Agenda_Event]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_Event]
GO
ALTER TABLE [dbo].[Attendee]  WITH CHECK ADD  CONSTRAINT [FK_Attendee_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Attendee] CHECK CONSTRAINT [FK_Attendee_Event]
GO
ALTER TABLE [dbo].[Catering]  WITH CHECK ADD  CONSTRAINT [FK_Catering_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Catering] CHECK CONSTRAINT [FK_Catering_Event]
GO
ALTER TABLE [dbo].[Presenter]  WITH CHECK ADD  CONSTRAINT [FK_Presenter_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Presenter] CHECK CONSTRAINT [FK_Presenter_Event]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_Event]
GO
