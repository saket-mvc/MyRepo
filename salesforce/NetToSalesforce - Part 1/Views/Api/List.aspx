<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>List Contacts (Web Service API)</title>
</head>
<body>
    <div>
        <p><a href="/">Return Home</a></p>
    </div>
    <div>
        <p>Contacts</p>
        <ul style="list-style-type: none;">
    <%
        foreach (NetToSalesforce.Api.Contact c in Model)
        {
			//Response.Write(
			//    String.Format("<li><a href='/api/edit/{0}'>{1}</a> {2}</li>",
			//        c.Id,
			//        c.Name,
			//        c.Email
			//        ));

			Response.Write(c.FirstName.ToString());//;, c.Name, c.Email);
			Response.Write("<br />");
					
        }
    %>
        </ul>
    </div>
</body>
</html>
