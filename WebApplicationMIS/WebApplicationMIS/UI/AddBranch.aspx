<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBranch.aspx.cs"   MasterPageFile="~/UI/AMCLCommon.master" Inherits="WebApplicationMIS.UI.AddBranch" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <form id="aspnetForm" runat="server" method="post">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="section-block" id="basicform">
                    <h3 class="section-title">Add Designation</h3>
                    
                </div>
                <div class="card">
                  <%--  <h5 class="card-header">Service register Form</h5>--%>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label for="input-select">BRANCH Id</label>
                                <asp:TextBox ID="TextBox_Branch_iD" class="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>

                        <div class="form-row">

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">BRANCH Name</label>
                                <asp:TextBox ID="txtBranchtName" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                       <div class="form-row">
                            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                                <label for="exampleFormControlTextarea1">BRANCH Address</label>
                                <textarea id="TextareaBRanchAddress" cols="20" class="form-control" rows="2" runat="server"></textarea>
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
                <%=TextBox_Branch_iD.UniqueID %>: {
                    required: true, number:true,
                    minlength: 1,maxlength: 5
                },
                <%=txtBranchtName.UniqueID %>: {
                    required: true 
                }
            }, 
            messages: {
               
            }
        });

        function Designation() {

            alert();
          // 
            var jsonObjM = [];
            var BRANCH_ID =$("#<%=TextBox_Branch_iD.ClientID%>").val();
            var BRANCH_NAME =$("#<%=txtBranchtName.ClientID%>").val();
            var baranchAddress = $("#<%=TextareaBRanchAddress.ClientID%>").val();
           

            var itemM = {};
            itemM["BRANCH_ID"] = BRANCH_ID;
            itemM["BRANCH_NAME"] = BRANCH_NAME;
            itemM["BRANCH_ADDRESS"] = baranchAddress;
            
            

            jsonObjM.push(itemM);

            // alert(itemM);
            $.ajax({

                url: 'AddBranch.aspx/InsertUpadateBranch',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Barnchmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'AddBranch.aspx';
                }
            });




       
        }   
       
    </script>
</asp:Content>
