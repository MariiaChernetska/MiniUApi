﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveVideo]
(
		
	@UserId uniqueidentifier,
	@Title NVARCHAR(50) = '',
	@Path NVARCHAR(MAX) = '',
	@ScreenShot NVARCHAR(MAX) = '',
	@Description NVARCHAR(MAX) = ''

)
AS
BEGIN

Declare @VideoId uniqueidentifier = NEWID()
Insert into Videos (ID, UserID, Title, Path, ScreenShot, DateAdded, Description)
values (@VideoId, @UserId, @Title, @Path, @ScreenShot, GETUTCDATE(), @Description)

select Videos.Id
	 , Users.FullName as UserName
	 , Videos.Title as Title
	 , Videos.Description
	 , Videos.Path
	 , Videos.ScreenShot
	 , Videos.DateAdded
	 , count (Ratings.Rating) as RatingsAmount
	 , avg(Ratings.Rating) as Rating
from Videos
     inner join Users on Videos.UserId = Users.Id
	 left join Ratings on Videos.Id = Ratings.VideoId
where Videos.ID = @VideoId
group by Videos.Id
	   , Users.FullName
	   , Videos.Title
	   , Videos.Path
	   , Videos.ScreenShot
	   , Videos.DateAdded
	   , Rating
	   , Videos.Description
END