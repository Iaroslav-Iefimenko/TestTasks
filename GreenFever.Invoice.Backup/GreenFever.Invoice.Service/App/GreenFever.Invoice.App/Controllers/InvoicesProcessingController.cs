using System;
using GreenFever.Invoice.BusinessLogic.Interfaces;
using GreenFever.Invoice.Dto.Objects;
using GreenFever.Invoice.Dto.Requests;
using GreenFever.Invoice.Dto.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GreenFever.Invoice.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesProcessingController : ControllerBase
    {
        private readonly IInvoicesProcessingService processingService;

        public InvoicesProcessingController(
            IInvoicesProcessingService processingService)
        {
            this.processingService = processingService ?? throw new ArgumentNullException(nameof(processingService));
        }

        #region batch processing

        [HttpPost]
        [Route("execute-batch")]
        public GenericResponse<InvoicesProcessingResult> ExecuteBatchInvoicesProcessing(BatchInvoiceProcessingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this.ExecuteActionSafely(() =>
            {
                var res = this.processingService.ExecuteBatchInvoicesProcessingRun(
                    request.InvoiceType,
                    request.RunDate);

                return new GenericResponse<InvoicesProcessingResult> { Data = res };
            });
        }

        [HttpPost]
        [Route("is-batch-available")]
        public GenericResponse<bool> IsBatchInvoicesProcessingRunAvailable()
        {
            return this.ExecuteActionSafely(() =>
            {
                var res = this.processingService.IsBatchInvoicesProcessingRunAvailable();
                return new GenericResponse<bool> { Data = res };
            });
        }

        [HttpPost]
        [Route("get-batch-percentage-info")]
        public GenericResponse<ProcessingPercentageInfo> GetBatchProcessingPercentageInfo(GetBatchPercentageInfoRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this.ExecuteActionSafely(() =>
            {
                var res = this.processingService.GetBatchProcessingPercentageInfo(request.BatchRunId);
                return new GenericResponse<ProcessingPercentageInfo> { Data = res };
            });
        }

        [HttpPost]
        [Route("get-active-batch-id")]
        public GenericResponse<int> GetActiveBatchRunId()
        {
            return this.ExecuteActionSafely(() =>
            {
                var res = this.processingService.GetActiveBatchRunId();
                return new GenericResponse<int> { Data = res };
            });
        }

        #endregion

        #region Single processing

        [HttpPost]
        [Route("execute-single")]
        public GenericResponse<InvoicesProcessingResult> ExecuteSingleInvoiceProcessing(SingleInvoiceProcessingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this.ExecuteActionSafely(() =>
            {
                var res = this.processingService.ExecuteSingleInvoiceProcessingRun(
                    request.InvoiceType,
                    request.ClientId,
                    request.RunDate);

                return new GenericResponse<InvoicesProcessingResult> { Data = res };
            });
        }

        [HttpPost]
        [Route("is-single-available")]
        public GenericResponse<bool> IsSingleInvoiceProcessingAvailable(IsSingleInvoiceProcessingAvailableRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this.ExecuteActionSafely(() =>
            {
                var res = this.processingService.IsSingleInvoiceProcessingRunAvailable(request.ClientId);
                return new GenericResponse<bool> { Data = res };
            });
        }

        #endregion

        #region Manual grouping

        [HttpPost]
        [Route("group-invoices")]
        public GenericResponse<string> ExecuteManualInvoicesGrouping(ExecuteManualInvoiceGroupingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this.ExecuteActionSafely(() =>
            {
                var res = this.processingService.ExecuteManualInvoiceGrouping(request.RunDate);
                return new GenericResponse<string> { Data = res };
            });
        }

        [HttpPost]
        [Route("get-cnt-for-invoices-group")]
        public GenericResponse<int> GetCountOfInvoicesForManualGrouping()
        {
            return this.ExecuteActionSafely(() =>
            {
                var res = this.processingService.GetCountOfInvoicesForManualGrouping();
                return new GenericResponse<int> { Data = res };
            });
        }

        #endregion

        private void ExecuteActionSafely(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                // point for general logging
                // TO-DO: add something like NLog
                throw e;
            }
        }

        private T ExecuteActionSafely<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                // point for general logging
                // TO-DO: add something like NLog
                throw e;
            }
        }
    }
}