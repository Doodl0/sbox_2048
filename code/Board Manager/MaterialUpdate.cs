using Sandbox;

public sealed class MaterialUpdate : Component
{
	[Property][Category( "Components" )] public ModelRenderer ModelRenderer { get; set; }
	[Property][Category( "Materials" )] public Material Material2 { get; set; }
	[Property][Category( "Materials" )] public Material Material4 { get; set; }
	[Property][Category( "Materials" )] public Material Material8 { get; set; }
	[Property][Category( "Materials" )] public Material Material16 { get; set; }
	[Property][Category( "Materials" )] public Material Material32 { get; set; }
	[Property][Category( "Materials" )] public Material Material64 { get; set; }
	[Property][Category( "Materials" )] public Material Material128 { get; set; }
	[Property][Category( "Materials" )] public Material Material256 { get; set; }
	[Property][Category( "Materials" )] public Material Material512 { get; set; }
	[Property][Category( "Materials" )] public Material Material1024 { get; set; }
	[Property][Category( "Materials" )] public Material Material2048 { get; set; }
	protected override void OnUpdate()
	{
		UpdateMaterial();
	}
	void UpdateMaterial()
	{
		if ( ModelRenderer == null ) return;
		var MaterialsDictionary = new Dictionary<string, Material>()
		{
		{"Material2", Material2},
		{"Material4", Material4},
		{"Material8", Material8},
		{"Material16", Material16},
		{"Material32", Material32},
		{"Material64", Material64},
		{"Material128", Material128},
		{"Material256", Material256},
		{"Material512", Material512},
		{"Material1024", Material1024},
		{"Material2048", Material2048}
		};

		try { ModelRenderer.SetMaterial( MaterialsDictionary[$"Material{GameObject.Tags.ToString()}"] ); }
		catch { }
	}
}

