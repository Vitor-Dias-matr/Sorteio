using Domain.Model;
using System.Collections.Generic;

namespace Domain.Infra
{
    public interface IFamiliaRepository
    {
        List<Familia> Query();
    }
}
