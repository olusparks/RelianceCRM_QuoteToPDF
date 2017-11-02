# RelianceCRM_QuoteToPDF
This converts RelianceCRM Quote to PDF and sends email to customers

Retrival of CRM data using SOAP service (IOrganizationContextService) was done in WCF Library.(SoftSparkWCFService)
The Library is consumed by another project titled: ConsumeSoftSparkWCFService
A RDLC quote template was developed and its DataSet points to the Library (SoftSparkWCFService)
Email Activity is created from C# using SOAP Service and it is recorded on CRM as an email activity.
NOTE: To make avoid errors during email activity: Please visit my blog http://bantale.blogspot.com for more info
