using Sandbox;

public sealed class GameEnd : Component
{
	[Property] public ModelRenderer Frame {  get; set; }
	[Property] public TextRenderer Title { get; set; }
	[Property] public TextRenderer Subtitle { get; set; }

	protected override void OnAwake()
	{
		GameObject.Components.Get<GameEnd>().Enabled = false;
	}

	protected override void OnEnabled()
	{
		Frame.Enabled = true;
		Title.Enabled = true;
		Subtitle.Enabled = true;
	}
	protected override void OnDisabled()
	{
		Frame.Enabled = false;
		Title.Enabled = false;
		Subtitle.Enabled = false;
	}
}
