using System;


namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get;}
    }
}
