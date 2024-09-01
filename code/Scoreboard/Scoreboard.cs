using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Sandbox.Services.Leaderboards;

public sealed class Scoreboard : Component
{
	[Property] BoardManager board {  get; set; }
	public string topscore { get; set; } = "0";
	public List<string> leaderboard { get; set; } = new List<string> { };
	public int Me { get; set; }

	public bool updated { get; set; }
	protected override async void OnAwake()
	{
		var scoreboard = Sandbox.Services.Leaderboards.Get( "score" );
		await scoreboard.Refresh();
		UpdatePlayerTopScore();
		UpdateLeaderboard();
	}
	protected override void OnFixedUpdate()
	{
		Update();
	}

	public async void Update()
	{
		updated = false;
		var scoreboard = Sandbox.Services.Leaderboards.Get( "score" );
		scoreboard.MaxEntries = 20;
		await scoreboard.Refresh();
		foreach ( var e in scoreboard.Entries )
		{
			if ( e.Me )
			{
				if ( e.FormattedValue != topscore )
				{
					UpdateLeaderboard();
					UpdatePlayerTopScore();
					updated = true;
				}
			}
		}
	}

	async void UpdatePlayerTopScore()
	{
		var scoreboard = Sandbox.Services.Leaderboards.Get( "score" );
		scoreboard.MaxEntries = 20;
		await scoreboard.Refresh();
		foreach ( var e in scoreboard.Entries )
		{
			if ( e.Me ) { topscore = e.FormattedValue; }
		}
	}

	public async void UpdateLeaderboard()
	{
		var scoreboard = Get( "score" );
		scoreboard.MaxEntries = 20;
		await scoreboard.Refresh();
		leaderboard.RemoveRange(0, leaderboard.Count);

		foreach ( var e in scoreboard.Entries )
		{
			Log.Info( $"[{e.Rank}] {e.DisplayName} - {e.Value}" );
			leaderboard.Add( $"[{e.Rank}] {e.DisplayName} - {e.Value}" );
			if ( e.Me ) { Me = leaderboard.Count; }
		}
	}
}
