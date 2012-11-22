namespace Skillster.Domain.Services
{
    public class SkillsService
    {
        protected ISkillsRepository _skillsRepository;

        public SkillsService(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        public Skill GetById(int skillId)
        {
            return _skillsRepository.GetById(skillId);
        }
    }
}
 