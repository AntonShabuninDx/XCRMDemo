#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

#IncludeFile XCRM_AddAccount.inc

#IncludeFile XCRM_AddContact.inc

*Action Navigation(Opportunities)

*Action View(Open Opportunities)

*Action New

*FillForm
 Name = Opportunity Topic
 Potential Customer = Account1
 Estimated Close Date = 12/12/2008
 Revenue Is User Provided = true
 Estimated Revenue = 100
 Probability = 50
 Rating = Hot

*Action Save and Close

*Action Navigation(Opportunities)

*CheckTable
 Columns = Topic, Potential Customer, Estimated Revenue
 Row = Opportunity Topic, Account1, $100.00
 
*ProcessRecord
 Topic = Opportunity Topic

#IfDef XCRMFullWeb
*Action Edit
#EndIf

*FillForm
 Potential Customer = Contact1First Contact1Last

*Action Save and Close

*Action Navigation(Opportunities)

*CheckTable
 Columns = Topic, Potential Customer
 Row = Opportunity Topic, Contact1First Contact1Last
