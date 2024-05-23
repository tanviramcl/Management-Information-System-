<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="AddEmployee.aspx.cs" Inherits="WebApplicationMIS.UI.AddEmployee" %>

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
                                <label for="input-select">Employee Id</label>
                                <asp:TextBox ID="TextBox_Employee_iD" class="form-control" OnTextChanged="TextBox_Employee_ID_TextChanged" runat="server"></asp:TextBox>

                            </div>
                        </div>

                        <div class="form-row">

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">Name</label>
                                <asp:TextBox ID="txtFirstName" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                      
                        <div class="form-row">
                            
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">NID No</label>
                                <asp:TextBox ID="TextBoxNIDNO" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputEmail">Email address</label>
                                <asp:TextBox ID="inputEmailTextBox" type="email" class="form-control" placeholder="name@example.com" runat="server"></asp:TextBox>
                                <%--     <asp:TextBox ID="inputEmailTextBox" type="email" class="form-control" placeholder="name@example.com"AutoPostBack="true" runat="server" ></asp:TextBox>--%>
                                <p>We'll never share your email with anyone else.</p>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label>Phone <small class="text-muted">(999) 999-9999</small></label>
                                <asp:TextBox ID="TextBoxPhone" class="form-control" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label>Join Date</label>
                                <asp:TextBox ID="JoinDateTextBox" class="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                                <label for="exampleFormControlTextarea1">Present Address</label>
                                <textarea id="AddressTextArea1" cols="20" class="form-control" rows="2" runat="server"></textarea>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                                <label for="exampleFormControlTextarea1">Permanent Address</label>
                                <textarea id="PermanentTextarea" cols="20" class="form-control" rows="2" runat="server"></textarea>
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
          
        $('#<%=JoinDateTextBox.ClientID%>').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            maxDate:"today",
            onSelect: function(selected) {
                    
            }
        });
        $.validator.addMethod("CheckDepartmentList", function (value, element, param) {  
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
                <%=TextBox_Employee_iD.UniqueID %>: {
                    required: true, number:true,
                    minlength: 1,maxlength: 5
                },
                <%=txtFirstName.UniqueID %>: {
                    required: true 
                },
                <%=JoinDateTextBox.UniqueID %>: {
                    required: true
                },
                <%=inputEmailTextBox.UniqueID %>: {
                    email:true
                },
                
                <%=TextBoxPhone.UniqueID %>: {
                    required:true
                },
                <%=AddressTextArea1.UniqueID %>: {
                    required: true
                },

                <%=PermanentTextarea.UniqueID %>: {
                    required: true
                },
                <%=inputselectDepartmentDropDownList.UniqueID %>: {
                    CheckDepartmentList: true 
                }
                ,
                <%=DropDownListDESIGNATION.UniqueID %>: {
                    CheckdegignationDownList: true 
                }
              
            }, 
            messages: {
                <%=txtFirstName.UniqueID %>:{  
                    required: "* Name is required*"
                  
                }
            }
        });

        function AddEmployee() {
           
            var jsonObjM = [];
           var Employee_iD =$("#<%=TextBox_Employee_iD.ClientID%>").val();
            var FirstName =$("#<%=txtFirstName.ClientID%>").val();
            var JoinDate =$("#<%=JoinDateTextBox.ClientID%>").val();
            var NID_NO = $("#<%=TextBoxNIDNO.ClientID%>").val();
            var EMAIL = $("#<%=inputEmailTextBox.ClientID%>").val();
            var MOBILE1 =$("#<%=TextBoxPhone.ClientID%>").val();
            var ADDRS1 = $("#<%=AddressTextArea1.ClientID%>").val();
            var permanentAddress = $("#<%=PermanentTextarea.ClientID%>").val();
            var Department = $("#<%=inputselectDepartmentDropDownList.ClientID%>").val();
            var DESIGNATION = $("#<%=DropDownListDESIGNATION.ClientID%>").val();

            var itemM = {};
            itemM["EMP_ID"] = Employee_iD;
            itemM["EMPLOYEE_NAME"] = FirstName;
            itemM["ADDR"] = ADDRS1;
            itemM["ADDR2"] = permanentAddress;
            itemM["JDATE"] = JoinDate;
            itemM["CONTRACT_NO"] = MOBILE1;
            itemM["NID"] = NID_NO;
            itemM["EMAIL"] = EMAIL;
            itemM["DEPARTMENT_ID"] = Department;
            itemM["DESIGNATION_ID"] = DESIGNATION;
            

            jsonObjM.push(itemM);

            // alert(itemM);
            $.ajax({

                url: 'AddEmployee.aspx/InsertUpadateEmployee',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Employeeresistationmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'AddEmployee.aspx';
                }
            });




       
        }   
       
    </script>
</asp:Content>