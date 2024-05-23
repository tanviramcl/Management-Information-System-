<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="AssignComplainToEmployeeList.aspx.cs" Inherits="WebApplicationMIS.UI.AssignComplainToEmployee" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <!-- ============================================================== -->
        <!-- basic table  -->
        <!-- ============================================================== -->
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">
                <h5 class="card-header">Service Resiter List</h5>
                <div class="card-body">
                    <div class="table-responsive">


                        <table id="example" class="table table-striped table-bordered" style="width: 100%">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>COMPLAIN ID</th>
                                    <th>SERVICE ID</th>
                                    <th>SERVICE SUB ID</th>
                                    <th>BRANCH ID</th>
                                    <th>COMPLAIN SUBJECT</th>
                                    <th>COMPLAIN DETAILS</th>
                                    <th>STATUS</th>
                                    <th>REMARKS</th>
                                    <th>Complain Date</th>
                                     <th>Urgency</th>
                                    <th>ENTRY_DATETIME</th>
                                    <th>Action</th>
                                    <th>Action</th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <%

                                    System.Data.DataTable dtallRssiter = (System.Data.DataTable)Session["AllComplainResister"];
                                    if (dtallRssiter.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtallRssiter.Rows.Count; i++)
                                        {
                                %>
                                <tr>
                                    <td align="center"><asp:CheckBoxList ID="chkCompain" runat="server"></asp:CheckBoxList></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["COMPLAIN_ID"].ToString());   %> </td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["SERVICE_ID"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["SERVICE_SUB_ID"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["BRANCH_ID"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["COMPLAIN_SUBJECT"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["COMPLAIN_DETAILS"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["STATUS"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["REMARKS"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(Convert.ToDateTime(dtallRssiter.Rows[i]["COMPLIAN_DATE"]).ToString("dd-MMM-yyyy"));   %></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["URGENCY"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtallRssiter.Rows[i]["ENTRY_DATETIME"].ToString());   %></td>
                                    <td  align="center"><a href="Service Resister.aspx?ID=<% Response.Write(dtallRssiter.Rows[i]["COMPLAIN_ID"].ToString());%>" class="btn btn-primary">Assign</a></td>
                                     <td  align="center"><a href="Service Resister.aspx?ID=<% Response.Write(dtallRssiter.Rows[i]["COMPLAIN_ID"].ToString());%>" class="btn btn-secondary">Close</a></td>
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
