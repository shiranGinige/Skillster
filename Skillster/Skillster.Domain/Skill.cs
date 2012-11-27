namespace Skillster.Domain
{
    public class Skill : INamedDocument
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SkillDescription { get; set; }
        public SkillGroup Group { get; set; }

    }

 
}
