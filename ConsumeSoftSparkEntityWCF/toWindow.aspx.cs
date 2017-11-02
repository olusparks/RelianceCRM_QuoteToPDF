using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using RelianceXRM;
using SoftSparkEntityWCF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Description;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Text;

namespace ConsumeSoftSparkEntityWCF
{
    public partial class toWindow : System.Web.UI.Page
    {
        private static byte[] pdfBytes;
        private static string htmlBody = string.Empty;
        private static IOrganizationService _service;
        private static OrganizationServiceProxy _serviceProxy;
        private static Uri uri;
        private static Uri homeRealmUri = null;
        private static ClientCredentials deviceCredentials = null;
        private static string urlName = "https://relianceinfo.crm4.dynamics.com/XRMServices/2011/Organization.svc";
        private static string fromLogicalName, toLogicalName, quoteLogicalName, fromName, toName;
        private static Guid fromGuid, toGuid, quoteGuid, customerGuid;
        //List<Stream> ms_streams;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllValues();
                hyp.Text = "RelianceQuote.pdf";
            }
        }


        List<Contact> iContactlist = new List<Contact>();
        List<Contact> iAddedContact = new List<Contact>();

        //Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        //    getContact()
        //End Sub
        public void getContact()
        {
            //iAddedContact.Add(New Contact(111, "Danjuma Audu", "danjuma@need-solution.com"))
            //iAddedContact.Add(New Contact(112, "Faith William", "faith@gmail.com"))
            //iAddedContact.Add(New Contact(113, "Danyo Ade", "danyo@need-solution.com"))
            //iAddedContact.Add(New Contact(114, "Bantele Aliu", "Bantele@reliance.com"))
            //iAddedContact.Add(New Contact(115, "Koya Mudaishuru", "koya@Dynamic.com"))
            //iAddedContact.Add(New Contact(116, "Jumoke Aliu", "jummy@Jide.com"))
            //iAddedContact.Add(New Contact(117, "Danjuma Igoche", "igoche@need-solution.com"))
            //iAddedContact.Add(New Contact(118, "John Audu", "john@need-solution.com"))
            //iContactlist.Add(New Contact(111, "Danjuma Audu", "danjuma@need-solution.com", "Reliance", "Head Client Engagement", "Marketting and Sales"))
            //iContactlist.Add(New Contact(112, "Faith William", "faith@gmail.com", "AUC Nigeria", "Head Admin", "Finance and Admin"))
            //iContactlist.Add(New Contact(113, "Danyo Ade", "danyo@need-solution.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
            //iContactlist.Add(New Contact(114, "Bantele Aliu", "Bantele@reliance.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
            //iContactlist.Add(New Contact(115, "Koya Mudaishuru", "koya@Dynamic.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
            //iContactlist.Add(New Contact(116, "Jumoke Aliu", "jummy@Jide.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
            //iContactlist.Add(New Contact(117, "Danjuma Igoche", "igoche@need-solution.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
            //iContactlist.Add(New Contact(118, "John Audu", "john@need-solution.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))

            this.MultiView1.ActiveViewIndex = 1;
            PropContactList = null;
            PopulateGrid();
            this.msgBody = txtBody.Text;

        }
        //protected void GridView1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
        //        e.Row.Attributes["style"] = "cursor:pointer";
        //        e.Row.ToolTip = "Click to select this row.";
        //        e.Row.Attributes["onmouseover"] = "this.style.color='#000';";
        //        //Dim imgBtn As System.Web.UI.WebControls.ImageButton = e.Row.FindControl("editImageButton")

        //        //ScriptManager1.Controls.Remove(imgBtn)

        //        //ScriptManager1.RegisterPostBackControl(imgBtn)

        //        // Dim btn As WebControls.ImageButton = DirectCast(e.Row.FindControl("editImageButton"), WebControls.ImageButton)
        //        // btn.OnClientClick = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString)
        //        //btn.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString)

        //    }
        //}


        protected List<Contact> PropContactList
        {
            get { return (List<Contact>)ViewState["PropContactList"]; }
            set
            {
                ViewState["PropContactList"] = value;
                this.ddlAddress.DataSource = value;
                this.ddlAddress.DataBind();
            }
        }

        protected string msgBody
        {
            get { return ViewState["msgBody"].ToString(); }
            set { ViewState["msgBody"] = value; }
        }

        protected void Button62_Click(object sender, System.EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 0;
            //  Dim iCon As Contact = 
        }


        //protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //if (!(GridView1.SelectedIndex == -1))
        //{
        //    string ContactID = GridView1.SelectedValue.ToString();
        //    //Dim id As Integer = GridView1.SelectedRow.Cells(1).Text
        //    string CName = GridView1.SelectedRow.Cells[2].Text;
        //    string CEmail = GridView1.SelectedRow.Cells[3].Text;
        //    string Org = GridView1.SelectedRow.Cells[4].Text;
        //    string Dept = GridView1.SelectedRow.Cells[5].Text;
        //    string Desig = GridView1.SelectedRow.Cells[6].Text;
        //    Contact NewContact = new Contact(Convert.ToInt32(ContactID), CName, CEmail, Org, Desig, Dept);
        //    List<Contact> Addedlist = new List<Contact>();
        //    //   Addedlist = PropContactList
        //    if ((PropContactList != null))
        //    {
        //        foreach (Contact iEmail in PropContactList)
        //        {
        //            if (!(iEmail.EmailAddress == CEmail))
        //            {
        //                Addedlist.Add(new Contact(iEmail.ContactID, iEmail.FullName, iEmail.EmailAddress, iEmail.organisationName, iEmail.Designation, iEmail.Department));
        //            }
        //        }
        //        //Else
        //        //    Addedlist.Add(New Contact(ContactID, CName, CEmail, Org, Desig, Dept))
        //    }
        //    //Contact contact = new Contact()
        //    //{
        //    //    ContactID = Convert.ToInt32(ContactID),
        //    //    FullName = CName,
        //    //    EmailAddress = CEmail,
        //    //    organisationName = Org,
        //    //    Designation = Desig,
        //    //    Department = Dept
        //    //};
        //    //Addedlist.Add(contact);

        //    Addedlist.Add(new Contact(Convert.ToInt32(ContactID), CName, CEmail, Org, Desig, Dept));
        //    PropContactList = Addedlist;

        //}
        //}

        //protected void ddladdress_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        //{

        //try
        //{
        //    string EmailToDelete = e.CommandArgument.ToString();
        //    List<Contact> iNewList = new List<Contact>();
        //    foreach (Contact iemail in PropContactList)
        //    {
        //        if (iemail.EmailAddress != EmailToDelete)
        //        {
        //            iNewList.Add(new Contact(iemail.ContactID, iemail.FullName, iemail.EmailAddress, iemail.organisationName, iemail.Designation, iemail.Department));
        //        }
        //    }
        //    PropContactList = iNewList;
        //}
        //catch (Exception ex)
        //{
        //    //Me.litEr1.Text = ex.ToString
        //}
        // }

        //protected void Button3_Click(object sender, System.EventArgs e)
        //{
        //    getContact();
        //}

        //protected void Button12_Click(object sender, System.EventArgs e)
        //{
        //    this.MultiView1.ActiveViewIndex = 0;
        //    this.ddlAddressF.DataSource = this.PropContactList;
        //    this.ddlAddressF.DataBind();
        //    this.txtBody.Text = this.msgBody;
        //}

        [Serializable()]
        public class Contact
        {
            public string ContactID { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string organisationName { get; set; }
            //public string Department { get; set; }
            public string Designation { get; set; }

            public Contact()
            {
                //   Me.RowNumber = 0
            }

            public Contact(string IntID, string iName, string iEmail)
            {
                this.ContactID = IntID;
                this.FullName = iName;
                this.Email = iEmail;
            }
            public Contact(string IntID, string iName, string iEmail, string Company, string Position)// string Dept
            {
                this.ContactID = IntID;
                this.FullName = iName;
                this.Email = iEmail;
                this.organisationName = Company;
                //this.Department = Dept;
                this.Designation = Position;
            }
        }

        protected void btnGetContact_Click(object sender, EventArgs e)
        {
            getContact();
        }

        protected void btnSaveContact_Click(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 0;
            this.ddlAddressF.DataSource = this.PropContactList;
            this.ddlAddressF.DataBind();
            this.txtBody.Text = msgBody;
        }

        protected void BtnRemoveContact_Click(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (!(GridView1.SelectedIndex == -1))
            {
                //int rowIndex = GridView1.SelectedRow.RowIndex;
                //GridViewRow row = GridView1.SelectedRow;
                //string someting = row.Cells[rowIndex].Text;


                string ContactID = GridView1.SelectedValue.ToString();
                //Dim id As Integer = GridView1.SelectedRow.Cells(1).Text
                string CName = (GridView1.SelectedRow.FindControl("lblFullName") as System.Web.UI.WebControls.Label).Text; // GridView1.SelectedRow.Cells[2].Text;
                string CEmail = (GridView1.SelectedRow.FindControl("lblEmail") as System.Web.UI.WebControls.Label).Text; //GridView1.SelectedRow.Cells[3].Text;
                string Org = (GridView1.SelectedRow.FindControl("lblAccountName") as System.Web.UI.WebControls.Label).Text; //GridView1.SelectedRow.Cells[4].Text;
                //string Dept = (GridView1.SelectedRow.FindControl("lblContactGuid") as System.Web.UI.WebControls.Label).Text; //GridView1.SelectedRow.Cells[5].Text;
                string Desig = (GridView1.SelectedRow.FindControl("lblJobTitle") as System.Web.UI.WebControls.Label).Text; //GridView1.SelectedRow.Cells[6].Text;
                Contact NewContact = new Contact(ContactID, CName, CEmail, Org, Desig);
                List<Contact> Addedlist = new List<Contact>();
                //   Addedlist = PropContactList
                if ((PropContactList != null))
                {
                    foreach (Contact iEmail in PropContactList)
                    {
                        if (!(iEmail.Email == CEmail))
                        {
                            Addedlist.Add(new Contact(iEmail.ContactID, iEmail.FullName, iEmail.Email, iEmail.organisationName, iEmail.Designation));
                        }
                    }
                    //Else
                    //    Addedlist.Add(New Contact(ContactID, CName, CEmail, Org, Desig, Dept))
                }
                //Contact contact = new Contact()
                //{
                //    ContactID = Convert.ToInt32(ContactID),
                //    FullName = CName,
                //    EmailAddress = CEmail,
                //    organisationName = Org,
                //    Designation = Desig,
                //    Department = Dept
                //};
                //Addedlist.Add(contact);

                Addedlist.Add(new Contact(ContactID, CName, CEmail, Org, Desig));
                 PropContactList = Addedlist;
                 


                ddlAddress.DataSource = PropContactList;
                ddlAddress.DataBind();
            }
        }

        protected void GridView1_RowCreated1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.ToolTip = "Click to select this row.";
                e.Row.Attributes["onmouseover"] = "this.style.color='#000';";
                //Dim imgBtn As System.Web.UI.WebControls.ImageButton = e.Row.FindControl("editImageButton")

                //ScriptManager1.Controls.Remove(imgBtn)

                //ScriptManager1.RegisterPostBackControl(imgBtn)

                // Dim btn As WebControls.ImageButton = DirectCast(e.Row.FindControl("editImageButton"), WebControls.ImageButton)
                // btn.OnClientClick = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString)
                //btn.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString)

            }
        }

        protected void ddlAddress_ItemCommand1(object source, DataListCommandEventArgs e)
        {
            try
            {
                string EmailToDelete = e.CommandArgument.ToString();
                List<Contact> iNewList = new List<Contact>();
                foreach (Contact iemail in PropContactList)
                {
                    if (iemail.Email != EmailToDelete)
                    {
                        iNewList.Add(new Contact(iemail.ContactID, iemail.FullName, iemail.Email, iemail.organisationName, iemail.Designation));
                    }
                }
                PropContactList = iNewList;
            }
            catch (Exception ex)
            {
                //Me.litEr1.Text = ex.ToString
            }
        }

        

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //ConvertToPDF();
                CreateEmail();
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Email sent successfully";
                lblmsg.Visible = true;
                //Clear TextBoxes
                Clear();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.InnerException.ToString();
                lblmsg.Visible = true;

                File.AppendAllText(Server.MapPath("Error.txt"), "Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace + "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                string New = Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine;
                File.AppendAllText(Server.MapPath("Error.txt"), New);
            }
        }

        protected void hyp_Click(object sender, EventArgs e)
        {
            DownloadPDF();
        }


        #region CRM_Methods
        /// <summary>
        /// GetAllValues
        /// </summary>
        public void GetAllValues()
        {
            try
            {
                CRMEntity crm = new CRMEntity();
                ///URLFormat: toWindow.aspx?quotenumber=QUO-01236-N0Z4S7&guidValue=7C5DB9C2-ACB4-E711-8121-70106FA5DD11
                //var quote = crm.GetQuote("QUO-01236-N0Z4S7", "7C5DB9C2-ACB4-E711-8121-70106FA5DD11");
                //var quoteDetail = crm.GetQuoteDetails("QUO-01236-N0Z4S7", "7C5DB9C2-ACB4-E711-8121-70106FA5DD11");

                string quoteNumber = HttpContext.Current.Request.QueryString["quotenumber"];// Request.Url.Query[0].ToString();
                string id = HttpContext.Current.Request.QueryString["guidValue"]; //Request.Url.Query[1].ToString();

                txtGuid.Text = id;
                txtQuote.Text = quoteNumber;

                if (string.IsNullOrEmpty(quoteNumber) && string.IsNullOrEmpty(id))
                {
                    Exception ex = new ArgumentNullException();
                    File.AppendAllText(Server.MapPath("~/Error.txt"), "Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace + "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    string New = Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine;
                    File.AppendAllText(Server.MapPath("~/Error.txt"), New);
                }


                var quote = crm.GetQuote(txtQuote.Text.ToString(), txtGuid.Text.ToString());
                var quoteDetail = crm.GetQuoteDetails(txtQuote.Text.ToString(), txtGuid.Text.ToString());

                fromLogicalName = quote.Select(x => x.CreatedbyLogicalName).SingleOrDefault();
                toLogicalName = quote.Select(x => x.ContactLogicalName).SingleOrDefault();
                quoteLogicalName = quote.Select(x => x.QuoteLogicalName).SingleOrDefault();

                fromGuid = quote.Select(x => x.CreatedbyGuid).SingleOrDefault();
                toGuid = quote.Select(x => x.ContactGuid).SingleOrDefault();
                quoteGuid = quote.Select(x => x.QuoteId).SingleOrDefault();
                customerGuid = quote.Select(x => x.CustomerGuid).SingleOrDefault();

                toName = quote.Select(x => x.ContactPerson).SingleOrDefault();
                fromName = quote.Select(x => x.CreatedbyName).SingleOrDefault();
                txtFrom.Text = fromName;
                MyTxtTo.Text = toName;

                DataTable GetQuoteDt = new DataTable("GetQuoteTb");
                DataTable GetQuoteDetailsDt = new DataTable("GetQuoteDetailsTb");
                GetQuoteDt = ConvertToDataTable(quote);
                GetQuoteDetailsDt = ConvertToDataTable(quoteDetail);

                DataSet ds1 = new DataSet("GetQuoteDs");
                DataSet ds2 = new DataSet("GetQuoteDetailDs");
                ds1.Tables.Add(GetQuoteDt);
                ds2.Tables.Add(GetQuoteDetailsDt);

                LocalReport report = new LocalReport();

                //var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                //string path = uri.GetLeftPart(UriPartial.Path);
                //path = path+"/Reports/QuoteReportt.rdlc";
                //lblMsgError.Text = Server.MapPath("QuoteReportt.rdlc");

                report.ReportPath = Server.MapPath("QuoteReportt.rdlc"); //Server.MapPath(@"Reports/QuoteReportt.rdl");//Server.MapPath("Reports/QuoteReportt.rdl");
                ReportDataSource rd1 = new ReportDataSource("DataSet1", GetQuoteDt);
                ReportDataSource rd2 = new ReportDataSource("DataSet2", GetQuoteDetailsDt);

                report.DataSources.Add(rd1);
                report.DataSources.Add(rd2);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                pdfBytes = report.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                //Build the HTML
                //string TemplatePath = "~/Templates/QuoteTemplate.html";
                //string imagePath = HttpContext.Current.Request.Url.AbsoluteUri + "/images/QuoteImg.PNG";
                //string notePath = HttpContext.Current.Request.Url.AbsoluteUri + "/images/Notes.PNG";
               // htmlBody = File.ReadAllText(HttpContext.Current.Server.MapPath(TemplatePath));
                //msgBody = msgBody.Replace("$relianceNote", notePath);
                //msgBody = msgBody.Replace("$relianceLogo", imagePath);
                //htmlBody = htmlBody.Replace("$customeridname", quote.Select(x => x.Customername).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$address1_line1", quote.Select(x => x.Address).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$address1_city", quote.Select(x => x.AddressCity).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$address1_stateorprovince", quote.Select(x => x.AddressState).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$Zip", quote.Select(x => x.Zip).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$new_contactpersonname", toName);
                //htmlBody = htmlBody.Replace("$createdon", quote.Select(x => x.Createdon).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$quotenumber", quote.Select(x => x.QuoteNumber).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$createdbyname", toName);
                //htmlBody = htmlBody.Replace("$address1_telephone1", quote.Select(x => x.CreatedbyNumber).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$new_customtotal_base", quote.Select(x => x.SubTotal).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$new_quotevat", quote.Select(x => x.Vat).SingleOrDefault());
                //htmlBody = htmlBody.Replace("$new_grandtotal_base", quote.Select(x => x.Total).SingleOrDefault());

                //string quoteDetailTable = string.Empty;
                //string quantityDetailTable = string.Empty;
                //string descriptionDetailTable = string.Empty;
                //string unitPriceDetailTable = string.Empty;
                //string totalDetailTable = string.Empty;
                //foreach (var item_q in quoteDetail)
                //{
                //    string quantity = item_q.Quantity.ToString();
                //    string description = item_q.ProductName.ToString();
                //    string unitPrice = item_q.Priceperunitbase.ToString();
                //    string total = item_q.ExtendedAmountbase.ToString();
                //    /*Generate quantity*/
                //    //double quantityTop = 473.356;
                //    //double DescripionTop = 423.043;
                //    //double UnitPriceTop = 470.888;
                //    //double TotalNGNTop = 470.888;
                //    //for (int i = 0; i < item_q.Quantity.Count(); i++)
                //    //{
                //    //                    string quantityDiv = @"<div style='position:absolute;top:{0}px;left:108.309px;'>
                //    //                                                <span style='font-size:14.108px;font-weight:bold;'>{1}</span>
                //    //                                            </div>";
                //    //                    quantityDetailTable = quantityDetailTable + string.Format(quantityDiv, quantityTop, quantity);
                //    //                    quantityTop = +45.214;

                //    //                    string DescriptionDiv = @"<div style='position:absolute;top:{0}px;left:152.158px;'>
                //    //                                                    <span style='font-size:14.108px;'>{1}</span>
                //    //                                                 </div>";
                //    //                    descriptionDetailTable = descriptionDetailTable + string.Format(DescriptionDiv, DescripionTop, description);
                //    //                    DescripionTop = DescripionTop + 45.214;

                //    //                    string UnitPriceDiv = @"<div style='position:absolute;top:{0}px;left:470.426px;'>
                //    //                                                    <span style='font-size:14.108px;'>₦{1}</span>
                //    //                                                </div>";
                //    //                    unitPriceDetailTable = unitPriceDetailTable + string.Format(UnitPriceDiv, UnitPriceTop, unitPrice);
                //    //                    UnitPriceTop = +45.214;

                //    //                    string TotalNGNDiv = @"<div style='position:absolute;top:{0}px;left:672.741px;'>
                //    //                                                <span style='font-size:14.108px;'>₦{1}</span>
                //    //                                            </div>";
                //    //                    totalDetailTable = totalDetailTable + string.Format(TotalNGNDiv, TotalNGNTop, total);
                //    //                    TotalNGNTop = +45.214;
                //    //}

                //    //string formatThis = "<tr><td width='10%' height='27'><strong>"+quantity+"</strong></td><td width='45%'>"+description+"</td><td width='25%'>"+unitPrice+"</td><td width='20%'>"+total+"</td></tr>";
                //    quoteDetailTable = quoteDetailTable + "<tr border='0'><td width='10%' height='27'><strong>" + quantity + "</strong></td><td width='45%'>" + description + "</td><td width='25%' style='text-align:right;'>" + unitPrice + "</td><td style='text-align:right;' width='20%'>" + total + "</td></tr>";
                //    //quoteDetailTable = quoteDetailTable + string.Format(formatThis, quantity, description, unitPrice, total);
                //}
                ////msgBody = msgBody.Replace("$quantity", quantityDetailTable);
                ////msgBody = msgBody.Replace("$Description", descriptionDetailTable);
                ////msgBody = msgBody.Replace("$UnitPrice", unitPriceDetailTable);
                ////msgBody = msgBody.Replace("$TotalNGN", totalDetailTable);
                //htmlBody = htmlBody.Replace("$quoteDetails", quoteDetailTable);
                //return msgBody;
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
                lblmsg.Visible = true;

                //lblMsgErr.Text = Server.MapPath("Error.txt");
                File.AppendAllText(Server.MapPath("Error.txt"), "Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace + "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                string New = Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine;
                File.AppendAllText(Server.MapPath("Error.txt"), New);
                //return msgBody;
            }

        }

        //private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        //{
        //    string nuName = Guid.NewGuid().ToString().Replace("-", string.Empty);
        //    Stream stream = new FileStream("path" + fileNameExtension, FileMode.Create);
        //    ms_streams.Add(stream);
        //    return stream;
        //}
        /// <summary>
        /// Create Email
        /// </summary>
        public void CreateEmail()
        {
            ClientCredentials cred = CRMEntity.getCredentials();

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


                    //Loop through AddressDataList
                    int totalCC = PropContactList.Count;
                    //EntityCollection cc = new EntityCollection();
                    List<ActivityParty> ccActivityPartyList = new List<ActivityParty>();
                    foreach (var item in PropContactList)
                    {
                        ActivityParty ccParty = new ActivityParty
                        {
                            PartyId = new EntityReference(toLogicalName, new Guid(item.ContactID))
                        };
                        ccActivityPartyList.Add(ccParty);
                    }

                    //for (int i = 0; i < totalCC; i++)
                    //{
                    //    ActivityParty ccPartyi = new ActivityParty
                    //    {
                    //        PartyId = new EntityReference("contatct", new Guid(PropContactList.Select(g => g.ContactID).SingleOrDefault()))
                    //    };
                    //    //email.Cc = new ActivityParty[] { ccPartyi }; //This gets the last cc: Not going to work.
                    //    //cc.Entities.Add(ccPartyi);
                    //    ccActivityPartyList.Add(ccPartyi);
                    //}

                    // Create an e-mail message.
                    Email email = new Email
                    {
                        To = new ActivityParty[] { toParty },
                        From = new ActivityParty[] { fromParty },
                        Subject = TxtSubject.Text.Trim(),
                        Description = msgBody,
                        DirectionCode = true,
                        Cc = ccActivityPartyList,
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

        /// <summary>
        /// ConvertToPDF
        /// </summary>
        public void ConvertToPDF()
        {
            try
            {
                //convert HTML TO PDF
                //StringBuilder sb = new StringBuilder(msgBody);
                StringReader sr = new StringReader(htmlBody);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                HTMLWorker htmlParser = new HTMLWorker(pdfDoc);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter wr = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    htmlParser.Parse(sr);
                    //XMLWorkerHelper.GetInstance().ParseXHtml(wr, pdfDoc, sr);
                    pdfDoc.Close();

                    //Get pdfBytes
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

        /// <summary>
        /// Download PDF
        /// </summary>
        /// <param name="pdf"></param>
        public void DownloadPDF()
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=RelianceQuote.pdf");
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(pdfBytes);
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.InnerException.ToString();
                lblmsg.Visible = true;
                File.AppendAllText(Server.MapPath("Error.txt"), "Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace + "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                string New = Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine;
                File.AppendAllText(Server.MapPath("Error.txt"), New);
            }
        }

        /// <summary>
        /// PopulateGrid
        /// </summary>
        public void PopulateGrid()
        {
            CRMEntity entityRelatedContact = new CRMEntity();
            var entityRelatedContactList = entityRelatedContact.GetAccountContact(customerGuid.ToString());
            //Session["entityRelatedContactList"] = entityRelatedContact.GetAccountContact(customerGuid.ToString());
            GridView1.DataSource = entityRelatedContactList;
            GridView1.DataBind();
        }

        /// <summary>
        /// Convert List to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        /// <summary>
        /// 2nd Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public void Clear()
        {
            TxtSubject.Text = string.Empty;
            txtBody.Text = string.Empty;
            PropContactList = new List<Contact>();
        }
        #endregion
    }

}