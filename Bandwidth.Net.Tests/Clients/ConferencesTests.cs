﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Fakes;
using System.Threading.Tasks;
using Bandwidth.Net.Data;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Net.Tests.Clients
{
    [TestClass]
    public class ConferencesTests
    {
        [TestMethod]
        public void CreateTest()
        {
            using (ShimsContext.Create())
            {
                ShimHttpClient.AllInstances.PostAsyncStringHttpContent = (c, url, content) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences", Helper.UserId), url);
                    var conference = Helper.ParseJsonContent<Conference>(content).Result;
                    Assert.AreEqual("http://localhost/", conference.CallbackUrl.ToString());
                    Assert.AreEqual("From", conference.From);
                    var response = new HttpResponseMessage(HttpStatusCode.Created);
                    response.Headers.Add("Location", string.Format("/v1/users/{0}/conferences/1", Helper.UserId));
                    return Task.Run(() => response);
                };
                using (var client = Helper.CreateClient())
                {
                    var id = client.Conferences.Create(new Conference
                    {
                        CallbackUrl = new Uri("http://localhost/"),
                        From = "From"
                    }).Result;
                    Assert.AreEqual("1", id);
                }
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (ShimsContext.Create())
            {
                ShimHttpClient.AllInstances.PostAsyncStringHttpContent = (c, url, content) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences/1", Helper.UserId), url);
                    var conference = Helper.ParseJsonContent<Conference>(content).Result;
                    Assert.AreEqual("http://localhost/", conference.CallbackUrl.ToString());
                    Assert.AreEqual("From", conference.From);
                    var response = new HttpResponseMessage(HttpStatusCode.Created);
                    response.Headers.Add("Location", string.Format("/v1/users/{0}/conferences/1", Helper.UserId));
                    return Task.Run(() => response);
                };
                using (var client = Helper.CreateClient())
                {
                    client.Conferences.Update("1", new Conference
                    {
                        CallbackUrl = new Uri("http://localhost/"),
                        From = "From"
                    }).Wait();
                }
            }
        }

        [TestMethod]
        public void GetTest()
        {
            using (ShimsContext.Create())
            {
                var conference = new Conference
                {
                    Id = "1",
                    CallbackUrl = new Uri("http://localhost/"),
                    From = "From"
                };
                ShimHttpClient.AllInstances.GetAsyncString = (c, url) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences/1", Helper.UserId), url);
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = Helper.CreateJsonContent(conference)
                    };
                    return Task.Run(() => response);
                };
                using (var client = Helper.CreateClient())
                {
                    var result = client.Conferences.Get("1").Result;
                    Helper.AssertObjects(conference, result);
                }
            }
        }

        
        [TestMethod]
        public void SetAudioTest()
        {
            using (ShimsContext.Create())
            {
                ShimHttpClient.AllInstances.PostAsyncStringHttpContent = (c, url, content) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences/1/audio", Helper.UserId), url);
                    var audio = Helper.ParseJsonContent<Audio>(content).Result;
                    Assert.AreEqual("Test", audio.Sentence);
                    return Task.Run(() => new HttpResponseMessage(HttpStatusCode.OK));
                };
                using (var client = Helper.CreateClient())
                {
                    client.Conferences.SetAudio("1", new Audio{Sentence = "Test"}).Wait();
                }
            }
        }

        
        [TestMethod]
        public void GetAllMembersTest()
        {
            using (ShimsContext.Create())
            {
                var members = new[]
                {
                    new ConferenceMember
                    {
                        Id = "1",
                        Call = new Uri("http://localhost/member1"),
                        State = MemberState.Active
                    },
                    new ConferenceMember
                    {
                        Id = "2",
                        Call = new Uri("http://localhost/member2"),
                        State = MemberState.Completed
                    }
                };
                ShimHttpClient.AllInstances.GetAsyncString = (c, url) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences/1/members", Helper.UserId), url);
                    return Task.Run(() => new HttpResponseMessage(HttpStatusCode.Created) { Content = Helper.CreateJsonContent(members) });
                };
                using (var client = Helper.CreateClient())
                {
                    var result = client.Conferences.GetAllMembers("1").Result;
                    Assert.AreEqual(2, result.Length);
                    Helper.AssertObjects(members[0], result[0]);
                    Helper.AssertObjects(members[1], result[1]);
                }
            }
        }

        [TestMethod]
        public void CreateMemberTest()
        {
            using (ShimsContext.Create())
            {
                ShimHttpClient.AllInstances.PostAsyncStringHttpContent = (c, url, content) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences/1/members", Helper.UserId), url);
                    var conference = Helper.ParseJsonContent<ConferenceMember>(content).Result;
                    Assert.AreEqual("http://localhost/member2", conference.Call.ToString());
                    Assert.AreEqual(MemberState.Completed, conference.State);
                    var response = new HttpResponseMessage(HttpStatusCode.Created);
                    response.Headers.Add("Location", string.Format("/v1/users/{0}/conferences/1/members/11", Helper.UserId));
                    return Task.Run(() => response);
                };
                using (var client = Helper.CreateClient())
                {
                    var id = client.Conferences.CreateMember("1", new ConferenceMember
                    {
                        Call = new Uri("http://localhost/member2"),
                        State = MemberState.Completed
                    }).Result;
                    Assert.AreEqual("11", id);
                }
            }
        }

        [TestMethod]
        public void UpdateMemberTest()
        {
            using (ShimsContext.Create())
            {
                ShimHttpClient.AllInstances.PostAsyncStringHttpContent = (c, url, content) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences/1/members/11", Helper.UserId), url);
                    var conference = Helper.ParseJsonContent<ConferenceMember>(content).Result;
                    Assert.AreEqual("http://localhost/member2", conference.Call.ToString());
                    Assert.AreEqual(MemberState.Completed, conference.State);
                    var response = new HttpResponseMessage(HttpStatusCode.Created);
                    response.Headers.Add("Location", string.Format("/v1/users/{0}/conferences/1/members/11", Helper.UserId));
                    return Task.Run(() => response);
                };
                using (var client = Helper.CreateClient())
                {
                    client.Conferences.UpdateMember("1", "11", new ConferenceMember
                    {
                        Call = new Uri("http://localhost/member2"),
                        State = MemberState.Completed
                    }).Wait();
                }
            }
        }

        [TestMethod]
        public void GetMemberTest()
        {
            using (ShimsContext.Create())
            {
                var member = new ConferenceMember
                {
                    Id = "1",
                    Call = new Uri("http://localhost/member2"),
                    State = MemberState.Completed
                };
                ShimHttpClient.AllInstances.GetAsyncString = (c, url) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences/1/members/11", Helper.UserId), url);
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = Helper.CreateJsonContent(member)
                    };
                    return Task.Run(() => response);
                };
                using (var client = Helper.CreateClient())
                {
                    var result = client.Conferences.GetMember("1", "11").Result;
                    Helper.AssertObjects(member, result);
                }
            }
        }


        [TestMethod]
        public void SetMemberAudioTest()
        {
            using (ShimsContext.Create())
            {
                ShimHttpClient.AllInstances.PostAsyncStringHttpContent = (c, url, content) =>
                {
                    Assert.AreEqual(string.Format("users/{0}/conferences/1/members/11/audio", Helper.UserId), url);
                    var audio = Helper.ParseJsonContent<Audio>(content).Result;
                    Assert.AreEqual("Test", audio.Sentence);
                    return Task.Run(() => new HttpResponseMessage(HttpStatusCode.OK));
                };
                using (var client = Helper.CreateClient())
                {
                    client.Conferences.SetMemberAudio("1", "11", new Audio { Sentence = "Test" }).Wait();
                }
            }
        }
    }
}