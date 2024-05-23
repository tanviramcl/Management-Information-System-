<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailSend.aspx.cs" MasterPageFile="~/UI/AMCLCommon.master" Inherits="WebApplicationMIS.UI.MailSend" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="aspnetForm" runat="server" method="post">
        <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Mail send </h3>

                </div>
                <div class="card">
                    <%--  <h5 class="card-header">Service register Form</h5>--%>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="ccol-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Certificate Type:</label>
                                <asp:DropDownList runat="server" class="form-control" ID="DropdownlistCert" OnSelectedIndexChanged="Certificatetype_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Text="Income Tax Certificate" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Investment Certificate" Value="2"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Fund Name</label>
                                <asp:DropDownList ID="fundNameDropDownList" class="form-control" runat="server" TabIndex="6"></asp:DropDownList>

                            </div>
                        </div>
                       <%-- <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Branch Name:</label>
                                <asp:DropDownList ID="branchDropDownList" class="form-control" runat="server" TabIndex="6"></asp:DropDownList>

                            </div>
                        </div>--%>
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Fiscal Year:</label>
                                <asp:DropDownList ID="incomeTaxFYDropDownList" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputEmail">FYPart:</label>
                                <asp:DropDownList ID="fyPartDropDownList" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>

                            </div>
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
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Close Date:</label>
                                <asp:TextBox ID="CloasingTextBox" class="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-row">

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">Subject</label>
                                <asp:TextBox ID="txtSubject" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                                <label for="exampleFormControlTextarea1">Message</label>
                                <textarea id="TextareaMessage" cols="20" class="form-control" rows="2" runat="server"></textarea>
                            </div>
                        </div>



                        <%--  </form>--%>
                    </div>
                </div>
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                    <div>

                        <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 24px;"></asp:Label>
                        <br />


                        <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                            <asp:Button ID="ButtonSubmitt" Text="Send" class="btn btn-primary" OnClick="sendButton_Click" runat="server" OnClientClick="fnCloseModal();" />
                        </div>

                    </div>
                </div>
                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 ">
                </div>
            </div>
        </div>

    </form>
    <script type="text/javascript">


        $('#<%=CloasingTextBox.ClientID%>').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            //maxDate:"today",
            onSelect: function (selected) {

            }
        });
    </script>


</asp:Content>
