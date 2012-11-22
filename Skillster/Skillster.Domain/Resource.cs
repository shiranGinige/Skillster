using System.Collections.Generic;

namespace Skillster.Domain
{
    public class Resource
    {
        public int ResourceId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        private IList<Skill> _skills;
        public IEnumerable<Skill> Skills
        {
            get { return _skills; }
        }

        public void AddSkill(Skill skill)
        {
            if(_skills == null)
                _skills = new List<Skill>();

            if(!_skills.Contains(skill))
            _skills.Add(skill);
        }


    }
}