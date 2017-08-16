@Code
    ViewData("Title") = "Contact Page"
End Code


<div class="row" ng-app="contacts">
    <div class="col-sm-12" ng-view>

    </div>
</div>

@section scripts
    <script src="~/Scripts/app/contactModule.js"></script>
End section