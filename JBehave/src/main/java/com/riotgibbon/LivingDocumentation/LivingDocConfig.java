package com.riotgibbon.LivingDocumentation;


import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class LivingDocConfig {
    Properties configFile;
    public LivingDocConfig()  {
        configFile = new Properties();
        try {
            InputStream inputStream = this.getClass().getResourceAsStream("/livingDoc.properties");
            configFile.load(inputStream);
        } catch (IOException e) {
            e.printStackTrace();  //To change body of catch statement use File | Settings | File Templates.
        }
    }

    public String getPropertyValue(String key){
        return configFile.getProperty(key);
    }

    public String getHost()
    {
        return getPropertyValue("host");
    }

    public String GetPassword() {
        return getPropertyValue("password");
    }

    public String GetUser() {
        return getPropertyValue("user");
    }
}
