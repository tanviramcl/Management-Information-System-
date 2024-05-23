<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloseComplain.aspx.cs" MasterPageFile="~/UI/AMCLCommon.master" Inherits="WebApplicationMIS.UI.CloseComplain" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Close Complain</h3>
                </div>
                <div class="card">
                
                    <div class="card-body">
                       
                  
                       
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
                                <label>Close Date</label>
                                <asp:TextBox ID="ClosedateDateTextBox" class="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                          <div class="form-row">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6">
                                <label for="inputDefault" class="col-form-label">Status</label>
                                <asp:DropDownList ID="DropDownListStatus"  class="form-control" runat="server">
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

            <asp:Button ID="ButtonSubmitt" Text="Close" class="btn btn-primary" runat="server" OnClick="Button1_Click" />
        </div>

    </form>
     <script type="text/javascript">
    
            
        $('#<%=ClosedateDateTextBox.ClientID%>').datepicker({
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
         }, "Please select a Complain .");
         $.validator.addMethod("DropDownListStatus", function (value, element, param) {
             if (value == '0')
                 return false;
             else
                 return true;
         }, "Please select a Status.");

          $("#aspnetForm").validate({
            submitHandler: function () {
                AssignComplain();
            },
            rules: 
            {
            
                <%=DropDownListStatus.UniqueID %>: {
                    DropDownListStatus: true 
                },
                <%=inputselectCompalinDropDownList.UniqueID %>: {
                    DropDownListComplain: true 
                },
               <%=ClosedateDateTextBox.UniqueID %>: {
                     required: true
                }
            }, 
            messages: {
               
            }
        });
         function AssignComplain() {
            
            var jsonObjM = [];
            var Complain_ID =$("#<%=inputselectCompalinDropDownList.ClientID%>").val();
            var status= $("#<%=DropDownListStatus.ClientID%>").val();
            var Complain_sub=$("#<%=TextBoxaComplainSubect.ClientID%>").val();
            var Complain_details=$("#<%=TextareaComplainDetails.ClientID%>").val();
             var CLOSE_DATE=$("#<%=ClosedateDateTextBox.ClientID%>").val();

         
            //alert(BranchNo);

            var itemM = {};
            itemM["COMPLAIN_ID"] = Complain_ID;
            itemM["COMPLAIN_SUBJECT"] = Complain_sub;
            itemM["COMPLAIN_DETAILS"] = Complain_details;
            itemM["STATUS"] = status;
            itemM["CLOSE_DATE"] = CLOSE_DATE;
         

            jsonObjM.push(itemM);
            $.ajax({

                url: 'CloseComplain.aspx/CloseComplainToAssign',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Complainesistationmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'CloseComplain.aspx';
                }
            });

        }
     </script>
</asp:Content>

