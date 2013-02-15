using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowLivingDoc.Steps
{
    [Binding]
    public class AddingSteps
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef
        private int sum = 0;
        [Given("I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredSomethingIntoTheCalculator(int number)
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see http://go.specflow.org/doc-sharingdata 
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 

            sum = sum + number;
        }

        [When("I press add")]
        public void WhenIPressAdd()
        {
            
        }

        [Then("the result should be (.*) on the screen")]
        public void ThenTheResultShouldBe(int result)
        {
            Assert.AreEqual(result,sum);
        }
    }
}
