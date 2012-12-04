using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Skillster.Domain.Exceptions;

namespace Skillster.Domain.Tests
{
    public class ResourceService : BaseService
    {
        public ResourceService(IDocumentSession documentSession):base(documentSession)
        {
        }

        public IEnumerable<Resource> GetTopPerformers(Skill skill , int count = 5)
        {
            throw new NotImplementedException();
            //return _resourceRepository.GetBySkillIdOrderByLevel(skill.Id).Take(count).Select(a=>a.Resource);
        }

        public void AddSkillForAResource(Resource resource , Skill skill , int strength)
        {
            var skillAlreadyExisting = DocumentSession.Load<Skill>(skill.Id);
            if (skillAlreadyExisting == null)
            {
                DocumentSession.Store(skill);
            }

            if (isResourceAlreadyHaveTheSkill(resource, skill))
                throw new ResourceAlreadyPossessingThisSkillException();

            resource.AddSkill(skill , strength);
            DocumentSession.Store(resource);
        }


        private bool isResourceAlreadyHaveTheSkill(Resource resource, Skill skill)
        {
            if (resource.Skills.Count == 0)
                return false;

            return  resource.Skills.FirstOrDefault(a => a.Skill.Id == skill.Id) != null;
        }

        public void RemoveSkillFromResource(Resource resource, Skill skill)
        {
            resource.RemoveSkill(skill);
        }
    }

    public abstract class BaseService
    {
        public BaseService(IDocumentSession documentSession)
        {
            DocumentSession = documentSession;
        }

        protected IDocumentSession DocumentSession { get; set; }
    }
}