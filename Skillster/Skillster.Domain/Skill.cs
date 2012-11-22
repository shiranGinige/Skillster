namespace Skillster.Domain
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }
        public SkillGroup Group { get; set; }

    }

 
}
