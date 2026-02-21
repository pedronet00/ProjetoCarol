using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Domain.Entities.Arquivo;

public class Arquivo
{
    public Guid Id { get; private set; }
    public string NomeOriginal { get; private set; }
    public string NomeSalvo { get; private set; }
    public string Caminho { get; private set; }
    public Idiomas Idioma { get; private set; }

    public int Nivel { get; private set; }
    public DateTime DataUpload { get; private set; }

    private Arquivo() { }

    public Arquivo(string nomeOriginal, string nomeSalvo, string caminho, Idiomas idioma, int nivel)
    {
        Id = Guid.NewGuid();
        NomeOriginal = nomeOriginal;
        NomeSalvo = nomeSalvo;
        Caminho = caminho;
        Idioma = idioma;
        DataUpload = DateTime.UtcNow;
        Nivel = nivel;
    }
}
