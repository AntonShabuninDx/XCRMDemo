#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Calendar)
*ExecuteTableAction
 SetCurrentDate = 10/10/2015

*Action New
*FillForm
 Subject = Appointment
 Start Date/Time = 8/8/2015 2:00:00 AM
 End Date/Time = 8/8/2015 3:00:00 AM
*Action Save and Close

*Action New
*FillForm
 Subject = Recurrence appointment
 Start Date/Time = 8/8/2015 4:00:00 AM
 End Date/Time = 8/8/2015 5:00:00 AM
*ExecuteEditorAction Recurrence
#IfDef XCRMFullWin
*Action OK
#EndIf
#IfDef XCRMFullWeb
*ExecuteEditorAction Recurrence(OK)
#EndIf
*Action Save and Close

#IfDef XCRMFullWeb
*ExecuteTableAction
 PopupMenuAction = Change View To.Day View
#EndIf
#IfDef XCRMFullWin
*ExecuteTableAction SchedulerControl
 ChangeViewTo = Day
#EndIf

*ExecuteTableAction
 SetCurrentDate = 8/8/2015

*SelectRecords
 Columns = Subject
 Row = Appointment
*Action Delete
*HandleDialog
 Respond = Yes
#IfDef XCRMFullWeb
*SelectRecords
 Columns = Subject
 Row = Recurrence appointment
*Action Edit
#EndIf
#IfDef XCRMFullWin
*ProcessRecord
 Subject = Recurrence appointment
*FillForm
 Edit this occurrence. = True
*Action OK
#EndIf
*FillForm
 Subject = Recurrence appointment1
*Action Save and Close

*SelectRecords
 Columns = Subject
 Row = Recurrence appointment1
*Action Delete
*HandleDialog
 Respond = Yes
#IfDef XCRMFullWin
*FillForm
 Delete this occurrence. = True
*Action OK
#EndIf
#IfDef XCRMFullWeb
*ExecuteTableAction
 HandleDeleteDialog = DeleteOccurrence
#EndIf

!SelectRecords
 Columns = Subject
 Row = Appointment

!SelectRecords
 Columns = Subject
 Row = Recurrence appointment

!SelectRecords
 Columns = Subject
 Row = Recurrence appointment1

*ExecuteTableAction
 SetCurrentDate = 8/9/2015

*SelectRecords
 Columns = Subject
 Row = Recurrence appointment
*Action Delete
*HandleDialog
 Respond = Yes
#IfDef XCRMFullWin
*FillForm
 Delete this occurrence. = True
*Action OK
#EndIf
#IfDef XCRMFullWeb
*ExecuteTableAction
 HandleDeleteDialog = DeleteOccurrence
#EndIf

#IfDef XCRMFullWeb
*ExecuteTableAction
 PopupMenuAction = Change View To.Day View
#EndIf
#IfDef XCRMFullWin
*ExecuteTableAction SchedulerControl
 ChangeViewTo = Day
#EndIf

!SelectRecords
 Columns = Subject
 Row = Recurrence appointment

*ExecuteTableAction
 SetCurrentDate = 8/10/2015

*SelectRecords
 Columns = Subject
 Row = Recurrence appointment
*Action Delete
*HandleDialog
 Respond = Yes
#IfDef XCRMFullWin
*FillForm
 Delete the series. = True
*Action OK
#EndIf
#IfDef XCRMFullWeb
*ExecuteTableAction
 HandleDeleteDialog = DeleteSeries
#EndIf

*ExecuteTableAction
 SetCurrentDate = 8/11/2015

!SelectRecords
 Columns = Subject
 Row = Recurrence appointment