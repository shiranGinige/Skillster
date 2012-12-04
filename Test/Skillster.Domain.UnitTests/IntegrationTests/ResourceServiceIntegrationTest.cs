using FizzWare.NBuilder;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using System;
using System.Diagnostics;
using System.Linq;
using Skillster.Domain.Exceptions;

namespace Skillster.Domain.Tests.IntegrationTests
{
    [TestFixture]
    public class ResourceServiceIntegrationTest
    {
        UniqueRandomGenerator _generator = new UniqueRandomGenerator();
        public static DocumentStore Store;

        public IDocumentSession RavenSession { get; protected set; }

        [TestFixtureSetUp]
        public void Init()
        {

        }

        [SetUp]
        public void Setup()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            Store = new EmbeddableDocumentStore()
                        {
                            DataDirectory = "Data",
                            RunInMemory = true,
                            UseEmbeddedHttpServer = true
                        };
            //Store = new DocumentStore() {Url = "http://ssg:8081" };
            Store.Initialize();

            RavenSession = Store.OpenSession();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        [TearDown]
        public void TearDown()
        {
            RavenSession.Dispose();
            Store.Dispose();
        }

        [Test]
        public void AddResources_FetchAndVerify()
        {
            var resources = Builder<Resource>.CreateListOfSize(10).All().With(a => a.FirstName, "FN_").Build();
            foreach (var resource in resources)
            {
                RavenSession.Store(resource);

            }
            RavenSession.SaveChanges();

            var fetchedResources = RavenSession.Query<Resource>().Where(a => a.FirstName.StartsWith("FN_")).ToList();
            Assert.AreEqual(10, fetchedResources.Count);

        }

        [Test]
        public void AddResource_AddSkills_FetchAndVerify()
        {

            //Setup
            var resource = Builder<Resource>.CreateNew().With(a=>a.Id ,"ResourceId1").Build();
            var sampleSkills = Builder<Skill>.CreateListOfSize(5).Build();
            var resourceService = new ResourceService(RavenSession);
            foreach (var resoureSkill in sampleSkills)
            {
                resourceService.AddSkillForAResource(resource , resoureSkill , 4);
            }

            //Act
            RavenSession.Store(resource);
            RavenSession.SaveChanges();

            //Verify

            var fetchedResource =
                RavenSession.Query<Resource>().FirstOrDefault(a => a.Id == resource.Id);
            Assert.IsNotNull(fetchedResource);
            Assert.AreEqual(5, resource.Skills.Count());
            Assert.AreEqual(4, resource.Skills.First().Strength);

        }

        [Test]
        public void AddResourceForSkill_MakeSureResourceIsSavedOnlyOnce()
        {

            var resource = CreateSampleResource();
            RavenSession.Store(resource);
            RavenSession.SaveChanges();

            var resources = RavenSession.Query<Resource>().ToList();
            Assert.AreEqual(1 , resources.Count);

            var resourceService = new ResourceService(RavenSession);
            resourceService.AddSkillForAResource(resource , CreateSampleSkill() , 5);

            RavenSession.SaveChanges();
            var resourcesAgain = RavenSession.Query<Resource>().ToList();
            Assert.AreEqual(1, resourcesAgain.Count);
            Assert.AreEqual(1 , resourcesAgain.First().Skills.Count());

        }

        [Test]
        public void AddSkillsForAResource_MakeSureDuplicateAreHandled()
        {
            //Setup
            var resource = CreateSampleResource();
            var skill = CreateSampleSkill();

            //Act
            var resourceService = new ResourceService(RavenSession);
            resourceService.AddSkillForAResource(resource, skill, 4);

            var resource2 = CreateSampleResource();
            resourceService.AddSkillForAResource(resource2, skill, 3);
            
            RavenSession.SaveChanges();
            
            var skills = RavenSession.Query<Skill>().Where(a=>a.Name == skill.Name);
            
            //Assert
            Assert.AreEqual(1, skills.Count());
            
        }
        [Test]
        [ExpectedException(typeof(ResourceAlreadyPossessingThisSkillException))]
        public void AddSkillsForAResources_AddTheSameSkillTwice_AnExceptionShouldBeThrown()
        {
            var resource = CreateSampleResource();
            var skill = CreateSampleSkill();

            var resourceService = new ResourceService(RavenSession);
            resourceService.AddSkillForAResource(resource, skill, 2);
            resourceService.AddSkillForAResource(resource, skill, 3);

        }

        [Test]
        public void RemoveSkillFromResource_TheResourceShouldRemainOneSkillLess()
        {
            var resource = CreateSampleResource();
            var skill = CreateSampleSkill();
            var skill2 = CreateSampleSkill();
            
            
            var resourceService = new ResourceService(RavenSession);

            resourceService.AddSkillForAResource(resource, skill, 2);
            resourceService.AddSkillForAResource(resource, skill2, 3);

            RavenSession.SaveChanges();

            var fetchedResource = RavenSession.Load<Resource>(resource.Id);
            Assert.AreEqual(2 , fetchedResource.Skills.Count);
            
            resourceService.RemoveSkillFromResource(resource, skill);

            RavenSession.SaveChanges();

            var fetchedResource2 = RavenSession.Load<Resource>(resource.Id);
            Assert.AreEqual(1, fetchedResource2.Skills.Count);


        }


        private Resource CreateSampleResource()
        {
            return Builder<Resource>.CreateNew().With(a => a.Id, _generator.Next(1,int.MaxValue).ToString()).Build();
        }
        private Skill CreateSampleSkill()
        {
            return Builder<Skill>.CreateNew().With(a => a.Id, _generator.Next(1, int.MaxValue).ToString()).Build();
        }
    }

}

