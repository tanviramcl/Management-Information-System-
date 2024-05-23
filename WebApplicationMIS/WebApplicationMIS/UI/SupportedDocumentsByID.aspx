<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="SupportedDocumentsByID.aspx.cs" Inherits="WebApplicationMIS.UI.SupportedDocumentsByID" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <!-- ============================================================== -->
            <!-- basic table  -->
            <!-- ============================================================== -->
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="card">
                    <h5 class="card-header">Supported Document List</h5>
                    <div class="card-body">
                        <div class="table-responsive">

                            <div class="row">
                                <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12">
                                    <div class="card">
                                       <%-- <h5 class="card-header">Bordered Table</h5>--%>
                                        <div class="card-body">
                                            <table class="table table-bordered">
                                                <thead>
                                                    <tr>

                                                        <th colspan="4" scope="col">Supported Document</th>
                                                        <th scope="col">Value</th>
                                                        <%--  <th scope="col">Value</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%

                                                        System.Data.DataTable dtallSupportedDoc = (System.Data.DataTable)Session["AllSupportedDucuments"];
                                                        if (dtallSupportedDoc.Rows.Count > 0)
                                                        {
                                                            for (int i = 0; i < dtallSupportedDoc.Rows.Count; i++)
                                                            {
                                                    %>

                                                    <tr>
                                                        <td colspan="4">Complain Id</td>
                                                        <td> <asp:TextBox ID="TextBoxBRESISTER_ID" class="form-control" readonly="true" AutoPostBack="true" runat="server"></asp:TextBox></td>
                                      
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Document ID</td>
                                                        <td> <asp:TextBox ID="TextBoxSUP_DOC_ID" readonly="true" class="form-control"  runat="server"></asp:TextBox></td>
                                                   
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Copy of BO Set up</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxBO_COPY" class="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                                                                    
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Copy of NID</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxNID_COPY" class="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                      
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Copy of e-TIN</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxE_TIN_COPY" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                      
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Applicant passport size photo</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxAPPLICANT_PIC" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["APPLICANT_PIC"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Nominee passport size photo</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxNOMINEE_PIC" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["NOMINEE_PIC"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Photocopy of check book leaf</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxCHECK_BOOK" class="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["CHECK_BOOK"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Photocopy of utility bill</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxUTILITY_BILL" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["UTILITY_BILL"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Copy of Original Certificates</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxORIGINAL_CERT" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["ORIGINAL_CERT"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Signature Screen Print Copy</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxSIG_SCREEN_PRINT" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["SIG_SCREEN_PRINT"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Allotment Letter</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxALLOTMENT_LETTER" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["ALLOTMENT_LETTER"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Affidavit</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxAFFIDAVIT" class="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["AFFIDAVIT"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">General Dairy-GD</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxGD" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["GD"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Indemnity Bond</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxINDEMNITY_BOND" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["INDEMNITY_BOND"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Paper Advertisement</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxPAPER_ADD" class="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["PAPER_ADD"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Last year dividend notice copy</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxDIVIDEND_NOTICE" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["DIVIDEND_NOTICE"].ToString());    %></td>--%>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="4">Death certificates</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxDEATH_CERTIFICATES" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["DEATH_CERTIFICATES"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Suggestion of high court</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxSUGG_HIGHCOURT" class="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["SUGG_HIGHCOURT"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Power of Attorney</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxPOWER_ATTORNEY" class="form-control"  runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><%     Response.Write(dtallSupportedDoc.Rows[i]["POWER_ATTORNEY"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Certificate of cementry</td>
                                                        <td>
                                                            <asp:textbox id="TextBoxCERT_CEMENTRY" class="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <%--<td><% Response.Write(dtallSupportedDoc.Rows[i]["CERT_CEMENTRY"].ToString());    %></td>--%>
                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:button id="ButtonUpdate" text="update" class="btn btn-primary" runat="server" />
                                                        </td>
                                                        <%--<td><% Response.Write(dtallSupportedDoc.Rows[i]["CERT_CEMENTRY"].ToString());    %></td>--%>
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

                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $.validator.addMethod("CheckValue", function (value, element, param) {  
            if (value != 'Y' && value != 'N' )  
                return false;  
            else  
                return true;  
        },"Please select a Fund Type."); 

        $("#aspnetForm").validate({
            submitHandler: function () {
                SupportedDocument();
            },
            rules: 
            {
              <%--  <%=TextBoxBO_COPY.UniqueID %>: {
                  
                },
                <%=TextBoxNID_COPY.UniqueID %>: {
                    CheckValue: true 
                },
                <%=TextBoxE_TIN_COPY.UniqueID %>: {
                    CheckValue: true 
                },
                <%=TextBoxAPPLICANT_PIC.UniqueID %>: {
                    CheckValue: true 
                },
                <%=TextBoxCHECK_BOOK.UniqueID %>: {
                    CheckValue: true 
                },
                <%=TextBoxUTILITY_BILL.UniqueID %>: {
                    CheckValue: true
                },
                <%=TextBoxORIGINAL_CERT.UniqueID %>: {
                    CheckValue:true
                },
                <%=TextBoxSIG_SCREEN_PRINT.UniqueID %>: {
                    CheckValue: true
                },
                <%=TextBoxALLOTMENT_LETTER.UniqueID %>: {
                    CheckValue: true
                },
                <%=TextBoxAFFIDAVIT.UniqueID %>: {
                    CheckValue: true 
                }
                ,
                <%=TextBoxGD.UniqueID %>: {
                    CheckValue: true 
                }
                ,
                <%=TextBoxINDEMNITY_BOND.UniqueID %>: {
                    CheckValue: true 
                }
                ,
                <%=TextBoxPAPER_ADD.UniqueID %>: {
                    CheckValue: true 
                }
                ,
                <%=TextBoxDEATH_CERTIFICATES.UniqueID %>: {
                    CheckValue: true 
                }
                ,
                <%=TextBoxSUGG_HIGHCOURT.UniqueID %>: {
                    CheckValue: true 
                }
                ,
                <%=TextBoxDIVIDEND_NOTICE.UniqueID %>: {
                    CheckValue: true 
                }
               ,
                <%=TextBoxCERT_CEMENTRY.UniqueID %>: {
                    CheckValue: true 
                }--%>
            }, 
            messages: {
               
            }
        });

        function SupportedDocument() {

            var jsonObjM = [];
         

            var RESISTER_ID = $("#<%=TextBoxBRESISTER_ID.ClientID%>").val();
            var SUP_DOC_ID = $("#<%=TextBoxSUP_DOC_ID.ClientID%>").val();
            var BO_COPY = $("#<%=TextBoxBO_COPY.ClientID%>").val();
            var NID_COPY = $("#<%=TextBoxNID_COPY.ClientID%>").val();
            var E_TIN_COPY = $("#<%=TextBoxE_TIN_COPY.ClientID%>").val();
            var APPLICANT_PIC = $("#<%=TextBoxAPPLICANT_PIC.ClientID%>").val();
            var NOMINEE_PIC = $("#<%=TextBoxNOMINEE_PIC.ClientID%>").val();
            var CHECK_BOOK = $("#<%=TextBoxCHECK_BOOK.ClientID%>").val();
            var UTILITY_BILL = $("#<%=TextBoxUTILITY_BILL.ClientID%>").val();
            var ORIGINAL_CERT = $("#<%=TextBoxORIGINAL_CERT.ClientID%>").val();
            var SIG_SCREEN_PRINT = $("#<%=TextBoxSIG_SCREEN_PRINT.ClientID%>").val();
            var ALLOTMENT_LETTER = $("#<%=TextBoxALLOTMENT_LETTER.ClientID%>").val();
            var AFFIDAVIT = $("#<%=TextBoxAFFIDAVIT.ClientID%>").val();
            var GD = $("#<%=TextBoxGD.ClientID%>").val();
            var INDEMNITY_BOND = $("#<%=TextBoxINDEMNITY_BOND.ClientID%>").val();
            var PAPER_ADD = $("#<%=TextBoxPAPER_ADD.ClientID%>").val();
            var DIVIDEND_NOTICE = $("#<%=TextBoxDIVIDEND_NOTICE.ClientID%>").val();
            var DEATH_CERTIFICATES = $("#<%=TextBoxDEATH_CERTIFICATES.ClientID%>").val();
            var SUGG_HIGHCOURT = $("#<%=TextBoxSUGG_HIGHCOURT.ClientID%>").val();
            var POWER_ATTORNEY = $("#<%=TextBoxPOWER_ATTORNEY.ClientID%>").val();
            var CERT_CEMENTRY = $("#<%=TextBoxCERT_CEMENTRY.ClientID%>").val();

            var itemM = {};
            itemM["RESISTER_ID"] = RESISTER_ID;
            itemM["SUP_DOC_ID"] = SUP_DOC_ID;
            itemM["BO_COPY"] = BO_COPY;
            itemM["NID_COPY"] = NID_COPY;
            itemM["E_TIN_COPY"] = E_TIN_COPY;
            itemM["APPLICANT_PIC"] = APPLICANT_PIC;
            itemM["NOMINEE_PIC"] = NOMINEE_PIC;
            itemM["CHECK_BOOK"] = CHECK_BOOK;
            itemM["UTILITY_BILL"] = UTILITY_BILL;
            itemM["ORIGINAL_CERT"] = ORIGINAL_CERT;
            itemM["SIG_SCREEN_PRINT"] = SIG_SCREEN_PRINT;
            itemM["ALLOTMENT_LETTER"] = ALLOTMENT_LETTER;
            itemM["AFFIDAVIT"] = AFFIDAVIT;
            itemM["GD"] = GD;
            itemM["INDEMNITY_BOND"] = INDEMNITY_BOND;
            itemM["PAPER_ADD"] = PAPER_ADD;
            itemM["DIVIDEND_NOTICE"] = DIVIDEND_NOTICE;
            itemM["DEATH_CERTIFICATES"] = DEATH_CERTIFICATES;
            itemM["SUGG_HIGHCOURT"] = SUGG_HIGHCOURT;
            itemM["POWER_ATTORNEY"] = POWER_ATTORNEY;
            itemM["CERT_CEMENTRY"] = CERT_CEMENTRY;




            jsonObjM.push(itemM);

            $.ajax({

                url: 'SupportedDocumentsByID.aspx/UpdateSupportedDocument',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ SupportedDocumentmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'SupportedDocuments.aspx';
                }
            });
        }
    </script>
</asp:Content>
