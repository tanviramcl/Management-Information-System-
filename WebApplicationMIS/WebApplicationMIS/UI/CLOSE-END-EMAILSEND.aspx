<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="CLOSE-END-EMAILSEND.aspx.cs" Inherits="WebApplicationMIS.UI.CLOSE_END_EMAILSEND" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Mail send </h3>
                    <div class="card">
                        <%--  <h5 class="card-header">Service register Form</h5>--%>
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                    <label for="input-select">Fund Name</label>
                                    <asp:DropDownList ID="fundNameDropDownList" class="form-control" runat="server" TabIndex="6"></asp:DropDownList>

                                </div>
                            </div>
                             <div class="form-row">
                                <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                    <label for="input-select">Attachment:</label>
                                    <asp:FileUpload ID="FileUpload" runat="server" /> 

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
                </div>

            </div>
        </div>





    </form>





    <script type="text/javascript">


    </script>


</asp:Content>
