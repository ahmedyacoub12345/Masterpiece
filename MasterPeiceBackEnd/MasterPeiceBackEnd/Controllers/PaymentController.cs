using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using MasterPieceBackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal;

namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        private readonly PayPalPaymentService _payPalService;

        public PaymentController(PayPalPaymentService payPalService,MedicalAppContext db)
        {
            _db = db;
            _payPalService = payPalService;
        }

        //[HttpPost("create-payment")]
        //public IActionResult CreatePayment([FromBody] PaymentRequestDto paymentRequest)
        //{
        //    try
        //    {
        //        var returnUrl = Url.Action("ExecutePayment", "Payment", null, Request.Scheme); // URL for after successful payment
        //        var cancelUrl = Url.Action("CancelPayment", "Payment", null, Request.Scheme);  // URL for when user cancels

        //        // Ensure URLs are set and valid
        //        PayPal.Api.Payment payment = _payPalService.CreatePayment(paymentRequest.Amount, "USD", returnUrl, cancelUrl);

        //        // Generate the transaction ID
        //        var transationId = Guid.NewGuid();

        //        // Save payment information from front-end (UserId) and other details
        //        var newPayment = new MasterPeiceBackEnd.Models.UserPayment
        //        {
        //            UserId = paymentRequest.UserId,  // Get the UserId from the front-end
        //            Amount = paymentRequest.Amount,
        //            PaymentStatus = "Approved",
        //            PaymentDate = DateTime.Now,
        //            PaymentMethod = "Paypal",
        //            TransactionId = transationId.ToString()
        //        };

        //        _db.Payments.Add(newPayment);
        //        _db.SaveChanges();

        //        // Get approval URL
        //        var approvalUrl = payment.links.FirstOrDefault(l => l.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;

        //        if (string.IsNullOrEmpty(approvalUrl))
        //        {
        //            return BadRequest("Could not retrieve the approval URL from PayPal.");
        //        }

        //        return Ok(new { approval_url = approvalUrl });
        //    }
        //    catch (PaymentsException ex)
        //    {
        //        return StatusCode(500, $"PayPal error: {ex.Response}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        [HttpPost("create-payment")]
        public IActionResult CreatePayment([FromBody] PaymentRequestDto paymentRequest)
        {
            try
            {
                var returnUrl = Url.Action("ExecutePayment", "Payment", null, Request.Scheme);
                var cancelUrl = Url.Action("CancelPayment", "Payment", null, Request.Scheme);

                // Assuming c is meant to be a fixed integer, for example, 25
                int c = 25;  // Define c with a meaningful value

                // Corrected line: currency is passed as a string
                var payment = _payPalService.CreatePayment(paymentRequest.Amount.ToString("F2"), 25, returnUrl, c);

                // Generate the transaction ID
                var transactionId = Guid.NewGuid();

                // Save payment information
                var newPayment = new MasterPeiceBackEnd.Models.UserPayment
                {
                    UserId = paymentRequest.UserId,
                    Amount = paymentRequest.Amount,
                    PaymentStatus = "Approved",
                    PaymentDate = DateTime.Now,
                    PaymentMethod = "Paypal",
                    TransactionId = transactionId.ToString()
                };

                _db.Payments.Add(newPayment);
                _db.SaveChanges();

                // Get approval URL
                var approvalUrl = payment.links.FirstOrDefault(l => l.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;

                if (string.IsNullOrEmpty(approvalUrl))
                {
                    return BadRequest("Could not retrieve the approval URL from PayPal.");
                }

                return Ok(new { approval_url = approvalUrl });
            }
            catch (PaymentsException ex)
            {
                return StatusCode(500, $"PayPal error: {ex.Response}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("execute-payment")]
        public IActionResult ExecutePayment(string paymentId, string payerId, int doctorId, TimeOnly bookingTime, DateTime bookingDate)
        {
            try
            {
                
                PayPal.Api.Payment executedPayment = _payPalService.ExecutePayment(paymentId, payerId);

                
                if (executedPayment.state.ToLower() != "approved")
                {
                    return BadRequest("Payment was not approved.");
                }

               
                var paymentRecord = _db.Payments.FirstOrDefault(p => p.TransactionId == paymentId);
                if (paymentRecord == null)
                {
                    return BadRequest("Payment record not found.");
                }

                
                var newBooking = new Booking
                {
                    UserId = paymentRecord.UserId, 
                    DoctorId = doctorId, 
                    Time = bookingTime, 
                    BookingDate = bookingDate, 
                    PaymentStatus = "Approved" 
                };

                
                _db.Bookings.Add(newBooking);
                _db.SaveChanges();

                return Ok("Payment successful and booking created.");
            }
            catch (PaymentsException ex)
            {
                return StatusCode(500, $"PayPal error: {ex.Response}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        private void SavePaymentDetails(PayPal.Api.Payment payment, int userId, int doctorId, TimeOnly bookingTime, DateTime bookingDate)
        {
            var transaction = payment.transactions[0];

            
            var newPayment = new Models.UserPayment
            {
                UserId = userId,
                Amount = Convert.ToDecimal(transaction.amount.total),
                PaymentMethod = payment.payer.payment_method,
                TransactionId = payment.id,
                PaymentStatus = payment.state,
                PaymentDate = DateTime.Now
            };

            _db.Payments.Add(newPayment);
            _db.SaveChanges();

            
            var newBooking = new Booking
            {
                UserId = userId,
                DoctorId = doctorId, 
                Time = bookingTime, 
                BookingDate = bookingDate, 
                PaymentStatus = payment.state 
            };

            _db.Bookings.Add(newBooking);
            _db.SaveChanges();
        }

        [HttpGet("cancel-payment")]
        public IActionResult CancelPayment()
        {
            // Handle payment cancellation here
            return BadRequest("Payment was cancelled by the user.");
        }

    }
}
