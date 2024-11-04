namespace BkSkate;
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
bool Morreu = false;
bool pulo = false;
const int TempoEntreFrames = 25;
int velocidade1 = 0;
int velocidade2 = 0;
int velocidade = 0;
int LarguraJanela = 0;
int AlturaJanela = 0;

	protected override void OnSizedAllocated(double w, double h)
	{
		base.OnSizedAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalcularVelocidade;
	}
	void CalcularVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade = (int)(w * 0.01);
	}
	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var a in HSLayer1.Children)
			(a as Image).WidthRequest = w;
		foreach (var b in HSLayer2.Children)
			(a as Image).WidthRequest = w;
		foreach (var c in HSLayerChao.Children)
			(a as Image).WidthRequest = w;
		HSLayer1.WidthRequest = w * 1.5;
		HSLayer2.WidthRequest = w * 1.5;
		HSLayerChao.WidthRequest = w * 1.5;

	}
	void GerenciarCenarios()
	{
		MoverCenarios();
		GerenciarCenario(HSLayer1);
		GerenciarCenario(HSLayer2);
		GerenciarCenario(HSLayerChao);
	}
	void MoverCenarios()
	{
		HSLayer1.TranslationX -= velocidade1;
		HSLayer2.TranslationX -= velocidade2;
		HSLayerChao.TranslationX -= velocidade;
	}
	void GerenciarCenario(HorizontalStackLayout HSL)
	{
		var view = (HSL.Children.First() as Image);
		if (view.WidthRequest + HSL.TranslationX < 0)
		{
			HSL.Children.Remove(view);
			HSL.Children.Add(view);
			HSL.TranslationX = view.TranslationX;
		}
	}
	asynic TaskDesenha()
	{
		while (!Morreu)
		{
			GerenciarCenarios();
			await TaskDelay(TempoEntreFrames);
		}
	}
	protected override void OnAppearing()
	{
		Base.OnAppearing();
		Desenha();
	}
}