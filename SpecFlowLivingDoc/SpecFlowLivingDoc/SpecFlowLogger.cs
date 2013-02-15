using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HAClient.HapiNet.Services;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace SpecFlowLivingDoc
{
     [Binding]
    public class SpecFlowLogger :ITraceListener
    {
        public const string Name = "SpecFlow";

        private static StringBuilder testOutput = new StringBuilder();
        private static StringBuilder toolOutput = new StringBuilder();
        private static StringBuilder featureText = new StringBuilder();
        private static string given;
        private static string when;
        private static string then;

        private static string scenarioStepsText;
        private static string scenarioSuccessText;
        private static StringBuilder scenarioText = new StringBuilder();

        private static List<StringBuilder> outputs = new List<StringBuilder>();
        private static int scenarioCount;
        private static int scenarioFailedCount;

        [BeforeFeature()]
        public static void BeforeFeature()
        {
            outputs = new List<StringBuilder>();
            scenarioCount = 0;
            scenarioFailedCount = 0;
            var fi = FeatureContext.Current.FeatureInfo;
            testOutput = new StringBuilder("Feature: ").AppendLine(fi.Title).AppendLine().AppendLine("User story: ").AppendLine(fi.Description).AppendLine();
            toolOutput = new StringBuilder("Feature: ").AppendLine(fi.Title).AppendLine();

            featureText.Append(testOutput);

            outputs.Add(testOutput);
            outputs.Add(toolOutput);

        }

        [BeforeScenario()]
        public static void BeforeScenario()
        {
            scenarioText = new StringBuilder();
            scenarioCount++;
            var sc = ScenarioContext.Current;
            outputs.ForEach(o => o.AppendLine().AppendLine().Append("Scenario ").Append(scenarioCount).Append(": ").AppendLine(sc.ScenarioInfo.Title));
            given = string.Empty;
            when = string.Empty;
            then = "not reached";

        }

        [BeforeScenarioBlock()]
        public static void BeforeScenarioBlock()
        {
            scenarioText = new StringBuilder();
            switch (ScenarioContext.Current.CurrentScenarioBlock)
            {
                case ScenarioBlock.Given:

                    break;
                case ScenarioBlock.When:

                    break;
                case ScenarioBlock.Then:

                    break;

            }
        }

        [AfterScenarioBlock()]
        public static void AfterScenarioBlock()
        {

            switch (ScenarioContext.Current.CurrentScenarioBlock)
            {
                case ScenarioBlock.Given:
                    given = scenarioText.ToString();
                    break;
                case ScenarioBlock.When:
                    when = scenarioText.ToString();
                    break;
                case ScenarioBlock.Then:
                    then = scenarioText.ToString();
                    break;

            }


        }

        [AfterScenario()]
        public static void AfterScenario()
        {
            outputs.ForEach(o => o.AppendLine().Append("Scenario executed: ").Append(DateTime.Now.ToString()));

            var sc = ScenarioContext.Current;
            if (sc.TestError != null)
            {
                scenarioFailedCount++;
                outputs.ForEach(o => o.AppendLine(" - failed:").AppendLine(sc.TestError.Message));
            }

        }


        [AfterScenario("TestCase")]
        public static void AfterTestCaseScenario()
        {

            var sc = ScenarioContext.Current;

            UpdateTestCase(new UpdateTestCaseParams
            {
                GivenText = given,
                WhenText = when,
                ThenText = then,
                LastStatus = sc.TestError == null,
                ScenarioInfo = sc.ScenarioInfo
            });


        }

        [AfterFeature("UserStory")]
        public static void AfterUserStoryFeature()
        {
            WriteSummary();
            UpdateUserStoryFromFeature(FeatureContext.Current.FeatureInfo, testOutput.ToString());
        }

        [AfterFeature("Bug")]
        public static void AfterBugFeature()
        {
            WriteSummary();
            UpdateBugFromFeature(FeatureContext.Current.FeatureInfo, testOutput.ToString());
        }


        private static void WriteSummary()
        {
            outputs.ForEach(
                o =>
                o.AppendLine().AppendLine().AppendFormat("{0} scenarios, {1} failures", scenarioCount, scenarioFailedCount).
                    AppendLine().AppendLine());
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {

            var fi = FeatureContext.Current.FeatureInfo;
            Console.WriteLine("AfterTestRun");
        }

        public void WriteTestOutput(string message)
        {
            scenarioText.AppendLine(message);
            testOutput.AppendLine(message);
        }

        public void WriteToolOutput(string message)
        {
            toolOutput.AppendLine(message);
        }

        public static void UpdateUserStoryFromFeature(FeatureInfo featureInfo, string description)
        {
            if (GetIdFromTags(featureInfo.Tags) != null)
            {
                var livingDocumentation = GetLivingDocumentation();
                var userStory = BuildUserStory(featureInfo, description);
                livingDocumentation.UpdateUserStory(userStory);
            }
        }


        internal static UserStory BuildUserStory(FeatureInfo featureInfo, string description)
        {
            return new UserStory
            {
                Id = GetIdFromTags(featureInfo.Tags),
                Name = featureInfo.Title,
                Description = description.Replace("\n", "<br/>")
            };
        }

        public static string GetIdFromTags(string[] tags)
        {
            return (from tag in tags
                    where tag.StartsWith("#")
                    select tag.Remove(0, 1)).FirstOrDefault();


        }

        internal static Bug BuildBug(FeatureInfo featureInfo, string description)
        {
            return new Bug
            {
                Id = GetIdFromTags(featureInfo.Tags),
                Name = featureInfo.Title,
                Description = description.Replace("\n", "<br/>")
            };
        }

        internal static void UpdateBugFromFeature(FeatureInfo featureInfo, string description)
        {
            if (GetIdFromTags(featureInfo.Tags) != null)
            {
                var livingDocumentation = GetLivingDocumentation();
                var bug = BuildBug(featureInfo, description);
                livingDocumentation.UpdateBug(bug);
            }
        }

        private static ILivingDocumentationRepository GetLivingDocumentation()
        {
            var httpRestRequestService = new HttpRestRequestService();
            httpRestRequestService.BaseUrl = ConfigurationManager.AppSettings["TargetProcessAPI"];
            return new TargetProcessLivingDocumentationRepository(httpRestRequestService);
        }

         public class UpdateTestCaseParams
        {
            public ScenarioInfo ScenarioInfo { get; set; }
            public string GivenText { get; set; }
            public string WhenText { get; set; }
            public string ThenText { get; set; }
            public bool LastStatus { get; set; }
        }

        internal static void UpdateTestCase(UpdateTestCaseParams updateTestCaseParams)
        {
            if (GetIdFromTags(updateTestCaseParams.ScenarioInfo.Tags) != null)
            {
                var livingDocumentation = GetLivingDocumentation();
                var testCase = BuildTestCase(updateTestCaseParams);
                livingDocumentation.UpdateTestCase(testCase);
            }
        }

        internal static TestCase BuildTestCase(UpdateTestCaseParams updateTestCaseParams)
        {
            return new TestCase
            {
                Id = GetIdFromTags(updateTestCaseParams.ScenarioInfo.Tags),
                Name = updateTestCaseParams.ScenarioInfo.Title,
                LastRunDate = DateTime.Now,
                LastStatus = updateTestCaseParams.LastStatus,
                Steps = (updateTestCaseParams.GivenText + updateTestCaseParams.WhenText).Replace("\n", "<br/>"),
                Success = updateTestCaseParams.ThenText.Replace("\n", "<br/>")
            };
        }
    }

}
