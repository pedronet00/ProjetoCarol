namespace ProjetoCarol.Application.Services;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.ViewModel;
using ProjetoCarol.Domain.Entities.Arquivo;
using ProjetoCarol.Domain.Enums;
using ProjetoCarol.Infrastructure.Context;

public class ArquivoService : IArquivoService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ArquivoService(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IEnumerable<ArquivoViewModel>> ListarPorIdioma(Idiomas idioma)
    {
        return await _context.Arquivos
            .Where(a => a.Idioma == idioma)
            .OrderByDescending(a => a.DataUpload)
            .Select(a => new ArquivoViewModel
            {
                Id = a.Id,
                NomeOriginal = a.NomeOriginal,
                Idioma = a.Idioma,
                DataUpload = a.DataUpload,
                Nivel = a.Nivel
            })
            .ToListAsync();
    }


    public async Task<ArquivoViewModel> Upload(IFormFile file, Idiomas idioma, int nivel)
    {
        var uploadsPath = Path.Combine(_env.ContentRootPath, "uploads");

        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var nomeSalvo = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var caminhoCompleto = Path.Combine(uploadsPath, nomeSalvo);

        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var arquivo = new Arquivo(
            file.FileName,
            nomeSalvo,
            caminhoCompleto,
            idioma,
            nivel
        );

        _context.Arquivos.Add(arquivo);
        await _context.SaveChangesAsync();

        return new ArquivoViewModel
        {
            Id = arquivo.Id,
            NomeOriginal = arquivo.NomeOriginal,
            Idioma = arquivo.Idioma,
            DataUpload = arquivo.DataUpload
        };
    }

    public async Task<(byte[] arquivo, string contentType, string nome)> Download(Guid id)
    {
        var arquivo = await _context.Arquivos.FirstOrDefaultAsync(x => x.Id == id);

        if (arquivo is null)
            throw new Exception("Arquivo não encontrado.");

        var bytes = await File.ReadAllBytesAsync(arquivo.Caminho);

        return (bytes, "application/octet-stream", arquivo.NomeOriginal);
    }

    public async Task<bool> Excluir(Guid id)
    {
        var arquivo = await _context.Arquivos.FirstOrDefaultAsync(x => x.Id == id);

        if (arquivo is null)
            return false;

        if (File.Exists(arquivo.Caminho))
            File.Delete(arquivo.Caminho);

        _context.Arquivos.Remove(arquivo);
        await _context.SaveChangesAsync();

        return true;
    }
}
