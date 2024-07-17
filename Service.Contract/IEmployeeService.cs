using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId,bool trackChanges);
        EmployeeDto GetEmployee(Guid companyId,Guid employeeId, bool trackChanges);
    }
}
