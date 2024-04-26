using Sandbox;
using System;
using System.Runtime.CompilerServices;

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
		
	}
	protected override void OnFixedUpdate()
	{

	}

	void Move()
	{

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

					}

				}
			}
		}
	}
	void Down() { }
	void Left() { }
	void Right() { }
	void AddRandom() { }
}
