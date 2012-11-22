using System;
using Skillster.Domain.Exceptions;

namespace Skillster.Domain
{
    public class ResourceSkill
    {
        public Resource Resource { get; set; }
        public Skill Skill { get; set; }

        private int _level;
        public int Level
        {
            get { return _level; } 
            set
            {
                if (_level > 5)
                    throw new MaximumSkillLevelExceededException();
                _level = value;
            }
        }
    }

}