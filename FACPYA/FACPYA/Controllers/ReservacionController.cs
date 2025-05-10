using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FACPYA.Models;
using System.Net;
using System.Net.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FACPYA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionController : ControllerBase
    {
        private readonly DbfacpyaContext _context;

        public ReservacionController(DbfacpyaContext context)
        {
            _context = context;
        }

        // GET: api/Reservacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservacion>>> GetReservacions()
        {
            return await _context.Reservacions.ToListAsync();
        }

        // GET: api/Reservacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservacion>> GetReservacion(int id)
        {
            var reservacion = await _context.Reservacions.FindAsync(id);

            if (reservacion == null)
            {
                return NotFound();
            }

            return reservacion;
        }

        // PUT: api/Reservacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservacion(int id, Reservacion reservacion)
        {
            if (id != reservacion.IdReservacion)
            {
                return BadRequest();
            }

            _context.Entry(reservacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservacion
        [HttpPost]
        public async Task<ActionResult<Reservacion>> PostReservacion(Reservacion reservacion)
        {
            _context.Reservacions.Add(reservacion);
            await _context.SaveChangesAsync();

            // Obtener datos del cliente y paquete
            var cliente = await _context.Clientes.FindAsync(reservacion.ClienteId);
            var paquete = await _context.PaqueteViajes.FindAsync(reservacion.PaqueteId);

            if (cliente != null && paquete != null)
            {
                EnviarCorreo(cliente.Correo, cliente.NombreCompleto, paquete.Nombre, paquete.Descripcion, paquete.Precio.ToString("C"));
                EnviarSms(cliente.Telefono, cliente.NombreCompleto, paquete.Nombre);
            }

            return CreatedAtAction("GetReservacion", new { id = reservacion.IdReservacion }, reservacion);
        }

        // DELETE: api/Reservacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservacion(int id)
        {
            var reservacion = await _context.Reservacions.FindAsync(id);
            if (reservacion == null)
            {
                return NotFound();
            }

            _context.Reservacions.Remove(reservacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservacionExists(int id)
        {
            return _context.Reservacions.Any(e => e.IdReservacion == id);
        }

        private void EnviarCorreo(string correoDestino, string nombreCliente, string nombrePaquete, string descripcionPaquete, string precioPaquete)
        {
            var remitente = new MailAddress("xrmissa@gmail.com", "Agencia de Viajes");
            var destinatario = new MailAddress(correoDestino);
            const string password = "dkcf wbdp ytxz cvuk";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(remitente.Address, password)
            };

            var mensaje = new MailMessage(remitente, destinatario)
            {
                Subject = "Reservación Confirmada",
                IsBodyHtml = true,
                Body = $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; }}
        .header {{ background-color: #4CAF50; color: white; padding: 10px; text-align: center; }}
        .content {{ padding: 20px; }}
        .footer {{ background-color: #f1f1f1; color: gray; padding: 10px; text-align: center; }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Confirmación de Reservación</h2>
    </div>
    <div class='content'>
        <p>Hola <strong>{nombreCliente}</strong>,</p>
        <p>Tu reservación para el paquete <strong>{nombrePaquete}</strong> ha sido registrada con éxito.</p>
        <p><strong>Detalles de la Reservación:</strong></p>
        <ul>
            <li><strong>Descripción:</strong> {descripcionPaquete}</li>
            <li><strong>Precio:</strong> {precioPaquete}</li>
        </ul>
        <p>¡Gracias por elegirnos!</p>
    </div>
    <div class='footer'>
        <p>FACPYA Agencia de Viajes | Contacto: info@facpya.com</p>
    </div>
</body>
</html>
"
            };

            smtp.Send(mensaje);
        }

        private void EnviarSms(string numeroDestino, string nombreCliente, string nombrePaquete)
        {
            const string accountSid = "ACb093215d29687b0c98c8fb2de830ea51";
            const string authToken = "b83c8cf875900213520680ebfb6e5249";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber("+52" + numeroDestino), // Asegúrate de que el número sea el formato correcto
                from: new PhoneNumber("+15075333102"), // tu número Twilio
                body: $"¡Hola {nombreCliente}! 🌟 Tu reservación para el paquete '{nombrePaquete}' ha sido confirmada. ¡Gracias por elegirnos! 🌍"
            );
        }
    }
}
