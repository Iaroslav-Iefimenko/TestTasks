using System;
using GreenFever.Invoice.BusinessLogic.Services;
using GreenFever.Invoice.UnitTests.Base;
using Moq;
using NUnit.Framework;

namespace GreenFever.Invoice.UnitTests.InvoicesProcessingServiceTests
{
    [TestFixture]
    public class ExecuteManualInvoiceGroupingTests : BaseTests
    {
        private readonly InvoicesProcessingService processingService;

        public ExecuteManualInvoiceGroupingTests()
        {
            this.processingService = new InvoicesProcessingService(
                this.ProcessingRepositoryMock.Object,
                this.OptionsMock.Object);           
        }

        [Test]
        public void Positive()
        {
            // arrange
            this.SetupMockMethods(true);

            // act
            var result = this.processingService.ExecuteManualInvoiceGrouping(this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(x => x.IsBatchInvoicesProcessingRunAvailable(), Times.Once);
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteManualInvoiceGrouping(It.Is<DateTime>(p => p == this.TestRunDate)),                
                Times.Once);

            Assert.AreEqual(string.Empty, result, "Result of ExecuteManualInvoiceGrouping function call is not empty");
        }

        [Test]
        public void NegativeNotStartRunIfBatchRunInProgress()
        {
            // arrange
            this.SetupMockMethods(false);

            // act
            var result = this.processingService.ExecuteManualInvoiceGrouping(this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(x => x.IsBatchInvoicesProcessingRunAvailable(), Times.Once);
            this.ProcessingRepositoryMock.Verify(x => x.ExecuteManualInvoiceGrouping(It.IsAny<DateTime>()), Times.Never);

            Assert.AreNotEqual(string.Empty, result, "Result of ExecuteManualInvoiceGrouping function call is empty");
        }

        private void SetupMockMethods(bool isBatchAvailable)
        {
            this.ProcessingRepositoryMock.Invocations.Clear();
            this.ProcessingRepositoryMock.Setup(x => x.IsBatchInvoicesProcessingRunAvailable()).Returns(isBatchAvailable);            
        }
    }
}
