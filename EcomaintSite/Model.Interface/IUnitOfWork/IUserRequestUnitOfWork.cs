namespace Model.Interface
{
    public interface IUserRequestUnitOfWork
    {
        IUserRequestRepository UserRequestRepository { get; }
        IUserRequestDetailRepository UserRequestDetailRepository { get; }
        IUserRequestComponentRepository UserRequestComponentRepository { get; }
        IUserRequestDocumentRepository UserRequestDocumentRepository { get; }
        void Save();
    }
}
