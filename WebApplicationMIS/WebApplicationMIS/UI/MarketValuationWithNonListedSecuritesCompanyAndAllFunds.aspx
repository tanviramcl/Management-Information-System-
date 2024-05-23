<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="MarketValuationWithNonListedSecuritesCompanyAndAllFunds.aspx.cs" Inherits="WebApplicationMIS.UI.MarketValuationWithNonListedSecuritesCompanyAndAllFunds" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="aspnetForm" runat="server" method="post">
        <div class="card">
            <div class="card-body">
                <div class="row">



                    <%-- <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">

                       <asp:DropDownList ID="fundNameDropDownList" class="form-control" runat="server" TabIndex="6" ></asp:DropDownList>

                    </div>--%>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2" id="dvGridFund" runat="server">
                        <!--- Following code renders the checkboxes and a label control on browser --->

                        <asp:CheckBox ID="chkAll" Text="Select All" runat="server" />
                        <asp:CheckBoxList ID="chkFruits" runat="server">
                        </asp:CheckBoxList>

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

               
                <div class="row ">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                          <asp:Button ID="ButtonSubmitt" Text="Print" class="btn btn-secondary" runat="server" OnClick="Button1_Click" />
                    </div>
                </div>
            </div>

        </div>


    </form>
    <script type="text/javascript">
        $(function () {
            $("[id*=chkAll]").bind("click", function () {
                if ($(this).is(":checked")) {
                    $("[id*=chkFruits] input").prop("checked", "checked");
                } else {
                    $("[id*=chkFruits] input").removeAttr("checked");
                }
            });
            $("[id*=chkFruits] input").bind("click", function () {
                if ($("[id*=chkFruits] input:checked").length == $("[id*=chkFruits] input").length) {
                    $("[id*=chkAll]").prop("checked", "checked");
                } else {
                    $("[id*=chkAll]").removeAttr("checked");
                }
            });
        });

            

     
    
       

        $("#aspnetForm").validate({
            
           
        });

        
    </script>
</asp:Content>

