#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Leads)
*Action New

*FillForm
 Topic = Lead Topic
 Company Name = Lead Company Name
 Last Name = Lead Last Name
 First Name = Lead First Name

*Action Save and Close

*Action Navigation(Leads)

*Action View(Open Leads)

*CheckTable
 Columns = Topic, Name
 Row = Lead Topic, Lead First Name Lead Last Name