CREATE PROCEDURE [dbo].[usp_Update_ACPD]
    @acpd_sid nvarchar(20),
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
    @acpd_updid nvarchar(20),
    @OutBox_ReturnValues nvarchar(max) OUTPUT
AS
BEGIN
    DECLARE @groupID uniqueidentifier = NEWID();
    DECLARE @_jsonData nvarchar(max);
    
    -- 更新 Myoffice_ACPD 表格資料
    UPDATE Myoffice_ACPD
    SET acpd_cname = @acpd_cname, acpd_ename = @acpd_ename, acpd_sname = @acpd_sname, acpd_email = @acpd_email, acpd_status = @acpd_status, acpd_stop = @acpd_stop, acpd_stopMemo = @acpd_stopMemo, acpd_LoginID = @acpd_LoginID, acpd_LoginPW = @acpd_LoginPW, acpd_memo = @acpd_memo, acpd_upddatetitme = GETDATE(), acpd_updid = @acpd_updid
    WHERE acpd_sid = @acpd_sid;
    
    -- 準備 JSON 日誌內容
    SET @_jsonData = (
        SELECT @acpd_sid AS 'SID', @acpd_cname AS 'CName', @acpd_ename AS 'EName', @acpd_sname AS 'SName', @acpd_email AS 'Email', @acpd_LoginID AS 'LoginID', @acpd_updid AS 'UpdatedBy'
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    -- 記錄日誌
    EXEC dbo.usp_AddLog @_InBox_ReadID = 0, @_InBox_SPNAME = 'usp_Update_ACPD', @_InBox_GroupID = @groupID, @_InBox_ExProgram = 'UPDATE', @_InBox_ActionJSON = @_jsonData, @_OutBox_ReturnValues = @OutBox_ReturnValues OUTPUT;
END
