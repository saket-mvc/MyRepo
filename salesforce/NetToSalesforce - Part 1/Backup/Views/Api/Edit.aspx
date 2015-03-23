<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Edit Contact</title>
</head>
<body>
    <div>
        <p><a href="/">Return Home</a></p>
    </div>
    <div>
        <% 
            NetToSalesforce.Api.Contact contact = (NetToSalesforce.Api.Contact)Model; 
        %>
        <% Html.BeginForm("Update", "Api"); %>
            <p>Contact</p>
            <ul style="list-style-type: none;">
                <li>First Name: <%= Html.Label(contact.FirstName) %></li>
                <li>Last Name:<%= Html.Label(contact.LastName) %> </li>
                <li>Email: <%= Html.TextBox("Email", contact.Email) %></li>
            </ul>

            <input type="submit" value="Save" />

            <%= Html.Hidden("Id", contact.Id)%>
        <% Html.EndForm(); %>
    </div>
</body>
</html>
