<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FoundMyPet.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Pet Listings -->
<!-- Pet Listings -->
<div class="container mt-4">
    <div class="row">
        <div class="col-md-8 offset-md-2">
            <div id="petList"></div> 
        </div>
    </div>
 
</div>



<!-- Loader -->
<div id="loading" style="display: none; text-align: center;">
    <img src="img/loader.gif" width="50px">
</div>

</asp:Content>
