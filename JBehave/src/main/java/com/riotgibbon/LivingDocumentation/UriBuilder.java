package com.riotgibbon.LivingDocumentation;

import java.io.IOException;
import java.util.Properties;

public class UriBuilder {


    private static String getHost() {
        LivingDocConfig livingDocConfig = new LivingDocConfig();
        return livingDocConfig.getHost();
    }


    public static String getUserStoryUri(UserStory userStory)  {
        return String.format("%sUserstories/%s", getHost(), userStory.Id);
    }

	public static String getTestCaseUri(TestCase testCase) {
        return String.format("%sTestCases/%s", getHost(), testCase.Id);
    }
}
