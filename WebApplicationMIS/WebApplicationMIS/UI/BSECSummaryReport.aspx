<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="BSECSummaryReport.aspx.cs" Inherits="WebApplicationMIS.UI.BSECSummaryReport" %>

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
                        <label><b>From Date<b></label>

                    </div>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">

                        <asp:TextBox ID="FromTextBox" class="form-control" runat="server"></asp:TextBox>

                    </div>

                </div>
                <div class="row">

                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                        <label><b>To Date<b></label>

                    </div>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">

                        <asp:TextBox ID="ToTextBox" class="form-control" runat="server"></asp:TextBox>

                    </div>

                </div>
                <div></div>
                <div class="row">

                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">
                        <label><b>Transaction Type:<b></label>

                    </div>


                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-2">

                        <asp:DropDownList ID="transTypeDropDownList" class="form-control" runat="server"
                            TabIndex="5">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Purchase of Share" Value="C"></asp:ListItem>
                            <asp:ListItem Text="Sale of Share" Value="S"></asp:ListItem>
                            <asp:ListItem Text="Right Share" Value="R"></asp:ListItem>
                            <asp:ListItem Text="Pre IPO Share" Value="I"></asp:ListItem>
                        </asp:DropDownList>
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

            
        $('#<%=FromTextBox.ClientID%>').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            //maxDate:"today",
            onSelect: function(selected) {
                    
            }
        });
             
        $('#<%=ToTextBox.ClientID%>').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            //maxDate:"today",
            onSelect: function(selected) {
                    
            }
        });
     
        $.validator.addMethod("DropDownListStatus", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Status."); 
       

        $("#aspnetForm").validate({
            
            rules: 
            {
               
                <%=FromTextBox.UniqueID %>: {
                    required: true
                },
                <%=ToTextBox.UniqueID %>: {
                    required:true
                }
            }, 
            messages: {
                <%=FromTextBox.UniqueID %>:{  
                    required: "* is required*",
                  
                },
                <%=ToTextBox.UniqueID %>:{  
                    required: "* is required*"
                }
            }
        });

        
    </script>
</asp:Content>

