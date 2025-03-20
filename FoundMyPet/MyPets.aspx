<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MyPets.aspx.cs" Inherits="FoundMyPet.MyPets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                <div id="petsContainer" runat="server">
                    <asp:Repeater ID="RepeatInformation" runat="server">
                        <HeaderTemplate>
                            <div class="pets-list">
                        </HeaderTemplate>

                        <ItemTemplate>
                            <div class="pet-card d-flex align-items-center p-3 shadow-sm">

                                <div>
                                    <img class="pet-photo" src="<%# Eval("pet_photo") %>" alt="Pet Image">
                                </div>

                                <div class="pet-info ms-3">
                                    <p><strong>Name:</strong> <%# Eval("pet_name") %></p>
                                    <p><strong>Contact:</strong> <%# Eval("contact_details") %></p>
                                    <p><strong>Lost at:</strong> <%# Eval("lost_location") %></p>
                                </div>

                                <div class="ms-auto">
                                    <a href="#" class="btn btn-success" onclick='markAsFound(<%# Eval("pet_id")%>)'>Found</a>
                                </div>
                            </div>
                        </ItemTemplate>

                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

                <div id="Div1" runat="server" visible="false" class="text-center mt-4">
                    <h5 class="text-muted">No pets found.</h5>
                </div>
            </div>

            <div id="noDataMessage" runat="server" visible="false" class="text-center mt-4">
                <h5 class="text-muted">No pets found.</h5>
            </div>

        </div>
    </div>
    <script>


        function markAsFound(id) {
            Swal.fire({
                title: "Are you sure?",
                text: "Once marked as found, you cannot undo this action!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#28a745",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, mark as found!",
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ ID: id }),
                        url: "MyPets.aspx/found",
                        success: function (data) {
                            if (data.d == 1) {
                                Swal.fire({
                                    title: "Success!",
                                    text: "Pet has been marked as found.",
                                    icon: "success",
                                    confirmButtonColor: "#28a745",
                                }).then(() => {
                                    window.location.reload();
                                });
                            } else {
                                Swal.fire({
                                    title: "Error!",
                                    text: "Failed to update record.",
                                    icon: "error",
                                    confirmButtonColor: "#d33",
                                });
                            }
                        },
                        error: function () {
                            Swal.fire({
                                title: "Error!",
                                text: "Something went wrong. Try again later.",
                                icon: "error",
                                confirmButtonColor: "#d33",
                            });
                        },
                    });
                }
            });
        }
    </script>

</asp:Content>
