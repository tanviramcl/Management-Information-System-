﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignationLIst.aspx.cs"  MasterPageFile="~/UI/AMCLCommon.master" Inherits="WebApplicationMIS.UI.DesignationLIst" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="row">
        <!-- ============================================================== -->
        <!-- basic table  -->
        <!-- ============================================================== -->
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">
                <h5 class="card-header">DESIGNATION List</h5>
                <div class="card-body">
                    <div class="table-responsive">


                        <table id="example" class="table table-striped table-bordered" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>DESIGNATION ID</th>
                                    <th>DESIGNATION NAME</th>
          
                                </tr>
                            </thead>
                            <tbody>
                                <%

                                    System.Data.DataTable dtAll_DESIGNATION = (System.Data.DataTable)Session["All_DESIGNATION"];
                                    if (dtAll_DESIGNATION.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtAll_DESIGNATION.Rows.Count; i++)
                                        {
                                %>
                                <tr>
                                    <td  align="center"><%   Response.Write(dtAll_DESIGNATION.Rows[i]["ID"].ToString());   %> </td>
                                    <td  align="center"><%   Response.Write(dtAll_DESIGNATION.Rows[i]["NAME"].ToString());   %></td>
                                   
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