using Skillster.Domain.Exceptions;

namespace Skillster.Domain
{
    
    public class ResourceSkill 
    {

        public DenormalizedReference<Skill> Skill { get; set; }

        private int _strength;
        public int Strength
        {
            get { return _strength; }
            set
            {
                if (value > 5)
                    throw new MaximumSkillLevelExceededException();
                if (value < 1)
                    throw new MinimumSkillLevelExceededException();

                _strength = value;
            }
        }
    }
}