using FeedbackService_Application.Services.Interfaces;
using FeedbackService_Application.Structs;
using FeedbackService_Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FeedbackService_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<EndpointReturn> Insert([FromBody] Feedback feedback)
        {
            try
            {
                await _feedbackService.Insert(feedback);
                return new EndpointReturn("Feedback enviado com sucesso", HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Feedback> Get([FromQuery] int idProduct)
        {
            try
            {
                return _feedbackService.GetAllByProduct(idProduct);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
