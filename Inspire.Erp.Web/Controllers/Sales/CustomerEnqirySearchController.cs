using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Domain.Modals.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Sales
{
    [Route("api/CustomerEnqirySearch")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerEnqirySearchController : ControllerBase
    {
        private ICustomerEnquiryService _customerEnquiryRepository;
        public CustomerEnqirySearchController(ICustomerEnquiryService customerEnquiryRepository)
        {
            _customerEnquiryRepository = customerEnquiryRepository;
        }

        [HttpPost]
        [Route("getCustomerEnquirySearch")]
        public async Task<IActionResult> CustomerEnquirySearch(CustomerEnquirySearchFilter filter)
        {
            return Ok(await _customerEnquiryRepository.GetCustomerEnquirySearch(filter));
        }
        [HttpGet]
        [Route("getEnquiryNo")]
        public async Task<IActionResult> getAllEnquiryNo()
        {
            return Ok(await _customerEnquiryRepository.GetAllEnquiryNo());
        }
        [HttpPost]
        [Route("GetCustomerEnquirybyStatuss")]
        public async Task<IActionResult> getCustomerEnquirybyStatuss(CustomerEnquirySearchFilterStatus filter)
        {
            return Ok(await _customerEnquiryRepository.getCustomerEnquirybyStatuss(filter));
        }
    }
}
