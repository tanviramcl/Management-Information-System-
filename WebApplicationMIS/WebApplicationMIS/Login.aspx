<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplicationMIS.Login" %>

<!doctype html>
<html lang="en">
 
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Login</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="assets/vendor/bootstrap/css/bootstrap.min.css">
    <link href="assets/vendor/fonts/circular-std/style.css" rel="stylesheet">
    <link rel="stylesheet" href="assets/libs/css/style.css">
    <link rel="stylesheet" href="assets/vendor/fonts/fontawesome/css/fontawesome-all.css">
    <script src="scripts/jquery.js"></script>
    <script src="scripts/jquery.validate.js"></script>
    <script src="scripts/jquery.validation.net.webforms.js"></script>
    <script src="scripts/jquery.validation.net.webforms.min.js"></script>
    <style>
    html,
    body {
        height: 100%;
    }

    body {
        display: -ms-flexbox;
        display: flex;
        -ms-flex-align: center;
        align-items: center;
        padding-top: 40px;
        padding-bottom: 40px;
    }
    .error {
            color: red;
            display: inline-flex;
        }
    </style>
    
  <script language="javascript" type="text/javascript"> 
    function fnValidation()
    {
         if(document.getElementById("<%=loginIDTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=loginIDTextBox.ClientID%>").focus();
            alert("Please Enter LoginID");
            return false;
            
        }
        if(document.getElementById("<%=loginPasswardTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=loginPasswardTextBox.ClientID%>").focus();
            alert("Please Enter Login Password");
            return false;   
        }
        
   <%--   //  if(document.getElementById("<%=txtCaptcha.ClientID%>").value =="")
      //  {
      //    document.getElementById("<%=txtCaptcha.ClientID%>").focus();
      //     alert("Type Captcha Code");
      //      return false;
      //  }  --%>
    }
  </script>
</head>

<body>
    <!-- ============================================================== -->
    <!-- login page  -->
    <!-- ============================================================== -->
    <div class="splash-container">
        <div class="card ">
            <div class="card-header text-center"><a href="../index.html" class="front-brand">Management Information System (MIS) For IAMCL</a><span class="splash-description">Please enter your user information.</span></div>
            <div class="card-body">
                <form id="aspnetForm" runat="server" method="post">
                    <div class="form-group">
                           <asp:TextBox ID="loginIDTextBox" class="form-control form-control-lg" placeholder="Username" autocomplete="off"   runat="server"></asp:TextBox>

                    </div>
                    <div class="form-group">
                    
                        <asp:TextBox ID="loginPasswardTextBox"   placeholder="Password" class="form-control form-control-lg" runat="server" TextMode="Password"   ></asp:TextBox>
                    </div>
            

                    <asp:Button ID="loginButton" runat="server" class="btn btn-primary btn-lg btn-block" Text="Login"  OnClientClick="return fnValidation();" onclick="loginButton_Click"  />
                </form>
            </div>
            <div class="card-footer bg-white p-0  ">
            <asp:Label runat="server" ID="loginErrorLabel" CssClass="error" Visible="false" Text="" class="style5"></asp:Label>
            
            </div>
        </div>
    </div>
  
    <!-- ============================================================== -->
    <!-- end login page  -->
    <!-- ============================================================== -->
    <!-- Optional JavaScript -->


    <script src="assets/vendor/jquery/jquery-3.3.1.min.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.js"></script>
</body>
 
</html>
