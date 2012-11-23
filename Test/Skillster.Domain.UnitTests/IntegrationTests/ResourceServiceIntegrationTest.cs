using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Transactions;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace Skillster.Domain.Tests.IntegrationTests
{
    [TestFixture]
    public class ResourceServiceIntegrationTest
    {
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
            Store.Initialize();

            RavenSession = Store.OpenSession();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void AddResources_FetchAndVerify()
        {

            for (int i = 0; i < 10; i++)
            {
                RavenSession.Store(CreateNewResource());
            }

            RavenSession.SaveChanges();

            var resources = RavenSession.Query<Resource>().Where(a => a.FirstName.StartsWith("FN_")).ToList();
            Assert.AreEqual(10, resources.Count);

        }

        [Test]
        public void AddResource_AddSkills_FetchAndVerify()
        {

            //Setup
            var resource = CreateNewResource();
            resource.AddSkill(new Skill() { Id = 1, Name = "test skill 1" }, 4);
            resource.AddSkill(new Skill() { Id = 2, Name = "test skill 2" }, 3);
            resource.AddSkill(new Skill() { Id = 3, Name = "test skill 3" }, 5);

            //Act
            RavenSession.Store(resource);
            RavenSession.SaveChanges();

            //Verify

            var fetchedResource =
                RavenSession.Query<Resource>().FirstOrDefault(a => a.ResourceId == resource.ResourceId);
            Assert.IsNotNull(fetchedResource);
            Assert.AreEqual(3, resource.Skills.Count());


        }


        private Resource CreateNewResource()
        {
            return new Resource() { FirstName = "FN_" + Guid.NewGuid().ToString(), SecondName = "LN_" + Guid.NewGuid().ToString(), ResourceId = Guid.NewGuid().GetHashCode() };
        }
    }
}