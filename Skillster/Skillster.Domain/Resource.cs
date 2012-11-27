using System.Collections.Generic;
using System.Linq;

namespace Skillster.Domain
{
    public class Resource
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        private IList<ResourceSkill> _skills = new List<ResourceSkill>();
        public ICollection<ResourceSkill> Skills
        {
            get { return _skills; }
        }

        internal void AddSkill(Skill skill , int skillStrength)
        {
            if(_skills == null)
                _skills = new List<ResourceSkill>();

            if(!_skills.Select(a=>a.Skill).Contains(skill))
            _skills.Add(new ResourceSkill(){Skill = skill , Strength = skillStrength});
        }


    }
}