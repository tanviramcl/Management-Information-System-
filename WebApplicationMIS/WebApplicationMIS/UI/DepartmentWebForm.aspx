<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="DepartmentWebForm.aspx.cs" Inherits="WebApplicationMIS.UI.WebForm" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Add Department</h3>
                    
                </div>
                <div class="card">
                  <%--  <h5 class="card-header">Service register Form</h5>--%>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Department Id</label>
                                <asp:TextBox ID="TextBox_Department_iD" class="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>

                        <div class="form-row">

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">Department Name</label>
                                <asp:TextBox ID="txtDepartmentName" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                      
                        

                <%--  </form>--%>
            </div>
        </div>
        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 ">

            <asp:Button ID="ButtonSubmitt" Text="Save" class="btn btn-primary" runat="server"  />
        </div>

    </form>


    <script type="text/javascript">
          
     

          $("#aspnetForm").validate({
            submitHandler: function () {
                Designation();
            },
            rules: 
            {
                <%=TextBox_Department_iD.UniqueID %>: {
                    required: true, number:true,
                    minlength: 1,maxlength: 5
                },
                <%=txtDepartmentName.UniqueID %>: {
                    required: true 
                }
            }, 
            messages: {
               
            }
        });

        function Designation() {

            alert();
           
            var jsonObjM = [];
            var Department_iD =$("#<%=TextBox_Department_iD.ClientID%>").val();
            var DepartmentName =$("#<%=txtDepartmentName.ClientID%>").val();
           

            var itemM = {};
            itemM["DEPARTMENT_ID"] = Department_iD;
            itemM["DEPARTMENT_NAME"] = DepartmentName;
            
            

            jsonObjM.push(itemM);

            // alert(itemM);
            $.ajax({

                url: 'WebForm.aspx/InsertUpadateDEPARTMENT',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Designationmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'WebForm.aspx';
                }
            });




       
        }   
       
    </script>
  </asp:Content>