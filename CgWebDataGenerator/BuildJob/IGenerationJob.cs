using CGDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CgWebDataGenerator.BuildJob
{
    public interface IGenerationJob
    {
        void InitalizeGenerationJob();
        void PerformGenerationJob(CGWebEntities webEntities);
    }
}
