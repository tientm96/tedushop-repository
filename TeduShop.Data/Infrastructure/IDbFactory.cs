﻿using System;

namespace TeduShop.Data.Infrastructure
{
    //IDisposable là Interface có sẵn: cho phép các class kế thừa từ nó sẽ tự động hủy.
    public interface IDbFactory : IDisposable
    {
        //method init tạo từ DbContext
        TeduShopDbContext Init();
    }
}