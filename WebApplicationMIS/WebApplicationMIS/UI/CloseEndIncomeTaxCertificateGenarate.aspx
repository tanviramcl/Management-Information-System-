<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master"  CodeBehind="CloseEndIncomeTaxCertificateGenarate.aspx.cs" Inherits="WebApplicationMIS.UI.CloseEndIncomeTaxCertificateGenarate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="aspnetForm" runat="server" method="post">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="card">
            <div class="card-body">
                <div class="row">

                    <div class="form-row">

                           
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Fund Name:</label>
                                <asp:DropDownList ID="fundNameDropDownList" class="form-control" runat="server" TabIndex="6"></asp:DropDownList>
                            </div>


                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputDefault" class="col-form-label">Fiscal Year:</label>
                             <asp:DropDownList ID="fyDropDownList"   class="form-control"  runat="server" OnSelectedIndexChanged="fyDropDownList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                            <label for="inputEmail">Record Date:</label>
                             <asp:DropDownList ID="recordDateDropDownList" class="form-control"  runat="server" AutoPostBack="True"></asp:DropDownList>

                        </div>
                      
            
                         <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">

                             <div>
                                 <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 24px; "></asp:Label>
                                        <br />
                                    <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">

                                        <asp:Button ID="ButtonPdfGenarate" Text="Pdf Generate" Visible="false" class="btn btn-secondary" runat="server" OnClick="pdfGenarateButton_Click" OnClientClick="fnCloseModal();" />
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                         <asp:Button ID="ButtonClear" Text="Clear" Visible="false" class="btn btn-secondary" runat="server" OnClick="ClearButton_Click" />
                                     </div>
                                       <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                                <asp:Button ID="ButtonMaxREgNumber" Text="Number of pdf" Visible="false" class="btn btn-secondary" runat="server" OnClick="ButtonMaxREgNumber_Click" />
                                     </div>
                              </div>


                       <%--     <div>

                                <asp:UpdateProgress ID="updProgress"
                                    AssociatedUpdatePanelID="UpdatePanel1"
                                    runat="server">
                                    <ProgressTemplate>
                                        <img src="../Image/Processing.gif" alt="processing" style="width: 186px; height: 128px; margin-left: 36px" />

                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 24px; "></asp:Label>
                                        <br />

                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                               
                                         <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">

                                                 <asp:Button ID="ButtonPdfGenarate" Text="Pdf Generate" Visible="false" class="btn btn-secondary" runat="server" OnClick="pdfGenarateButton_Click" OnClientClick="fnCloseModal();" />
                                             </div>
                                         <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                                <asp:Button ID="ButtonClear" Text="Clear" Visible="false" class="btn btn-secondary" runat="server" OnClick="ClearButton_Click" />
                                              </div>
                                       
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>--%>
                        </div>

                      
                    
                </div>

            </div>

         </div>
            </div>
    </form>

    <script type="text/javascript">


       <%-- function fnCloseModal() {

            if (confirm("Do you want to Proceed......!!!!")) {
                //  $("#HiddenField1").val("Yes");
                $("#<%=HiddenField1.ClientID%>").val("Yes");
            } else {
                // $("#HiddenField1").val("No");
                $("#<%=HiddenField1.ClientID%>").val("No");
            }
        }--%>

       
    </script>


</asp:Content>
