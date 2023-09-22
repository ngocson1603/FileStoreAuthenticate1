using System;
using Microsoft.AspNetCore.Mvc;
using FileStore.Filters;

namespace FileStore.Attributes
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {

        public JwtAuthorizeAttribute()
            : base(typeof(JwtAuthorizeFilter))
        {

        }
    }
}

