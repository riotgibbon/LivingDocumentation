package com.riotgibbon.LivingDocumentation;

import java.util.*;

import org.jbehave.core.model.ExamplesTable;
import org.jbehave.core.model.GivenStories;
import org.jbehave.core.model.Meta;
import org.jbehave.core.model.Narrative;
import org.jbehave.core.model.OutcomesTable;
import org.jbehave.core.model.Scenario;
import org.jbehave.core.model.Story;
import org.jbehave.core.model.StoryDuration;
import org.jbehave.core.reporters.StoryReporter;
import org.springframework.web.util.HtmlUtils;


public class TargetProcessStoryReporter implements StoryReporter {

private String userStoryKey = "UserStory";
private String userStoryId;
private Story thisStory;
private Narrative thisNarrative;
private TestCase currentTestCase;
private List<TestCase> storyTestCases;
private Hashtable stories;
private StringBuilder testCaseDescription;
private String newLine = "<br/>";

public TargetProcessStoryReporter() {
	// TODO Auto-generated constructor stub
	writeMessage("creating MyStoryReporter");
    stories= new Hashtable();
	
}
	


	public void beforeStory(Story story, boolean givenStory) {

		writeMessage(story.toString());
		if (hasUserStoryTag(story))
		{
            thisStory = story;
            storyTestCases = new ArrayList<TestCase>();
            thisNarrative = thisStory.getNarrative();
			userStoryId = getUserStoryId(story);
            stories.put(userStoryId,story);
			writeMessage("Running UserStory: " + userStoryId);
		}
	}

	private String getUserStoryId(Story story) {
		String userStoryId = story.getMeta().getProperty(userStoryKey);
		return userStoryId;
	}

	private boolean hasUserStoryTag(Story story) {
		return story.getMeta().hasProperty(userStoryKey);
	}

	public void afterStory(boolean givenStory) {
		// TODO Auto-generated method stub
		if (userStoryId!=null){
			writeMessage("UserStory: " + userStoryId + " completed" );				
			RestService.postUserStory(getUserStory());
		}
	}

	private UserStory getUserStory() {
		UserStory userStory = new UserStory();
		userStory.Id = userStoryId;
		userStory.Description = getStoryDescription();
		userStory.Name = thisStory.getDescription().asString();
		return userStory;
	}

	private String getStoryDescription() {
		StringBuilder storyDescription = new StringBuilder();		
		String narrativeText = getNarrativeText(thisNarrative);
		
		storyDescription.append(narrativeText).append(newLine).append(newLine);
		storyDescription.append("Scenarios:" ).append(storyTestCases.size()).append(newLine);
		int scenarioCount =0;
		for (TestCase testCase: storyTestCases)
		{
			scenarioCount++;
			storyDescription
				.append("(").append(scenarioCount).append(") ")
				.append(testCase.Name)
				.append(": ")
				.append(testCaseStatus(testCase))
				.append(newLine)
				.append(testCase.Steps)
				.append(newLine)
				.append("examples:")				
				.append(testCase.Success)
				.append(testCase.LastRunDate.toString())
				.append(newLine)
				.append(newLine);
		}
		return storyDescription.toString();
	}

	private String getNarrativeText(Narrative narrative) {
		return "As a " + narrative.asA() + ", in order to " + narrative.inOrderTo() + " I wish to " + narrative.iWantTo();
	}



	public void beforeScenario(String scenarioTitle) {
		writeMessage("new scenario:" + scenarioTitle);
		currentTestCase = new TestCase(scenarioTitle);
		testCaseDescription = new StringBuilder();
		storyTestCases.add(currentTestCase);
	}
	
	public void scenarioMeta(Meta meta) {
		writeMessage("Scenario: Meta-data:" + meta.toString());
		String testCaseId = meta.getProperty("TestCase");
		currentTestCase.Id= testCaseId;
	}
	
	public void successful(String step) {
		testCaseDescription.append(step + " succeeded").append(newLine);
	}

	public void failed(String step, Throwable cause) {
		testCaseDescription.append(step + " failed").append(newLine);
		currentTestCase.LastFailureComment += HtmlUtils.htmlEscape(cause.getCause().getMessage());
		currentTestCase.LastStatus=false;
	}

	public void afterScenario() {
		String success = testCaseStatus(currentTestCase);
		writeMessage("Scenario: " + currentTestCase.Name + ": " + success);
		currentTestCase.Description = currentTestCase.Steps + currentTestCase.Success;
		Date now = new Date();
		currentTestCase.LastRunDate = now;
		RestService.postTestCase(currentTestCase);
	}



	private String testCaseStatus(TestCase testCase) {
		return testCase.LastStatus ? "passed" : "failed: " + testCase.LastFailureComment;
	}

	private void writeMessage(String message){
		System.out.println("storyReporter >> " + message);
	}

	
	public void givenStories(GivenStories givenStories) {
		// TODO Auto-generated method stub

	}

	public void givenStories(List<String> storyPaths) {
		// TODO Auto-generated method stub

	}

	public void beforeExamples(List<String> steps, ExamplesTable table) {
		currentTestCase.Steps = getExampleSteps(steps);
		currentTestCase.Success = "<pre>" + table.asString() + "</pre>";
		writeMessage("Steps: " + getExampleSteps(steps) + " ; " + table.toString()); 
	}



	private String getExampleSteps(List<String> steps) {
		StringBuilder stepsText = new StringBuilder();
		for(String step: steps)
		{
			stepsText.append(HtmlUtils.htmlEscape( step)).append(newLine);
		}		
		return stepsText.toString();
	}

	public void example(Map<String, String> tableRow) {
		// TODO Auto-generated method stub

	}

	public void afterExamples() {
		// TODO Auto-generated method stub

	}

	public void beforeStep(String step) {
		// TODO Auto-generated method stub

	}

	
	public void ignorable(String step) {
		// TODO Auto-generated method stub

	}

	public void pending(String step) {
		// TODO Auto-generated method stub

	}

	public void notPerformed(String step) {
		// TODO Auto-generated method stub

	}

	public void failedOutcomes(String step, OutcomesTable table) {
		// TODO Auto-generated method stub

	}

	public void restarted(String step, Throwable cause) {
		// TODO Auto-generated method stub

	}

	public void dryRun() {
		// TODO Auto-generated method stub

	}

	public void pendingMethods(List<String> methods) {
		// TODO Auto-generated method stub

	}
	

	public void narrative(Narrative narrative) {
		// TODO Auto-generated method stub
		
	}

	public void scenarioNotAllowed(Scenario scenario, String filter) {
		// TODO Auto-generated method stub
		
	}
	
	public void storyNotAllowed(Story story, String filter) {
		// TODO Auto-generated method stub

	}

	public void storyCancelled(Story story, StoryDuration storyDuration) {
		// TODO Auto-generated method stub

	}
	
	

}


