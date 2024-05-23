<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloseEndMailSend.aspx.cs" MasterPageFile="~/UI/AMCLCommon.master" Inherits="WebApplicationMIS.UI.CloseEndMailSend" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="aspnetForm" runat="server" method="post">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="section-block" id="basicform">
                            <h3 class="section-title">CLose END-Mail send </h3>


                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Fund Name:</label>
                                <asp:DropDownList ID="fundNameDropDownList" class="form-control" runat="server" TabIndex="6"></asp:DropDownList>
                            </div>


                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Fiscal Year:</label>
                                <asp:DropDownList ID="fyDropDownList" class="form-control" runat="server" OnSelectedIndexChanged="fyDropDownList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputEmail">Record Date:</label>
                                <asp:DropDownList ID="recordDateDropDownList" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>

                            </div>
                           
                          

                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">Subject</label>
                                <asp:TextBox ID="txtSubject" class="form-control" runat="server"></asp:TextBox>
                            </div>
                       
                    
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="exampleFormControlTextarea1">Message</label>
                                <textarea id="TextareaMessage" cols="20" class="form-control" rows="2" runat="server"></textarea>
                            </div>
                       
                             <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                                <asp:Button ID="ButtonMaillSend" Text="Send Mail" Visible="true" class="btn btn-secondary" runat="server" OnClick="sendButton_Click" OnClientClick="fnCloseModal();" />
                            </div>
                             <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 24px; "></asp:Label>
                        </div>
                    </div>
                </div>
                </div>
            </div>
    </form>
    <script type="text/javascript">
  
    </script>


</asp:Content>
