using Model.Interface;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Model.UnitOfWork
{
    
    public class UserRequestUnitOfWork : IUserRequestUnitOfWork, IDisposable
    {
        private Model.Data.Model1 db;
        public UserRequestUnitOfWork() => db = new Model.Data.Model1();

        private IUserRequestRepository _userRequestRepository = null;
        private IUserRequestDetailRepository _userRequestDetailRepository = null;
        private IUserRequestComponentRepository _userRequestComponentRepository = null;
        private IUserRequestDocumentRepository _userRequestDocumentRepository = null;

        public IUserRequestRepository UserRequestRepository
        {
            get => this._userRequestRepository ?? new UserRequestRepository(db);
        }

        public IUserRequestDetailRepository UserRequestDetailRepository
        {
            get => this._userRequestDetailRepository ?? new UserRequestDetailRepository(db);
        }

        public IUserRequestComponentRepository UserRequestComponentRepository
        {
            get => this._userRequestComponentRepository ?? new UserRequestComponentRepository(db);
        }

        public IUserRequestDocumentRepository UserRequestDocumentRepository
        {
            get => this._userRequestDocumentRepository ?? new UserRequestDocumentRepository(db);
        }

        public void Save() => db.SaveChanges();

        private bool disposed = false;
        protected  void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
