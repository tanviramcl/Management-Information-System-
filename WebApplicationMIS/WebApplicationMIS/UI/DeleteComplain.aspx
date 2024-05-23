<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteComplain.aspx.cs"   MasterPageFile="~/UI/AMCLCommon.master"  Inherits="WebApplicationMIS.UI.DeleteComplain" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Delete Complain</h3>
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
                    </div>
                </div>

                <%--  </form>--%>
            </div>
        </div>
        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 ">

            <asp:Button ID="ButtonSubmitt" Text="Delete" class="btn btn-primary" runat="server" OnClick="Button1_Click" />
        </div>

    </form>
     <script type="text/javascript">
    
     

         $.validator.addMethod("DropDownListComplain", function (value, element, param) {
             if (value == '0')
                 return false;
             else
                 return true;
         }, "Please select a Complain .");
         

          $("#aspnetForm").validate({
            submitHandler: function () {
                AssignComplain();
            },
            rules: 
            {
            
                <%=inputselectCompalinDropDownList.UniqueID %>: {
                    DropDownListComplain: true 
                }
            }, 
            messages: {
               
            }
        });
         function AssignComplain() {
            
            var jsonObjM = [];
            var Complain_ID =$("#<%=inputselectCompalinDropDownList.ClientID%>").val();
            var Complain_sub=$("#<%=TextBoxaComplainSubect.ClientID%>").val();
            var Complain_details=$("#<%=TextareaComplainDetails.ClientID%>").val();
          

         
            //alert(BranchNo);

            var itemM = {};
            itemM["COMPLAIN_ID"] = Complain_ID;
            itemM["COMPLAIN_SUBJECT"] = Complain_sub;
            itemM["COMPLAIN_DETAILS"] = Complain_details;
            
         

            jsonObjM.push(itemM);
            $.ajax({

                url: 'DeleteComplain.aspx/DeleteComplainList',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Complainesistationmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'AllComplainResister.aspx';
                }
            });

        }
     </script>
</asp:Content>

