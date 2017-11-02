using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftSparkEntityWCF;
using RelianceXRM;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;
using Microsoft.Crm.Sdk.Messages;
using AjaxControlToolkit.HtmlEditor;
using System.Web.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace ConsumeSoftSparkEntityWCF
{
    public partial class index : System.Web.UI.Page
    {
        private static byte[] pdfBytes;
        private static string msgBody = string.Empty;
        private static IOrganizationService _service;
        private static OrganizationServiceProxy _serviceProxy;
        private static Uri uri;
        private static Uri homeRealmUri = null;
        private static ClientCredentials deviceCredentials = null;
        private static string urlName = "https://relianceinfo.crm4.dynamics.com/XRMServices/2011/Organization.svc";
        private static string fromLogicalName, toLogicalName, quoteLogicalName;
        private static Guid fromGuid, toGuid, quoteGuid, customerGuid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Thread thAllAvalues = new Thread(new ThreadStart(GetAllValues));
                //thAllAvalues.IsBackground = true;
                //thAllAvalues.Start();
                GetAllValues();
                PopulateDataList();
            }
        }


        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {

                //byte[] pdfByteArray = ConvertToPDF();
                //Thread th = new Thread(new ThreadStart(ConvertToPDF));
                //th.IsBackground = true;
                //th.Start();
                //th.Join();

                //if (th.ThreadState.Equals(ThreadState.Stopped))
                //{
                //    HideTables();
                //    successMessage.Visible = true;
                //    DownloadPDF(pdfBytes);
                //}
                //th.Abort();

                ConvertToPDF();
                HideTables();
                //successMessage.Visible = true;
                //CreateEmail();
                DownloadPDF(pdfBytes);
                //ConvertToPDF();
                //DownloadPDF(pdfBytes);


        //        <div runat="server" id="successMessage" visible="false">
        //    <div class="row">
        //        <div class="col-md-8">
        //            <div>
        //                <div class="alert alert-success" role="alert" style="text-align:center;">Your email has been successfully sent.</div>
        //            </div>
        //            <br />
        //            <div>
        //                <asp:Button ID="btnExit" runat="server" Text="Close" CssClass="btn btn-danger" OnClientClick="closeWindow()" />
        //            </div>
        //        </div>
        //    </div>
        //</div>
                //Build the div element here
                
                StringBuilder sb = new StringBuilder();
                sb.Append("div class='row'");
                sb.Append("div class='col-md-8'");
                
                
                //Response.Flush();
                //Response.Clear();


            }
            catch (Exception ex)
            {
                File.AppendAllText(Server.MapPath("~/Error.txt"), "Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace + "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                string New = Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine;
                File.AppendAllText(Server.MapPath("~/Error.txt"), New);
            }
        }


        #region HelperMethods

        #region Email
        //Function that sends email with attachment in CRM
        public void CreateEmail()
        {
            ClientCredentials cred = CRMEntity.getCredentials();
            //ClientCredentials cred = new ClientCredentials();
            //cred.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
            //cred.UserName.UserName = "bantale@relianceinfosystems.com";
            //cred.UserName.Password = "Password@123";
            

            uri = new Uri(urlName);
            using (_serviceProxy = new OrganizationServiceProxy(uri, homeRealmUri, cred, deviceCredentials))
            {
                _serviceProxy.EnableProxyTypes();
                //_serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.EndpointBehaviors.Add(new ProxyTypesBehavior());
                _service = (IOrganizationService)_serviceProxy;

                //Guid userID = ((WhoAmIResponse)_service.Execute(new WhoAmIRequest())).UserId;
                using (RelianceInfoServiceContext context = new RelianceInfoServiceContext(_service))
                {
                    //Create the 'From:' activity party for the email
                    ActivityParty fromParty = new ActivityParty
                    {
                        PartyId = new EntityReference(fromLogicalName, fromGuid)
                    };

                    // Create the 'To:' activity party for the email
                    ActivityParty toParty = new ActivityParty
                    {
                        PartyId = new EntityReference(toLogicalName, toGuid)
                    };


                    // Create an e-mail message.
                    Email email = new Email
                    {
                        To = new ActivityParty[] { toParty },
                        From = new ActivityParty[] { fromParty },
                        Subject = txtSubject.Text.Trim(),
                        Description = txtMsgBox.Text,
                        DirectionCode = true,
                        RegardingObjectId = new EntityReference(quoteLogicalName, quoteGuid)

                    };
                    Guid _emailId = _serviceProxy.Create(email);

                    ActivityMimeAttachment _sampleAttachment = new ActivityMimeAttachment
                    {
                        ObjectId = new EntityReference(Email.EntityLogicalName, _emailId),
                        ObjectTypeCode = Email.EntityLogicalName,
                        Subject = "Quote Attachment in PDF",
                        Body = System.Convert.ToBase64String(pdfBytes),
                        FileName = "Quotation.pdf"
                    };
                    Guid emailAttachment = _serviceProxy.Create(_sampleAttachment);

                    SendEmailRequest sendEmailreq = new SendEmailRequest
                    {
                        EmailId = _emailId,
                        TrackingToken = "",
                        IssueSend = true
                    };

                    SendEmailResponse sendEmailresp = (SendEmailResponse)_serviceProxy.Execute(sendEmailreq);
                }
            }
        }

        #endregion



        #region GetQuoteAndQuoteLine
        /// <summary>
        /// GetAllValues
        /// </summary>
        public void GetAllValues()
        {
            try
            {
                CRMEntity crm = new CRMEntity();
                ///URLFormat: index.aspx?quotenumber=QUO-00015-H3S0K8&guidValue=F9196979-2CA8-E711-80E7-1458D04377A8
                var quote = crm.GetQuote("QUO-01236-N0Z4S7", "7C5DB9C2-ACB4-E711-8121-70106FA5DD11");
                var quoteDetail = crm.GetQuoteDetails("QUO-01236-N0Z4S7", "7C5DB9C2-ACB4-E711-8121-70106FA5DD11");


                //string quoteNumber = HttpContext.Current.Request.QueryString["quotenumber"];// Request.Url.Query[0].ToString();
                //string id = HttpContext.Current.Request.QueryString["guidValue"]; //Request.Url.Query[1].ToString();

                //txtGuid.Text = id;
                //txtQuote.Text = quoteNumber;

                //if (string.IsNullOrEmpty(quoteNumber) && string.IsNullOrEmpty(id))
                //{
                //    Exception ex = new ArgumentNullException();
                //    File.AppendAllText(Server.MapPath("~/Error.txt"), "Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace + "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                //    string New = Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine;
                //    File.AppendAllText(Server.MapPath("~/Error.txt"), New);
                //}


                //var quote = crm.GetQuote(txtQuote.Text.ToString(), txtGuid.Text.ToString());
                //var quoteDetail = crm.GetQuoteDetails(txtQuote.Text.ToString(), txtGuid.Text.ToString());

                fromLogicalName = quote.Select(x => x.CreatedbyLogicalName).SingleOrDefault();
                toLogicalName = quote.Select(x => x.ContactLogicalName).SingleOrDefault();
                quoteLogicalName = quote.Select(x => x.QuoteLogicalName).SingleOrDefault();

                fromGuid = quote.Select(x => x.CreatedbyGuid).SingleOrDefault();
                toGuid = quote.Select(x => x.ContactGuid).SingleOrDefault();
                quoteGuid = quote.Select(x => x.QuoteId).SingleOrDefault();
                customerGuid = quote.Select(x => x.CustomerGuid).SingleOrDefault();

                //Build the HTML
                string TemplatePath = "~/Templates/QuoteTemplate.html";
                //string imagePath = HttpContext.Current.Request.Url.AbsoluteUri + "/images/QuoteImg.PNG";
                //string notePath = HttpContext.Current.Request.Url.AbsoluteUri + "/images/Notes.PNG";
                msgBody = File.ReadAllText(HttpContext.Current.Server.MapPath(TemplatePath));
                //msgBody = msgBody.Replace("$relianceNote", notePath);
                //msgBody = msgBody.Replace("$relianceLogo", imagePath);
                msgBody = msgBody.Replace("$customeridname", quote.Select(x => x.Customername).SingleOrDefault());
                msgBody = msgBody.Replace("$address1_line1", quote.Select(x => x.Address).SingleOrDefault());
                msgBody = msgBody.Replace("$address1_city", quote.Select(x => x.AddressCity).SingleOrDefault());
                msgBody = msgBody.Replace("$address1_stateorprovince", quote.Select(x => x.AddressState).SingleOrDefault());
                msgBody = msgBody.Replace("$Zip", quote.Select(x => x.Zip).SingleOrDefault());
                msgBody = msgBody.Replace("$new_contactpersonname", quote.Select(x => x.ContactPerson).SingleOrDefault());
                msgBody = msgBody.Replace("$createdon", quote.Select(x => x.Createdon).SingleOrDefault());
                msgBody = msgBody.Replace("$quotenumber", quote.Select(x => x.QuoteNumber).SingleOrDefault());
                msgBody = msgBody.Replace("$createdbyname", quote.Select(x => x.CreatedbyName).SingleOrDefault());
                msgBody = msgBody.Replace("$address1_telephone1", quote.Select(x => x.CreatedbyNumber).SingleOrDefault());
                msgBody = msgBody.Replace("$new_customtotal_base", quote.Select(x => x.SubTotal).SingleOrDefault());
                msgBody = msgBody.Replace("$new_quotevat", quote.Select(x => x.Vat).SingleOrDefault());
                msgBody = msgBody.Replace("$new_grandtotal_base", quote.Select(x => x.Total).SingleOrDefault());

                string quoteDetailTable = string.Empty;
                //string quantityDetailTable = string.Empty;
                //string descriptionDetailTable = string.Empty;
                //string unitPriceDetailTable = string.Empty;
                //string totalDetailTable = string.Empty;
                foreach (var item_q in quoteDetail)
                {
                    string quantity = item_q.Quantity.ToString();
                    string description = item_q.ProductName.ToString();
                    string unitPrice = item_q.Priceperunitbase.ToString();
                    string total = item_q.ExtendedAmountbase.ToString();
                    /*Generate quantity*/
                    //double quantityTop = 473.356;
                    //double DescripionTop = 423.043;
                    //double UnitPriceTop = 470.888;
                    //double TotalNGNTop = 470.888;
                    //for (int i = 0; i < item_q.Quantity.Count(); i++)
                    //{
                    //                    string quantityDiv = @"<div style='position:absolute;top:{0}px;left:108.309px;'>
                    //                                                <span style='font-size:14.108px;font-weight:bold;'>{1}</span>
                    //                                            </div>";
                    //                    quantityDetailTable = quantityDetailTable + string.Format(quantityDiv, quantityTop, quantity);
                    //                    quantityTop = +45.214;

                    //                    string DescriptionDiv = @"<div style='position:absolute;top:{0}px;left:152.158px;'>
                    //                                                    <span style='font-size:14.108px;'>{1}</span>
                    //                                                 </div>";
                    //                    descriptionDetailTable = descriptionDetailTable + string.Format(DescriptionDiv, DescripionTop, description);
                    //                    DescripionTop = DescripionTop + 45.214;

                    //                    string UnitPriceDiv = @"<div style='position:absolute;top:{0}px;left:470.426px;'>
                    //                                                    <span style='font-size:14.108px;'>₦{1}</span>
                    //                                                </div>";
                    //                    unitPriceDetailTable = unitPriceDetailTable + string.Format(UnitPriceDiv, UnitPriceTop, unitPrice);
                    //                    UnitPriceTop = +45.214;

                    //                    string TotalNGNDiv = @"<div style='position:absolute;top:{0}px;left:672.741px;'>
                    //                                                <span style='font-size:14.108px;'>₦{1}</span>
                    //                                            </div>";
                    //                    totalDetailTable = totalDetailTable + string.Format(TotalNGNDiv, TotalNGNTop, total);
                    //                    TotalNGNTop = +45.214;
                    //}

                    //string formatThis = "<tr><td width='10%' height='27'><strong>"+quantity+"</strong></td><td width='45%'>"+description+"</td><td width='25%'>"+unitPrice+"</td><td width='20%'>"+total+"</td></tr>";
                    quoteDetailTable = quoteDetailTable + "<tr border='0'><td width='10%' height='27'><strong>" + quantity + "</strong></td><td width='45%'>" + description + "</td><td width='25%' style='text-align:right;'>" + unitPrice + "</td><td style='text-align:right;' width='20%'>" + total + "</td></tr>";
                    //quoteDetailTable = quoteDetailTable + string.Format(formatThis, quantity, description, unitPrice, total);
                }
                ////msgBody = msgBody.Replace("$quantity", quantityDetailTable);
                ////msgBody = msgBody.Replace("$Description", descriptionDetailTable);
                ////msgBody = msgBody.Replace("$UnitPrice", unitPriceDetailTable);
                ////msgBody = msgBody.Replace("$TotalNGN", totalDetailTable);
                msgBody = msgBody.Replace("$quoteDetails", quoteDetailTable);
                //return msgBody;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Server.MapPath("~/Error.txt"), "Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace + "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                string New = Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine;
                File.AppendAllText(Server.MapPath("~/Error.txt"), New);
                //return msgBody;
            }

        }
        #endregion



        #region ConvertToPDF
        /// <summary>
        /// ConvertToPDF
        /// </summary>
        public void ConvertToPDF()
        {
            try
            {

                //convert HTML TO PDF
                //StringBuilder sb = new StringBuilder(msgBody);
                StringReader sr = new StringReader(msgBody);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                HTMLWorker htmlParser = new HTMLWorker(pdfDoc);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter wr = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    htmlParser.Parse(sr);
                    //XMLWorkerHelper.GetInstance().ParseXHtml(wr, pdfDoc, sr);
                    pdfDoc.Close();

                    pdfBytes = memoryStream.ToArray();
                    memoryStream.Close();
                }

                //CreateEmail(pdfBytes);
                //DownloadPDF(pdfBytes);
                //return pdfBytes;
                ////Create Email activity in CRM
                //CreateEmail(pdfBytes);


            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion




        #region DownloadPDF
        public void DownloadPDF(byte[] pdf)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=RelianceQuote.pdf");
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(pdfBytes);
        }

        #endregion


        /// <summary>
        /// Populate DataList with Related contact
        /// </summary>
        /// 
        public void PopulateDataList()
        {
            CRMEntity entityRelatedContact = new CRMEntity();
            var entityRelatedContactList = entityRelatedContact.GetAccountContact(customerGuid.ToString());
            Session["entityRelatedContactList"] = entityRelatedContact.GetAccountContact(customerGuid.ToString());
            DataList1.DataSource = entityRelatedContactList;
            DataList1.DataBind();
        }
        #endregion

        private void HideTables()
        {
            //successMessage.Visible = false;
            //emailDiv.Visible = false;
        }

        #region HelperClasses
        public void ShowPdf(string strFileName)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            Response.ContentType = "application/pdf";
            Response.WriteFile(strFileName);
            Response.Flush();
            Response.Clear();
        }
        public static class FileTools
        {
            public static string ReadFileString(string path)
            {
                // Use StreamReader to consume the entire text file.
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        #endregion



    }



}