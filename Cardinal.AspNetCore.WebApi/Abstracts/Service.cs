using Cardinal.AspNetCore.Repositories;
using Cardinal.AspNetCore.Services;
using System;

namespace Cardinal.AspNetCore.WebApi.Abstracts
{
    public abstract class Service<TRepository> : Service where TRepository : IRepository
    {
        protected TRepository Repository { get; set; }

        public Service(IServiceProvider provider) : base(provider)
        {
            this.Repository = this.GetService<TRepository>();
        }
    }

}
