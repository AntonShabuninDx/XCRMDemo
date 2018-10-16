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
 Account = True
 Opportunity = True

*Action OK

; check account
*Action Navigation(Accounts)

*ProcessRecord
 Account Name = Lead1CompanyName

*Action Contacts
*CheckTable Contacts
 Columns = Full Name
 Row = Lead1First Lead1Last

; check contact
*Action Navigation(Contacts)

*CheckTable
 Columns = Full Name
 Row = Lead1First Lead1Last

; check opportunity

*Action Navigation(Opportunities)
*Action View(Open Opportunities)

*CheckTable
 Columns = Topic, Potential Customer
 Row = 'Interested in Product1, Product2', Lead1CompanyName