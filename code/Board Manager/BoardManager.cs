using Sandbox;
using Sandbox.UI.Tests;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public sealed class BoardManager : Component
{
	[Property] BoardUI boardUI { get; set; }
	[Property][Category( "End Screens" )] Win WinScreen { get; set; }
	[Property][Category( "End Screens" )] Lose LoseScreen { get; set; }
	[Property][Category( "Scoreboard" )] Scoreboard scoreboard { get; set; } 

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
	protected override void OnUpdate()
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

		if ( Input.Pressed( "Up" ) ) Up();
		else if ( Input.Pressed( "Down" ) ) Down();
		else if ( Input.Pressed( "Left" ) ) Left();
		else if ( Input.Pressed( "Right" ) ) Right();
	}

	void UpdateBoard()
	{
		Board = BoardWorking;
		boardUI.Update = !boardUI.Update;
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
					Sandbox.Services.Stats.SetValue( "score", Score );
					WinScreen.Enabled = true;
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

		Sandbox.Services.Stats.SetValue( "score", Score );
		LoseScreen.Enabled = true;
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
