using System.Collections.Generic;
using System.Linq;

namespace Skillster.Domain.Tests
{
    public class ResourceService
    {
        protected IResourceRepository _resourceRepository;
        public ResourceService(IResourceRepository  resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public IEnumerable<Resource> GetTopPerformers(Skill skill , int count = 5)
        {
            return _resourceRepository.GetBySkillIdOrderByLevel(skill.SkillId).Take(count).Select(a=>a.Resource);
        }

    }
}