<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeBehind="SaleSms.aspx.cs" Inherits="WebApplicationMIS.UI.SaleSms" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="aspnetForm" runat="server" method="post">
        <div class="section-block" id="basicform">
            <h3 class="section-title">Sale SMS send </h3>

        </div>
        <div class="card">
            <div class="card-body">
                <div class="form-row">
                    <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                        <label for="input-select">Fund Name</label>
                        <asp:DropDownList ID="fundNameDropDownList" class="form-control" runat="server" TabIndex="6"></asp:DropDownList>

                    </div>
                </div>
                <div class="form-row">

                    <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12 mb-2">
                        <label class="col-form-label">Sale Number From</label>
                        <asp:TextBox ID="txtSaleNumberFrom" class="form-control" runat="server"></asp:TextBox>
                        <label class="col-form-label">Sale Number To</label>
                        <asp:TextBox ID="txtSaleNumberTo" class="form-control" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="form-row">
                    <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                        <label for="exampleFormControlTextarea1">Message</label>
                        <textarea id="TextareaMessage" cols="20" class="form-control" rows="2" runat="server"></textarea>
                    </div>
                </div>

                <div class="form-row">
                <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8 col-8">
                    <label for="exampleFormControlFormat">Format:</label>

                    <asp:dropdownlist runat="server"  class="form-control" AutoPostBack="true" id="ddlTest">
                         <asp:ListItem Text="Excel" Value="Excel" Selected="True" />
                        <asp:ListItem Text="Word" Value="Word"  />
                        <asp:ListItem Text="PDF" Value="PDF" />
                        <asp:ListItem Text="CSV" Value="CSV" />
                    </asp:dropdownlist>

                
                </div>
            </div>
                 
    

            </div>
        </div>
        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 ">

            <asp:Button ID="ButtonSubmitt" Text="Export" class="btn btn-primary"  onclick="sendButton_Click" runat="server"  />
            <a href="https://powersms.banglaphone.net.bd/" target="_blank"  class="btn btn-primary">Sent SMS</a>
            
        </div>
    </form>



</asp:Content>
