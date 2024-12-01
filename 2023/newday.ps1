param (
    [int] $Day
)
if (-not (Test-Path "$($PSScriptRoot)\AoC2023\Days\Day$($Day).cs"))
{
	[System.IO.File]::WriteAllText("$($PSScriptRoot)\AoC2023\Days\Day$($Day).cs",[System.IO.File]::ReadAllText("$($PSScriptRoot)\AoC2023\Days\DayTemplate.cs").Replace("Template", $day, [System.StringComparison]::Ordinal))
	[System.IO.File]::WriteAllText("$($PSScriptRoot)\AoC2023.UnitTests\DayTests\Day$($Day)Tests.cs",[System.IO.File]::ReadAllText("$($PSScriptRoot)\AoC2023.UnitTests\DayTests\DayTemplateTests.cs").Replace("Template", $day, [System.StringComparison]::Ordinal))
}
