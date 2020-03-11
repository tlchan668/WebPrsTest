using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPrsTest.Data;
using WebPrsTest.Models;

namespace WebPrsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public const string StatusNew = "NEW";
        public const string StatusEdit = "EDIT";
        public const string StatusReview = "REVIEW";
        public const string StatusApproved = "APPROVED";
        public const string StatusRejected = "REJECTED";

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }
       
        

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Request.ToListAsync();
        }

        //getall requests back that are in review status but not your id 
        // GET: api/requests/getRequestsReview/5
        [HttpGet("GetRequestsReview/{id}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsReview(int id) {

            return await _context.Request.Where(x => x.UserId != id && x.Status == StatusReview).ToListAsync();
        }
        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }
        //Post: api/requests/SetToReview
        [HttpPost("SetToReview")]
        public bool SetToReview(Request request) {
            if (request.Total <= 50) {
                request.Status = StatusApproved;
            } else {
                request.Status = StatusReview;
            }
            return Update(request.Id, request);
        }
        //POST: api/requests/SetToApproved
        [HttpPost("SetToApproved")]
        public bool SetToApproved(Request request) {
            request.Status = StatusApproved;
            return Update(request.Id, request);
        }
        // POST: api/requests/SetToRejected/5
        [HttpPost("SetToRejected")]
        public bool SetToRejected(Request request) {
            request.Status = StatusRejected;
            return Update(request.Id, request);
        }

        public bool Update(int id, Request request) {
            //do so only updating the one you want
            if (request == null) throw new Exception("request cannot be null");
            if (id != request.Id) throw new Exception("Id and request.Id must match");

            _context.Entry(request).State = EntityState.Modified;//tells state that it is an update not add

            try {
                _context.SaveChanges();//trap exception for a dup vendor by doing a try catch
            } catch (DbUpdateException ex) {
                //if get it what will we do
                throw new Exception("request must be unique", ex);
                //give developer the origianl exception thrown by doing ex above
            } catch (Exception ex) {
                throw new Exception("request", ex);
            }
            return true;
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}
