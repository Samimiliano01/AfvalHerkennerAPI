using Sensoring_API.Dto;

namespace Sensoring_API.Repositories;

public interface ILitterRepository
{
    Task Create(LitterCreateDto litterCreateDto);
    Task<List<LitterReadDto>> Read();
    Task Delete(int id);
}