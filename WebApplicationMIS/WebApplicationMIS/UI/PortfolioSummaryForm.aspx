<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PortfolioSummaryForm.aspx.cs" MasterPageFile="~/UI/AMCLCommon.master" Inherits="WebApplicationMIS.UI.PortfolioSummaryForm" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="aspnetForm" runat="server" method="post">
        <div class="card">
            <div class="card-body">
                <div class="row">


                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                        <label><b>Fund Name<b></label>

                    </div>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">

                       <asp:DropDownList ID="fundNameDropDownList" class="form-control" runat="server" TabIndex="6" ></asp:DropDownList>

                    </div>
                 
                </div>
                <div class="row">

                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                        <label><b>Portfolio As On:<b></label>

                    </div>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">

                         <asp:DropDownList ID="portfolioAsOnDropDownList"   class="form-control" runat="server" TabIndex="8"></asp:DropDownList>

                    </div>

                </div>
                <div></div>
                
                <div class="row ">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                        <asp:Button ID="ButtonSubmitt" Text="Print" class="btn btn-secondary" runat="server" OnClick="Button1_Click" />
                    </div>
                </div>
            </div>

        </div>


    </form>
    <script type="text/javascript">
    
            

             
     
     
        $.validator.addMethod("DropDownListStatus", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Status."); 
       

        $("#aspnetForm").validate({
            
            rules: 
            {
               
            }, 
            messages: {
              
               
            }
        });

        
    </script>
</asp:Content>
