﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrderBL iorderBL;
        public OrderController(IOrderBL iorderBL)
        {
            this.iorderBL = iorderBL;       
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddFeedback(OrderModel orderModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iorderBL.AddOrder(userId, orderModel);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order Addition UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
