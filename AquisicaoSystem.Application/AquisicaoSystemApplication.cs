var softwareSystemBuilder = SoftwareSystem.CreateBuilder<AquisicaoSystem>();

var app = softwareSystemBuilder.Builder.Build();

await app.RunAsync();