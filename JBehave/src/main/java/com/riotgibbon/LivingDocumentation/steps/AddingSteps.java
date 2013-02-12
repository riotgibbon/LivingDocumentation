package com.riotgibbon.LivingDocumentation.steps;

import junit.framework.Assert;

import org.jbehave.core.annotations.Given;
import org.jbehave.core.annotations.Named;
import org.jbehave.core.annotations.Then;
import org.jbehave.core.annotations.When;

import com.riotgibbon.LivingDocumentation.pages.PageFactory;

public class AddingSteps {

	 public AddingSteps(PageFactory pageFactory) {
		   
		  }
	int num1;
	int num2;
	int sum;
	
	@Given("I enter <n1>")
	public void iHave20(@Named("n1") String n1) {
		num1 = Integer.parseInt(n1);
	}
	
	@Given("I enter <n2>")
	public void iHave30(@Named("n2") String n2) {
		num2 = Integer.parseInt(n2);
	}
	
	@When("I add")
	public void add() {
		sum = num1 + num2;
	}
	
	@Then("I should get <sum>")
	public void shouldGetExpected(@Named("sum") String expectedSum) {
		Assert.assertEquals("Sum should be " + expectedSum, Integer.parseInt(expectedSum), sum);
	}
}
