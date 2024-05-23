<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignComplainToEmployee.aspx.cs"  MasterPageFile="~/UI/AMCLCommon.master" Inherits="WebApplicationMIS.UI.AssignComplainToEmployee1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Assign Complain to Employee</h3>
                </div>
                <div class="card">
                
                    <div class="card-body">
                       
                  
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Employee Name</label>
                                <asp:DropDownList ID="inputEmployeeNamedropdownList"  class="form-control" runat="server" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Complain</label>
                                <asp:DropDownList ID="inputselectCompalinDropDownList" class="form-control" OnSelectedIndexChanged="ComplainDropDownList_SelectedIndexChanged" runat="server" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                            </div>
                     
                        </div>
                       <div class="form-row">
                            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                                <label for="exampleFormControlTextarea1">Complain Subject</label>
                                <asp:TextBox ID="TextBoxaComplainSubect" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                                <label for="exampleFormControlTextarea1">Complain Details</label>
                                <textarea id="TextareaComplainDetails" cols="20" class="form-control" rows="2" runat="server"></textarea>
                            </div>
                        </div>

                        <div class="form-row">
                           <%-- <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label>Phone <small class="text-muted">(999) 999-9999</small></label>
                                <asp:TextBox ID="TextBoxPhone" class="form-control" runat="server"></asp:TextBox>

                            </div>--%>
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label>Assign Date</label>
                                <asp:TextBox ID="AssigndateDateTextBox" class="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                      <%--  <div class="form-row">
                            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                                <label for="exampleFormControlTextarea1">Address</label>
                                <textarea id="AddressTextArea1" cols="20" class="form-control" rows="2" runat="server"></textarea>
                            </div>
                        </div>--%>
                        <div class="form-row">
                            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                                <label for="exampleFormControlTextarea1">Comments</label>
                                <textarea id="TextareaComments" cols="20" class="form-control" rows="2" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                </div>

                <%--  </form>--%>
            </div>
        </div>
        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 ">

            <asp:Button ID="ButtonSubmitt" Text="Assign" class="btn btn-primary" runat="server" OnClick="Button1_Click" />
        </div>

    </form>
     <script type="text/javascript">
    
            
        $('#<%=AssigndateDateTextBox.ClientID%>').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            //maxDate:"today",
            onSelect: function(selected) {
                    
            }
        });

         $.validator.addMethod("DropDownListComplain", function (value, element, param) {
             if (value == '0')
                 return false;
             else
                 return true;
         }, "Please select a Complain Type.");
         $.validator.addMethod("inputEmployeeNamedropdownList", function (value, element, param) {
             if (value == '0')
                 return false;
             else
                 return true;
         }, "Please select a Employee.");

          $("#aspnetForm").validate({
            submitHandler: function () {
                AssignComplain();
            },
            rules: 
            {
            
                <%=inputEmployeeNamedropdownList.UniqueID %>: {
                    inputEmployeeNamedropdownList: true 
                },
                <%=inputselectCompalinDropDownList.UniqueID %>: {
                    DropDownListComplain: true 
                },
               <%=AssigndateDateTextBox.UniqueID %>: {
                     required: true
                }
            }, 
            messages: {
               
            }
        });
         function AssignComplain() {
            
            var jsonObjM = [];
            var Complain_ID =$("#<%=inputselectCompalinDropDownList.ClientID%>").val();
            var Employee_id= $("#<%=inputEmployeeNamedropdownList.ClientID%>").val();
            var Complain_sub=$("#<%=TextBoxaComplainSubect.ClientID%>").val();
            var Complain_details=$("#<%=TextareaComplainDetails.ClientID%>").val();
            var comments = $("#<%=TextareaComments.ClientID%>").val();
            var AssignDate=$("#<%=AssigndateDateTextBox.ClientID%>").val();

         
            //alert(BranchNo);

            var itemM = {};
            itemM["COMPLAIN_ID"] = Complain_ID;
            itemM["EMP_ID"] = Employee_id;
            itemM["COMPLAIN_SUBJECT"] = Complain_sub;
            itemM["COMPLAIN_DETAILS"] = Complain_details;
            itemM["COMMENTS"] = comments;
            itemM["ASSIGN_DATE"] = AssignDate;
         

            jsonObjM.push(itemM);
            $.ajax({

                url: 'AssignComplainToEmployee.aspx/AssignComplainToEmployee',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Complainesistationmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'AssignComplainToEmployee.aspx';
                }
            });

        }
     </script>
</asp:Content>

