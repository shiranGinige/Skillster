using System.Collections.Generic;
using System.Linq;

namespace Skillster.Domain.Tests
{
    //So far repos are obsolete with Raven DB. Planning to keep it that way :) 
    public interface IResourceRepository
    {
        IQueryable<ResourceSkill> GetBySkillIdOrderByLevel(int skillId);
    }
}