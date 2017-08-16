Imports System.Web.Optimization
Imports System.Web.Http
Imports System.Web.Routing

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
End Class
