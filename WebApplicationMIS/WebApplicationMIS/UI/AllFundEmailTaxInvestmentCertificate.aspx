<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="AllFundEmailTaxInvestmentCertificate.aspx.cs" Inherits="WebApplicationMIS.UI.AllFundEmailTaxInvestmentCertificate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <form id="aspnetForm" runat="server" method="post">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                <label for="inputDefault" class="col-form-label">Fiscal Year:</label>
                <asp:DropDownList ID="incomeTaxFYDropDownList" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
            </div>
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                <label for="inputEmail">FYPart:</label>
                <asp:DropDownList ID="fyPartDropDownList" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
            </div>
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                <label for="inputDefault" class="col-form-label">Close Date:</label>
                <asp:TextBox ID="CloasingTextBox" class="form-control" runat="server"></asp:TextBox>

            </div>
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                <asp:Button ID="ButtonSearch" Text="Search" Visible="true" class="btn btn-secondary" runat="server" OnClick="SearchButton_Click" />
                <%--  <asp:Button ID="ButtonSendMail" Text="Send Mail"  Visible="false"  class="btn btn-secondary" runat="server" OnClick="SendEmailButton_Click" />--%>
            </div>

        </form>
    </div>
    <div class="row">
        <!-- ============================================================== -->
        <!-- basic table  -->
        <!-- ============================================================== -->

        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">
                <h5 class="card-header">Dividend List</h5>
                <div class="card-body">
                    <div class="table-responsive">


                        <table id="example" class="table table-striped table-bordered" style="width: 100%">
                            <thead>
                                <tr>

                                    <th>Fund Id</th>
                                    <th>Fund NAME</th>
                                    <th>Fund Type</th>
                                    <th>Tax Certificate Genarate</th>
                                     <th>Tax Certificate Email</th>
                                    <th>INVESTMENT Certificate Genarate</th>
                                     <th>INVESTMENT Certificate Email</th>
                                    <th>Action</th>
                                     <th>Action</th>
                                    <%-- <th>Action</th>--%>
                                </tr>
                            </thead>
                            <tbody>
                                <%

                                    System.Data.DataTable dtAllFund = (System.Data.DataTable)Session["dtFundName"];

                                    if (dtAllFund != null && dtAllFund.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtAllFund.Rows.Count; i++)
                                        {
                                %>
                                <tr>

                                    <td align="center"><%   Response.Write(dtAllFund.Rows[i]["F_cd"].ToString());   %> </td>
                                    <td align="center"><%   Response.Write(dtAllFund.Rows[i]["F_NAME"].ToString());   %></td>
                                    <td align="center"><%   Response.Write(dtAllFund.Rows[i]["F_TYPE"].ToString());   %></td>
                                    <td align="center"><%   Response.Write(dtAllFund.Rows[i]["TAX_CERT_FILE_status"].ToString());   %></td>
                                    <td align="center"><%   Response.Write(dtAllFund.Rows[i]["TAX_CERT_MAIL_SEND"].ToString());   %></td>
                                    <td align="center"><%   Response.Write(dtAllFund.Rows[i]["INV_CERT_FILE_STATUS"].ToString());   %></td>
                                    <td align="center"><%   Response.Write(dtAllFund.Rows[i]["INV_CERT_MAIL_SEND"].ToString());   %></td>
                                    <% string tax_cert_file = dtAllFund.Rows[i]["TAX_CERT_FILE_status"].ToString();
                                        if (string.IsNullOrEmpty(tax_cert_file))
                                        {
                                    %>
                                    <td align="center"><a href="Email_Tax_InvestmentCertificate.aspx?F_CD=<% Response.Write(dtAllFund.Rows[i]["F_cd"].ToString());%>&Action=PDFGenarate" class="btn btn-secondary">Income Tax Pdf Generate</a></td>
                                     
                                    <%
                                        }
                                        else
                                        {
                                            %>
                                            <td></td>
                                    <%
                                        }
                                    %>
                                        
                                    <% string investment_cert=dtAllFund.Rows[i]["INV_CERT_FILE_STATUS"].ToString();
                                        if (string.IsNullOrEmpty(investment_cert))
                                        {
                                             %>
                                      <td align="center"><a href="Email_Tax_InvestmentCertificate.aspx?F_CD=<% Response.Write(dtAllFund.Rows[i]["F_cd"].ToString());%>&Action=InvestmentPDFGenarate" class="btn btn-secondary">Investment cert. Pdf Generate</a></td>
                                     <%
                                        }
                                        else {
                                               %>
                                            <td></td>
                                    <%
                                        }
                                        %>
                                  
                                </tr>

                                <%

                                        }
                                    }

                                %>
                            </tbody>

                        </table>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $('#<%=CloasingTextBox.ClientID%>').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            //maxDate:"today",
            onSelect: function (selected) {

            }
        });
        $('#example').DataTable();

    </script>
</asp:Content>
