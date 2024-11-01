namespace BkSkate;
bool morreu = false;
bool pulo = false;
const int TempoFrames = 25;
int velocidade1 = 0;
int velocidade2 = 0;
int velocidade3 = 0;
int velocidade = 0;
int LarguraJanela = 0;
int AlturaJanela = 0;


public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
}

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
	velocidade3 = (int)(w * 0.008);
	velocidade = (int)(w * 0.01);
}
void CorrigeTamanhoCenario(double w, double h)
{
	foreach (var a in HSLayer1.Children)
		(a as Image).WidthRequest = w;
	foreach (var b in HSLayer2.Children)
		(a as Image).WidthRequest = w;
	foreach (var c in HSLayer3.Children)
		(a as Image).WidthRequest = w;
	foreach (var c in HSLayerChao.Children)
		(a as Image).WidthRequest = w;
	HSLayer1.WidthRequest = w * 1.5;
	HSLayer2.WidthRequest = w * 1.5;
	HSLayer3.WidthRequest = w * 1.5;
	HSLayerChao.WidthRequest = w * 1.5;

}