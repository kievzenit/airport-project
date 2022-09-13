using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Common.Abstract
{
    public interface ICaster<InputType, OutputType>
    {
        public Task<OutputType> Cast(InputType inputObject, CancellationToken cancellationToken);
        public Task<ICollection<OutputType>> Cast(
            ICollection<InputType> inputObjects, CancellationToken cancellationToken);
    }
}
