package com.riotgibbon.LivingDocumentation;

import java.util.Date;

import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
class TestCase
{
	 public String Name;
	 public String Id;
	 public String Description;
	 public Boolean LastStatus;
	public String Success;
	public String Steps;
	public Date LastRunDate;
	public String LastFailureComment;
	 
	 public TestCase(){}
	 public TestCase(String name)
	 {
		 Name = name;
		 LastStatus = true;
		 LastFailureComment="";
	 }
	
	
}