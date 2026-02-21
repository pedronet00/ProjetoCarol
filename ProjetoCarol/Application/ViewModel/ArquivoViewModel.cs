namespace ProjetoCarol.Application.ViewModel;

using ProjetoCarol.Domain.Enums;

public class ArquivoViewModel
{
    public Guid Id { get; set; }
    public string NomeOriginal { get; set; }
    public Idiomas Idioma { get; set; }
    public DateTime DataUpload { get; set; }

    public int Nivel { get; set; }
}

