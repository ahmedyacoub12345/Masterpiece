﻿using MasterPeiceBackEnd.Models;
using MasterPieceBackEnd.Model;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;

namespace E_Commerce_Clothes.DTO
{
    public class PayPalPaymentService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly MedicalAppContext _db;


        public PayPalPaymentService(IConfiguration config, MedicalAppContext db)
        {
            _clientId = config["PayPal:ClientId"];
            _clientSecret = config["PayPal:ClientSecret"];
            _db = db;

        }

        // Method to create a payment
        public PayPal.Api.Payment CreatePayment(decimal amount, string currency, string returnUrl, string cancelUrl)
        {
            var apiContext = GetAPIContext();

            var payer = new Payer { payment_method = "paypal" };

            // Make sure cancelUrl and returnUrl are not null
            if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrEmpty(cancelUrl))
            {
                throw new ArgumentException("Return URL and Cancel URL are required.");
            }

            var redirUrls = new RedirectUrls
            {
                cancel_url = cancelUrl,  // This is required when payment method is PayPal
                return_url = returnUrl   // Redirect the user back after approval
            };

            var details = new Details { tax = "0", shipping = "0", subtotal = amount.ToString() };

            var amountObj = new Amount
            {
                currency = currency,
                total = amount.ToString(), // Total must match details
                details = details
            };

            var transactionList = new List<Transaction>
    {
        new Transaction
        {
            description = "Payment for your order",
            amount = amountObj,

        }
    };

            var payment = new PayPal.Api.Payment
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls  // Make sure redirect URLs are set
            };

            
            return payment.Create(apiContext);  // Create payment
        }


        // Method to execute a payment
        public PayPal.Api.Payment ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = GetAPIContext();

            var paymentExecution = new PaymentExecution { payer_id = payerId };
            var payment = new PayPal.Api.Payment { id = paymentId };

            // Execute the payment
            return payment.Execute(apiContext, paymentExecution);
        }

        // Helper method to get the PayPal API context
        private APIContext GetAPIContext()
        {
            var accessToken = new OAuthTokenCredential(_clientId, _clientSecret).GetAccessToken();
            return new APIContext(accessToken);
        }
    }
}