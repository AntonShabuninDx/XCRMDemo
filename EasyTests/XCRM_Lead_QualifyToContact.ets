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

*Action Qualify Lead...

*FillForm
 Contact = True

*Action OK

*Action Navigation(Contacts)

*CheckTable
 Columns = Full Name
 Row = Lead1First Lead1Last

