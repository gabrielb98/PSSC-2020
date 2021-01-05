using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IQuestionGrain : Orleans.IGrainWithIntegerKey
    {
        Task OnActivateAsync();
    }

}
