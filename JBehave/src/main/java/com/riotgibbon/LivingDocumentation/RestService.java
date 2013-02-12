package com.riotgibbon.LivingDocumentation;

import javax.ws.rs.core.MediaType;

import org.apache.cxf.jaxrs.client.WebClient;

public class RestService {

	public static void postUserStory(UserStory userStory) {
		String uri = UriBuilder.getUserStoryUri(userStory);
		postData(userStory, uri);		
	}
	
	public static void postTestCase(TestCase testCase) {
		String uri = UriBuilder.getTestCaseUri(testCase);
		postData(testCase, uri);		
	}
	
	static WebClient getWebClient(String uri) {
        LivingDocConfig livingDocConfig = new LivingDocConfig();
		String user = livingDocConfig.GetUser();
		String pass = livingDocConfig.GetPassword();
		return WebClient.create(uri, user, pass, null);
	}
	
	 static void postData(Object data, String uri) {
		WebClient client = RestService.getWebClient(uri);
		String msg = client.type(MediaType.APPLICATION_XML_TYPE).post(data, String.class);
		System.out.println("Response from uri: " + msg + ": status=" + client.getResponse().getStatus());
	}

}
