<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="Email_Tax_InvestmentCertificate.aspx.cs" Inherits="WebApplicationMIS.UI.Email_Tax_InvestmentCertificate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="aspnetForm" runat="server" method="post">

        <div class="card">
            <div class="card-body">
                <div class="row">

                    <div class="form-row">

                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">Certificate Type:</label>
                            <asp:DropDownList runat="server" class="form-control" ID="DropdownlistCert" AutoPostBack="True">
                                <asp:ListItem Text="Income Tax Certificate" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Investment Certificate" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">Fund Name:</label>
                            <asp:DropDownList ID="fundNameDropDownList" class="form-control" runat="server" TabIndex="6"></asp:DropDownList>
                        </div>

                        <%--                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">Branch Name:</label>
                            <asp:DropDownList ID="branchDropDownList" class="form-control" runat="server" TabIndex="6"></asp:DropDownList>

                        </div>--%>


                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">Fiscal Year:</label>
                            <asp:DropDownList ID="incomeTaxFYDropDownList" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </div>



                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">Close Date:</label>
                            <asp:TextBox ID="CloasingTextBox" class="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputEmail">FYPart:</label>
                            <asp:DropDownList ID="fyPartDropDownList" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">FY From:</label>
                            <asp:TextBox ID="FYFromTextBox" class="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">FY To:</label>
                            <asp:TextBox ID="FYToTextBox" class="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">Investment Type:</label>
                            <asp:DropDownList runat="server" class="form-control" ID="ddlInvestmentType" AutoPostBack="True">
                                <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                                <asp:ListItem Text="CIP" Value="1"></asp:ListItem>
                                <asp:ListItem Text="NON CIP" Value="2"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <label class="col-form-label">Range</label>
                            <asp:TextBox ID="txtFrom" class="form-control" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtTo" class="form-control" runat="server"></asp:TextBox>
                        </div>

                    </div>

                    <div class="form-row">
                    </div>

                    <div class="form-row ">
                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                            <div>

                                <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 24px; color: green;"></asp:Label>
                                <br />




                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">

                            <asp:Button ID="ButtonPdfGenarate" Text="Pdf Generate" Visible="false" class="btn btn-secondary" runat="server" OnClick="pdfGenarateButton_Click" OnClientClick="fnCloseModal();" />
                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <asp:Button ID="ButtonClear" Text="Clear" Visible="false" class="btn btn-secondary" runat="server" OnClick="ClearButton_Click" />
                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <asp:Button ID="ButtonMaxREgNumber" Text="Get Maximum register" Visible="false" class="btn btn-secondary" runat="server" OnClick="ButtonMaxREgNumber_Click" />
                        </div>
                </div>

            </div>

        </div>
        </div>
    </form>

    <script type="text/javascript">


        function fnCloseModal() {



            $('#<%=CloasingTextBox.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                //maxDate:"today",
                onSelect: function (selected) {

                }
            });
            $('#<%=FYFromTextBox.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                //maxDate:"today",
                onSelect: function (selected) {

                }
            });
            $('#<%=FYToTextBox.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                //maxDate:"today",
                onSelect: function (selected) {

                }
            });
        }
    </script>


</asp:Content>

