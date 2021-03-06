using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using iBalekaAPI.Models.Responses;
using iBalekaAPI.Core.Extensions;

namespace iBalekaAPI.Core.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public class EventController : Controller
    {
        private IEventService _context;
        private IRouteService _routeContext;
        
        public EventController(IEventService _repo, IRouteService _rContext)
        {
            _context = _repo;
            _routeContext = _rContext;
        }
        /// <summary>
        /// Get all user created events
        /// </summary>
        /// <param name="userId" type="int">User Id</param>
        /// <remarks>Get user created events</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        // GET: Event/Events
        [HttpGet]
        [Route("User/[action]")]
        public async Task<IActionResult> GetUserEvents([FromQuery]string userId)
        {
            var response = new ListModelResponse<Event>() 
                as IListModelResponse<Event>;
            try
            {
                if (userId == null)
                    throw new Exception("User Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Event> evnts = _context.GetUserEvents(userId); ;
                    if (evnts == null)
                        throw new Exception("No Events");
                    return evnts;
                });
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        /// <summary>
        /// Get all club created events
        /// </summary>
        /// <param name="clubId" type="int">Club Id</param>
        /// <remarks>Get club created events</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        // GET: Event/Events
        [HttpGet]
        [Route("Club/[action]")]
        public async Task<IActionResult> GetClubEvents([FromQuery]int clubId)
        {
            var response = new ListModelResponse<Event>()
                as IListModelResponse<Event>;
            try
            {
                if (clubId<=0)
                    throw new Exception("Club Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Event> evnts = _context.GetClubEvents(clubId); ;
                    if (evnts == null)
                        throw new Exception("No Events");
                    return evnts;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        /// <summary>
        /// Get all events
        /// </summary>
        /// <remarks>Get all events</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetEvents()
        {
            var response = new ListModelResponse<Event>()
                as IListModelResponse<Event>;
            try
            {
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Event> evnts =_context.GetEvents();
                    if(evnts==null)
                        throw new Exception("No Events");
                    return evnts;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        /// <summary>
        /// Get all Event EventRoutes
        /// </summary>
        /// <param name="evntId" type="int">Event Id</param>
        /// <remarks>Get all Event EventRoutes</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetEventRoutes([FromQuery]int evntId)
        {
            var response = new ListModelResponse<EventRoute>()
                as IListModelResponse<EventRoute>;
            try
            {
                if (evntId <=0)
                    throw new Exception("Event Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<EventRoute> evnts = _context.GetEventRoutes(evntId);
                    if (evnts == null)
                        throw new Exception("No Event Routes");
                    return evnts;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        // GET: Event/Details/5
        /// <summary>
        /// Get a particular event
        /// </summary>
        /// <param name="eventId" type="int">Event Id</param>
        /// <remarks>Get an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetEvent([FromQuery]int eventId)
        {
            var response = new SingleModelResponse<Event>()
                as ISingleModelResponse<Event>;
            try
            {
                if (eventId <1)
                    throw new Exception("User Id is null");
                response.Model = await Task.Run(() =>
                {
                    Event evnt= _context.GetEventByID(eventId);
                    if (evnt == null)
                        throw new Exception("Event does not Exist");
                    return evnt;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Get a events by route
        /// </summary>
        /// <param name="routeId" type="int">Route Id</param>
        /// <remarks>Gets events by route</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Route/[action]")]
        public async Task<IActionResult> GetEventByRoute([FromQuery]int routeId)
        {
            var response = new ListModelResponse<Event>()
                as IListModelResponse<Event>;
            try
            {
                if (routeId < 1)
                    throw new Exception("User Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Event> evnt = _context.GetEventByRoute(routeId);
                    if (evnt == null)
                        throw new Exception("Event does not Exist");
                    return evnt;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        //save event
        /// <summary>
        /// Saves an event
        /// </summary>
        /// <param name="evnt" type="Event">Event Model</param>
        /// <remarks>Saves an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SaveEvent([FromBody]Event evnt)
        {
            var response = new SingleModelResponse<Event>()
               as ISingleModelResponse<Event>;
            try
            {
                if(evnt==null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                   Event nevnt= _context.AddEvent(evnt);

                    return nevnt;                   
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
            
        }
        // POST: Event/Edit/5
        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="evnt" type="Event">Event Model</param>
        /// <remarks>Update an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("Update/[action]")]
        public async Task<IActionResult> EditEvent([FromBody]Event evnt)
        {

            var response = new SingleModelResponse<Event>()
               as ISingleModelResponse<Event>;
            try
            {
                if (evnt == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    Event nevnt=_context.UpdateEvent(evnt);

                    return nevnt;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // POST: Event/Delete/5
        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="evnt" type="int">Event Id</param>
        /// <remarks>Delete an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("[action]")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent([FromQuery]int evnt)
        {
            var response = new SingleModelResponse<Event>()
               as ISingleModelResponse<Event>;
            try
            {
                if (evnt.ToString() == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.Delete(evnt);
                    Event dEvent = new Event();
                    dEvent.EventId = evnt;
                    return dEvent;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();

        }
    }
}