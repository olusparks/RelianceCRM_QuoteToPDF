# RelianceCRM_QuoteToPDF
This converts RelianceCRM Quote to PDF and sends email to customers

1. Retrival of CRM data using SOAP service (IOrganizationContextService) was done in WCF Library.(SoftSparkWCFService).

2. The Library is consumed by another project titled: ConsumeSoftSparkWCFService

3. A RDLC quote template was developed and its DataSet points to the Library (SoftSparkWCFService)

4. Email Activity is created from C# using SOAP Service and it is recorded on CRM as an email activity.

5. NOTE: To make avoid errors during email activity: Please visit my blog http://bantale.blogspot.com for more info
