using System.Collections.Generic;
using System.Linq;

namespace Skillster.Domain
{
    public class Resource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        private IList<ResourceSkill> _skills;
        public IEnumerable<ResourceSkill> Skills
        {
            get { return _skills; }
        }

        public void AddSkill(Skill skill , int skillStrength)
        {
            if(_skills == null)
                _skills = new List<ResourceSkill>();

            if(!_skills.Select(a=>a.Skill).Contains(skill))
            _skills.Add(new ResourceSkill(){Skill = skill , Strength = skillStrength});
        }


    }
}