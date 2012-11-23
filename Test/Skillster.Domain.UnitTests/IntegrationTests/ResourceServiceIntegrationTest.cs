using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Transactions;
using FizzWare.NBuilder;
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
            var resource = Builder<Resource>.CreateNew().Build();
            var sampleSkills = Builder<Skill>.CreateListOfSize(5).Build();
            foreach (var resoureSkill in sampleSkills)
            {
                resource.AddSkill(resoureSkill, 4);
            }

            //Act
            RavenSession.Store(resource);
            RavenSession.SaveChanges();

            //Verify

            var fetchedResource =
                RavenSession.Query<Resource>().FirstOrDefault(a => a.Id == resource.Id);
            Assert.IsNotNull(fetchedResource);
            Assert.AreEqual(5, resource.Skills.Count());
            Assert.AreEqual(1, resource.Skills.First().Skill.Id);


        }
    }

}