using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Utilities.Interfaces
{
    public interface IEntity<TIdentity>
    {
        TIdentity Id { get; set; }
        DateTime? CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
