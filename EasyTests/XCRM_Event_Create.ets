#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Calendar)
*Action New

*FillForm
 Subject = Important meeting
 Start Date/Time = 8/12/2008 2:00:00 AM
 End Date/Time = 8/12/2008 3:00:00 AM
 Description = "I'm not a reporter"

*Action Save and Close

*Action Navigation(Calendar)

#IfDef XCRMFullWin
*ExecuteEditorAction DateNavigator(8/12/2008)

;animation after navigation
*Sleep 1000

*SelectRecords SchedulerControl
 Columns = Subject
 Row = Important meeting
 
*CheckTable
 Columns = Start, End, Subject
 Row = 8/12/2008 2:00:00 AM, 8/12/2008 3:00:00 AM, Important meeting
#EndIf

#IfDef XCRMFullWeb
*ExecuteTableAction
 PopupMenuAction = Change View To.Work Week View

*ExecuteTableAction
 SetCurrentDate = 8/12/2008

*CheckTable
 Columns = StartOn, EndOn, Subject
 Row = 08/12/2008 2:00, 08/12/2008 3:00, Important meeting
#EndIf
