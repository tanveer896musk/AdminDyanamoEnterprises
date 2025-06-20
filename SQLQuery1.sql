SELECT TOP (1000) [CategoryId]
      ,[CategoryName]
  FROM [DynamoEnterprise].[Dynamo].[MasterCategory]

SELECT TOP (1000) [SubCategoryId]
      ,[CategoryId]
      ,[SubCategoryName]
  FROM [DynamoEnterprise].[Dynamo].[MasterSubCategory]

