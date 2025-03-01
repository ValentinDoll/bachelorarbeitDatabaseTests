-----------------BEGIN: Script to be run at Publisher 'VALENTIN-PC\MSSQLSERVER_1'-----------------
use [PublisherDB]
exec sp_addmergesubscription @publication = N'MyPublication', @subscriber = N'VALENTIN-PC\MSSQLSERVER_2', @subscriber_db = N'SubscriberDB', @subscription_type = N'Push', @sync_type = N'Automatic', @subscriber_type = N'Global', @subscription_priority = 75, @description = null, @use_interactive_resolver = N'False'
exec sp_addmergepushsubscription_agent @publication = N'MyPublication', @subscriber = N'VALENTIN-PC\MSSQLSERVER_2', @subscriber_db = N'SubscriberDB', @job_login = null, @job_password = null, @subscriber_security_mode = 0, @subscriber_login = N'sa', @subscriber_password = null, @publisher_security_mode = 1, @frequency_type = 4, @frequency_interval = 1, @frequency_relative_interval = 1, @frequency_recurrence_factor = 1, @frequency_subday = 4, @frequency_subday_interval = 1, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, @active_start_date = 20250130, @active_end_date = 99991231, @enabled_for_syncmgr = N'False'
GO
-----------------END: Script to be run at Publisher 'VALENTIN-PC\MSSQLSERVER_1'-----------------

