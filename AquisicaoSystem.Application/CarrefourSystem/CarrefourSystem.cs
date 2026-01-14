public class CarrefourSystem(IDistributedApplicationBuilder builder) : SoftwareSystem(builder)
{
    const string SystemName = nameof(CarrefourSystem);
    protected override string Name { get; init; } = SystemName;

    protected const string CarrefourDiagramsPage = "https://diagrams.carrefoursolucoes.com.br/";
    protected override string SystemDiagramUrl { get; init; } = CarrefourDiagramsPage;
}