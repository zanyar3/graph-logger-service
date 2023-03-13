# Graph Logger Service

[![Hack Together: Microsoft Graph and .NET](https://img.shields.io/badge/Microsoft%20-Hack--Together-orange?style=for-the-badge&logo=microsoft)](https://github.com/microsoft/hack-together)

# Graph Logger Service system using Microsoft Graph, Azure AD, and Microsoft Teams

### Description and Purpose:
This library provides organizations with a simple and easy way to monitor sites or applications through a Microsoft Teams channel, 
ensuring that their sites are always up and running/ UP OR DONW. 
With this library, you can set up custom alerts for important metrics and receive notifications in real-time, directly in your Microsoft Teams channel.

By integrating with Microsoft Teams, this library allows you to easily share important information with your team, collaborate on troubleshooting, and quickly take action to resolve any issues that arise. Whether you're a small startup or a large enterprise, this library can help you streamline your monitoring process and keep your sites running smoothly.


### Features:

- Easy setup and configuration
- Real-time alerts and notifications in Microsoft Teams
- Check site is UP Or Downing
- Logs handling by custom log provider.

### app-settings.josn
```
"GraphLogger": {
    "AzureSetting": {
      "ClientId": "XXXXXXXXXXXXXXXXXXXXXXXX",
      "TenantId": "XXXXXXXXXXXXXXXXXXXXXXXX",
      "ClientSecret": "XXXXXXXXXXXXXXXXXXXXXXXX",
      "AccessToken": "XXXXXXXXXXXXXXXXXXXXXXXX"
    },
    "GraphSettings": {
      "TeamId": "XXXXXXXXXXXXXXXXXXXXXXXX",
      "TeamTenantId": "XXXXXXXXXXXXXXXXXXXXXXXX",
      "ChannelId": "XXXXXXXXXXXXXXXXXXXXXXXX",
      "ChannelTenantId": "XXXXXXXXXXXXXXXXXXXXXXXX"
    }
  }
```

### Demo

**API for test logs**  
![apis](https://user-images.githubusercontent.com/45498591/224829945-e2fd0942-17f1-4cd7-908c-be40c06dc509.PNG) 

**Alert Uping site** 
![image](https://user-images.githubusercontent.com/45498591/224830386-b28b91c6-ae45-47c3-8c76-93d3b0fed4bf.png) 


**ALert Doning site** <br />
![image](https://user-images.githubusercontent.com/45498591/224830632-7eba3585-b6f9-4630-9915-9ba12d54d9c5.png)


**Error Logs**  <br />
![image](https://user-images.githubusercontent.com/45498591/224831535-e7d6cf0a-1f8b-40f8-88b5-940e2d4201f7.png)


