<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PortfolioWithNonListedReportViewer.aspx.cs" Inherits="WebApplicationMIS.UI.ReportViewer.PortfolioWithNonListedReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Portfolio Statement Page</title>
</head>
<body>
    <form id="PfolioWithNonListed" runat="server">
    <div>
    <CR:CrystalReportViewer ID="CRV_PfolioWithNonListed" runat="server" 
            AutoDataBind="True" Height="80px" 
            Width="650px" />
    </div>
    </form>
</body>
</html>

