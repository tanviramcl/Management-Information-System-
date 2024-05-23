<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="Complain Resister.aspx.cs" Inherits="WebApplicationMIS.UI.Service_Resister" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Complain register</h3>
                    <p>Use custom form to register Complain.</p>
                </div>
                <div class="card">
                    <h5 class="card-header">Complain register Form</h5>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Complain Id</label>
                                <asp:TextBox ID="TextBox_RESISTER_iD" class="form-control" OnTextChanged="TextBox_COmPLAIN_ID_TextChanged" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Complain Type</label>
                                <asp:DropDownList ID="DropDownListComplain" class="form-control" runat="server">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Complain Box </asp:ListItem>
                                    <asp:ListItem Value="2">Internal Complain </asp:ListItem>
                                    <asp:ListItem Value="3">External Complain </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                           
                        </div>
                  
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Service Name</label>
                                <asp:DropDownList ID="inputselectservicedropdownList" OnSelectedIndexChanged="ServiceDropDownList_SelectedIndexChanged" class="form-control" runat="server" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                            </div>

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Sub Service Name</label>
                                <asp:DropDownList ID="inputselectsubservicedropdownList" class="form-control" runat="server" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">Branch Name</label>
                                <asp:DropDownList ID="inputselectBRANCHDropDownList" class="form-control" runat="server" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                            </div>
                             <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Urgency</label>
                                <asp:DropDownList ID="DropDownListUrgency" class="form-control" runat="server">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="L">Low </asp:ListItem>
                                    <asp:ListItem Value="M">Medium</asp:ListItem>
                                    <asp:ListItem Value="H">High</asp:ListItem>
                                </asp:DropDownList>
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
                <%--        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputDefault" class="col-form-label">Name</label>
                                <asp:TextBox ID="TextBoxName" class="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="inputEmail">Email address</label>
                                <asp:TextBox ID="inputEmailTextBox" type="email" class="form-control" placeholder="name@example.com" runat="server"></asp:TextBox>
                                <%--     <asp:TextBox ID="inputEmailTextBox" type="email" class="form-control" placeholder="name@example.com"AutoPostBack="true" runat="server" ></asp:TextBox>--%>
                             <%--   <p>We'll never share your email with anyone else.</p>
                            </div>
                        </div>--%>

                        <div class="form-row">
                           <%-- <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label>Phone <small class="text-muted">(999) 999-9999</small></label>
                                <asp:TextBox ID="TextBoxPhone" class="form-control" runat="server"></asp:TextBox>

                            </div>--%>
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label>Complain Date</label>
                                <asp:TextBox ID="complaindateDateTextBox" class="form-control" runat="server"></asp:TextBox>

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
                                <label for="exampleFormControlTextarea1">Remarks</label>
                                <textarea id="TextareaRemarks" cols="20" class="form-control" rows="2" runat="server"></textarea>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6">
                                <label for="inputDefault" class="col-form-label">Upload Supported Document File :</label>
                                <asp:FileUpload ID="SupportedDocumentFileUpload" runat="server" />
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6">
                                <label for="inputDefault" class="col-form-label">Status</label>
                                <asp:DropDownList ID="DropDownListStatus" visible="false" class="form-control" runat="server">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="P">Primary </asp:ListItem>
                                    <asp:ListItem Value="I">Processing</asp:ListItem>
                                    <asp:ListItem Value="C">Close</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <%--  </form>--%>
            </div>
        </div>
        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 ">

            <asp:Button ID="ButtonSubmitt" Text="Save" class="btn btn-primary" runat="server" OnClick="Button1_Click" />
        </div>

    </form>


    <script type="text/javascript">
    
            
        $('#<%=complaindateDateTextBox.ClientID%>').datepicker({
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
        },"Please select a Fund Type."); 
        $.validator.addMethod("CheckServiceDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Service."); 
        $.validator.addMethod("CheckSUBServiceDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Sub Service."); 

        $.validator.addMethod("CheckBranchDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Branch.");
        $.validator.addMethod("DropDownListUrgency", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select.");

        $("#aspnetForm").validate({
            submitHandler: function () {
                ServiceResister();
            },
            rules: 
            {
            
                <%=DropDownListComplain.UniqueID %>: {
                    DropDownListComplain: true 
                },
               <%-- <%=inputselectservicedropdownList.UniqueID %>: {
                    CheckServiceDropDownList: true 
                },
                <%=inputselectsubservicedropdownList.UniqueID %>: {
                    CheckSUBServiceDropDownList: true 
                },
                <%=inputselectBRANCHDropDownList.UniqueID %>: {
                    CheckBranchDropDownList: true 
                },--%>
              <%--  <%=TextBoxName.UniqueID %>: {
                    required: true
                },
                <%=inputEmailTextBox.UniqueID %>: {
                    email:true
                },
                <%=AddressTextArea1.UniqueID %>: {
                    required: true
                },--%>
                <%=complaindateDateTextBox.UniqueID %>: {
                    required: true
                },
                <%=DropDownListUrgency.UniqueID %>: {
                    DropDownListUrgency: true 
                },
               <%=TextBoxaComplainSubect.UniqueID %>: {
                     required: true
                },
               <%=TextareaComplainDetails.UniqueID %>: {
                     required: true
                },
               <%=complaindateDateTextBox.UniqueID %>: {
                     required: true
                }
            }, 
            messages: {
               
            }
        });

        function ServiceResister() {
            
            var jsonObjM = [];
            var Complain_ID =$("#<%=TextBox_RESISTER_iD.ClientID%>").val();
            var Complain_Type =$("#<%=DropDownListComplain.ClientID%>").val();
            var SERVICE_ID = $("#<%=inputselectservicedropdownList.ClientID%>").val();
            var SERVICE_SUB_ID = $("#<%=inputselectsubservicedropdownList.ClientID%>").val();
            var BRANCH_Code= $("#<%=inputselectBRANCHDropDownList.ClientID%>").val();
            var Complain_sub=$("#<%=TextBoxaComplainSubect.ClientID%>").val();
            var Complain_details=$("#<%=TextareaComplainDetails.ClientID%>").val();
           <%-- var EMAIL = $("#<%=inputEmailTextBox.ClientID%>").val();
            var NAME =  $("#<%=TextBoxName.ClientID%>").val();
            var MOBILE =$("#<%=TextBoxPhone.ClientID%>").val();
            var ADDRS1 = $("#<%=AddressTextArea1.ClientID%>").val();--%>
            var Remarks = $("#<%=TextareaRemarks.ClientID%>").val();
            var Urgency = $("#<%=DropDownListUrgency.ClientID%>").val();
            var ComplainDate=$("#<%=complaindateDateTextBox.ClientID%>").val();

         
            //alert(BranchNo);

            var itemM = {};
            itemM["Complain_ID"] = Complain_ID;
            itemM["SERVICE_ID"] = SERVICE_ID;
            itemM["SERVICE_SUB_ID"] = SERVICE_SUB_ID;
            itemM["Complain_Type"] = Complain_Type;
            itemM["BRANCH_Code"] = BRANCH_Code;
            itemM["COMPLAIN_SUBJECT"] = Complain_sub;
            itemM["COMPLAIN_DETAILS"] = Complain_details;
            //itemM["NAME"] = NAME;
            //itemM["EMAIL"] = EMAIL;
            //itemM["MOBILE1"] = MOBILE;
            //itemM["ADDRS1"] = ADDRS1;
            itemM["REMARKS"] = Remarks;
            itemM["URGENCY"] = Urgency; 
            itemM["ComplainDate"] = ComplainDate;
         

            jsonObjM.push(itemM);
            $.ajax({

                url: 'Complain Resister.aspx/InsertUpdateComplainResister',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Complainesistationmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'Complain Resister.aspx';
                }
            });

        }
    </script>
</asp:Content>

