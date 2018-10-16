#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

#IncludeFile XCRM_AddLead.inc

*Action Navigation(Leads)

*Action View(Open Leads)

*SelectRecords
 Columns = Name
 Row = Lead1First Lead1Last

*Action Disqualify Lead...

*FillForm
 Status = Lost

*Action OK

*Action Navigation(Leads)

*Action View(Closed Leads)

*CheckTable
 Columns = Name, Status
 Row = Lead1First Lead1Last, Lost
