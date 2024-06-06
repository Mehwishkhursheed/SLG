using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SLG.Data;
using SLG.Models;
using SLG.ViewModels;
using System;

namespace SLG.Controllers
{
    public class Sale_OrderController : Controller
    {
        private int count = 0;

        private readonly SLGC _context;


        public Sale_OrderController(SLGC context)
        {
            _context = context;
            
        }

        // GET: Sale_Order
        public async Task<IActionResult> Index()
        {
            var fYPContext = _context.Sale_Orders.Include(s => s.customer);
            return View(await fYPContext.ToListAsync());
        }

        // GET: Sale_Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //var viewModel = new SalesViewModel
            //{
            //    SaleOrder = new Sale_Order(),
            //    SaleItems = new List<Sale_Order_Detail>(),
            //};
            //if (id == null || _context.Sale_Orders == null)
            //{
            //    return NotFound();
            //}

            //var sale_Order = await _context.Sale_Orders
            //    .Include(s => s.customer)
            //    .Include(s => s.payment_methodNavigation)
            //    .FirstOrDefaultAsync(m => m.sale_id == id);
            //if (sale_Order == null)
            //{
            //    return NotFound();
            //}

            //return View(viewModel);

            if (id == null)
            {
                return NotFound();
            }

            var sale_Order = await _context.Sale_Orders
                .Include(s => s.customer)
                
                .FirstOrDefaultAsync(m => m.sale_id == id);

            if (sale_Order == null)
            {
                return NotFound();
            }

            var viewModel = new SalesViewModel
            {
                SaleOrder = sale_Order,
                SaleItems = await _context.Sale_Order_Details
                .Include(d => d.product)
            .Where(d => d.sale_id == id)
            .ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Sale_Order/Create

        public IActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_name");
            
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");
            var viewModel = new SalesViewModel
            {
                SaleOrder = new Sale_Order(),
                SaleItems = new List<Sale_Order_Detail>(),
            };

            return View(viewModel);
        }


        // POST: Sale_Order/Create
        //public async Task<IActionResult> Create([Bind("sale_id,customer_id,name,total_amount,payment_method,date_created,state")] Sale_Order sale_Order)
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesViewModel viewModel)
        {
            // Save the sale order header to the database
            _context.Sale_Orders.Add(viewModel.SaleOrder);
            _context.SaveChanges();

            // Associate the line items with the sale order

            foreach (var item in viewModel.SaleItems)
            {
                var lineItem = new Sale_Order_Detail
                {
                    sale_id = viewModel.SaleOrder.sale_id,
                    product_id = item.product_id,
                    quantity = item.quantity,
                    price = item.price
                };
                //if (lineItem.quantity < lineItem.product.quantity)
                //{
                _context.Sale_Order_Details.Add(lineItem);
                _context.SaveChanges();
                //}
            }


            // Redirect to a success page or take appropriate action
            return RedirectToAction("Index");
            //}
            return View(viewModel);
        }

        // GET: Sale_Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Sale_Orders == null)
            //{
            //    return NotFound();
            //}

            //var sale_Order = await _context.Sale_Orders.FindAsync(id);
            //if (sale_Order == null)
            //{
            //    return NotFound();
            //}

            //ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", sale_Order.customer_id);
            //ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", sale_Order.payment_method);
            //return View(sale_Order);

            if (id == null || _context.Sale_Orders == null)
            {
                return NotFound();
            }

            var sale_Order = await _context.Sale_Orders
                            .Include(s => s.customer)
                            .FirstOrDefaultAsync(m => m.sale_id == id);
            if (sale_Order == null)
            {
                return NotFound();
            }

            var viewModel = new SalesViewModel
            {
                SaleOrder = sale_Order,
                SaleItems = await _context.Sale_Order_Details
                .Include(d => d.product)
            .Where(d => d.sale_id == id)
            .ToListAsync()
            };
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");

            ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", sale_Order.customer_id);
            
            return View(viewModel);

        }

        // POST: Sale_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("sale_id,customer_id,name,total_amount,payment_method,date_created,state")] Sale_Order sale_Order)
        public async Task<IActionResult> Edit(int id, SalesViewModel viewModel)
        {
            if (id != viewModel.SaleOrder.sale_id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                _context.Update(viewModel.SaleOrder);
                await _context.SaveChangesAsync();
                _context.Update(viewModel.SaleItems);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Sale_OrderExists(viewModel.SaleOrder.sale_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //}
            ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", viewModel.SaleOrder.customer_id);
            
            return View(viewModel);
        }


        // GET: Sale_Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sale_Orders == null)
            {
                return NotFound();
            }

            var sale_Order = await _context.Sale_Orders
                .Include(s => s.customer)
               
                .FirstOrDefaultAsync(m => m.sale_id == id);
            if (sale_Order == null)
            {
                return NotFound();
            }

            return View(sale_Order);
        }

        // POST: Sale_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sale_Orders == null)
            {
                return Problem("Entity set 'FYPContext.Sale_Orders'  is null.");
            }
            var sale_Order = await _context.Sale_Orders.FindAsync(id);
            if (sale_Order != null)
            {
                _context.Sale_Orders.Remove(sale_Order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Sale_OrderExists(int id)
        {
            return (_context.Sale_Orders?.Any(e => e.sale_id == id)).GetValueOrDefault();
        }


        // POST: SaleOrders/ConfirmSaleOrder/5
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmSaleOrder(int id)
        {
            // Find the sale order by ID
            var saleOrder = await _context.Sale_Orders
                .Include(so => so.Sale_Order_Details)
                .FirstOrDefaultAsync(so => so.sale_id == id);

            if (saleOrder == null)
            {
                return NotFound();
            }

            // Check if the sale order is already confirmed
            if (saleOrder.state == "Sale Order")
            {
                //return BadRequest("Sale order is already confirmed.");
                TempData["SalePopupMessage"] = "Sale order is already confirmed.";
                return RedirectToAction(nameof(Index));
            }

            // Update the state to "Sale"
            saleOrder.state = "Sale Order";
            _context.Update(saleOrder);
            await _context.SaveChangesAsync();

            // Add items to the transfer table
            var transfer = new Transfer
            {
                Doc_name = "WH-OUT/" + DateTime.Now.Date.Year + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Day + "/" + saleOrder.name,
                Source_Document = saleOrder.name,
                created_date = DateTime.Now.Date,
                status = "Initial",
                operation_type = "DN"
            };

            _context.Transfers.Add(transfer);
            await _context.SaveChangesAsync();

            // Add details to the transfer detail table
            foreach (var item in saleOrder.Sale_Order_Details)
            {
                var transferDetail = new Transfer_Detail
                {
                    transfer_id = transfer.ID,
                    product_id = item.product_id,
                    demand = item.quantity,
                    done = 0
                };

                _context.Transfer_Details.Add(transferDetail);
            }

            await _context.SaveChangesAsync();
            TempData["SalePopupMessage"] = "Sale Order confirmed.";

            return RedirectToAction(nameof(Index));
        }

        
    }
}
