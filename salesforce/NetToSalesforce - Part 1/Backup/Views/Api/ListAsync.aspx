<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>List Contacts Async</title>
    <script src="/Scripts/jquery-1.4.4.js" type="text/javascript"></script>
    <script type="text/javascript">
        function startQuery() {
            if ($("#ID").val() == "") {
                $.ajax({
                    type: "POST",
                    url: "/Api/StartQuery",
                    dataType: "json",
                    success: function (result) { $("#ID").val(result.id) }
                });
            }
        }

        function getQuery() {

            if ($("#ID").val() != "") {
                var id = { "id": $("#ID").val() };

                $.ajax({
                    type: "POST",
                    url: "/Api/GetQuery",
                    data: id,
                    dataType: "json",
                    success: function (result) {
                        $("#Contacts").empty();
                        $("#Contacts").append("<p>Contacts</p>");
                        var text = "<ul style='list-style-type: none;'>";

                        for (var i = 0; i < result.contacts.length; i++) {
                            text = text + "<li>";
                            text = text + result.contacts[i].FirstName + " ";
                            text = text + result.contacts[i].LastName + " (";
                            text = text + result.contacts[i].Email + ")";
                            text = text + "</li>";
                        }

                        $("#Contacts").append(text + "</ul>");
                    }
                });
            }
        }
    </script>
</head>
<body>
    <div id="Contacts">
        No contacts listed.
        <br />
    </div>
    <div>
        <input type="hidden" id="ID" />
        <button id="StartButton" onclick="startQuery()">Start Query</button>
        <button id="GetButton" onclick="getQuery()">Get Results</button>
    </div>
</body>
</html>