<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Index</title>
</head>
<body>
    <div>
        <p>Calls to Salesforce</p>
        <ul>
            <li><a href="/Api/List/">Web Service API</a></li>
            <li><a href="Api/ListAsync/">Async Query Example</a></li>
        </ul>
    </div>
</body>
</html>
