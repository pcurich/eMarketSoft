using System;
using Soft.Core;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Orders;
using Soft.Core.Domain.Payments;
using Soft.Core.Domain.Shipping;

namespace Soft.Services.Customers
{
    /// <summary>
    /// Customer report service interface
    /// </summary>
    public partial interface ICustomerReportService
    {
        /// <summary>
        /// Get best customers
        /// </summary>
        /// <param name="createdFromUtc">Order created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Order created date to (UTC); null to load all records</param>
        /// <param name="os">Order status; null to load all records</param>
        /// <param name="ps">Order payment status; null to load all records</param>
        /// <param name="ss">Order shipment status; null to load all records</param>
        /// <param name="orderBy">1 - order by order total, 2 - order by number of orders</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Report</returns>
        IPagedList<BestCustomerReportLine> GetBestCustomersReport(DateTime? createdFromUtc,
            DateTime? createdToUtc, OrderStatus? os, PaymentStatus? ps, ShippingStatus? ss, int orderBy,
            int pageIndex = 0, int pageSize = 214748364);
        
        /// <summary>
        /// Gets a report of customers registered in the last days
        /// </summary>
        /// <param name="days">Customers registered in the last days</param>
        /// <returns>Number of registered customers</returns>
        int GetRegisteredCustomersReport(int days);
    }
}