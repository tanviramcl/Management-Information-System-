<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="SupportedDocuments_1.aspx.cs" Inherits="WebApplicationMIS.UI.AllServiceResiter" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <!-- ============================================================== -->
        <!-- basic table  -->
        <!-- ============================================================== -->
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">
                <h5 class="card-header">Supported Document List</h5>
                <div class="card-body">
                    <div class="table-responsive">


                        <table id="example" class="table table-striped table-bordered" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>Complain Id</th>
                                    <th>Document ID</th>
                                    <th>Service name</th>
                                    <th>Copy of BO Set up </th>
                                    <th>Copy of NID</th>
                                    <th>Copy of e-TIN </th>
                                    <th>Applicant passport size photo</th>
                                    <th>Nominee passport size photo</th>
                                    <th>Photocopy of check book leaf</th>
                                    <th>Photocopy of utility bill</th>
                                    <th>Copy of Original Certificates</th>
                                    <th>Signature Screen Print Copy</th>
                                    <th>Action</th>
                                    
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
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["RESISTER_ID"].ToString());   %> </td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["SUP_DOC_ID"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["BO_COPY"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["NID_COPY"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["E_TIN_COPY"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["APPLICANT_PIC"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["NOMINEE_PIC"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["CHECK_BOOK"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["UTILITY_BILL"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["ORIGINAL_CERT"]);   %></td>
                                    <td  align="center"><%   Response.Write(dtallSupportedDoc.Rows[i]["SIG_SCREEN_PRINT"].ToString());   %></td>
                                    <td  align="center"><a href="Service Resister.aspx?ID=<% Response.Write(dtallSupportedDoc.Rows[i]["RESISTER_ID"].ToString());%>" class="btn btn-secondary">Update</a></td>
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
        $('#example').DataTable();
    </script>
</asp:Content>
