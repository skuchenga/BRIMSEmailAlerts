Text
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE proc_UTTrxEmailAlerts
(@OurBranchID nvarchar(6))
AS
SET NOCOUNT ON
BEGIN
 SET @OurBranchID=ISNULL(@OurBranchID,'001')
	SELECT UTC.UTCFirstName, UTT.TrxType, UTC.UTCLastName,UTT.ClientID,UTT.TranID, UTT.AccountID,UTC.UTCContactEmail AS UTCEmail, UTC.UTCMobile
	FROM t_UTTransaction UTT JOIN t_UTClients UTC ON UTT.ClientID = UTC.UTCID JOIN t_UTClientChequeDetails UTCC ON UTT.UTtrxColumnID = UTCC.IntColumnId
	WHERE UTT.OurBranchID = '001'
	AND IsMailSent is null
	AND AccountType <> 'G'	
	AND EntryCode IN (1,2)
	AND (ISNULL(UTC.UTCContactEmail,'') <> '' )
	AND utcc.[Status] = 'S'
	ORDER BY UTT.TranID DESC
END

exec proc_UTTrxEmailAlerts '001'

select * from sysobjects where name like '%transaction%' and xtype = 'p' order by crdate

Text
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE proc_UTTrxEmailAlerts
(@OurBranchID nvarchar(6))
AS
SET NOCOUNT ON
BEGIN
	SELECT UTC.UTCFirstName, UTT.TrxType, UTC.UTCLastName,UTT.ClientID,UTT.TranID, UTT.AccountID,UTC.UTCContactEmail AS UTCEmail, UTC.UTCMobile
	FROM t_UTTransaction UTT JOIN t_UTClients UTC ON UTT.ClientID = UTC.UTCID JOIN t_UTClientChequeDetails UTCC ON UTT.UTtrxColumnID = UTCC.IntColumnId
	WHERE UTT.OurBranchID = '001'
	AND IsMailSent is null
	AND AccountType <> 'G'	
	AND EntryCode IN (1,2)
	AND (ISNULL(UTC.UTCContactEmail,'') <> '' )
	AND utcc.[Status] = 'S'
	ORDER BY UTT.TranID DESC
END

