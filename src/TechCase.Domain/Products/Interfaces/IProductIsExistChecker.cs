using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCase.Domain.Products.Interfaces
{
    public interface IProductIsExistChecker: IChecker
    {
        bool IsExist(List<long> ids);
    }
}
