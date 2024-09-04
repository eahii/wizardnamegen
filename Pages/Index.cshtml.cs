@page
@model IndexModel

@{
    ViewData["Title"] = "Wizard Name Generator";
}

<head>
    <link href="https://fonts.googleapis.com/css2?family=Garamond:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="~/css/site.css">
    <title>@ViewData["Title"]</title>
</head>

<body>
    <div class="container">
        <div class="form-container">
            <h2>@ViewData["Title"]</h2>

            <form method="post">
                <button type="submit" asp-page-handler="GenerateName">Generate Name</button>
            </form>

            @if (!string.IsNullOrEmpty(Model.GeneratedName))
            {
                <p class="wizard-name">Your wizard name: <strong>@Model.GeneratedName</strong></p>
            }
        </div>
    </div>
</body>
