using Sandbox;
using System;

public sealed class BoardManager : Component
{
	//Create properties for all board spaces - easier but more code
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

	List<GameObject> OccupiedSpaces = new List<GameObject>();

	protected override void OnAwake()
	{
		//Make a dictionary of every board space - ineffiecent but easier
		var BoardDictionary = new Dictionary<int, GameObject>(){
		{0, Space00},
		{1, Space01},
		{2, Space02},
		{3, Space03},
		{4, Space10},
		{5, Space11},
		{6, Space12},
		{7, Space13},
		{8, Space20},
		{9, Space21},
		{10, Space22},
		{11, Space23},
		{12, Space30},
		{13, Space31},
		{14, Space32},
		{15, Space33},
		};

		//Set spawn postion of 2 starting tiles
		int SpawnBlock1 = new Random().Next( 0, 16 );
		int SpawnBlock2 = new Random().Next( 0, 16 );

		//Check tiles are not on the same space - else change location and try again
		while ( true )
		{
			if ( SpawnBlock2 != SpawnBlock1 ) break;
			else SpawnBlock2 = new Random().Next( 0, 16 );
		}

		BoardDictionary[SpawnBlock1].Tags.Add( "2" );
		BoardDictionary[SpawnBlock2].Tags.Add( "2" );

		OccupiedSpaces.Add( BoardDictionary[SpawnBlock1] );
		OccupiedSpaces.Add( BoardDictionary[SpawnBlock2] );

		//Disable model renderer on all but chosen starting tiles - makes tiles "empty"
		for ( int i = 0; i <= 15; i++ )
		{
			if ( i == SpawnBlock1 || i == SpawnBlock2 ) continue;
			else BoardDictionary[i].Tags.Add( "0" );
		}
	}
	protected override void OnFixedUpdate()
	{
		Move();
	}

	void Move()
	{
		var VectorDictionary = new Dictionary<GameObject, Vector2>(){
		{Space00, new Vector2(0,0)},
		{Space01, new Vector2(0,1)},
		{Space02, new Vector2(0,2)},
		{Space03, new Vector2(0,3)},
		{Space10, new Vector2(1,0)},
		{Space11, new Vector2(1,1)},
		{Space12, new Vector2(1,2)},
		{Space13, new Vector2(1,3)},
		{Space20, new Vector2(2,0)},
		{Space21, new Vector2(2,1)},
		{Space22, new Vector2(2,2)},
		{Space23, new Vector2(2,3)},
		{Space30, new Vector2(3,0)},
		{Space31, new Vector2(3,1)},
		{Space32, new Vector2(3,2)},
		{Space33, new Vector2(3,3)},
		};

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

		if ( Input.Pressed( "Forward" )  || Input.Pressed( "Up" ) )
		{
			Log.Info( OccupiedSpaces.Count );
			Log.Info( OccupiedSpaces );
			for ( int i = 0; i <= OccupiedSpaces.Count; i++ )
			{
				if ( VectorDictionary[OccupiedSpaces[i]].y == 0 ) return;
				for ( int j = 0; j <= OccupiedSpaces.Count; j++ )
				{
					if ( VectorDictionary[OccupiedSpaces[i]] - new Vector2( 0, 1 ) == VectorDictionary[OccupiedSpaces[j]] ) return;
				}
				SpaceDictionary[VectorDictionary[OccupiedSpaces[i]] - new Vector2( 0, 1 )].Tags.RemoveAll();
				SpaceDictionary[VectorDictionary[OccupiedSpaces[i]] - new Vector2( 0, 1 )].Tags.Add( OccupiedSpaces[i].Tags.ToString() );
				OccupiedSpaces[i].Tags.RemoveAll();
				OccupiedSpaces[i].Tags.Add("0");
			}
		
		}
		else if ( Input.Pressed( "Forward" ) || Input.Pressed( "Down" ) )
		{

		}
		else if ( Input.Pressed( "Leftward" ) || Input.Pressed( "Left" ) )
		{

		}
		else if ( Input.Pressed( "Rightward" ) || Input.Pressed( "Right" ) )
		{

		}
	}
}
