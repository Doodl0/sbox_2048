using Sandbox;

public sealed class Scoreboard : Component
{
	public string playerscore { get; private set; } = "cannot find";
	protected override void OnAwake()
	{
		var scoreboard = Sandbox.Services.Leaderboards.Get( "topscore" );
		if ( scoreboard.TotalEntries > 0 ) 
		{
			playerscore = scoreboard.Entries[scoreboard.MaxEntries].FormattedValue;
		}
	}
}
