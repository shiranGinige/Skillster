namespace Skillster.Domain.Services
{
    public interface ISkillsRepository
    {
        Skill GetById(int skillId);
    }
}