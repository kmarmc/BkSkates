using FFImageLoading.Maui;

namespace BkSkate;

public class Inimigo
{
    Image imageView;
    public Inimigo(Image a)
    {
        imageView=a;
    }
    public void MoveX(double s)
    {
        imageView.TranslationX-=s;
    }
    public double GetX()
    {
        return imageView.TranslationX;
    }
    public void Reset()
    {
        imageView.TranslationX=500;
    }
}

public class Inimigos
{
    List<Inimigo> inimigos= new List<Inimigo>();
    private Inimigo atual = null;
    double minX=0;
    
    public Inimigos(double a)
    {
        minX=a;
    }
    public void Add(Inimigo a)
    {
    inimigos.Add(a);
    if (atual==null)
        atual =a;
    Iniciar ();
    }
    public void Iniciar ()
    {
        foreach(var e in inimigos)
         e.Reset();
    }
}