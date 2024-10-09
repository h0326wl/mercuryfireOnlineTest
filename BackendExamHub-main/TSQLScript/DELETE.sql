CREATE PROCEDURE [dbo].[usp_Delete_ACPD]
    @acpd_sid nvarchar(20),
    @acpd_updid nvarchar(20),
    @OutBox_ReturnValues nvarchar(max) OUTPUT
AS
BEGIN
    DECLARE @groupID uniqueidentifier = NEWID();
    DECLARE @_jsonData nvarchar(max);

    -- 刪除 Myoffice_ACPD 表格中的資料
    DELETE FROM Myoffice_ACPD
    WHERE acpd_sid = @acpd_sid;
    
    -- 準備 JSON 日誌內容
    SET @_jsonData = (
        SELECT @acpd_sid AS 'SID', @acpd_updid AS 'DeletedBy'
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    -- 記錄日誌
    EXEC dbo.usp_AddLog @_InBox_ReadID = 0, @_InBox_SPNAME = 'usp_Delete_ACPD', @_InBox_GroupID = @groupID, @_InBox_ExProgram = 'DELETE', @_InBox_ActionJSON = @_jsonData, @_OutBox_ReturnValues = @OutBox_ReturnValues OUTPUT;
END
