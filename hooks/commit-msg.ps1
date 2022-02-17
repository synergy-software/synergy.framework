Param(
  [string]$commitMsgFile
)

$branch = git rev-parse --abbrev-ref HEAD
 
Write-Host "Current branch: "$branch 
$commitMsg = (Get-Content $commitMsgFile -Raw)

if(!($branch -match "[0-9]+"))
{
	if($commitMsg -match '^\#[0-9]+:|^Merge branch')
	{
		exit 0
	}
	else
	{
		Write-Host "!!!!!!!!!!!!! Commit MUST conatin task number !!!!!!!!!!!!!" 
		exit 1
	}
}
else{
	$taskNo = ([regex]("([0-9]+)")).matches($branch)[0].groups[0].value

	if($commitMsg -match '^\#[0-9]+:')
	{
		$taskNoInCommit = ([regex]("\#([0-9]+)")).matches($commitMsg)[0].groups[1].value
		if(!$taskNoInCommit.equals($taskNo))
		{
			Write-Host "!!!!!!!!!!!!! Branch task number #$taskNo is not the same as commit number #$taskNoInCommit !!!!!!!!!!!!!"
			exit 0;
		}
		exit 0
	}
	else
	{
		if($commitMsg -match '^\#[0-9]+')
		{
			Write-Host "!!!!!!!!!!!!! Missing ':' after task number !!!!!!!!!!!!!"
			exit 1
		}
		"#" + $taskNo + ": " + (Get-Content $commitMsgFile -Raw) | Set-Content $commitMsgFile
	}
}
