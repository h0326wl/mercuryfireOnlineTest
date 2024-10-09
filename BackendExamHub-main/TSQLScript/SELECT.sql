CREATE PROCEDURE [dbo].[usp_Select_ACPD]
    @acpd_sid nvarchar(20)
AS
BEGIN
    DECLARE @_jsonData nvarchar(max);

    -- 查詢 Myoffice_ACPD 表格中的資料
    SELECT acpd_sid, acpd_cname, acpd_ename, acpd_sname, acpd_email, acpd_status, acpd_stop, acpd_stopMemo, acpd_LoginID, acpd_LoginPW, acpd_memo, acpd_nowdatetime, appd_nowid, acpd_upddatetitme, acpd_updid
    FROM Myoffice_ACPD
    WHERE acpd_sid = @acpd_sid
    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
END
