<%@ Page Language="C#" AutoEventWireup="true"   MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="BranchList.aspx.cs" Inherits="WebApplicationMIS.BranchList" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <div class="row">
        <!-- ============================================================== -->
        <!-- basic table  -->
        <!-- ============================================================== -->
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">
                <h5 class="card-header">Branch List</h5>
                <div class="card-body">
                    <div class="table-responsive">


                        <table id="example" class="table table-striped table-bordered" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>Branch ID</th>
                                    <th>Branch NAME</th>
                                    <th>Branch Address</th>
          
                                </tr>
                            </thead>
                            <tbody>
                                <%

                                    System.Data.DataTable dtBranchList = (System.Data.DataTable)Session["BranchList"];
                                    if (dtBranchList.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtBranchList.Rows.Count; i++)
                                        {
                                %>
                                <tr>
                                    <td  align="center"><%   Response.Write(dtBranchList.Rows[i]["BRANCH_ID"].ToString());   %> </td>
                                    <td  align="center"><%   Response.Write(dtBranchList.Rows[i]["BRANCH_NAME"].ToString());   %> </td>
                                    <td  align="center"><%   Response.Write(dtBranchList.Rows[i]["BRANCH_ADDRESS"].ToString());   %></td>
                                   
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
