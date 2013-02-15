using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace SpecFlowLivingDoc
{
    public interface ILivingDocumentationRepository
    {
        void UpdateUserStory(UserStory userStory);
        void UpdateBug(Bug bug);
        void UpdateTestCase(TestCase testCase);
    }

    public class TargetProcessLivingDocumentationRepository: ILivingDocumentationRepository
    {
        private IHttpRestRequestService restService;
        

        public TargetProcessLivingDocumentationRepository(IHttpRestRequestService restService)
        {
            this.restService = restService;
        }

      
        public class UpdateLDItemParams
        {
            public DocumentationEntity DocumentationEntity { get; set; }
            public string ServiceCall { get; set; }
            public string UrlSegmentName { get; set; }
            public string UrlSegmentValue { get; set; }
        }

        private async void UpdateLDItem(UpdateLDItemParams updateLdItemParams)
        {
            //TODO PostJson subclass
            restService.RequestMethod = "POST";
            restService.AddAcceptHeader("application/json");
            restService.TargetResource = string.Format("/api/v1/{0}/{1}", updateLdItemParams.ServiceCall, updateLdItemParams.UrlSegmentValue);
            restService.SetContent(JsonConvert.SerializeObject(updateLdItemParams.DocumentationEntity), "application/json");
            restService.GetRestResponse();

        }
        public void UpdateUserStory(UserStory userStory)
        {
            UpdateLDItem(new UpdateLDItemParams
            {
                DocumentationEntity = userStory,
                ServiceCall = "Userstories",
                UrlSegmentName = "UserStoryId",
                UrlSegmentValue = userStory.Id
            });
        }


        public void UpdateBug(Bug bug)
        {
            UpdateLDItem(new UpdateLDItemParams
            {
                DocumentationEntity = bug,
                ServiceCall = "Bugs",
                UrlSegmentName = "BugId",
                UrlSegmentValue = bug.Id
            });
        }

        public void UpdateTestCase(TestCase testCase)
        {
            UpdateLDItem(new UpdateLDItemParams
            {
                DocumentationEntity = testCase,
                ServiceCall = "TestCases",
                UrlSegmentName = "TestCaseId",
                UrlSegmentValue = testCase.Id
            });
        }
    }




    public interface DocumentationEntity
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }


   [Serializable]
    public class UserStory : DocumentationEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

   [Serializable]
   public class Bug : DocumentationEntity
   {
       public string Id { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
   }

   [Serializable]
   public class TestCase : DocumentationEntity
   {
       public string Id { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public string Steps { get; set; }
       public string Success { get; set; }
       public bool LastStatus { get; set; }
       public DateTime LastRunDate { get; set; }
   }
}
