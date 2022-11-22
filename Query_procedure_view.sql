CREATE OR ALTER PROCEDURE [spGetCoursesByCategory] 
    @CategoryId UNIQUEIDENTIFIER
AS
    SELECT * FROM [Course] WHERE [CategoryId] = @CategoryId

EXEC [spGetCoursesByCategory] '09ce0b7b-cfca-497b-92c0-3290ad9d5142'

SELECT * FROM [vwCourses]


SELECT
    [Career].[Id],
    [Career].[Title],
    [Career].[CareerId],
    [CareerItem].[Title]
FROM
    [Career]
INNER JOIN
    [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
ORDER BY
    [Career].[Title]