using Sandbox;
using Sandbox.UI.Tests;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public sealed class BoardManager : Component
{
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

	protected override void OnAwake()
	{
		AddRandom();
		AddRandom();
		UpdateBoard();
	}
	protected override void OnFixedUpdate()
	{
		if ( Input.Pressed( "Forward" ) ) Up();
		else if ( Input.Pressed( "Backward" ) ) Down();
		else if ( Input.Pressed( "Left" ) ) Left();
		else if ( Input.Pressed( "Right" ) ) Right();
	}

	void UpdateBoard()
	{
		Board = BoardWorking;
		Log.Info( Board );
	}

	void Up() 
	{ 
		//Search every grid space
		for ( int x = 0; x <= 3; x++ )
		{
			for ( int y = 0; y <= 3; y++ )
			{
				// if the space is empty continue
				if ( BoardWorking[x, y] == 0 ) continue;

				else
				{
					//if the space is not empty check ahead of it and see if they are available
					if ( BoardWorking[x, y + 1] == 0 && y + 1 <= 3 )
						{
						//if space ahead is free, copy 
						BoardWorking[x, y + 1] = BoardWorking[x, y];
						BoardWorking[x, y] = 0;
						y--;
						continue;
					}
					if ( BoardWorking[x, y] == BoardWorking[x, y + 1] && BoardMerged[x, y + 1] == false )
					{
						BoardWorking[x, y + 1] = BoardWorking[x, y] * 2;
						BoardMerged[x, y + 1] = true;
						BoardWorking[x, y] = 0;
						y--;
						continue;
					}

				}
			}
		}

		AddRandom();
		UpdateBoard();
	}
	void Down() 
	{
		//Search every grid space
		for ( int x = 3; x >= 0; x-- )
		{
			for ( int y = 3; y >= 0; y-- )
			{
				// if the space is empty continue
				if ( BoardWorking[x, y] == 0 ) continue;

				else
				{
					//if the space is not empty check ahead of it and see if they are available
					if ( BoardWorking[x, y - 1] == 0 && y - 1 <= 3 )
					{
						//if space ahead is free, copy 
						BoardWorking[x, y - 1] = BoardWorking[x, y];
						BoardWorking[x, y] = 0;
						y++;
						continue;
					}
					if ( BoardWorking[x, y] == BoardWorking[x, y - 1] && BoardMerged[x, y - 1] == false )
					{
						BoardWorking[x, y - 1] = BoardWorking[x, y] * 2;
						BoardMerged[x, y - 1] = true;
						BoardWorking[x, y] = 0;
						y++;
						continue;
					}

				}
			}
		}

		AddRandom();
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
				if ( BoardWorking[x, y] == 0 ) continue;

				else
				{
					//if the space is not empty check ahead of it and see if they are available
					if ( BoardWorking[x + 1, y] == 0 && x + 1 <= 3 )
					{
						//if space ahead is free, copy 
						BoardWorking[x + 1, y] = BoardWorking[x, y];
						BoardWorking[x + 1, y] = 0;
						x--;
						continue;
					}
					if ( BoardWorking[x, y] == BoardWorking[x + 1, y] && BoardMerged[x + 1, y] == false )
					{
						BoardWorking[x + 1, y] = BoardWorking[x, y] * 2;
						BoardMerged[x + 1, y] = true;
						BoardWorking[x, y] = 0;
						x--;
						continue;
					}

				}
			}
		}

		AddRandom();
		UpdateBoard();
	}
	void Right() 
	{
		//Search every grid space
		for ( int y = 3; y >= 0; y-- )
		{
			for ( int x = 0; x <= 3; x++ )
			{
				// if the space is empty continue
				if ( BoardWorking[x, y] == 0 ) continue;

				else
				{
					//if the space is not empty check ahead of it and see if they are available
					if ( BoardWorking[x + 1, y] == 0 && x + 1 <= 3 )
					{
						//if space ahead is free, copy 
						BoardWorking[x + 1, y] = BoardWorking[x, y];
						BoardWorking[x, y] = 0;
						x--;
						continue;
					}
					if ( BoardWorking[x, y] == BoardWorking[x + 1, y] && BoardMerged[x + 1, y] == false )
					{
						BoardWorking[x + 1, y] = BoardWorking[x, y] * 2;
						BoardMerged[x + 1, y] = true;
						BoardWorking[x, y] = 0;
						x--;
						continue;
					}

				}
			}
		}

		AddRandom();
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

			if ( BoardWorking[x,y] == 0)
			{
				BoardWorking[x,y] = value;
				added = true;
			}
		}
	}
}
