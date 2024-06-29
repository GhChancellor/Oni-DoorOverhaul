using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Door_Overhaul
{
    internal interface IDoorOperations
    {
        (float[], float) Create();
        (float[], float) Replace();
        void Destroy(Deconstructable deconstructable);
    }
}
