package com.riotgibbon.LivingDocumentation.steps;

import org.jbehave.core.annotations.Given;
import org.jbehave.core.annotations.Named;
import org.jbehave.core.annotations.Then;
import org.jbehave.core.annotations.When;

import com.riotgibbon.LivingDocumentation.pages.Home;
import com.riotgibbon.LivingDocumentation.pages.PageFactory;
import com.riotgibbon.LivingDocumentation.pages.SearchResults;

public class AmazonSteps {

  private Home home;
  private SearchResults searchresults;

  public AmazonSteps(PageFactory pageFactory) {
    home = pageFactory.newHome();
    searchresults = pageFactory.newSearchResults();
  }

  @Given("I am on amazon.co.uk")
  public void homepageOnAmazon() {
    home.go();
  }

  @When("I search for product: <product>")
  public void searchForProduct(@Named("product") String product) {
    home.search(product);
  }

  @Then("I can see a product image and rating")
  public void verifyProductImageAndRating() {
    searchresults.isFirstResultImagePresent();
    searchresults.isFirstResultRatingPresent();
  }

}
