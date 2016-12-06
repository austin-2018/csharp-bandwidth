﻿# IConference.CreateMemberAsync Method 
 

Add a member to a conference.

**Namespace:**&nbsp;<a href ="N_Bandwidth_Net_Catapult.md">Bandwidth.Net.Catapult</a><br />**Assembly:**&nbsp;Bandwidth.Net (in Bandwidth.Net.dll) Version: 4.0.0

## Syntax

**C#**<br />
``` C#
Task<string> CreateMemberAsync(
	string conferenceId,
	CreateConferenceMemberData data,
	Nullable<CancellationToken> cancellationToken = null
)
```


#### Parameters
&nbsp;<dl><dt>conferenceId</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Id of conference to add member</dd><dt>data</dt><dd>Type: <a href ="T_Bandwidth_Net_Catapult_CreateConferenceMemberData.md">Bandwidth.Net.Catapult.CreateConferenceMemberData</a><br />Data for creation of new member</dd><dt>cancellationToken (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/b3h38hb0" target="_blank">System.Nullable</a>(<a href="http://msdn2.microsoft.com/en-us/library/dd384802" target="_blank">CancellationToken</a>)<br />Optional token to cancel async operation</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/dd321424" target="_blank">Task</a>(<a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">String</a>)<br />Created member Id

## Examples

```
var memberId = await client.Conference.CreateMemberAsync("conferenceId", new CreateConferenceMemberData{From = "+1234567980"});
```


## See Also


#### Reference
<a href ="T_Bandwidth_Net_Catapult_IConference.md">IConference Interface</a><br /><a href ="N_Bandwidth_Net_Catapult.md">Bandwidth.Net.Catapult Namespace</a><br />