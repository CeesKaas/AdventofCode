param (
    [int] $Day
)
[System.IO.File]::WriteAllText("$($PSScriptRoot)\AoC2022\Days\Day$($Day).cs",[System.IO.File]::ReadAllText("$($PSScriptRoot)\AoC2022\Days\DayTemplate.cs").Replace("Template", $day, [System.StringComparison]::Ordinal))
[System.IO.File]::WriteAllText("$($PSScriptRoot)\AoC2022.UnitTests\DayTests\Day$($Day)Tests.cs",[System.IO.File]::ReadAllText("$($PSScriptRoot)\AoC2022.UnitTests\DayTests\DayTemplateTests.cs").Replace("Template", $day, [System.StringComparison]::Ordinal))
