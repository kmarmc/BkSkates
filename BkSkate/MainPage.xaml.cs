namespace BkSkate;
public partial class MainPage : ContentPage
{
	bool Morreu = false;
	bool pulo = false;
	const int TempoEntreFrames = 25;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade = 0;
	int LarguraJanela = 0;
	int AlturaJanela = 0;
	Player player;
	const int ForcaGravidade = 6;
	bool EstaNoChao = true;
	bool EstaNoAr = false;
	bool EstaPulando = false;
	int TempoPulando = 0;
	int TempoNoAr = 0;
	const int ForcaPulo = 12;
	const int maxTempoPulando = 10;
	const int maxTempoNoAr = 4;
	Inimigos inimigos;

	public MainPage()
	{
		InitializeComponent();
		player = new Player(imgplayer);
		player.Run();
	}


	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalcularVelocidade(w);
		inimigos = new Inimigos(-w);
		inimigos.Add(new Inimigo(imgInimigo1));
		inimigos.Add(new Inimigo(imgInimigo2));
		inimigos.Add(new Inimigo(imgInimigo3));
		inimigos.Add(new Inimigo(imgInimigo4));
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
			(b as Image).WidthRequest = w;
		foreach (var c in HSLayerChao.Children)
			(c as Image).WidthRequest = w;
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
	async Task Desenha()
	{
		while (!Morreu)
		{
			GerenciarCenarios();
			if (inimigos != nuul)
				inimigos.Desenha(velocidade);
			if (!EstaPulando && !EstaNoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
			else
				AplicaPulo();
			await Task.Delay(TempoEntreFrames);
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}
	void AplicaPulo()
	{
		EstaNoChao = false;
		if (EstaPulando && TempoPulando >= maxTempoPulando)
		{
			EstaPulando = false;
			EstaNoAr = true;
			TempoNoAr = 0;
		}
		else if (EstaNoAr && TempoNoAr >= maxTempoNoAr)
		{
			EstaPulando = false;
			EstaNoAr = false;
			TempoPulando = 0;
			TempoNoAr = 0;
		}
		else if (EstaPulando && TempoPulando < maxTempoPulando)
		{
			player.MoveY(-ForcaPulo);
			TempoPulando++;
		}
		else if (EstaNoAr)
			TempoNoAr++;
	}
	void OnGridTapped(object o, TappedEventArgs a)
	{
		if (EstaNoChao)
			EstaPulando = true;
	}
	void AplicaGravidade()
	{
		if (player.GetY() < 0)
			player.MoveY(ForcaGravidade);
		else if (player.GetY() >= 0)
		{
			player.SetY(0);
			EstaNoChao = true;
		}
	}
}