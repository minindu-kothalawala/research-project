namespace ResearchProject.Factories
{
    public interface IProductRepositoryFactory
    {
        Repositories.IProductRepository Create();
    }
}
