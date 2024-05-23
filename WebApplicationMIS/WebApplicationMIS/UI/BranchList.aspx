<%@ Page Language="C#" AutoEventWireup="true"   MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="BranchList.aspx.cs" Inherits="WebApplicationMIS.UI.BranchList" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="row">
        <!-- ============================================================== -->
        <!-- basic table  -->
        <!-- ============================================================== -->
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">
                <h5 class="card-header">Branch  List</h5>
                <div class="card-body">
                    <div class="table-responsive">


                        <table id="example" class="table table-striped table-bordered" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>BRANCH ID </th>
                                    <th>BRANCH NAME</th>
                                    <th>BRANCH_ADDRESS </th>
                                   <%-- <th>Action</th>
                                    <th>Action</th>--%>
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <%

                                    System.Data.DataTable dtAllBRanch = (System.Data.DataTable)Session["ALLBRANCHINFO"];
                                    if (dtAllBRanch.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtAllBRanch.Rows.Count; i++)
                                        {
                                %>
                                <tr>
                                    <td  align="center"><%   Response.Write(dtAllBRanch.Rows[i]["BRANCH_ID"].ToString());   %> </td>
                                    <td  align="center"><%   Response.Write(dtAllBRanch.Rows[i]["BRANCH_NAME"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtAllBRanch.Rows[i]["BRANCH_ADDRESS"].ToString());   %></td>    
                                   <%-- <td  align="center"><a href="AddBranch.aspx?ID=<% Response.Write(dtAllBRanch.Rows[i]["BRANCH_ID"].ToString());%>" class="btn btn-secondary">Update</a></td>
                                     <td  align="center"><a href="AddBranch.aspx?DeleteID=<% Response.Write(dtAllBRanch.Rows[i]["BRANCH_ID"].ToString());%>" class="btn btn-secondary">Delete</a></td>--%>
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
</html></asp:Content>