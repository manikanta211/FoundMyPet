<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="FoundMyPet.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">

</head>
<body>
    <form id="form1" runat="server">
   <div class="d-flex justify-content-center align-items-center vh-100">
    <div class="card p-4 shadow-lg" style="width: 350px;">
        <h3 class="text-center">Login</h3>

        <div class="mb-3">
            <label for="email" class="form-label">Email address</label>
            <input type="email" runat="server" class="form-control" id="email" aria-describedby="emailHelp" required />
            <div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>
        </div>

        <div class="mb-3 form-check">
            <input type="checkbox" class="form-check-input" id="rememberMe" runat="server" />
            <label class="form-check-label" for="rememberMe">Remember me</label>
        </div>

        <input type="submit" class="btn btn-primary w-100" runat="server" value="Login" onserverclick="Login_ServerClick" />

        <div class="text-center mt-3">
            <a href="index.aspx" class="text-decoration-none">← Back to Home</a>
        </div>
    </div>
</div>

    </form>
</body>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
</html>
