select * from dbo.Member
select * from dbo.HistoryOfMemberUpdate

drop table dbo.Member
drop table dbo.HistoryOfMemberUpdate

delete from dbo.Member where id = 3
delete from dbo.HistoryOfMemberUpdate where id = 3

update dbo.Member set status = 0 where id=2
