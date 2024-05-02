using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Sandbox.Services.Leaderboards;

public sealed class Scoreboard : Component
{
	public string playerscore { get; set; } = "0";
	public List<string> leaderboard { get; set; } = new List<string> { };
	public int Me {  get; set; }

	public bool updated { get; set; }
	protected override async void OnAwake()
	{
		var scoreboard = Sandbox.Services.Leaderboards.Get( "topscore" );
		await scoreboard.Refresh();
		UpdatePlayerTopScore();
		UpdateLeaderboard();
	}
	protected override async void OnFixedUpdate()
	{
		updated = false;
		var scoreboard = Sandbox.Services.Leaderboards.Get( "topscore" );
		scoreboard.MaxEntries = 1;
		await scoreboard.Refresh();
		if ( scoreboard.Entries[0].FormattedValue != playerscore )
		{
			UpdatePlayerTopScore();
			UpdateLeaderboard();
		}
		updated = true;
	}

	async void UpdatePlayerTopScore()
	{
		var scoreboard = Sandbox.Services.Leaderboards.Get( "topscore" );
		scoreboard.MaxEntries = 1;
		await scoreboard.Refresh();
		playerscore = scoreboard.Entries[0].FormattedValue;
	}

	public async void UpdateLeaderboard()
	{
		var scoreboard = Get( "topscore" );
		scoreboard.MaxEntries = 20;
		await scoreboard.Refresh();
		leaderboard.RemoveRange(0, leaderboard.Count);

		foreach ( var e in scoreboard.Entries )
		{
			Log.Info( $"[{e.Rank}] {e.DisplayName} - {e.Value}" );
			leaderboard.Add( $"[{e.Rank}] {e.DisplayName} - {e.Value}" );
			if ( e.Me ) { Me = leaderboard.Count; }
			Log.Info( leaderboard );
		}
		Log.Info(Me);
	}
}
