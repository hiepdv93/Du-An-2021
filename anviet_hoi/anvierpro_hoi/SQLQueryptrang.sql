/****** Script for SelectTopNRows command from SSMS  ******/
with bangtam as(
SELECT [id_emp]
      ,[group_emp]
      ,[name_emp]
      ,[email_emp]
      ,[age_emp]
      ,[add_emp]
      ,[active_emp]
	  , ROW_NUMBER() over(order by id_emp) as chiso
  FROM [Nhanvien].[dbo].[Emplyoee] where 1=1
  )

  select  [id_emp]
      ,[group_emp]
      ,[name_emp]
      ,[email_emp]
      ,[age_emp]
      ,[add_emp]
      ,[active_emp]	  
      ,[chiso]
	  ,(select count(*) from bangtam) as tong_bangghi
	   from bangtam where chiso between 5 and 8



  