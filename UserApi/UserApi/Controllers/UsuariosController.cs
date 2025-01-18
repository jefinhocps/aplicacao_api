using Microsoft.AspNetCore.Mvc;
using UserApi.Repositories;
using UserApi.Models;
using System.Collections.Generic;


namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository("Server=localhost;Database=prod;Uid=root;Pwd=;");
        }

        // GET: API/usuarios
        [HttpGet]
        public ActionResult<List<Usuario>> GetUsuarios() {
            var usuarios = _usuarioRepository.GetUsuarios();
            return Ok(usuarios);
        }

        // POST: API/usuarios
        [HttpPost]
        public ActionResult<Usuario> CreateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados do usuário são necessários.");
            }

            try
            {
                _usuarioRepository.CreateUsuario(usuario);
                return CreatedAtAction(nameof(GetUsuariosById), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar usuário: " + ex.Message);
            }
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuariosById(int id)
        {
            var usuario = _usuarioRepository.GetUsuariosById(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
        }

        // DELETE api/usuarios
        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario(int id)
        {
            try
            {
                bool usuarioDeletado = _usuarioRepository.DeleteUsuario(id);

                if (!usuarioDeletado)
                {
                    return NotFound("Usuário não encontrado.");
                }
                return NoContent(); // Retorna 204 (Sem conteúdo) se exclusão for bem sucedida
            }

            catch (Exception ex)
            {
                {
                    return StatusCode(500, "Erro ao Excluir usuário: " + ex.Message);
                }
            }
        }
    }
}
