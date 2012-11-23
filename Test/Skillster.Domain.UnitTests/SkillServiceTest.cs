using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Skillster.Domain.Services;

namespace Skillster.Domain.Tests
{
    //[TestFixture]
    //public class SkillServiceTest
    //{
    //    [Test]
    //    public void GetSkillById()
    //    {
    //        //Setup
    //        var skill = CreateANewSkill();
    //        Mock<ISkillsRepository> skillRepository = new Mock<ISkillsRepository>();
    //        var skillService = new SkillsService(skillRepository.Object);
    //        skillRepository.Setup(a => a.GetById(skill.SkillId)).Returns(skill);
            
    //        //Act
    //        var retrievedSkill =  skillService.GetById(skill.SkillId);

    //        //Assert
    //        Assert.AreEqual(retrievedSkill , skill);
    //    }

    //    [Test]
    //    public void GetTopFivePerformersForAGivenSkill()
    //    {
    //        //Setup
    //        var skill = CreateANewSkill();
    //        var skillResources = CreateAListOfResourceSkills(skill);
            
    //        Mock<IResourceRepository> resourceRepository = new Mock<IResourceRepository>();
    //        resourceRepository.Setup(a => a.GetBySkillIdOrderByLevel(skill.SkillId)).Returns(skillResources.AsQueryable());

    //        //Act
    //        var retrievedResources = (new ResourceService(resourceRepository.Object)).GetTopPerformers(skill, 6);

    //        //Asseert
    //        Assert.AreEqual(retrievedResources.Count() , 6);
    //    }

    //    [Test]
    //    public void AddSkillsForAResource()
    //    {
    //        var resource = CreateNewResource();

    //        resource.AddSkill(new Skill() { } , 1);
    //        resource.AddSkill(new Skill() { } , 1);

    //        Assert.That(resource.Skills.Count().Equals(2));
    //    }

    //    private Skill CreateANewSkill()
    //    {

    //        var skill = new Skill() {SkillId = Guid.NewGuid().GetHashCode(), SkillDescription = "C# language experience", SkillName = "C#"};
    //        return skill;
    //    }

    //    private Resource CreateNewResource()
    //    {
    //        return new Resource() { FirstName = "FN_" + Guid.NewGuid().ToString(), SecondName = "LN_" + Guid.NewGuid().ToString() , ResourceId = Guid.NewGuid().GetHashCode()};
    //    }
        
    //    private IList<ResourceSkill> CreateAListOfResourceSkills(Skill skill )
    //    {
    //        Resource resource = CreateNewResource();
    //        var resourceSkills = new List<ResourceSkill>();
    //        for (int i = 0; i < 10; i++)
    //        {
    //            resourceSkills.Add( new ResourceSkill(resource , skill ,2 ));
    //        }

    //        return resourceSkills;
    //    }


    //}
}