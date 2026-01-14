using Aspire.Hosting.JavaScript;

public class AquisicaoSystem(IDistributedApplicationBuilder builder) : KeycloakSystem(builder)
{
    const string SystemName = nameof(AquisicaoSystem);
    protected override string Name { get; init; } = SystemName;
    protected override string SystemDiagramUrl { get; init; } = "https://s.icepanel.io/6u4TEKQpKS43bz/Q0dS/";//$"{CarrefourDiagramsPage}{SystemName}/";
    protected override string SystemUrl { get; init; } = "https://aquisicao.carrefoursolucoes.com.br/";
    public Service<ProjectResource> WebApi { get { return GetService<Service<ProjectResource>>(); } }
    public Service<JavaScriptAppResource> WebApp { get { return GetService<Service<JavaScriptAppResource>>(); } }

    public override IResourceBuilder<ExternalServiceResource> AddSystem()
    {
        var system = base.AddSystem();
        AddWebApi();
        AddWebApp();
        return system
                .WithChildRelationship(WebApi.Resource)
                .WithChildRelationship(WebApp.Resource);
    }

    private void AddWebApi()
    {
        var webApiService = AddService<Service<ProjectResource>>(nameof(WebApi));
        webApiService.Resource = Builder.AddProject(WebApi.Name, $"../{SystemName}.WebApi")
                .WithReferenceRelationship(KeycloakService.Resource)
                .WaitFor(KeycloakService.Resource)
                .WithHttpEndpoint(name: WebApi.Name, port: WebApi.Port, isProxied: false);
    }

    private void AddWebApp()
    {
        var webAppService = AddService<Service<JavaScriptAppResource>>(nameof(WebApp));
        webAppService.Resource = Builder.AddJavaScriptApp(webAppService.Name, $"../{SystemName}.WebApp")
                            .WaitFor(WebApi.Resource)
                            .WithHttpEndpoint(name: webAppService.Name, port: webAppService.Port, isProxied: false)
                            .WithEnvironment("PORT", webAppService.Port.ToString());
    }
}