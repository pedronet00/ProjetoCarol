namespace ProjetoCarol.Application.Interfaces;

using Microsoft.AspNetCore.Http;
using ProjetoCarol.Application.ViewModel;
using ProjetoCarol.Domain.Enums;

public interface IArquivoService
{
    Task<IEnumerable<ArquivoViewModel>> ListarPorIdioma(Idiomas idioma);
    Task<ArquivoViewModel> Upload(IFormFile file, Idiomas idioma);
    Task<(byte[] arquivo, string contentType, string nome)> Download(Guid id);
    Task<bool> Excluir(Guid id);
}

