package com.riotgibbon.LivingDocumentation.pages;

import org.jbehave.web.selenium.WebDriverProvider;
import org.openqa.selenium.By;

import com.riotgibbon.LivingDocumentation.common.CommonPageMethods;

/**
 * The Amazon search results page
 * 
 * @author Ben Snape
 * 
 */
public class SearchResults extends CommonPageMethods {

  private static final String FIRST_SEARCH_RESULT_IMAGE = "//*[@id='result_0']/div[2]/a/img";

  private static final String FIRST_SEARCH_RESULT_RATING = "//*[@id='result_0']/div[3]/div[4]";

  public SearchResults(WebDriverProvider webDriverProvider) {
    super(webDriverProvider);
  }

  /**
   * Checks if the product image is present for the first search result
   */
  public void isFirstResultImagePresent() {
    waitForElement(By.xpath(FIRST_SEARCH_RESULT_IMAGE));
  }

  /**
   * Checks if the product rating is present for the first search result
   */
  public void isFirstResultRatingPresent() {
    waitForElement(By.xpath(FIRST_SEARCH_RESULT_RATING));
  }

}
