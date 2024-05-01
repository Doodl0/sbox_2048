using Sandbox;
using Sandbox.UI.Tests;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public sealed class BoardManager : Component
{
	[Property][Category( "Board Spaces" )] GameObject Space00 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space01 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space02 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space03 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space10 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space11 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space12 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space13 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space20 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space21 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space22 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space23 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space30 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space31 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space32 { get; set; }
	[Property][Category( "Board Spaces" )] GameObject Space33 { get; set; }
	[Property][Category( "End Screens" )] Win WinScreen { get; set; }
	[Property][Category( "End Screens" )] Lose LoseScreen { get; set; }

	//Gets copied to ingame board
	public int[,] Board =
	{
		{0, 0, 0, 0 },
		{0, 0, 0, 0 },
		{0, 0, 0, 0 },
		{0, 0, 0, 0 }
	};

	//Used while working to see if a piece can move
	public int[,] BoardWorking =
	{
		{0, 0, 0, 0 },
		{0, 0, 0, 0 },
		{0, 0, 0, 0 },
		{0, 0, 0, 0 }
	};

	public bool[,] BoardMerged =
	{
		{false, false, false, false },
		{false, false, false, false },
		{false, false, false, false },
		{false, false, false, false },
	};

	public int Score = 0;

	protected override void OnAwake()
	{
		NewGame();
	}
	protected override void OnFixedUpdate()
	{
		if ( Win() == true )
		{
			if ( Input.Pressed( "Restart" ) )
			{
				WinScreen.Enabled = false;
				NewGame();
			}
			else return;
		}
		if ( Lose() == true )
		{
			if ( Input.Pressed( "Restart" ) )
			{
				LoseScreen.Enabled = false;
				NewGame();
			}
			else return;
		}

		if ( Input.Pressed( "Forward" ) || Input.Pressed( "Up" ) ) Up();
		else if ( Input.Pressed( "Backward" ) || Input.Pressed( "Down" ) ) Down();
		else if ( Input.Pressed( "Leftward" ) || Input.Pressed( "West" ) ) Left();
		else if ( Input.Pressed( "Rightward" ) || Input.Pressed( "East" ) ) Right();
	}

	void UpdateBoard()
	{
		var SpaceDictionary = new Dictionary<Vector2, GameObject>(){
		{new Vector2(0,0), Space00},
		{new Vector2(0,1), Space01},
		{new Vector2(0,2), Space02},
		{new Vector2(0,3), Space03},
		{new Vector2(1,0), Space10},
		{new Vector2(1,1), Space11},
		{new Vector2(1,2), Space12},
		{new Vector2(1,3), Space13},
		{new Vector2(2,0), Space20},
		{new Vector2(2,1), Space21},
		{new Vector2(2,2), Space22},
		{new Vector2(2,3), Space23},
		{new Vector2(3,0), Space30},
		{new Vector2(3,1), Space31},
		{new Vector2(3,2), Space32},
		{new Vector2(3,3), Space33},
		};

		Board = BoardWorking;
		Log.Info( "----------------" );
		for ( int i = 0; i <= 3; i++ )
		{
			Log.Info( $"{Board[i,0]}, {Board[i, 1]}, {Board[i, 2]}, {Board[i, 3]}" );
		}

		for ( int x = 0; x <= 3; x++ )
		{
			for ( int y = 0; y <= 3; y++ )
			{
				SpaceDictionary[new Vector2( x, y )].Tags.RemoveAll();
				SpaceDictionary[new Vector2( x, y )].Tags.Add( Board[y,x].ToString() );
			}
		}
	}

	void Right() 
	{
		//Search every grid space
		for ( int y = 3; y >= 0; y-- )
		{
			for ( int x = 0; x <= 3; x++ )
			{
				// if the space is empty continue
				if ( BoardWorking[y, x] == 0 ) continue;

				else
				{
					if ( x + 1 > 3 ) continue;
					//if the space is not empty check ahead of it and see if they are available
					if ( BoardWorking[y, x + 1] == 0 )
					{
						//if space ahead is free, copy 
						BoardWorking[y, x + 1] = BoardWorking[y, x];
						BoardWorking[y, x] = 0;
						x--;
						continue;
					}
					if ( BoardWorking[y, x] == BoardWorking[y, x + 1] && BoardMerged[y, x + 1] == false )
					{
						BoardWorking[y, x + 1] *= 2;
						Score += BoardWorking[y, x] * 2;
						BoardMerged[y, x + 1] = true;
						BoardWorking[y, x] = 0;
						x--;
						continue;
					}

				}
			}
		}

		AddRandom();
		ResetBoardMereged();
		UpdateBoard();
	}
	void Left() 
	{
		//Search every grid space
		for ( int y = 0; y <= 3; y++ )
		{
			for ( int x = 0; x <= 3; x++ )
			{
				// if the space is empty continue
				if ( BoardWorking[y, x] == 0 ) continue;

				else
				{
					if ( x - 1 < 0 ) continue;
					//if the space is not empty check ahead of it and see if they are available
					if ( BoardWorking[y, x - 1] == 0 )
					{
						//if space ahead is free, copy 
						BoardWorking[y, x - 1] = BoardWorking[y, x];
						BoardWorking[y, x] = 0;
						x -= 2;
						continue;
					}
					if ( BoardWorking[y, x] == BoardWorking[y, x - 1] && BoardMerged[y, x - 1] == false )
					{
						BoardWorking[y, x - 1] *= 2;
						Score += BoardWorking[y, x] * 2;
						BoardMerged[y, x - 1] = true;
						BoardWorking[y, x] = 0;
						x -= 2;
						continue;
					}

				}
			}
		}

		AddRandom();
		ResetBoardMereged();
		UpdateBoard();
	}
	void Down()
	{
		//Search every grid space
		for ( int x = 0; x <= 3; x++ )
		{
			for ( int y = 3; y >= 0; y-- )
			{
				// if the space is empty continue
				if ( BoardWorking[y, x] == 0 ) continue;

				else
				{
					if ( y + 1 > 3 ) continue;
					//if the space is not empty check ahead of it and see if they are available
					if ( BoardWorking[y + 1, x] == 0 )
					{
						//if space ahead is free, copy 
						BoardWorking[y + 1, x] += BoardWorking[y, x];
						BoardWorking[y, x] = 0;
						y += 2;
						continue;
					}
					if ( BoardWorking[y, x] == BoardWorking[y + 1, x] && BoardMerged[y + 1, x] == false )
					{
						BoardWorking[y + 1, x] *= 2;
						Score += BoardWorking[y, x] * 2;
						BoardMerged[y + 1, x] = true;
						BoardWorking[y, x] = 0;
						y += 1;
						continue;
					}

				}
			}
		}

		AddRandom();
		ResetBoardMereged();
		UpdateBoard();
	}
	void Up() 
	{
		//Search every grid space
		for ( int x = 0; x <= 3; x++ )
		{
			for ( int y = 0; y <= 3; y++ )
			{
				// if the space is empty continue
				if ( BoardWorking[y, x] == 0 ) continue;

				else
				{
					if ( y - 1 < 0 ) continue;
					//if the space is not empty check ahead of it and see if they are available
					if ( BoardWorking[y - 1, x] == 0 )
					{
						//if space ahead is free, copy 
						BoardWorking[y - 1, x] = BoardWorking[y, x];
						BoardWorking[y, x] = 0;
						y -= 2;
						continue;
					}
					if ( BoardWorking[y, x] == BoardWorking[y - 1, x] && BoardMerged[y - 1, x] == false )
					{
						BoardWorking[y - 1, x] *= 2;
						Score += BoardWorking[y, x] * 2;
						BoardMerged[y - 1, x] = true;
						BoardWorking[y, x] = 0;
						y -= 2;
						continue;
					}

				}
			}
		}

		AddRandom();
		ResetBoardMereged();
		UpdateBoard();
	}
	void AddRandom()
	{
		var added = false;

		while ( !added )
		{
			int x = new Random().Next( 0, 4 );
			int y = new Random().Next( 0, 4 );

			int value = new Random().Next( 1, 3 );
			value *= 2;

			if ( BoardWorking[x, y] == 0 )
			{
				BoardWorking[x, y] = value;
				added = true;
			}
			int SpareSpaces = 0;
			for ( int i = 0; i <= 3; i++ )
			{
				for ( int j = 0; j <= 3; j++ )
				{
					if ( BoardWorking[i, j] == 0 ) SpareSpaces++;
				}
			}

			if ( SpareSpaces == 0 ) { added = true; }
			else { continue; }

		}
	}

	void ResetBoardMereged()
	{
		for ( int x = 0; x <= 3; x++)
		{
			for ( int y = 0; y <= 3; y++ )
			{
				BoardMerged[x, y] = false;
			}
		}
	}

	bool Win()
	{
		for ( int x = 0; x <= 3; x++ )
		{
			for ( int y = 0; y <= 3; y++ )
			{
				if ( BoardWorking[y, x] >= 2048 ) 
				{
					WinScreen.Enabled = true;
					Sandbox.Services.Stats.SetValue( "topscore", Score );
					return true;
				}
			}
		}
		return false;
	}

	bool Lose()
	{
		for ( int x = 0; x <= 3; x++ )
		{
			for (int y = 0; y <= 3; y++ )
			{
				if ( BoardWorking[y, x] == 0 ) return false;
				if ( y + 1 < 3 ) { if ( BoardWorking[y + 1, x] == BoardWorking[y, x] || BoardWorking[y + 1, x] == 0 ) return false; }
				if ( y - 1 > 0 ) { if ( BoardWorking[y - 1, x] == BoardWorking[y, x] || BoardWorking[y - 1, x] == 0 ) return false; }
				if ( x + 1 < 3 ) { if ( BoardWorking[y, x + 1] == BoardWorking[y, x] || BoardWorking[y, x + 1] == 0 ) return false; }
				if ( x - 1 > 0 ) { if ( BoardWorking[y, x - 1] == BoardWorking[y, x] || BoardWorking[y, x - 1] == 0 ) return false; }
			}
		}

		LoseScreen.Enabled = true;
		Sandbox.Services.Stats.SetValue( "topscore", Score );
		return true;
	}

	void NewGame()
	{
		Score = 0;
		ResetBoardMereged();
		for ( int x = 0; x <= 3; x++ )
		{
			for ( int y = 0; y <= 3; y++ )
			{
				Board[x, y] = 0;
				BoardWorking[x, y] = 0;
			}
		}
		AddRandom();
		UpdateBoard();
	}
}
