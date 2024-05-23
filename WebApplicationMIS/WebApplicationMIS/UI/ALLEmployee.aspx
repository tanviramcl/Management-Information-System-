<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master"  CodeBehind="ALLEmployee.aspx.cs" Inherits="WebApplicationMIS.UI.ALLEmployee" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <!-- ============================================================== -->
        <!-- basic table  -->
        <!-- ============================================================== -->
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">
                <h5 class="card-header">Employee List</h5>
                <div class="card-body">
                    <div class="table-responsive">


                        <table id="example" class="table table-striped table-bordered" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>Employee Id</th>
                                    <th>EMPLOYEE NAME</th>
                                   <%-- <th>Joining date</th>--%>
                                    <th>Phone</th>
                                    <th>NID</th>
                                    <th>Email</th>
                                    <th>DESIGNATION ID </th>
                                    <th>DESIGNATION </th>
                                    <th>Action</th>
                                    <th>Action</th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <%

                                    System.Data.DataTable dtAllEmployeer = (System.Data.DataTable)Session["AllEmployee"];
                                    if (dtAllEmployeer.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtAllEmployeer.Rows.Count; i++)
                                        {
                                %>
                                <tr>
                                    <td  align="center"><%   Response.Write(dtAllEmployeer.Rows[i]["ID"].ToString());   %> </td>
                                    <td  align="center"><%   Response.Write(dtAllEmployeer.Rows[i]["NAME"].ToString());   %></td>
          <%--                          <td  align="center"><%   Response.Write(Convert.ToDateTime(dtAllEmployeer.Rows[i]["JDATE"]).ToString("dd-MMM-yyyy"));   %></td>--%>
                                    <td  align="center"><%   Response.Write(dtAllEmployeer.Rows[i]["CONTRACT_NO"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtAllEmployeer.Rows[i]["NID"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtAllEmployeer.Rows[i]["EMAIL"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtAllEmployeer.Rows[i]["DESIG_ID"].ToString());   %></td>
                                    <td  align="center"><%   Response.Write(dtAllEmployeer.Rows[i]["DEPT_ID"].ToString());   %></td>
                                    <td  align="center"><a href="AddEmployee.aspx?ID=<% Response.Write(dtAllEmployeer.Rows[i]["EMP_ID"].ToString());%>" class="btn btn-secondary">Update</a></td>
                                     <td  align="center"><a href="AddEmployee.aspx?DeleteID=<% Response.Write(dtAllEmployeer.Rows[i]["EMP_ID"].ToString());%>" class="btn btn-secondary">Delete</a></td>
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
