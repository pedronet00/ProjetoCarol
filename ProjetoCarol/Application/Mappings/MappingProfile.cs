using AutoMapper;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Entities.Usuario;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UsuarioDTO, Usuario>()
            .ConstructUsing(dto =>
                new Usuario(dto.FullName!, DateTime.Now))
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName ?? src.Email))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber));

        CreateMap<Usuario, UsuarioViewModel>()
            .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(x => x.NomeCompleto))
            .ForMember(dest => dest.NumeroTelefone, opt => opt.MapFrom(x => x.PhoneNumber))
            .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(x => x.DataNascimento))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Ativo))
            .ReverseMap();

        CreateMap<UsuarioPagamento, UsuarioPagamentoViewModel>();
        CreateMap<UsuarioPagamentoDTO, UsuarioPagamento>();

        CreateMap<AtualizarUsuarioDTO, Usuario>();
        CreateMap<Usuario, UsuarioViewModel>();
        
        CreateMap<UsuarioAula, UsuarioAulaViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UsuarioMatriculaId, opt => opt.MapFrom(src => src.UsuarioMatriculaId))
            .ForMember(dest => dest.IdiomaAula, opt => opt.MapFrom(src => src.UsuarioMatricula.Idioma))
            .ForMember(dest => dest.NomeAluno, opt => opt.MapFrom(src => src.UsuarioMatricula.Usuario.NomeCompleto))
            .ForMember(dest => dest.DataAula, opt => opt.MapFrom(src => src.DataAula))
            .ForMember(dest => dest.StatusAula, opt => opt.MapFrom(src => src.StatusAula));
    }
}
