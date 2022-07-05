[![Build status](https://ibizsolutions.visualstudio.com/BizTalkComponents/_apis/build/status/CopyContextProperty?branchName=master)](https://ibizsolutions.visualstudio.com/BizTalkComponents/_build/latest?definitionId=617)


Copies a value from a specified context property to another context property.

This component is useful when you need to promote a property based on another property.

## Properties
|Property|Type|Description|
|--|--|--|
|Source Property |String (Required)| The source context property, must be in format (http://namespace#propertyname)|
|Destination Property |String (Required)| The destination context property, must be in format (http://namespace#propertyname)|
|No Promotion| Boolean | If true, the component will not promote the destination property.|
|Disabled| Boolean| if true, the component will be disabled.|

More information available at:
https://biztalkcomponents.github.io/ref/copy-context-property.html