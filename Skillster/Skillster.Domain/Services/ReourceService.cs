using System;
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
            throw new NotImplementedException();
            //return _resourceRepository.GetBySkillIdOrderByLevel(skill.Id).Take(count).Select(a=>a.Resource);
        }

    }
}