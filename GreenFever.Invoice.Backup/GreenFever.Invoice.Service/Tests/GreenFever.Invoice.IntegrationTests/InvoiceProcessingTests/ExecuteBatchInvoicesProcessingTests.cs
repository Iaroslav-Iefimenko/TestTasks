using System;
using GreenFever.Invoice.App.Controllers;
using GreenFever.Invoice.Dto.Enums;
using GreenFever.Invoice.Dto.Requests;
using GreenFever.Invoice.IntegrationTests.Base;
using NUnit.Framework;

namespace GreenFever.Invoice.IntegrationTests.InvoiceProcessingTests
{
    [TestFixture]
    public class ExecuteBatchInvoicesProcessingTests : BaseTests
    {
        private InvoicesProcessingController testController;

        public ExecuteBatchInvoicesProcessingTests()
        {
            this.testController = new InvoicesProcessingController(this.ProcessingService);
        }

        [Test]
        public void ExecuteBatchInvoicesProcessingPositive()
        {
            // arrange
            var request = new BatchInvoiceProcessingRequest { InvoiceType = InvoiceType.Settlement, RunDate = DateTime.Now };

            // act
            this.testController.ExecuteBatchInvoicesProcessing(request);

            // assert
            Assert.True(true);
        }
    }
}
