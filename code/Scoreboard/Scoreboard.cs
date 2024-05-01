using Sandbox;
using static Sandbox.Services.Leaderboards;

public sealed class Scoreboard : Component
{
	[Property] Win WinScreen { get; set; }
	[Property] Lose LoseScreen { get; set; }
	public string playerscore { get; private set; } = "cannot find";
	public string previousscore { get; private set; } = "cannot find";
	protected override async void OnAwake()
	{
		var scoreboard = Sandbox.Services.Leaderboards.Get( "topscore" );
		scoreboard.Group = "global";
		scoreboard.MaxEntries = 1;
		await scoreboard.Refresh();
		playerscore = scoreboard.Entries[0].FormattedValue;
		previousscore = playerscore;
	}
	protected override async void OnUpdate()
	{
		var scoreboard = Sandbox.Services.Leaderboards.Get( "topscore" );
		await scoreboard.Refresh();
		playerscore = scoreboard.Entries[0].FormattedValue;
	}
}
