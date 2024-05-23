<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="EmloyeeAssigntoLogin.aspx.cs" Inherits="WebApplicationMIS.UI.EmloyeeAssigntoLogin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Add Employee</h3>
                    
                </div>
                <div class="card">
                  <%--  <h5 class="card-header">Service register Form</h5>--%>
                    <div class="card-body">
                      
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Employee Name</label>
                                <asp:DropDownList ID="inputEmployeeNamedropdownList"  class="form-control" OnSelectedIndexChanged="EmployeeDropDownList_SelectedIndexChanged" runat="server" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                     
                        <div class="form-row">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6">
                                <label for="inputDefault" class="col-form-label">Designation</label>
                                 <asp:DropDownList ID="inputselectDepartmentDropDownList" class="form-control" runat="server" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                      
                        <div class="form-row">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6">
                                <label for="inputDefault" class="col-form-label">Department Name</label>
                                <asp:DropDownList ID="DropDownListDESIGNATION" class="form-control" runat="server" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                          <div class="form-row">

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">UserID</label>
                                <asp:TextBox ID="txtUserID" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">User Lavel</label>
                                <asp:TextBox ID="UserLabelTextBox" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                          <div class="form-row">

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">Password</label>
                                <asp:TextBox ID="txtPassword"  class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                      


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
     
        $.validator.addMethod("CheckDepartmentList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Department."); 
        $.validator.addMethod("EmployeeList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Department."); 
        $.validator.addMethod("CheckdegignationDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Designation."); 

          $("#aspnetForm").validate({
            submitHandler: function () {
                AddEmployee();
            },
            rules: 
            {
                 <%=inputEmployeeNamedropdownList.UniqueID %>: {
                     EmployeeList: true
                },
            
                <%=inputselectDepartmentDropDownList.UniqueID %>: {
                    CheckDepartmentList: true 
                }
                ,
                <%=DropDownListDESIGNATION.UniqueID %>: {
                    CheckdegignationDownList: true 
                },
                <%=txtUserID.UniqueID %>: {
                    required: true 
                },
                <%=txtPassword.UniqueID %>: {
                    required: true
                },
                <%=UserLabelTextBox.UniqueID %>: {
                    required: true
                },
              
            }, 
            messages: {
             
            }
        });

        function AddEmployee() {
           
            var jsonObjM = [];
            var Employee_iD =$("#<%=inputEmployeeNamedropdownList.ClientID%>").val();
            var USER_NM =$("#<%=inputEmployeeNamedropdownList.ClientID%>").find("option:selected").text();
            var Department = $("#<%=inputselectDepartmentDropDownList.ClientID%>").val();
            var DESIGNATION = $("#<%=DropDownListDESIGNATION.ClientID%>").val();
            var UserID =$("#<%=txtUserID.ClientID%>").val();
            var Password =$("#<%=txtPassword.ClientID%>").val();
            var userLavel =$("#<%=UserLabelTextBox.ClientID%>").val();
 

            var itemM = {};
            itemM["EMP_ID"] = Employee_iD;   
            itemM["USER_NM"] = USER_NM;   
            itemM["DEPARTMENT_ID"] = Department;
            itemM["DESIGNATION_ID"] = DESIGNATION;
            itemM["USER_ID"] = UserID;
            itemM["USER_PASS"] = Password;
            itemM["USER_LEVEL"] = userLavel;
            

            jsonObjM.push(itemM);

            // alert(itemM);
            $.ajax({

                url: 'EmloyeeAssigntoLogin.aspx/InsertUSER',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Employeeresistationmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'EmloyeeAssigntoLogin.aspx';
                }
            });


         

       
        }   
       
    </script>
</asp:Content>