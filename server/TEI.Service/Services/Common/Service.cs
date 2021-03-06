﻿using System;
using TEI.Common;

namespace TEI.Service.Services
{
    public abstract class Service : IDisposable
    {
        private IUnitOfWork _unitOfWork;
        private static readonly object padlock = new object();

        public IUnitOfWork UnitOfWork
        {
            protected get
            {
                return _unitOfWork;
            }
            set
            {
                if (_unitOfWork == null)
                {
                    lock (padlock)
                    {
                        if (_unitOfWork == null)
                        {
                            _unitOfWork = value;
                        }
                    }
                }
            }
        }

        public bool IsUnitOfWorkInjected => UnitOfWork != null;

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
