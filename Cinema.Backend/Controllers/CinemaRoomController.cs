using Cinema.DAL.Implemantations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CinemaRoomController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;

    public CinemaRoomController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetAllRooms")]
    public async Task<ActionResult<List<CinemaRoom>>> GetAllAsync()
    {
        try
        {
            var rooms = await _unitOfWork.CinemaRoomRepository.Get();
            return Ok(rooms);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Помилка під час отримання всіх залів перегляду: {e.Message}");
        }
        
    }

    [HttpGet("GetRoomById/{id}")]
    public async Task<ActionResult<CinemaRoom>> GetByIdAsync(int id)
    {
        try
        {
            var room = await _unitOfWork.CinemaRoomRepository.GetByIDAsync(id);
            return Ok(room);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Помилка під час отримання залу перегляду по id: {e.Message}");
        }
    }

    [HttpPost("AddRoom")]
    public async Task<ActionResult> PostAsync([FromBody] CinemaRoom room)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            await _unitOfWork.CinemaRoomRepository.InsertAsync(room);
            await _unitOfWork.SaveAsync();
            return Ok(room);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Помилка під час додавання нової кімнати: {e.Message}");
        }
    }

    [HttpPut("UpdateRoom")]
    public async Task<ActionResult> UpdateAsync([FromBody] CinemaRoom room)
    {
        try
        {
            await _unitOfWork.CinemaRoomRepository.UpdateAsync(room);
            return Ok(room);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Помилка під час оновлення даних кімнати: {e.Message}");
        }
    }

    [HttpDelete("DeleteRoom/{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var roomFromDb = await _unitOfWork.CinemaRoomRepository.GetByIDAsync(id);
        if (roomFromDb == null)
        {
            return NotFound();
        }
            
        try
        {
            await _unitOfWork.CinemaRoomRepository.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Помилка під час видалення кімнати з id({id}): {e.Message}");
        }
        
        
    }
    


}