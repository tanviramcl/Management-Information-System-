<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDesignation.aspx.cs"   MasterPageFile="~/UI/AMCLCommon.master" Inherits="WebApplicationMIS.UI.AddDesignation1" %>
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
                                <label for="input-select">Designation Id</label>
                                <asp:TextBox ID="TextBox_Designation_iD" class="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>

                        <div class="form-row">

                            <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                                <label class="col-form-label">Designation Name</label>
                                <asp:TextBox ID="txtDesignationtName" class="form-control" runat="server"></asp:TextBox>
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
                <%=TextBox_Designation_iD.UniqueID %>: {
                    required: true, number:true,
                    minlength: 1,maxlength: 5
                },
                <%=txtDesignationtName.UniqueID %>: {
                    required: true 
                }
            }, 
            messages: {
               
            }
        });

        function Designation() {

          //  alert();
           
            var jsonObjM = [];
            var Designation_iD =$("#<%=TextBox_Designation_iD.ClientID%>").val();
            var DesignationtName =$("#<%=txtDesignationtName.ClientID%>").val();
           

            var itemM = {};
            itemM["Designation_ID"] = Designation_iD;
            itemM["Designation_Name"] = DesignationtName;
            
            

            jsonObjM.push(itemM);

            // alert(itemM);
            $.ajax({

                url: 'AddDesignation.aspx/InsertUpadateDesignation',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ Designationmdl: JSON.stringify(jsonObjM) }),
                success: function (data) {
                    var msg = JSON.stringify(data);
                    var obj = JSON.parse(msg);
                    alert(obj.d);
                    window.location = 'AddDesignation.aspx';
                }
            });




       
        }   
       
    </script>
</asp:Content>
