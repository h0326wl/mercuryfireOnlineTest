CREATE PROCEDURE [dbo].[usp_Insert_ACPD]
    @acpd_cname nvarchar(60),
    @acpd_ename nvarchar(40),
    @acpd_sname nvarchar(40),
    @acpd_email nvarchar(60),
    @acpd_status tinyint,
    @acpd_stop bit,
    @acpd_stopMemo nvarchar(600),
    @acpd_LoginID nvarchar(30),
    @acpd_LoginPW nvarchar(60),
    @acpd_memo nvarchar(120),
    @appd_nowid nvarchar(20),
    @OutBox_ReturnValues nvarchar(max) OUTPUT
AS
BEGIN
    DECLARE @newSID nvarchar(20);
    DECLARE @groupID uniqueidentifier = NEWID();
    DECLARE @_jsonData nvarchar(max);
    
    -- 生成新的 SID
    EXEC dbo.NewSID @TableName = 'Myoffice_ACPD', @ReturnSID = @newSID OUTPUT;
    
    -- 將資料插入 Myoffice_ACPD
    INSERT INTO Myoffice_ACPD (acpd_sid, acpd_cname, acpd_ename, acpd_sname, acpd_email, acpd_status, acpd_stop, acpd_stopMemo, acpd_LoginID, acpd_LoginPW, acpd_memo, acpd_nowdatetime, appd_nowid)
    VALUES (@newSID, @acpd_cname, @acpd_ename, @acpd_sname, @acpd_email, @acpd_status, @acpd_stop, @acpd_stopMemo, @acpd_LoginID, @acpd_LoginPW, @acpd_memo, GETDATE(), @appd_nowid);
    
    -- 準備 JSON 日誌內容
    SET @_jsonData = (
        SELECT @newSID AS 'SID', @acpd_cname AS 'CName', @acpd_ename AS 'EName', @acpd_sname AS 'SName', @acpd_email AS 'Email', @acpd_LoginID AS 'LoginID', @appd_nowid AS 'CreatedBy'
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    -- 記錄日誌
    EXEC dbo.usp_AddLog @_InBox_ReadID = 0, @_InBox_SPNAME = 'usp_Insert_ACPD', @_InBox_GroupID = @groupID, @_InBox_ExProgram = 'INSERT', @_InBox_ActionJSON = @_jsonData, @_OutBox_ReturnValues = @OutBox_ReturnValues OUTPUT;
    
    SELECT @newSID AS 'NewSID'; -- 回傳新產生的 SID
END
