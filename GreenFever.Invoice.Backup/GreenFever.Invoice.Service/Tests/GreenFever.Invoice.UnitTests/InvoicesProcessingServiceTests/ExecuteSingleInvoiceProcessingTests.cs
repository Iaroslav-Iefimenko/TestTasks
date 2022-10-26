using System;
using GreenFever.Invoice.BusinessLogic.Services;
using GreenFever.Invoice.Dto.Enums;
using GreenFever.Invoice.UnitTests.Base;
using Moq;
using NUnit.Framework;

namespace GreenFever.Invoice.UnitTests.InvoicesProcessingServiceTests
{
    [TestFixture]
    public class ExecuteSingleInvoiceProcessingTests : BaseTests
    {
        private readonly InvoicesProcessingService processingService;

        private int clientId = 321;
        private InvoiceType testInvoiceType = InvoiceType.Invoice;

        public ExecuteSingleInvoiceProcessingTests()
        {
            this.processingService = new InvoicesProcessingService(
                this.ProcessingRepositoryMock.Object,
                this.OptionsMock.Object);
        }

        [Test]
        public void Positive()
        {
            // arrange
            this.SetupMockMethods(true, true);
            this.testInvoiceType = InvoiceType.Invoice;

            // act
            var result = this.processingService.ExecuteSingleInvoiceProcessingRun(
                this.testInvoiceType,
                this.clientId,
                this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(
                x => x.IsBatchInvoicesProcessingRunAvailable(),
                Times.Once,
                "IsBatchInvoicesProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.IsSingleInvoiceProcessingRunAvailable(It.Is<int>(p => p == this.clientId)),
                Times.Once,
                "IsSingleInvoiceProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteSingleInvoiceProcessingRunForInvoiceType(
                    It.Is<int>(p => p == this.clientId),
                    It.Is<DateTime>(p => p == this.TestRunDate)),
                Times.Once,
                "ExecuteSingleInvoiceProcessingRunForInvoiceType function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteSingleInvoiceProcessingRunForSettlementType(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>()),
                Times.Never,
                "ExecuteSingleInvoiceProcessingRunForSettlementType is called");

            Assert.False(result.ContainsErrors, "Result of ExecuteSingleInvoiceProcessingRun call contains incorrect errors flag");
            Assert.AreEqual(this.clientId, result.ClientId, "Result of ExecuteSingleInvoiceProcessingRun call contains incorrect client Id");
        }

        [Test]
        public void NegativeNotStartTwoRunsForOneClientSimultaneously()
        {
            // arrange
            this.SetupMockMethods(true, false);

            // act
            var result = this.processingService.ExecuteSingleInvoiceProcessingRun(
                this.testInvoiceType,
                this.clientId,
                this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(
                x => x.IsBatchInvoicesProcessingRunAvailable(),
                Times.Once,
                "IsBatchInvoicesProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.IsSingleInvoiceProcessingRunAvailable(It.Is<int>(p => p == this.clientId)),
                Times.Once,
                "IsSingleInvoiceProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteSingleInvoiceProcessingRunForInvoiceType(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>()),
                Times.Never,
                "ExecuteSingleInvoiceProcessingRunForInvoiceType function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteSingleInvoiceProcessingRunForSettlementType(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>()),
                Times.Never,
                "ExecuteSingleInvoiceProcessingRunForSettlementType is called");

            Assert.True(result.ContainsErrors, "Result of ExecuteSingleInvoiceProcessingRun call contains incorrect errors flag");
            Assert.AreEqual(this.clientId, result.ClientId, "Result of ExecuteSingleInvoiceProcessingRun call contains incorrect client Id");
        }

        [Test]
        public void NegativeNotStartRunIfBatchRunInProgress()
        {
            // arrange
            this.SetupMockMethods(false, true);

            // act
            var result = this.processingService.ExecuteSingleInvoiceProcessingRun(
                this.testInvoiceType,
                this.clientId,
                this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(
                x => x.IsBatchInvoicesProcessingRunAvailable(),
                Times.Once,
                "IsBatchInvoicesProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.IsSingleInvoiceProcessingRunAvailable(It.IsAny<int>()),
                Times.Never,
                "IsSingleInvoiceProcessingRunAvailable function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteSingleInvoiceProcessingRunForInvoiceType(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>()),
                Times.Never,
                "ExecuteSingleInvoiceProcessingRunForInvoiceType function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteSingleInvoiceProcessingRunForSettlementType(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>()),
                Times.Never,
                "ExecuteSingleInvoiceProcessingRunForSettlementType is called");

            Assert.True(result.ContainsErrors, "Result of ExecuteSingleInvoiceProcessingRun call contains incorrect errors flag");
            Assert.AreEqual(this.clientId, result.ClientId, "Result of ExecuteSingleInvoiceProcessingRun call contains incorrect client Id");
        }

        private void SetupMockMethods(bool isBatchAvailable, bool isSingleAvailable)
        {
            this.ProcessingRepositoryMock.Invocations.Clear();
            testInvoiceType = InvoiceType.Settlement;

            this.ProcessingRepositoryMock.Setup(x => x.IsBatchInvoicesProcessingRunAvailable()).Returns(isBatchAvailable);
            this.ProcessingRepositoryMock.Setup(x => x.IsSingleInvoiceProcessingRunAvailable(It.IsAny<int>())).Returns(isSingleAvailable);
        }
    }
}
