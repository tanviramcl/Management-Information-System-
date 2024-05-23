﻿<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="form.aspx.cs" Inherits="UI_Home" Title=" ICB ASSET MANAGEMENT COMPANY LIMITED" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <form id="form" runat="server" method="post">
     <div class="row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <div class="section-block" id="basicform">
                                    <h3 class="section-title">Basic Form Elements</h3>
                                    <p>Use custom button styles for actions in forms, dialogs, and more with support for multiple sizes, states, and more.</p>
                                </div>
                                <div class="card">
                                    <h5 class="card-header">Basic Form</h5>
                                    <div class="card-body">
                                       
                                            <div class="form-group">
                                                <label for="inputText3" class="col-form-label">Input Text</label>
                                                <input id="inputText3" type="text" class="form-control">
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail">Email address</label>
                                                <input id="inputEmail" type="email" placeholder="name@example.com" class="form-control">
                                                <p>We'll never share your email with anyone else.</p>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputText4" class="col-form-label">Number Input</label>
                                                <input id="inputText4" type="number" class="form-control" placeholder="Numbers">
                                            </div>
                                            <div class="form-group">
                                                <label for="inputPassword">Password</label>
                                                <input id="inputPassword" type="password" placeholder="Password" class="form-control">
                                            </div>
                                            <div class="custom-file mb-3">
                                                <input type="file" class="custom-file-input" id="customFile">
                                                <label class="custom-file-label" for="customFile">File Input</label>
                                            </div>
                                            <div class="form-group">
                                                <label for="exampleFormControlTextarea1">Example textarea</label>
                                                <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
                                            </div>
                                       
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 ">

                <asp:Button ID="ButtonSubmitt" Text="Save" class="btn btn-primary" runat="server" OnClick="Button1_Click" />
            </div>
                        </div>
     
                     </form>
    
    <script type="text/javascript">
    
     
    
     

        $("#aspnetForm").validate({
            submitHandler: function () {
                test();
            },
            rules: 
            {
              
            }, 
            messages: {
               
          
        }
        });

        function test() {
      
        }
    </script>
</asp:Content>

