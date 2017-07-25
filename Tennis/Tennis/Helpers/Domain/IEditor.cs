namespace Tennis.Helpers.Domain
{
    public interface IEditor<T>
        where T : class
    {
        void ValidateModifications();
    }
}